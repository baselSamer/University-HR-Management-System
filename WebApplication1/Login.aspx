<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>University Login</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(160deg, #e6f1ff, #c9ddff);
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-container {
            width: 420px;
            background: #ffffff;
            padding: 40px 45px;
            border-radius: 18px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.15);
            text-align: center;
            animation: fadein 0.8s ease;
        }

        @keyframes fadein {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .title {
            font-size: 2rem;
            margin-bottom: 30px;
            color: #1e4e9d; /* deep blue */
            font-weight: 700;
            letter-spacing: 1px;
        }

        label {
            font-size: 0.9rem;
            color: #333;
            display: block;
            text-align: left;
            margin-bottom: 6px;
            margin-top: 18px;
            font-weight: 600;
        }

        .input-box {
            width: 100%;
            padding: 12px;
            border-radius: 10px;
            border: 1px solid #bcd0f7;
            background: #f8faff;
            font-size: 1rem;
            color: #333;
            transition: 0.25s;
            box-sizing: border-box; /* Ensures padding doesn't break width */
        }

        .input-box:focus {
            border-color: #1e4e9d;
            box-shadow: 0 0 0 3px rgba(30, 78, 157, 0.15);
            outline: none;
            background: #ffffff;
        }

        .login-btn {
            width: 100%;
            margin-top: 28px;
            padding: 12px;
            border: none;
            border-radius: 12px;
            background: #1e4e9d;
            color: white;
            font-size: 1.1rem;
            font-weight: 600;
            cursor: pointer;
            transition: 0.3s;
        }

        .login-btn:hover {
            background: #163b77;
            transform: translateY(-2px);
            box-shadow: 0 8px 18px rgba(30,78,157,0.25);
        }

        .error-label {
            display: block;
            color: #d00000;
            font-weight: bold;
            margin-top: 20px;
            min-height: 22px;
            font-size: 0.9rem;
        }

    </style>
</head>
<body>

    <form id="form1" runat="server">

        <div class="login-container">

            <div class="title">University System</div>

            <label>User ID</label>
            <!-- Changed ID to 'username' to match backend -->
            <asp:TextBox ID="username" runat="server" CssClass="input-box" placeholder="Enter your ID"></asp:TextBox>

            <label>Password</label>
            <!-- Changed ID to 'password' to match backend -->
            <asp:TextBox ID="password" runat="server" CssClass="input-box" TextMode="Password" placeholder="Enter your password"></asp:TextBox>

            <asp:Button ID="signin" runat="server"
                Text="Log In"
                CssClass="login-btn"
                OnClick="login" />

            <!-- This label will show errors nicely instead of alert boxes -->
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>

        </div>

    </form>

</body>
</html>