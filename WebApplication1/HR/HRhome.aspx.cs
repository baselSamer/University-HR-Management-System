using System;
using System.Web.UI;

namespace WebApplication1.HR
{
    public partial class HRhome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Authorization Check using your project's logic
            if (Session["user_type"] == null || Session["user_type"].ToString() != "HR")
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        // Navigation Redirects to the specific pages in the HR folder

        protected void btnUnpaid_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/UnpaidLeavesStatus.aspx");
        }

        protected void btnAccidental_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/AnnualAndAccidentalLeavesStatus.aspx");
        }

        protected void btnCompensation_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/CompensationLeaveStatus.aspx");
        }

        protected void btnMonthlyPayroll_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/GenerateMonthlyPayroll.aspx");
        }

        protected void btnDaysDeduction_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/MissingDaysDeduction.aspx");
        }

        protected void btnHoursDeduction_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/MissingHoursDeduction.aspx");
        }

        protected void btnUnpaidDeduction_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HR/UnpaidLeaveDeduction.aspx");
        }
    }
}