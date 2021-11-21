<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebSite_2.Register" %>

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
            <asp:Label ID="Label1" runat="server" Text="Register" Font-Size="Larger"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Id"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:DropDownList ID="ddlQuestion" runat="server">
                <asp:ListItem>Favourite Movie?</asp:ListItem>
                <asp:ListItem>City born in?</asp:ListItem>
                <asp:ListItem>Dog&#39;s name?</asp:ListItem>
                <asp:ListItem>Childhood superhero?</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Answer"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="lblRegister" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
