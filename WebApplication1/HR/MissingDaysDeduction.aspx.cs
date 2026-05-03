using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class MissingDaysDeduction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddDaysDeduction_Click(object sender, EventArgs e)
        {
            Label StatusLabel = Status;

            if (!int.TryParse(TEmployeeID.Text, out int empID) || empID <= 0)
            {
                StatusLabel.Text = "Invalid Employee ID";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!DateTime.TryParse(TStartDate.Text, out DateTime startDate))
            {
                StatusLabel.Text = "Invalid Start Date";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!DateTime.TryParse(TEndDate.Text, out DateTime endDate))
            {
                StatusLabel.Text = "Invalid End Date";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (endDate < startDate)
            {
                StatusLabel.Text = "End Date must be after Start Date";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            int missingDays = (endDate - startDate).Days + 1;
            decimal deductionAmount = missingDays * 1000m;

            string cs = ConfigurationManager.ConnectionStrings["OurApp"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = @"
                    INSERT INTO Deduction (emp_ID, date, amount, type, status)
                    VALUES (@id, GETDATE(), @amount, 'missing_days', 'finalized');

                    UPDATE Payroll
                    SET deductions_amount = ISNULL(deductions_amount, 0) + @amount,
                        final_salary_amount = final_salary_amount - @amount
                    WHERE emp_ID = @id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", empID);
                    cmd.Parameters.AddWithValue("@amount", deductionAmount);
                    cmd.ExecuteNonQuery();
                }

                StatusLabel.Text = $"SUCCESS: {missingDays} missing days → {deductionAmount} EGP deducted from Employee {empID}";
                StatusLabel.ForeColor = Color.LimeGreen;
                StatusLabel.Font.Bold = true;
                StatusLabel.Font.Size = FontUnit.Large;

                TEmployeeID.Text = TStartDate.Text = TEndDate.Text = "";
            }
        }
    }
}