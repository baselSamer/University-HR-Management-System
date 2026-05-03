using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Academic_home : System.Web.UI.Page
    {
        string connStr = WebConfigurationManager.ConnectionStrings["OurApp"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_type"] == null || Session["user_type"].ToString() != "Academic")
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                // Load initial data if needed
                string userId = Session["user_id"]?.ToString();
                if (!string.IsNullOrEmpty(userId))
                {
                    txtPerfSem.Text = "W25"; // Default semester
                    LoadInitialData();
                }
            }
        }

        private void LoadInitialData()
        {
            // Load current month attendance on page load
            string q = $"SELECT * FROM dbo.MyAttendance({Session["user_id"]})";
            BindGrid(q, gridAtt);
        }

        private void BindGrid(string query, GridView grid)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                grid.DataSource = rdr;
                grid.DataBind();
                if (!rdr.HasRows)
                {
                    lblMsg.Text = "No data found.";
                    lblMsg.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        private void RunProc(string procName, Action<SqlCommand> paramsAction)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (paramsAction != null) paramsAction(cmd);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "Action completed successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnViewPerf_Click(object sender, EventArgs e)
        {
            // 2.5.b MyPerformance
            string q = $"SELECT * FROM dbo.MyPerformance({Session["user_id"]}, '{txtPerfSem.Text.Trim()}')";
            BindGrid(q, gridPerf);
        }

        protected void btnViewAtt_Click(object sender, EventArgs e)
        {
            // 2.5.c MyAttendance
            string q = $"SELECT * FROM dbo.MyAttendance({Session["user_id"]})";
            BindGrid(q, gridAtt);
        }

        protected void btnViewPay_Click(object sender, EventArgs e)
        {
            // 2.5.d Last_month_payroll
            string q = $"SELECT * FROM dbo.Last_month_payroll({Session["user_id"]})";
            BindGrid(q, gridFinance);
        }

        protected void btnViewDed_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtDedMonth.Text))
            {
                lblMsg.Text = "Please enter a month number (1-12) to view deductions.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(txtDedMonth.Text, out int month) || month < 1 || month > 12)
            {
                lblMsg.Text = "Please enter a valid month number (1-12).";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string q = $"SELECT * FROM dbo.Deductions_Attendance({Session["user_id"]}, {month})";
            BindGrid(q, gridFinance);
        }

        protected void btnLeaveStatus_Click(object sender, EventArgs e)
        {
            // 2.5.h Status_leaves
            string q = $"SELECT * FROM dbo.Status_leaves({Session["user_id"]})";
            BindGrid(q, gridLeaveStatus);
        }


        protected void btnSubAnnual_Click(object sender, EventArgs e)
        {
            // 2.5.g Submit_annual
            RunProc("Submit_annual", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@replacement_emp", Convert.ToInt32(txtAnnRep.Text));
                cmd.Parameters.AddWithValue("@start_date", Convert.ToDateTime(txtAnnStart.Text));
                cmd.Parameters.AddWithValue("@end_date", Convert.ToDateTime(txtAnnEnd.Text));
            });
        }

        protected void btnSubAcc_Click(object sender, EventArgs e)
        {
            // 2.5.j Submit_accidental
            DateTime startDate = Convert.ToDateTime(txtAnnStart.Text);
            DateTime endDate = Convert.ToDateTime(txtAnnEnd.Text);
            TimeSpan duration = endDate - startDate;

            if (duration.Days != 0)
            {
                lblMsg.Text = "Accidental leave duration should be exactly 1 day.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            RunProc("Submit_accidental", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@start_date", startDate);
                cmd.Parameters.AddWithValue("@end_date", endDate);
            });
        }

        protected void btnSubMed_Click(object sender, EventArgs e)
        {
            // 2.5.k Submit_medical
            string medType = txtMedType.Text.Trim();
            if (medType != "sick" && medType != "maternity")
            {
                lblMsg.Text = "Medical type must be either 'sick' or 'maternity'.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            RunProc("Submit_medical", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@start_date", Convert.ToDateTime(txtMedStart.Text));
                cmd.Parameters.AddWithValue("@end_date", Convert.ToDateTime(txtMedEnd.Text));
                cmd.Parameters.AddWithValue("@medical_type", medType);
                cmd.Parameters.AddWithValue("@insurance_status", chkInsurance.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@disability_details", txtMedDisability.Text);
                cmd.Parameters.AddWithValue("@document_description", txtMedDocDesc.Text);
                cmd.Parameters.AddWithValue("@file_name", txtMedFile.Text);
            });
        }

        protected void btnSubUnpaid_Click(object sender, EventArgs e)
        {
            // 2.5.l Submit_unpaid
            RunProc("Submit_unpaid", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@start_date", Convert.ToDateTime(txtUnpStart.Text));
                cmd.Parameters.AddWithValue("@end_date", Convert.ToDateTime(txtUnpEnd.Text));
                cmd.Parameters.AddWithValue("@document_description", txtUnpDesc.Text);
                cmd.Parameters.AddWithValue("@file_name", txtUnpFile.Text);
            });
        }

        protected void btnSubComp_Click(object sender, EventArgs e)
        {
            // 2.5.n Submit_compensation
            DateTime compDate = Convert.ToDateTime(txtCompDate.Text);
            DateTime workDay = Convert.ToDateTime(txtCompWorkDay.Text);

            if (compDate.Month != workDay.Month || compDate.Year != workDay.Year)
            {
                lblMsg.Text = "Compensation date must be in the same month as the original workday.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            RunProc("Submit_compensation", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(Session["user_id"]));
                cmd.Parameters.AddWithValue("@compensation_date", compDate);
                cmd.Parameters.AddWithValue("@date_of_original_workday", workDay);
                cmd.Parameters.AddWithValue("@reason", txtCompReason.Text);
                cmd.Parameters.AddWithValue("@rep_emp_id", Convert.ToInt32(txtCompRep.Text));
            });
        }

        protected void btnApproveAnnual_Click(object sender, EventArgs e)
        {
            // 2.5.i Upperboard_approve_annual
            RunProc("Upperboard_approve_annual", cmd => {
                cmd.Parameters.AddWithValue("@request_ID", Convert.ToInt32(txtAppAnnReqID.Text));
                cmd.Parameters.AddWithValue("@replacement_ID", Convert.ToInt32(txtAppAnnRepID.Text));
                cmd.Parameters.AddWithValue("@Upperboard_ID", Convert.ToInt32(Session["user_id"]));
            });
        }

        protected void btnApproveUnpaid_Click(object sender, EventArgs e)
        {
            // 2.5.m Upperboard_approve_unpaids
            RunProc("Upperboard_approve_unpaids", cmd => {
                cmd.Parameters.AddWithValue("@request_ID", Convert.ToInt32(txtAppUnpReqID.Text));
                cmd.Parameters.AddWithValue("@Upperboard_ID", Convert.ToInt32(Session["user_id"]));
            });
        }

        protected void btnEvaluate_Click(object sender, EventArgs e)
        {
            // 2.5.o Dean_andHR_Evaluation
            if (!int.TryParse(txtEvalRating.Text, out int rating) || rating < 1 || rating > 5)
            {
                lblMsg.Text = "Rating must be a number between 1 and 5.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string semester = txtEvalSem.Text.Trim();
            if (semester.Length != 3 || (semester[0] != 'W' && semester[0] != 'S') ||
                !char.IsDigit(semester[1]) || !char.IsDigit(semester[2]))
            {
                lblMsg.Text = "Semester must be in format like W25 or S24.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            RunProc("Dean_andHR_Evaluation", cmd => {
                cmd.Parameters.AddWithValue("@employee_ID", Convert.ToInt32(txtEvalEmpID.Text));
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@comment", txtEvalComm.Text);
                cmd.Parameters.AddWithValue("@semester", semester);
            });
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}