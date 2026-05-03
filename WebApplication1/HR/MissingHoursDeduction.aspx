<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="MissingHoursDeduction.aspx.cs"
    Inherits="WebApplication1.HR.MissingHoursDeduction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missing Hours Deduction</title>

    <style>
        /* Global Blue/White HR Theme */
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

        /* Main Form Card */
        #MissingHoursDeductionForm {
            width: 520px;
            background: #ffffff;
            padding: 38px;
            border-radius: 16px;
            box-shadow: 0 10px 28px rgba(0,0,0,0.12);
            animation: fadeSlide .6s ease-out;
        }

        @keyframes fadeSlide {
            from { opacity: 0; transform: translateY(22px); }
            to   { opacity: 1; transform: translateY(0); }
        }

        /* Header */
        h2 {
            text-align: center;
            font-size: 1.95rem;
            font-weight: 700;
            color: #1e4e9d;
            margin-bottom: 25px;
            letter-spacing: .5px;
        }

        /* Labels + Inputs */
        label {
            display: block;
            font-weight: 600;
            margin-top: 16px;
        }

        .input-box {
            width: 100%;
            padding: 11px;
            margin-top: 6px;
            border-radius: 10px;
            border: 1px solid #bcd0f7;
            background: #f7faff;
            font-size: 1rem;
            color: #111;
            transition: 0.25s ease;
        }

        .input-box:focus {
            outline: none;
            background: #ffffff;
            border-color: #1e4e9d;
            box-shadow: 0 0 0 3px rgba(30,78,157,0.18);
        }

        /* Main Submit Button */
        .action-btn {
            width: 100%;
            padding: 12px;
            margin-top: 28px;
            background: #1e4e9d;
            border: none;
            border-radius: 12px;
            color: white;
            font-size: 1.1rem;
            font-weight: 600;
            cursor: pointer;
            transition: .25s;
        }

        .action-btn:hover {
            background: #163b77;
            transform: translateY(-2px);
            box-shadow: 0 6px 15px rgba(30,78,157,0.25);
        }

        /* Back Button */
        .back-btn {
            background: #e8efff;
            border: 1px solid #bcd0f7;
            color: #1e4e9d;
            padding: 8px 14px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: 600;
            margin-bottom: 15px;
            transition: .25s ease;
        }

        .back-btn:hover {
            background: #d6e4ff;
            border-color: #1e4e9d;
        }

        /* Status Label */
        .status-label {
            margin-top: 20px;
            min-height: 22px;
            font-weight: 600;
            color: #d00000;
            display: block;
        }
    </style>

</head>

<body>
    <form id="MissingHoursDeductionForm" runat="server">

        <asp:Button ID="btnBack"
            runat="server"
            Text="← Back"
            PostBackUrl="~/HR/HRhome.aspx"
            CssClass="back-btn" />

        <h2>Missing Hours Deduction</h2>

        <label>Employee ID:</label>
        <asp:TextBox ID="TEmployeeID" runat="server" CssClass="input-box" placeholder="e.g. 20"></asp:TextBox>

        <label>Date:</label>
        <asp:TextBox ID="TMissingDate" runat="server" CssClass="input-box" TextMode="Date"></asp:TextBox>

        <label>Total Missing Hours:</label>
        <asp:TextBox ID="TMissingHours" runat="server" CssClass="input-box" placeholder="e.g. 3.5"></asp:TextBox>

        <asp:Button 
            ID="btnAddDeduction"
            runat="server"
            Text="Record Deduction"
            CssClass="action-btn"
            OnClick="btnAddDeduction_Click" />

        <asp:Label ID="Status" runat="server" CssClass="status-label"></asp:Label>

    </form>
</body>
</html>
