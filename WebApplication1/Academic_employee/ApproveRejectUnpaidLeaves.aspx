<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRejectUnpaidLeaves.aspx.cs" Inherits="UniversityHR.Academic.ApproveRejectUnpaidLeaves" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approve Unpaid Leaves</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Approve Unpaid Leave (Upper Board)</h2>
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br />

            Leave Request ID: <asp:TextBox ID="txtRequestID" runat="server"></asp:TextBox><br /><br />
            
            <asp:Button ID="btnApprove" runat="server" Text="Process Approval" OnClick="btnApprove_Click" />
            <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>