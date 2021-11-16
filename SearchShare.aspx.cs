using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace WebSite_2
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearchBy.Attributes.Add("autocomplete", "off");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearchBy_Click(object sender, EventArgs e)
        {
            if (txtSearchBy.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Name FROM [Image] INNER JOIN [Access] on [Image.Id] = [Access.ImageId] WHERE [Access.UserId] = " + Login.ID + " AND [Image." + ddSearchBy1.SelectedValue + "] = " + txtSearchBy.Text + ");", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmd.Connection.Open();
                lbltest.Text = cmd.ExecuteScalar().ToString();
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT [Access] (UserId, ImageId) VALUES ('" + txtId.Text + "')";
                cmd.Connection = sqlCon;
            }
        }
    }
}