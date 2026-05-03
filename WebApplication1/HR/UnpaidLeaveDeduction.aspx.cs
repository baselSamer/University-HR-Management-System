using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class UnpaidLeaveDeduction : System.Web.UI.Page
    {
       

        protected void btnAddUnpaidLeaveDeduction_Click(object sender, EventArgs e)
        {
            TextBox txtEmpID = (TextBox)FindControl("TEmployeeID");
            TextBox txtLeaveID = (TextBox)FindControl("TLeaveID");
            TextBox txtAmount = (TextBox)FindControl("TDeductionAmount");
            Label StatusLabel = (Label)FindControl("Status");

            if (!int.TryParse(txtEmpID.Text, out int empID) || empID <= 0)
            {
                StatusLabel.Text = "Invalid Employee ID";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!int.TryParse(txtLeaveID.Text, out int leaveID) || leaveID <= 0)
            {
                StatusLabel.Text = "Invalid Unpaid Leave ID";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                StatusLabel.Text = "Invalid Amount";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();


                    string checkSql = "SELECT COUNT(*) FROM Unpaid_Leave WHERE request_ID = @leaveID";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@leaveID", leaveID);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            StatusLabel.Text = "ERROR: Unpaid Leave ID not found in Unpaid_Leave table";
                            StatusLabel.ForeColor = Color.Red;
                            return;
                        }
                    }


                    string sql = @"
                        INSERT INTO Deduction (emp_ID, date, amount, type, status, unpaid_ID)
                        VALUES (@empID, GETDATE(), @amount, 'unpaid', 'finalized', @leaveID);

                        UPDATE Payroll 
                        SET deductions_amount = ISNULL(deductions_amount, 0) + @amount,
                            final_salary_amount = final_salary_amount - @amount
                        WHERE emp_ID = @empID 
                          AND payment_date = (SELECT MAX(payment_date) FROM Payroll WHERE emp_ID = @empID);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@empID", empID);
                        cmd.Parameters.AddWithValue("@leaveID", leaveID);
                        cmd.Parameters.AddWithValue("@amount", amount);

                        int rows = cmd.ExecuteNonQuery();

                        StatusLabel.Text = rows > 0
                            ? $"SUCCESS: {amount} EGP deducted from Employee {empID} (Leave ID: {leaveID})"
                            : "Deduction recorded but Payroll not updated";
                        StatusLabel.ForeColor = rows > 0 ? Color.LimeGreen : Color.Orange;
                        StatusLabel.Font.Bold = true;
                        StatusLabel.Font.Size = FontUnit.Large;

                        txtEmpID.Text = txtLeaveID.Text = txtAmount.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "ERROR: " + ex.Message;
                    StatusLabel.ForeColor = Color.Red;
                }
            }
        }
    }
}