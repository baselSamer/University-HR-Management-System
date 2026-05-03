<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateMonthlyPayroll.aspx.cs" Inherits="WebApplication1.HR.GenerateMonthlyPayroll" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Monthly Payroll</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            background: linear-gradient(160deg, #e6f1ff, #c9ddff);
            font-family: 'Segoe UI', sans-serif;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            padding-top: 60px;
        }

        #MonthlyPayrollForm {
            width: 540px;
            background: #ffffff;
            padding: 38px;
            border-radius: 18px;
            box-shadow: 0 12px 30px rgba(0,0,0,0.15);
            animation: slideFade .7s ease;
        }

        @keyframes slideFade {
            from { opacity: 0; transform: translateY(25px); }
            to { opacity: 1; transform: translateY(0); }
        }

        h2 {
            text-align: center;
            font-size: 1.9rem;
            font-weight: 700;
            color: #1e4e9d;
            margin-bottom: 20px;
            letter-spacing: 1px;
        }

        label {
            display: block;
            margin-top: 16px;
            font-weight: 600;
            color: #333;
        }

        .input-box {
            width: 100%;
            padding: 11px;
            margin-top: 5px;
            border-radius: 10px;
            border: 1px solid #bcd0f7;
            background: #f7faff;
            font-size: 1rem;
            color: #222;
            transition: 0.25s;
        }

        .input-box:focus {
            outline: none;
            background: #ffffff;
            border-color: #1e4e9d;
            box-shadow: 0 0 0 3px rgba(30,78,157,0.15);
        }

        .action-btn {
            width: 100%;
            padding: 12px;
            margin-top: 30px;
            border: none;
            border-radius: 12px;
            background: #1e4e9d;
            color: white;
            font-size: 1.1rem;
            font-weight: bold;
            cursor: pointer;
            transition: .3s;
        }

        .action-btn:hover {
            background: #163b77;
            transform: translateY(-2px);
            box-shadow: 0 6px 16px rgba(30,78,157,0.25);
        }

        .status-label {
            margin-top: 20px;
            display: block;
            color: #d00000;
            font-weight: 600;
            min-height: 22px;
        }

        .back-btn {
            margin-bottom: 15px;
            background: #e8efff;
            border: 1px solid #bcd0f7;
            color: #1e4e9d;
            padding: 8px 14px;
            border-radius: 10px;
            cursor: pointer;
            font-weight: 600;
            transition: .25s;
        }

        .back-btn:hover {
            background: #d6e4ff;
            border-color: #1e4e9d;
        }
    </style>
</head>

<body>
    <form id="MonthlyPayrollForm" runat="server">

        <asp:Button ID="btnBack"
            runat="server"
            Text="← Back"
            PostBackUrl="~/HR/HRhome.aspx"
            CssClass="back-btn" />

        <h2>Generate Monthly Payroll</h2>

        <label>Month (1–12):</label>
        <asp:TextBox ID="TMonth" runat="server" CssClass="input-box"></asp:TextBox>

        <label>Year (YYYY):</label>
        <asp:TextBox ID="TYear" runat="server" CssClass="input-box"></asp:TextBox>

        <asp:Button ID="btnGeneratePayroll"
            runat="server"
            Text="Generate Payroll"
            CssClass="action-btn"
            OnClick="btnGeneratePayroll_Click" />

        <asp:Label ID="Status" runat="server" CssClass="status-label"></asp:Label>

    </form>
</body>
</html>


