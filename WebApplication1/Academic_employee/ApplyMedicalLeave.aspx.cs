using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UniversityHR.Academic
{
    public partial class ApplyMedicalLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) Response.Redirect("../Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Submit_medical", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Inputs
                cmd.Parameters.Add(new SqlParameter("@employee_ID", Session["user"]));
                cmd.Parameters.Add(new SqlParameter("@start_date", txtStart.Text));
                cmd.Parameters.Add(new SqlParameter("@end_date", txtEnd.Text));
                cmd.Parameters.Add(new SqlParameter("@medical_type", ddlType.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@insurance_status", chkInsurance.Checked ? 1 : 0));
                cmd.Parameters.Add(new SqlParameter("@disability_details", txtDisability.Text));
                
                // Document Handling
                cmd.Parameters.Add(new SqlParameter("@document_description", txtDocDesc.Text));
                
                if (fileUploadDoc.HasFile)
                {
                    cmd.Parameters.Add(new SqlParameter("@file_name", fileUploadDoc.FileName));
                }
                else
                {
                    lblMessage.Text = "You must upload a document for medical leave.";
                    return;
                }

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Medical leave submitted successfully!";
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