using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class MissingHoursDeduction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnAddDeduction_Click(object sender, EventArgs e)
        {
            Label StatusLabel = Status;

            if (!int.TryParse(TEmployeeID.Text, out int empID) || empID <= 0)
            {
                StatusLabel.Text = "Invalid Employee ID";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!DateTime.TryParse(TMissingDate.Text, out DateTime missingDate))
            {
                StatusLabel.Text = "Invalid Date";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!decimal.TryParse(TMissingHours.Text, out decimal missingHours) || missingHours <= 0 || missingHours > 24)
            {
                StatusLabel.Text = "Invalid Hours (0.1–24)";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            decimal deductionAmount = missingHours * 200m;

            string cs = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = @"
                    INSERT INTO Deduction (emp_ID, date, amount, type, status)
                    VALUES (@id, @date, @amount, 'missing_hours', 'finalized');

                    UPDATE Payroll 
                    SET deductions_amount = ISNULL(deductions_amount,0) + @amount,
                        final_salary_amount = final_salary_amount - @amount
                    WHERE emp_ID = @id 
                      AND MONTH(payment_date) = MONTH(@date)
                      AND YEAR(payment_date) = YEAR(@date);";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", empID);
                    cmd.Parameters.AddWithValue("@date", missingDate);
                    cmd.Parameters.AddWithValue("@amount", deductionAmount);
                    cmd.ExecuteNonQuery();
                }

                StatusLabel.Text = $"SUCCESS: {missingHours} hours → {deductionAmount} EGP deducted";
                StatusLabel.ForeColor = Color.LimeGreen;
                StatusLabel.Font.Bold = true;
                StatusLabel.Font.Size = FontUnit.Large;

                TEmployeeID.Text = TMissingDate.Text = TMissingHours.Text = "";
            }
        }
    }
}