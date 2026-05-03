<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_home.aspx.cs" Inherits="WebApplication1.Admin_home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body { font-family: sans-serif; padding: 20px; }
        .section { border: 1px solid #ccc; padding: 15px; margin-bottom: 20px; border-radius: 5px; }
        h2 { border-bottom: 2px solid #333; padding-bottom: 10px; }
        h3 { margin-top: 0; color: #007bff; }
        .grid-container { margin-top: 10px; overflow-x: auto; }
        .btn { padding: 5px 10px; cursor: pointer; background-color: #28a745; color: white; border: none; }
        .btn:hover { background-color: #218838; }
        .input-group { margin-bottom: 10px; }
        label { display: inline-block; width: 150px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Admin Dashboard</h1>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>

        <div class="section">
            <h2>View System Data</h2>
            
            <h3>All Employees</h3>
            <asp:Button ID="btnViewEmployees" runat="server" Text="View Employees" OnClick="btnViewEmployees_Click" CssClass="btn" />
            <div class="grid-container"><asp:GridView ID="gridEmployees" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"></asp:GridView></div>

            <h3>Employees per Department</h3>
            <asp:Button ID="btnViewDeptCount" runat="server" Text="View Count" OnClick="btnViewDeptCount_Click" CssClass="btn" />
            <div class="grid-container"><asp:GridView ID="gridDeptCount" runat="server"></asp:GridView></div>

            <h3>Rejected Medical Leaves</h3>
            <asp:Button ID="btnViewRejected" runat="server" Text="View Rejected" OnClick="btnViewRejected_Click" CssClass="btn" />
            <div class="grid-container"><asp:GridView ID="gridRejected" runat="server"></asp:GridView></div>

            <h3>Yesterday's Attendance</h3>
            <asp:Button ID="btnViewAttendance" runat="server" Text="View Attendance" OnClick="btnViewAttendance_Click" CssClass="btn" />
            <div class="grid-container"><asp:GridView ID="gridAttendance" runat="server"></asp:GridView></div>

            <h3>Winter Performance</h3>
            <asp:Button ID="btnViewPerformance" runat="server" Text="View Performance" OnClick="btnViewPerformance_Click" CssClass="btn" />
            <div class="grid-container"><asp:GridView ID="gridPerformance" runat="server"></asp:GridView></div>
        </div>

        <div class="section">
            <h2>Administrative Actions</h2>

            <div class="input-group">
                <h3>General Actions</h3>
                <asp:Button ID="btnRemoveDeductions" runat="server" Text="Remove Resigned Deductions" OnClick="btnRemoveDeductions_Click" CssClass="btn" />
                <asp:Button ID="btnInitAttendance" runat="server" Text="Initiate Daily Attendance" OnClick="btnInitAttendance_Click" CssClass="btn" />
                <asp:Button ID="btnRemoveHolidayAtt" runat="server" Text="Clean Holiday Attendance" OnClick="btnRemoveHolidayAtt_Click" CssClass="btn" />
                <asp:Button ID="btnUpdateDocs" runat="server" Text="Update Expired Documents" OnClick="btnUpdateDocs_Click" CssClass="btn" />
            </div>

            <hr />

            <div class="input-group">
                <h3>Add New Holiday</h3>
                <label>Name:</label> <asp:TextBox ID="txtHolName" runat="server"></asp:TextBox><br />
                <label>From:</label> <asp:TextBox ID="txtHolStart" runat="server" TextMode="Date"></asp:TextBox><br />
                <label>To:</label> <asp:TextBox ID="txtHolEnd" runat="server" TextMode="Date"></asp:TextBox><br />
                <asp:Button ID="btnAddHoliday" runat="server" Text="Add Holiday" OnClick="btnAddHoliday_Click" CssClass="btn" />
            </div>

            <hr />

            <div class="input-group">
                <h3>Update Attendance</h3>
                <label>Emp ID:</label> <asp:TextBox ID="txtAttEmpID" runat="server"></asp:TextBox><br />
                <label>Check In:</label> <asp:TextBox ID="txtCheckIn" runat="server" TextMode="Time"></asp:TextBox><br />
                <label>Check Out:</label> <asp:TextBox ID="txtCheckOut" runat="server" TextMode="Time"></asp:TextBox><br />
                <asp:Button ID="btnUpdateAtt" runat="server" Text="Update" OnClick="btnUpdateAtt_Click" CssClass="btn" />
            </div>

            <hr />

            <div class="input-group">
                <h3>Employee Management</h3>
                <label>Target Emp ID:</label> <asp:TextBox ID="txtTargetEmp" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnRemoveDayOff" runat="server" Text="Remove DayOff Attendance" OnClick="btnRemoveDayOff_Click" CssClass="btn" />
                <asp:Button ID="btnRemoveApproved" runat="server" Text="Remove Approved Leave Attendance" OnClick="btnRemoveApproved_Click" CssClass="btn" />
                <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Employment Status" OnClick="btnUpdateStatus_Click" CssClass="btn" />
            </div>

            <hr />

            <div class="input-group">
                <h3>Replace Employee</h3>
                <label>Old Emp ID:</label> <asp:TextBox ID="txtRepOld" runat="server"></asp:TextBox><br />
                <label>New Emp ID:</label> <asp:TextBox ID="txtRepNew" runat="server"></asp:TextBox><br />
                <label>From:</label> <asp:TextBox ID="txtRepStart" runat="server" TextMode="Date"></asp:TextBox><br />
                <label>To:</label> <asp:TextBox ID="txtRepEnd" runat="server" TextMode="Date"></asp:TextBox><br />
                <asp:Button ID="btnReplace" runat="server" Text="Replace" OnClick="btnReplace_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</body>
</html>