<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCompensationLeave.aspx.cs" Inherits="UniversityHR.Academic.ApplyCompensationLeave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply Compensation Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Apply for Compensation Leave</h2>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br /><br />

            Compensation Date (Day you want off): <asp:TextBox ID="txtCompDate" runat="server" TextMode="Date"></asp:TextBox><br />
            Reason: <asp:TextBox ID="txtReason" runat="server"></asp:TextBox><br />
            Date of Original Work (Day you worked extra): <asp:TextBox ID="txtOriginalDate" runat="server" TextMode="Date"></asp:TextBox><br />
            Replacement Employee ID: <asp:TextBox ID="txtRepID" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnComp" runat="server" Text="Submit Request" OnClick="btnComp_Click" />
             <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>