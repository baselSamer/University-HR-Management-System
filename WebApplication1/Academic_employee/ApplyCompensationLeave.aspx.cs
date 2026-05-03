protected void btnComp_Click(object sender, EventArgs e)
{
    string connStr = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
    using (SqlConnection conn = new SqlConnection(connStr))
    {
        SqlCommand cmd = new SqlCommand("Submit_compensation", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@employee_ID", Session["user"]));
        cmd.Parameters.Add(new SqlParameter("@compensation_date", txtCompDate.Text));
        cmd.Parameters.Add(new SqlParameter("@reason", txtReason.Text));
        cmd.Parameters.Add(new SqlParameter("@date_of_original_workday", txtOriginalDate.Text));
        cmd.Parameters.Add(new SqlParameter("@rep_emp_id", txtRepID.Text));

        conn.Open();
        cmd.ExecuteNonQuery();
        lblMsg.Text = "Compensation request submitted.";
    }
}