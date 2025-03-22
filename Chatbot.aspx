<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chatbot.aspx.cs" Inherits="chatdemo.Chatbot" Async="true" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Chatbot Thông Minh</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Chatbot Thông Minh</h2>

        <asp:TextBox ID="txtUserInput" runat="server" Width="400px" placeholder="Nhập câu hỏi..."></asp:TextBox>
        <asp:Button ID="btnSend" runat="server" Text="Gửi" OnClick="btnSend_Click" />

        <div>
            <h3>Kết quả:</h3>
            <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>