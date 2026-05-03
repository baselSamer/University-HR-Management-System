<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="MissingDaysDeduction.aspx.cs" 
    Inherits="WebApplication1.HR.MissingDaysDeduction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missing Days Deduction</title>

    <style>
        /* GLOBAL THEME */
        body {
            margin: 0;
            padding: 0;
            background: #f0f5ff;
            font-family: 'Segoe UI', sans-serif;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            padding-top: 60px;
            color: #1e2a42;
        }

        /* MAIN CARD */
        #MissingDaysDeductionForm {
            width: 520px;
            background: #ffffff;
            padding: 40px 38px;
            border-radius: 16px;
            box-shadow: 0 8px 26px rgba(0,0,0,0.12);
            animation: fadeSlide .65s ease-out;
        }

        @keyframes fadeSlide {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        /* HEADER */
        h2 {
            text-align: center;
            font-size: 1.95rem;
            font-weight: 700;
            color: #1e4e9d;
            margin-bottom: 25px;
            letter-spacing: 0.5px;
        }

        /* LABELS & INPUTS */
        label {
            display: block;
            margin-top: 16px;
            font-weight: 600;
        }

        .input-box {
            width: 100%;
            padding: 11px;
            margin-top: 6px;
            border-radius: 10px;
            border: 1px solid #bcd0f7;
            background: #f7faff;
            color: #111;
            font-size: 1rem;
            transition: 0.25s;
        }

        .input-box:focus {
            outline: none;
            background: #ffffff;
            border-color: #1e4e9d;
            box-shadow: 0 0 0 3px rgba(30,78,157,0.18);
        }

        /* ACTION BUTTON */
        .action-btn {
            width: 100%;
            padding: 12px;
            margin-top: 28px;
            border: none;
            border-radius: 12px;
            background: #1e4e9d;
            color: white;
            font-size: 1.1rem;
            font-weight: 600;
            cursor: pointer;
            transition: 0.25s;
        }

        .action-btn:hover {
            background: #163b77;
            transform: translateY(-2px);
            box-shadow: 0 6px 16px rgba(30,78,157,0.24);
        }

        /* STATUS LABEL */
        .status-label {
            margin-top: 18px;
            min-height: 22px;
            font-weight: 600;
            color: #d00000;
            display: block;
        }

        /* BACK BUTTON */
        .back-btn {
            background: #e8efff;
            border: 1px solid #bcd0f7;
            color: #1e4e9d;
            padding: 8px 14px;
            border-radius: 8px;
            font-weight: 600;
            cursor: pointer;
            transition: 0.25s;
            margin-bottom: 15px;
        }

        .back-btn:hover {
            background: #d6e4ff;
            border-color: #1e4e9d;
        }
    </style>
</head>

<body>
    <form id="MissingDaysDeductionForm" runat="server">

        <asp:Button ID="btnBack"
            runat="server"
            Text="← Back"
            PostBackUrl="~/HR/HRhome.aspx"
            CssClass="back-btn" />

        <h2>Missing Days Deduction</h2>

        <label>Employee ID:</label>
        <asp:TextBox ID="TEmployeeID" runat="server" CssClass="input-box"></asp:TextBox>

        <label>Start Date:</label>
        <asp:TextBox ID="TStartDate" runat="server" CssClass="input-box" TextMode="Date"></asp:TextBox>

        <label>End Date:</label>
        <asp:TextBox ID="TEndDate" runat="server" CssClass="input-box" TextMode="Date"></asp:TextBox>

        <label>Total Missing Days:</label>
        <asp:TextBox ID="TTotalDays" runat="server" CssClass="input-box"></asp:TextBox>

        <asp:Button 
            ID="btnAddDaysDeduction"
            runat="server"
            Text="Record Deduction"
            CssClass="action-btn"
            OnClick="btnAddDaysDeduction_Click" />

        <asp:Label ID="Status" runat="server" CssClass="status-label"></asp:Label>

    </form>
</body>
</html>

