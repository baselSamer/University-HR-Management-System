<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Academic_home.aspx.cs" Inherits="WebApplication1.Academic_home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Employee Dashboard</title>
    <style>
        body { font-family: sans-serif; padding: 20px; }
        .container { display: flex; flex-wrap: wrap; gap: 20px; }
        .section { border: 1px solid #ccc; padding: 15px; border-radius: 5px; flex: 1; min-width: 300px; }
        h2 { border-bottom: 2px solid #333; padding-bottom: 5px; font-size: 1.2em;}
        h3 { color: #0056b3; margin-top: 10px; font-size: 1em; }
        .grid-container { margin-top: 10px; overflow-x: auto; max-height: 200px; }
        .btn { padding: 5px 10px; cursor: pointer; background-color: #007bff; color: white; border: none; margin-top: 5px; }
        .btn:hover { background-color: #0056b3; }
        label { display: block; margin-top: 5px; font-weight: bold; font-size: 0.9em; }
        input[type="text"], input[type="date"] { width: 90%; padding: 4px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Academic Dashboard</h1>
        <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>

        <div class="container">
            <div class="section">
                <h2>My Data</h2>
                
                <h3>View Performance</h3>
                <label>Semester (e.g., W24):</label>
                <asp:TextBox ID="txtPerfSem" runat="server"></asp:TextBox>
                <asp:Button ID="btnViewPerf" runat="server" Text="View" OnClick="btnViewPerf_Click" CssClass="btn" />
                <div class="grid-container"><asp:GridView ID="gridPerf" runat="server"></asp:GridView></div>

                <h3>View Attendance</h3>
                <asp:Button ID="btnViewAtt" runat="server" Text="Load Current Month" OnClick="btnViewAtt_Click" CssClass="btn" />
                <div class="grid-container"><asp:GridView ID="gridAtt" runat="server"></asp:GridView></div>

                <h3>Payroll & Deductions</h3>
                <asp:Button ID="btnViewPay" runat="server" Text="Last Month Payroll" OnClick="btnViewPay_Click" CssClass="btn" /><br />
                <label>Deductions Month (1-12):</label> <asp:TextBox ID="txtDedMonth" runat="server" Width="50px"></asp:TextBox>
                <asp:Button ID="btnViewDed" runat="server" Text="View Deductions" OnClick="btnViewDed_Click" CssClass="btn" />
                <div class="grid-container"><asp:GridView ID="gridFinance" runat="server"></asp:GridView></div>
                
                <h3>My Leave Status</h3>
                <asp:Button ID="btnLeaveStatus" runat="server" Text="Refresh Status" OnClick="btnLeaveStatus_Click" CssClass="btn" />
                <div class="grid-container"><asp:GridView ID="gridLeaveStatus" runat="server"></asp:GridView></div>
            </div>

            <div class="section">
                <h2>Apply for Leave</h2>

                <h3>Annual / Accidental</h3>
                <label>Start:</label> <asp:TextBox ID="txtAnnStart" runat="server" TextMode="Date"></asp:TextBox>
                <label>End:</label> <asp:TextBox ID="txtAnnEnd" runat="server" TextMode="Date"></asp:TextBox>
                <label>Replacement ID (Annual only):</label> <asp:TextBox ID="txtAnnRep" runat="server"></asp:TextBox>
                <asp:Button ID="btnSubAnnual" runat="server" Text="Submit Annual" OnClick="btnSubAnnual_Click" CssClass="btn" />
                <asp:Button ID="btnSubAcc" runat="server" Text="Submit Accidental" OnClick="btnSubAcc_Click" CssClass="btn" />

                <h3>Medical Leave</h3>
                <label>Dates (Start/End):</label> <asp:TextBox ID="txtMedStart" runat="server" TextMode="Date"></asp:TextBox> <asp:TextBox ID="txtMedEnd" runat="server" TextMode="Date"></asp:TextBox>
                <label>Type (sick/maternity):</label> <asp:TextBox ID="txtMedType" runat="server"></asp:TextBox>
                <label>Disability Details:</label> <asp:TextBox ID="txtMedDisability" runat="server"></asp:TextBox>
                <label>Doc Description:</label> <asp:TextBox ID="txtMedDocDesc" runat="server"></asp:TextBox>
                <label>File Name:</label> <asp:TextBox ID="txtMedFile" runat="server"></asp:TextBox>
                <asp:CheckBox ID="chkInsurance" runat="server" Text="Covered by Insurance?" /> <br />
                <asp:Button ID="btnSubMed" runat="server" Text="Submit Medical" OnClick="btnSubMed_Click" CssClass="btn" />

                <h3>Unpaid Leave</h3>
                <label>Start/End:</label> <asp:TextBox ID="txtUnpStart" runat="server" TextMode="Date"></asp:TextBox> <asp:TextBox ID="txtUnpEnd" runat="server" TextMode="Date"></asp:TextBox>
                <label>Desc/File:</label> <asp:TextBox ID="txtUnpDesc" runat="server"></asp:TextBox> <asp:TextBox ID="txtUnpFile" runat="server"></asp:TextBox>
                <asp:Button ID="btnSubUnpaid" runat="server" Text="Submit Unpaid" OnClick="btnSubUnpaid_Click" CssClass="btn" />

                <h3>Compensation Leave</h3>
                <label>Comp Date:</label> <asp:TextBox ID="txtCompDate" runat="server" TextMode="Date"></asp:TextBox>
                <label>Worked Day:</label> <asp:TextBox ID="txtCompWorkDay" runat="server" TextMode="Date"></asp:TextBox>
                <label>Reason:</label> <asp:TextBox ID="txtCompReason" runat="server"></asp:TextBox>
                <label>Replacement ID:</label> <asp:TextBox ID="txtCompRep" runat="server"></asp:TextBox>
                <asp:Button ID="btnSubComp" runat="server" Text="Submit Compensation" OnClick="btnSubComp_Click" CssClass="btn" />
            </div>

            <div class="section">
                <h2>Management (Dean/Upper Board)</h2>
                
                <h3>Approve Annual Leave</h3>
                <label>Request ID:</label> <asp:TextBox ID="txtAppAnnReqID" runat="server"></asp:TextBox>
                <label>Replacement ID:</label> <asp:TextBox ID="txtAppAnnRepID" runat="server"></asp:TextBox>
                <asp:Button ID="btnApproveAnnual" runat="server" Text="Process Annual" OnClick="btnApproveAnnual_Click" CssClass="btn" />

                <h3>Approve Unpaid Leave</h3>
                <label>Request ID:</label> <asp:TextBox ID="txtAppUnpReqID" runat="server"></asp:TextBox>
                <asp:Button ID="btnApproveUnpaid" runat="server" Text="Process Unpaid" OnClick="btnApproveUnpaid_Click" CssClass="btn" />

                <h3>Evaluate Employee (Dean)</h3>
                <label>Emp ID:</label> <asp:TextBox ID="txtEvalEmpID" runat="server"></asp:TextBox>
                <label>Rating (1-5):</label> <asp:TextBox ID="txtEvalRating" runat="server"></asp:TextBox>
                <label>Semester:</label> <asp:TextBox ID="txtEvalSem" runat="server"></asp:TextBox>
                <label>Comment:</label> <asp:TextBox ID="txtEvalComm" runat="server"></asp:TextBox>
                <asp:Button ID="btnEvaluate" runat="server" Text="Submit Evaluation" OnClick="btnEvaluate_Click" CssClass="btn" />
            </div>
        </div>
        <div style="text-align: right; margin-bottom: 20px;">
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn" />
</div>
    </form>
</body>
</html>