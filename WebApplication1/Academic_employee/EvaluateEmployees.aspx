<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluateEmployees.aspx.cs" Inherits="UniversityHR.Academic.EvaluateEmployees" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluate Employees</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Evaluate Employee (Dean Only)</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label><br /><br />

            Target Employee ID: <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox><br />
            Rating (1-5): <asp:TextBox ID="txtRating" runat="server" TextMode="Number"></asp:TextBox><br />
            Comments: <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            Semester (e.g., W24): <asp:TextBox ID="txtSemester" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnEvaluate" runat="server" Text="Submit Evaluation" OnClick="btnEvaluate_Click" />
            <br /><br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>