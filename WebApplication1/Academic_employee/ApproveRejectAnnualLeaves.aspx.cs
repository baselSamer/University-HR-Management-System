using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UniversityHR.Academic
{
    public partial class ApproveRejectAnnualLeaves : System.Web.UI.Page
    {
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Upperboard_approve_annual", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@request_ID", txtRequestID.Text));
                cmd.Parameters.Add(new SqlParameter("@Upperboard_ID", Session["user"])); // The logged in Dean/Prez
                cmd.Parameters.Add(new SqlParameter("@replacement_ID", txtReplacementID.Text));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Action Processed (Approved if valid, Rejected otherwise).";
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