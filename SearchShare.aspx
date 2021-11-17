<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchShare.aspx.cs" Inherits="WebSite_2.Search" %>

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
            <asp:Label ID="Label1" runat="server" Text="Search/Share"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Search photo by:"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddSearchBy1" runat="server">
                <asp:ListItem>UserUpload</asp:ListItem>
                <asp:ListItem>Location</asp:ListItem>
                <asp:ListItem>Type</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lblSearchBy" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSearchBy" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSearchBy" runat="server" Text="Search" OnClick="btnSearchBy_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddImages" runat="server">
            </asp:DropDownList>
            &nbsp;
            <br />
&nbsp;
            <br />
            <asp:Label ID="Label3" runat="server" Text="If you would like to share/unshare this image, please select the Id of the person to share/unshare with and click share."></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="ddShare" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnShare" runat="server" OnClick="btnShare_Click" Text="Share" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUnshare" runat="server" OnClick="btnUnshare_Click" Text="Unshare" />
            <br />
            <br />
            <asp:Label ID="lblOutput" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
