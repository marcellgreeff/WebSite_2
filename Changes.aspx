<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Changes.aspx.cs" Inherits="WebSite_2.Changes" %>

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
            <asp:Label ID="Label1" runat="server" Text="Upload/Download"></asp:Label>
            <br />
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Location"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddType" runat="server">
                <asp:ListItem>Nature</asp:ListItem>
                <asp:ListItem>Personal</asp:ListItem>
                <asp:ListItem>Workout</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload Image and Data" OnClick="btnUpload_Click" />
            <br />
            <br />
            <asp:GridView ID="gvDelete" runat="server" AutoGenerateColumns="False" OnRowCommand="gvDelete_RowCommand" Width="256px">
                <Columns>
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDownload" runat="server" CommandArgument='<%# Eval("File") %>' CommandName="Download" Text='<%# Eval("File") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
        </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
