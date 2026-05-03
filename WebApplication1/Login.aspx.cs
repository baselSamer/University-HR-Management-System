using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = ""; 
        }

        protected void login(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["OurApp"].ToString();
            string user_id_str = username.Text;
            string pass = password.Text;

            
            if (user_id_str == "admin" && pass == "admin")
            {
                Session["user_type"] = "admin";
                Response.Redirect("Admin_home.aspx");
                return;
            }

            
            if (string.IsNullOrWhiteSpace(user_id_str) || string.IsNullOrWhiteSpace(pass))
            {
                lblError.Text = "Please enter both ID and Password.";
                return;
            }

            int id;
            if (!int.TryParse(user_id_str, out id))
            {
                lblError.Text = "User ID must be a number.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    
                    SqlCommand hrCmd = new SqlCommand("SELECT dbo.HRLoginValidation(@id, @pass)", conn);
                    hrCmd.Parameters.AddWithValue("@id", id);
                    hrCmd.Parameters.AddWithValue("@pass", pass);

                    object hrResult = hrCmd.ExecuteScalar();

                    if (hrResult != null && Convert.ToBoolean(hrResult) == true)
                    {
                        Session["user_id"] = id;
                        Session["user_type"] = "HR";
                        Response.Redirect("~/HR/HRhome.aspx");
                        return;
                    }

                    
                    SqlCommand empCmd = new SqlCommand("SELECT dbo.EmployeeLoginValidation(@id, @pass)", conn);
                    empCmd.Parameters.AddWithValue("@id", id);
                    empCmd.Parameters.AddWithValue("@pass", pass);

                    object empResult = empCmd.ExecuteScalar();

                    if (empResult != null && Convert.ToBoolean(empResult) == true)
                    {
                        Session["user_id"] = id;
                        Session["user_type"] = "Academic";
                        Response.Redirect("Academic_home.aspx");
                        return;
                    }

                    
                    lblError.Text = "Invalid ID or Password.";
                }
                catch (Exception ex)
                {
                    lblError.Text = "Database Connection Error.";
                }
            }
        }
    }
}