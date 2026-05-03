<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRhome.aspx.cs" Inherits="WebApplication1.HR.HRhome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Dashboard</title>
    <style>
        body, html { margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; height: 100%; background: linear-gradient(160deg, #e6f1ff, #c9ddff); }
        form { height: 100%; }
        .topbar { width: 100%; height: 60px; background: #1e4e9d; color: white; display: flex; align-items: center; padding: 0 25px; font-size: 1.25rem; font-weight: bold; box-shadow: 0 4px 10px rgba(0,0,0,0.15); position: fixed; top: 0; left: 0; z-index: 100; }
        .wrapper { display: flex; height: 100%; padding-top: 60px; }
        .sidebar { width: 260px; background: #ffffff; box-shadow: 2px 0 12px rgba(0,0,0,0.1); display: flex; flex-direction: column; padding-top: 25px; border-right: 1px solid #e2e8f0; }
        .side-title { padding: 12px 20px; font-size: 0.8rem; font-weight: 700; text-transform: uppercase; color: #1e4e9d; opacity: 0.7; margin-top: 20px; }
        .nav-item { padding: 12px 20px; font-size: 1rem; color: #1f2937; text-decoration: none; display: block; border-left: 4px solid transparent; border-radius: 4px; margin: 2px 12px; transition: all 0.25s ease; cursor: pointer; }
        .nav-item:hover { background: #1e4e9d; color: #ffffff; border-left-color: #ffc400; }
        .content-area { flex: 1; padding: 50px 40px; overflow-y: auto; display: flex; justify-content: center; align-items: flex-start; }
        .welcome-box { background: #ffffff; padding: 35px 40px; border-radius: 15px; box-shadow: 0 8px 30px rgba(0,0,0,0.1); max-width: 900px; width: 100%; text-align: center; }
        .welcome-box h1 { margin: 0; font-size: 2.2rem; color: #1e4e9d; }
        .welcome-box p { margin-top: 15px; font-size: 1.1rem; color: #4b5563; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="topbar">HR Control Panel</div>
        <div class="wrapper">
            <div class="sidebar">
                <div class="side-title">Leave Approvals</div>
                <asp:LinkButton ID="btnUnpaid" CssClass="nav-item" runat="server" OnClick="btnUnpaid_Click">Unpaid Leave Requests</asp:LinkButton>
                <asp:LinkButton ID="btnAccidental" CssClass="nav-item" runat="server" OnClick="btnAccidental_Click">Annual & Accidental Requests</asp:LinkButton>
                <asp:LinkButton ID="btnCompensation" CssClass="nav-item" runat="server" OnClick="btnCompensation_Click">Compensation Leave Requests</asp:LinkButton>

                <div class="side-title">Payroll Management</div>
                <asp:LinkButton ID="btnMonthlyPayroll" CssClass="nav-item" runat="server" OnClick="btnMonthlyPayroll_Click">Generate Monthly Payroll</asp:LinkButton>
                <asp:LinkButton ID="btnDaysDeduction" CssClass="nav-item" runat="server" OnClick="btnDaysDeduction_Click">Missing Days Deduction</asp:LinkButton>
                <asp:LinkButton ID="btnHoursDeduction" CssClass="nav-item" runat="server" OnClick="btnHoursDeduction_Click">Missing Hours Deduction</asp:LinkButton>
                <asp:LinkButton ID="btnUnpaidDeduction" CssClass="nav-item" runat="server" OnClick="btnUnpaidDeduction_Click">Unpaid Leave Deduction</asp:LinkButton>
            </div>
            <div class="content-area">
                <div class="welcome-box">
                    <h1>Welcome, HR Employee</h1>
                    <p>Select any function from the left sidebar to get started.</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>