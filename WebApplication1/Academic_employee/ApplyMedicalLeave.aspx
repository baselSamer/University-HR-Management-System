<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMedicalLeave.aspx.cs" Inherits="UniversityHR.Academic.ApplyMedicalLeave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply Medical Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Apply for Medical Leave</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label><br /><br />

            Start Date: <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox><br />
            End Date: <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox><br />
            
            Type: 
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Value="sick">Sick</asp:ListItem>
                <asp:ListItem Value="maternity">Maternity</asp:ListItem>
            </asp:DropDownList><br />

            Covered by Insurance? <asp:CheckBox ID="chkInsurance" runat="server" /><br />
            
            Disability Details: <asp:TextBox ID="txtDisability" runat="server"></asp:TextBox><br />
            
            Document Description: <asp:TextBox ID="txtDocDesc" runat="server"></asp:TextBox><br />
            Upload File: <asp:FileUpload ID="fileUploadDoc" runat="server" /><br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit Medical Leave" OnClick="btnSubmit_Click" />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Academic_Home.aspx">Back to Dashboard</asp:HyperLink>
        </div>
    </form>
</body>
</html>