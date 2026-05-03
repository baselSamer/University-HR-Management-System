<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyAccidentalLeave.aspx.cs" Inherits="UniversityHR.Academic.ApplyAccidentalLeave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply Accidental Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Apply for Accidental Leave</h2>
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br />

            Start Date: <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox><br />
            End Date: <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox><br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit Accidental Leave" OnClick="btnSubmit_Click" />
            <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>