using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class UnpaidLeavesStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessingStatus.Text = "No pending unpaid leave requests.";
                ProcessingStatus.ForeColor = Color.Orange;
                ProcessingStatus.Font.Bold = true;
            }

        }
        private void BindPendingLeaves()
        {

            int hrID = (int)Session["UserID"];
            string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(conStr))
            {

                string find = @"
                    SELECT 
                        L.request_ID, 
                        L.start_date, 
                        L.end_date, 
                        DATEDIFF(day, L.start_date, L.end_date) + 1 AS duration_days,
                        E.employee_id AS emp_ID,
                        E.first_name + ' ' + E.last_name AS employee_name,
                        'Unpaid' AS leave_type
                    FROM Leave L
                    INNER JOIN Employee_Approve_Leave EAL ON L.request_ID = EAL.leave_ID
                    INNER JOIN Employee E ON E.employee_id = L.emp_ID // Assume L.emp_ID is the employee requesting the leave
                    INNER JOIN Unpaid_Leave UL on L.request_ID = UL.request_ID
                    WHERE L.final_approval_status = 'Pending' 
                      AND EAL.Emp1_ID = @HR_ID // Filter by logged-in HR employee's ID
                ";

                SqlCommand cd = new SqlCommand(find, conn);
                cd.Parameters.AddWithValue("@HR_ID", hrID);

                SqlDataAdapter u = new SqlDataAdapter(cd);
                DataTable t = new DataTable();


                conn.Open();
                u.Fill(t);


                UnpaidLeavesTable.DataSource = t;
                UnpaidLeavesTable.DataBind();

                if (t.Rows.Count == 0)
                {
                    ProcessingStatus.Text = "NO pending Unpaid requests found for your approval.";
                    ProcessingStatus.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void UnpaidLeavesTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ProcessLeave")
            {

                int requestID = Convert.ToInt32(e.CommandArgument);
                int HRID = (int)Session["UserID"];

                String cStr = ConfigurationManager.ConnectionStrings["HR_System_Connection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(cStr))
                {

                    SqlCommand cd = new SqlCommand("HR_approval_unpaid", conn);
                    cd.CommandType = CommandType.StoredProcedure;


                    cd.Parameters.AddWithValue("@req_id", requestID);
                    cd.Parameters.AddWithValue("@hr_emp_ID", HRID);

                    try
                    {
                        conn.Open();
                        cd.ExecuteNonQuery();


                        ProcessingStatus.Text = $"Unpaid Leave Request ID {requestID} processed successfully.";
                        ProcessingStatus.ForeColor = System.Drawing.Color.Green;


                        BindPendingLeaves();
                    }
                    catch (Exception ex)
                    {

                        ProcessingStatus.Text = $"Error processing request ID {requestID}: {ex.Message}";
                        ProcessingStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}