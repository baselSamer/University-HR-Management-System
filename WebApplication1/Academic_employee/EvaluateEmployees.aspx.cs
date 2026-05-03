using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UniversityHR.Academic
{
    public partial class EvaluateEmployees : System.Web.UI.Page
    {
        protected void btnEvaluate_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Dean_andHR_Evaluation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Note: The Procedure just inserts into Performance table.
                // It does not explicitly check if the logged-in user is a Dean, 
                // but usually the UI hides this button if they aren't.
                
                cmd.Parameters.Add(new SqlParameter("@employee_ID", txtEmpID.Text)); // Target Employee
                cmd.Parameters.Add(new SqlParameter("@rating", int.Parse(txtRating.Text))); // 1-5
                cmd.Parameters.Add(new SqlParameter("@comment", txtComment.Text));
                cmd.Parameters.Add(new SqlParameter("@semester", txtSemester.Text)); // e.g., W24

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Evaluation submitted.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}