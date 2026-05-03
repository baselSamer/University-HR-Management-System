<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnnualAndAccidentalLeavesStatus.aspx.cs" Inherits="WebApplication1.HR.AnnualAndAccidentalLeavesStatus" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Leave Requests</title>

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

        #form1 {
            width: 95%;
            max-width: 1100px;
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

        p {
            font-size: 1rem;
            margin-bottom: 10px;
            color: #333;
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

        .grid-box {
            margin-top: 15px;
            overflow-x: auto;
            border-radius: 10px;
        }

        asp\:GridView, .grid-table {
            width: 100%;
            border-collapse: collapse;
        }

        asp\:GridView th, asp\:GridView td, .grid-table th, .grid-table td {
            border: 1px solid #bcd0f7;
            padding: 10px 8px;
            text-align: left;
            color: #222;
            background: #f7faff;
        }

        asp\:GridView th, .grid-table th {
            background: #e8efff;
            font-weight: 600;
        }

        .process-button {
            background: #1e4e9d;
            color: #fff;
            border: none;
            padding: 6px 10px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            transition: .3s;
        }

        .process-button:hover {
            background: #163b77;
        }

        .status-label {
            margin-top: 20px;
            display: block;
            color: #d00000;
            font-weight: 600;
            min-height: 22px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <asp:Button ID="btnBack" runat="server" 
            Text="← Back"
            PostBackUrl="~/HR/HRhome.aspx"
            CssClass="back-btn" />

        <h2>Annual & Accidental Leaves</h2>

        <p>These requests are pending your approval:</p>

        <div class="grid-box">
            <asp:GridView ID="PendingLeavesTable" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="request_ID"
                OnRowCommand="PendingLeavesTable_RowCommand"
                GridLines="None"
                CellPadding="6"
                ForeColor="#222"
                BackColor="#f7faff">

                <Columns>
                    <asp:BoundField DataField="request_ID" HeaderText="Request ID" />
                    <asp:BoundField DataField="emp_ID" HeaderText="Employee ID" />
                    <asp:BoundField DataField="start_date" HeaderText="Start" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="end_date" HeaderText="End" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="duration_days" HeaderText="Days" />
                    <asp:BoundField DataField="leave_type" HeaderText="Type" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnProcess" runat="server"
                                Text="Process"
                                CssClass="process-button"
                                CommandName="ProcessLeave"
                                CommandArgument='<%# Eval("request_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>

        <asp:Label ID="ProcessingStatus" runat="server" CssClass="status-label"></asp:Label>

    </form>
</body>
</html>
