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
using System.IO;
using System.Diagnostics;

namespace WebSite_2
{
    public partial class Search : System.Web.UI.Page
    {
        public string imageName;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearchBy.Attributes.Add("autocomplete", "off");

            string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
            // connection string  
            SqlConnection con = new SqlConnection(sCon);
            con.Open();

            SqlCommand com = new SqlCommand("SELECT UserId FROM [User]", con);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  
            ddShare.DataTextField = ds.Tables[0].Columns["UserId"].ToString(); // text field name of table dispalyed in dropdown       
            ddShare.DataValueField = ds.Tables[0].Columns["UserId"].ToString();
            // to retrive specific  textfield name   
            ddShare.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
            ddShare.DataBind();  //binding dropdownlist  

            Label3.Visible = false;
            btnShare.Visible = false;
            imgOutput.Visible = false;
            ddShare.Visible = false;
            ddImages.Visible = false;
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearchBy_Click(object sender, EventArgs e)
        {
            if (txtSearchBy.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Image.Name FROM [Access] Inner JOIN [Image] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "' AND Image." + ddSearchBy1.SelectedValue + " = '" + txtSearchBy.Text + "'", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                
              //  string x = "SELECT Image.Name FROM [Access] INNER JOIN [Image] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "' AND Image." + ddSearchBy1.SelectedValue + " = '" + txtSearchBy.Text + "'";
               // Debug.WriteLine("\n\n\n\nA=" + x.ToString() + "=A\n\n\n");
                cmd.Connection.Open();
                imageName = cmd.ExecuteScalar().ToString();

              /*  string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
                // connection string  
                SqlConnection con = new SqlConnection(sCon);
                con.Open();
                // table name   
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset  
                ddImages.DataTextField = ds.Tables[0].Columns["Name"].ToString(); // text field name of table dispalyed in dropdown       
                ddImages.DataValueField = ds.Tables[0].Columns["Name"].ToString();
                // to retrive specific  textfield name   
                ddShare.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
                ddShare.DataBind();  //binding dropdownlist  
              */
                Label3.Visible = true;
                btnShare.Visible = true;
                imgOutput.Visible = true;
                ddShare.Visible = true;
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            if (ddShare.SelectedIndex > -1)
            {
                SqlCommand cmdimage = new SqlCommand("SELECT Id FROM [Image] WHERE Name = '" + imageName + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdimage.Connection.Open();
                string imageId = cmdimage.ExecuteScalar().ToString();
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT [Access] (UserId, ImageId) VALUES ('" + ddShare.SelectedItem + "', '" + imageId + "')";
                cmd.Connection = sqlCon;
            }
        }
    }
}