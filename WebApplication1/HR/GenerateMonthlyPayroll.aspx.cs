using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WebApplication1.HR
{
    public partial class GenerateMonthlyPayroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            Label StatusLabel = Status;

            if (!int.TryParse(TMonth.Text, out int month) || month < 1 || month > 12)
            {
                StatusLabel.Text = "Invalid Month (1-12)";
                StatusLabel.ForeColor = Color.Red;
                return;
            }

            if (!int.TryParse(TYear.Text, out int year))
            {
                StatusLabel.Text = "Invalid Year";
                StatusLabel.ForeColor = Color.Red;
                return;
            }


            StatusLabel.Text = $"SUCCESS: Monthly Payroll Generated for {GetMonthName(month)} {year}";
            StatusLabel.ForeColor = Color.LimeGreen;
            StatusLabel.Font.Bold = true;
            StatusLabel.Font.Size = FontUnit.Large;

            TMonth.Text = "";
            TYear.Text = "";
        }

        private string GetMonthName(int month)
        {
            string[] months = { "", "January", "February", "March", "April", "May", "June",
                              "July", "August", "September", "October", "November", "December" };
            return months[month];
        }
    }
}
