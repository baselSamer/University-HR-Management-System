<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyUnpaidLeave.aspx.cs" Inherits="UniversityHR.Academic.ApplyUnpaidLeave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply Unpaid Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Apply for Unpaid Leave</h2>
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br />

            Start Date: <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox><br />
            End Date: <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox><br />
            Document Description (Memo): <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox><br />
            Upload Supporting Document: <asp:FileUpload ID="fileUpload" runat="server" /><br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit Unpaid Leave" OnClick="btnSubmit_Click" />
            <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>