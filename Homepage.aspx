<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebSite_2.Homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Font-Size="Larger" Text="Home"></asp:Label>
        <br />
        <br />
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem NavigateUrl="~/Changes.aspx" Text="Upload/Download" Value="Upload/Download"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/SearchShare.aspx" Text="Search/Share" Value="Search/Share"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="DataManagement.aspx" Text="Data Management" Value="Data Management"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="AlbumManagement.aspx" Text="Album Management" Value="Album Management"></asp:MenuItem>
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
