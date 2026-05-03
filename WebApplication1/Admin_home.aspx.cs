using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Admin_home : System.Web.UI.Page
    {
        string connStr = WebConfigurationManager.ConnectionStrings["OurApp"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_type"] == null || Session["user_type"].ToString() != "admin")
            {
                Response.Redirect("Login.aspx");
            }
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
                if (grid.Rows.Count == 0) lblMessage.Text = "No records found.";
                else lblMessage.Text = "Data loaded successfully.";
            }
        }

        
        private void ExecuteProc(string procName, Action<SqlCommand> addParams = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(procName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (addParams != null) addParams(cmd);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Action completed successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        

        protected void btnViewEmployees_Click(object sender, EventArgs e) => BindGrid("SELECT * FROM allEmployeeProfiles", gridEmployees);
        protected void btnViewDeptCount_Click(object sender, EventArgs e) => BindGrid("SELECT * FROM NoEmployeeDept", gridDeptCount);
        protected void btnViewRejected_Click(object sender, EventArgs e) => BindGrid("SELECT * FROM allRejectedMedicals", gridRejected);
        protected void btnViewAttendance_Click(object sender, EventArgs e) => BindGrid("SELECT * FROM allEmployeeAttendance", gridAttendance);
        protected void btnViewPerformance_Click(object sender, EventArgs e) => BindGrid("SELECT * FROM allPerformance", gridPerformance);

        protected void btnRemoveDeductions_Click(object sender, EventArgs e) => ExecuteProc("Remove_Deductions");
        protected void btnInitAttendance_Click(object sender, EventArgs e) => ExecuteProc("Initiate_Attendance");
        protected void btnRemoveHolidayAtt_Click(object sender, EventArgs e) => ExecuteProc("Remove_Holiday");
        protected void btnUpdateDocs_Click(object sender, EventArgs e) => ExecuteProc("Update_Status_Doc");

        protected void btnAddHoliday_Click(object sender, EventArgs e)
        {
            ExecuteProc("Add_Holiday", cmd => {
                cmd.Parameters.AddWithValue("@holiday_name", txtHolName.Text);
                cmd.Parameters.AddWithValue("@from_date", txtHolStart.Text);
                cmd.Parameters.AddWithValue("@to_date", txtHolEnd.Text);
            });
        }

        protected void btnUpdateAtt_Click(object sender, EventArgs e)
        {
            ExecuteProc("Update_Attendance", cmd => {
                cmd.Parameters.AddWithValue("@Employee_id", txtAttEmpID.Text);
                cmd.Parameters.AddWithValue("@check_in_time", txtCheckIn.Text);
                cmd.Parameters.AddWithValue("@check_out_time", txtCheckOut.Text);
            });
        }

        protected void btnRemoveDayOff_Click(object sender, EventArgs e)
        {
            ExecuteProc("Remove_DayOff", cmd => cmd.Parameters.AddWithValue("@Employee_ID", txtTargetEmp.Text));
        }

        protected void btnRemoveApproved_Click(object sender, EventArgs e)
        {
            ExecuteProc("Remove_Approved_Leaves", cmd => cmd.Parameters.AddWithValue("@Employee_ID", txtTargetEmp.Text));
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            ExecuteProc("Update_Employment_Status", cmd => cmd.Parameters.AddWithValue("@Employee_ID", txtTargetEmp.Text));
        }

        protected void btnReplace_Click(object sender, EventArgs e)
        {
            ExecuteProc("Replace_employee", cmd => {
                cmd.Parameters.AddWithValue("@Emp1_ID", txtRepOld.Text);
                cmd.Parameters.AddWithValue("@Emp2_ID", txtRepNew.Text);
                cmd.Parameters.AddWithValue("@from_date", txtRepStart.Text);
                cmd.Parameters.AddWithValue("@to_date", txtRepEnd.Text);
            });
        }
    }
}