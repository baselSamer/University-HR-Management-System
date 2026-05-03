using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class AnnualAndAccidentalLeavesStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessingStatus.Text = "No pending annual/accidental leave requests.";
                ProcessingStatus.ForeColor = Color.Orange;
                ProcessingStatus.Font.Bold = true;
                BindPendingLeaves();
            }
        }

        private void BindPendingLeaves()
        {
            int hrID = (int)Session["UserID"];
            string connStr = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string fetch = @"
                    SELECT 
                        L.request_ID, 
                        L.start_date, 
                        L.end_date, 
                        DATEDIFF(day, L.start_date, L.end_date) + 1 AS duration_days,
                        E.employee_id AS emp_ID,
                        E.first_name + ' ' + E.last_name AS employee_name,
                        CASE 
                            WHEN AL.request_ID IS NOT NULL THEN 'Annual'
                            WHEN AC.request_ID IS NOT NULL THEN 'Accidental'
                            ELSE 'Unknown'
                        END AS leave_type
                    FROM Leave L
                    INNER JOIN Employee_Approve_Leave EAL ON L.request_ID = EAL.leave_ID
                    INNER JOIN Employee E ON E.employee_id = L.emp_ID 
                    LEFT JOIN Annual_Leave AL ON L.request_ID = AL.request_ID
                    LEFT JOIN Accidental_Leave AC ON L.request_ID = AC.request_ID
                    WHERE L.final_approval_status = 'Pending' 
                      AND EAL.Emp1_ID = @HR_ID 
                      AND (AL.request_ID IS NOT NULL OR AC.request_ID IS NOT NULL);
                ";

                SqlCommand cmd = new SqlCommand(fetch, conn);
                cmd.Parameters.AddWithValue("@HR_ID", hrID);

                SqlDataAdapter d = new SqlDataAdapter(cmd);
                DataTable tab = new DataTable();

                conn.Open();
                d.Fill(tab);


                PendingLeavesTable.DataSource = tab;
                PendingLeavesTable.DataBind();

                if (tab.Rows.Count == 0)
                {
                    ProcessingStatus.Text = "NO pending Annual or Accidental requests found for your approval.";
                    ProcessingStatus.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void PendingLeavesTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ProcessLeave")
            {
                int requestID = Convert.ToInt32(e.CommandArgument);
                int hrID = (int)Session["UserID"];

                string connStr = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("HR_approval_an_acc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@req_id", requestID);
                    cmd.Parameters.AddWithValue("@hr_emp_ID", hrID);


                    conn.Open();
                    cmd.ExecuteNonQuery();


                    ProcessingStatus.Text = $"Leave Request ID {requestID} processed successfully.";
                    ProcessingStatus.ForeColor = System.Drawing.Color.Green;


                    BindPendingLeaves();
                }
            }
        }
    }
}
