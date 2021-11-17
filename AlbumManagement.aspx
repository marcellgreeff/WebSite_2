<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumManagement.aspx.cs" Inherits="WebSite_2.AlbumManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:LinkButton ID="lbBack" runat="server" OnClick="lbBack_Click">Back</asp:LinkButton>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Album Management"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Add New Album"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Album Name"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtAlbumName" runat="server" OnTextChanged="txtAlbumName_TextChanged"></asp:TextBox>
            <br />
            <asp:Button ID="btnAddAlbum" runat="server" OnClick="btnAddAlbum_Click" Text="Add Album" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Select Album"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddAlbums" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete Album" OnClick="btnDelete_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Insert New Album Name"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtNewAlbumName" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update Album" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Select user to give acces to the Album"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnGiveAccess" runat="server" OnClick="btnGiveAccess_Click" Text="Give Access" />
            <br />
            <br />
            <asp:Label ID="lblOutput" runat="server"></asp:Label>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
