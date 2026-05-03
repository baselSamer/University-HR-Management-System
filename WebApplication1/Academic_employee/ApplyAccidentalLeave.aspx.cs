using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UniversityHR.Academic
{
    public partial class ApplyAccidentalLeave : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Submit_Accidental", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@employee_ID", Session["user"]));
                cmd.Parameters.Add(new SqlParameter("@start_date", txtStart.Text));
                cmd.Parameters.Add(new SqlParameter("@end_date", txtEnd.Text));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Accidental leave submitted successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}