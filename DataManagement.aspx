<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataManagement.aspx.cs" Inherits="WebSite_2.DataManagement" %>

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
            <br />
            <asp:Label ID="Label1" runat="server" Text="Search image to update data."></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Image Id"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtImageId" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete Image and Data" />
            <br />
            <br />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search Image" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblOutput" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Type"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" style="height: 26px" Text="Update" />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
