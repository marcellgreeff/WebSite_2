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
        public string imageId;
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
            ddShare.Visible = false;
            ddImages.Visible = false;
            lblOutput.Text = "";
            btnUnshare.Visible = false;
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearchBy_Click(object sender, EventArgs e)
        {
            if (txtSearchBy.Text != "")
            {
                string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
               /* SqlCommand cmd = new SqlCommand("SELECT Image.Name FROM [Access] Inner JOIN [Image] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "' AND Image." + ddSearchBy1.SelectedValue + " = '" + txtSearchBy.Text + "'", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                
              //  string x = "SELECT Image.Name FROM [Access] INNER JOIN [Image] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "' AND Image." + ddSearchBy1.SelectedValue + " = '" + txtSearchBy.Text + "'";
               // Debug.WriteLine("\n\n\n\nA=" + x.ToString() + "=A\n\n\n");
                cmd.Connection.Open();
                imageName = cmd.ExecuteScalar().ToString();
                Session["imageName"] = imageName;*/

                using (SqlConnection con = new SqlConnection(sCon))
                {
                    using (SqlCommand cmd1 = new SqlCommand("SELECT Image.Name FROM [Access] Inner JOIN [Image] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "' AND Image." + ddSearchBy1.SelectedValue + " = '" + txtSearchBy.Text + "'"))
                    {
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = con;
                        con.Open();
                        ddImages.DataSource = cmd1.ExecuteReader();
                        ddImages.DataTextField = "Name";
                        ddImages.DataValueField = "Name";
                        ddImages.DataBind();
                        con.Close();
                        cmd1.Connection.Close();
                    }
                }
                ddImages.Visible = true;
                Label3.Visible = true;
                btnShare.Visible = true;
                ddShare.Visible = true;
                btnUnshare.Visible = true;
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            if (ddShare.SelectedIndex > -1)
            {
                SqlCommand cmdimage = new SqlCommand("SELECT Id FROM [Image] WHERE Name = '" + ddImages.SelectedValue + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdimage.Connection.Open();
                imageId = cmdimage.ExecuteScalar().ToString();
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT [Access] (UserId, ImageId) VALUES ('" + ddShare.SelectedItem + "', '" + imageId + "')";
                cmd.Connection = sqlCon;
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                lblOutput.Text = "Image has been shared!";
                txtSearchBy.Text = "";
            }
        }

        protected void btnUnshare_Click(object sender, EventArgs e)
        {
            if (ddShare.SelectedIndex > -1)
            {
                SqlCommand cmdimage = new SqlCommand("SELECT Id FROM [Image] WHERE Name = '" + ddImages.SelectedValue + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdimage.Connection.Open();
                imageId = cmdimage.ExecuteScalar().ToString();
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM [Access] WHERE UserId = '" + ddShare.SelectedItem + "' AND ImageId ='" + imageId + "'";
                cmd.Connection = sqlCon;
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                lblOutput.Text = "Image has been Unshared!";
                txtSearchBy.Text = "";
            }
        }
    }
}