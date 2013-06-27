<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label runat="server" Text="" ID="InfoLabel" />
    <asp:TextBox ID="AddressTB" runat="server" Text="http://www.google.com" />
    <asp:Button ID="GoButton" runat="server" onclick="GoButton_Click" Text="Go" />
    </div>
    </form>
</body>
</html>
