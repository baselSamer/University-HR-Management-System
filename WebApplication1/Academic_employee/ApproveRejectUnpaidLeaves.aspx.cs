using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UniversityHR.Academic
{
    public partial class ApproveRejectUnpaidLeaves : System.Web.UI.Page
    {
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Upperboard_approve_unpaids", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@request_ID", txtRequestID.Text));
                cmd.Parameters.Add(new SqlParameter("@upperboard_ID", Session["user"]));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Request processed successfully.";
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