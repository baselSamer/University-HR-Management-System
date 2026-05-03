using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class CompensationLeaveStatus : System.Web.UI.Page
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
            int HRID = (int)Session["UserID"];
            string conStr = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string get = @"
                    SELECT 
                        L.request_ID, 
                        L.start_date, 
                        L.end_date, 
                        DATEDIFF(day, L.start_date, L.end_date) + 1 AS duration_days,
                        E.employee_id AS emp_ID,
                        E.first_name + ' ' + E.last_name AS employee_name,
                        'Compensation' AS leave_type
                    FROM Leave L
                    INNER JOIN Employee_Approve_Leave EAL ON L.request_ID = EAL.leave_ID
                    INNER JOIN Employee E ON E.employee_id = L.emp_ID
                    INNER JOIN Compensation_Leave CL ON L.request_ID = CL.request_ID
                    WHERE L.final_approval_status = 'Pending'
                      AND EAL.Emp1_ID = @HR_ID;
                ";

                SqlCommand cd = new SqlCommand(get, con);
                cd.Parameters.AddWithValue("@HR_ID", HRID);

                SqlDataAdapter da = new SqlDataAdapter(cd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    CompensationLeavesTable.DataSource = dt;
                    CompensationLeavesTable.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        ProcessingStatus.Text = "No pending compensation requests.";
                        ProcessingStatus.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                catch (Exception ex)
                {
                    ProcessingStatus.Text = "Database Error: " + ex.Message;
                    ProcessingStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void CompensationLeavesTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ProcessLeave")
            {
                int requestID = Convert.ToInt32(e.CommandArgument);
                int HRID = (int)Session["UserID"];

                string conStr = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("HR_approval_comp", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@req_id", requestID);
                    cmd.Parameters.AddWithValue("@hr_emp_ID", HRID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        ProcessingStatus.Text = $"Request {requestID} processed.";
                        ProcessingStatus.ForeColor = System.Drawing.Color.Green;

                        BindPendingLeaves();
                    }
                    catch
                    {
                        ProcessingStatus.Text = $"Error processing request {requestID}.";
                        ProcessingStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}
