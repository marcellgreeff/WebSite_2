<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebSite_2.Homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem NavigateUrl="~/Changes.aspx" Text="Changes" Value="Changes"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/SearchShare.aspx" Text="Search/Share" Value="Search/Share"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="DataManagement.aspx" Text="DataManagement" Value="DataManagement"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Login.aspx" Text="Log Out" Value="Log Out"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
