<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRejectAnnualLeaves.aspx.cs" Inherits="UniversityHR.Academic.ApproveRejectAnnualLeaves" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approve Annual Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Approve/Reject Annual Leave</h2>
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br />

            Request ID: <asp:TextBox ID="txtRequestID" runat="server"></asp:TextBox><br />
            Replacement Employee ID: <asp:TextBox ID="txtReplacementID" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnProcess" runat="server" Text="Process Request" OnClick="btnProcess_Click" />
             <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>