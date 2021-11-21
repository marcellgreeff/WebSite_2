using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite_2
{
    public partial class AlbumManagement : System.Web.UI.Page
    {
        public string constr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAlbumName.Attributes.Add("autocomplete", "off");
            txtNewAlbumName.Attributes.Add("autocomplete", "off");
            txtUserId.Attributes.Add("autocomplete", "off");


            string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
            // connection string  
            SqlConnection con = new SqlConnection(sCon);
            con.Open();

            SqlCommand com = new SqlCommand("SELECT AlbumName FROM [Albums]", con);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  
            ddAlbums.DataTextField = ds.Tables[0].Columns["AlbumName"].ToString(); // text field name of table dispalyed in dropdown       
            ddAlbums.DataValueField = ds.Tables[0].Columns["AlbumName"].ToString();
            // to retrive specific  textfield name   
            ddAlbums.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
            ddAlbums.DataBind();  //binding dropdownlist

        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void txtAlbumName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddAlbum_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT [Albums] (AlbumName) VALUES ('" + txtAlbumName.Text + "')";
                cmd.Connection = sqlCon;
                
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                SqlCommand cmdAlbum = new SqlCommand("SELECT Id FROM [Albums] WHERE AlbumName = '" + txtAlbumName.Text + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdAlbum.Connection.Open();
                string albumId = cmdAlbum.ExecuteScalar().ToString();
                cmdAlbum.Connection.Close();

                System.Data.SqlClient.SqlCommand cmdAccess = new System.Data.SqlClient.SqlCommand();
                cmdAccess.CommandType = System.Data.CommandType.Text;
                cmdAccess.CommandText = "INSERT [AlbumAccess] (AlbumId, UserId) VALUES ('" + albumId + "', '" + Session["Id"].ToString() + "')";
                cmdAccess.Connection = sqlCon;
                sqlCon.Open();
                cmdAccess.ExecuteNonQuery();
                sqlCon.Close();

                lblOutput.Text = "Album created!";
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmdAlbum = new SqlCommand("SELECT Id FROM [Albums] WHERE AlbumName = '" + ddAlbums.SelectedValue + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdAlbum.Connection.Open();
                string albumId = cmdAlbum.ExecuteScalar().ToString();
                cmdAlbum.Connection.Close();
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM [Albums] WHERE AlbumName ='" + ddAlbums.SelectedValue + "'";
                cmd.Connection = sqlCon;

                System.Data.SqlClient.SqlCommand cmdAccess = new System.Data.SqlClient.SqlCommand();
                cmdAccess.CommandType = System.Data.CommandType.Text;
                cmdAccess.CommandText = "DELETE FROM [AlbumImages] WHERE AlbumId ='" + albumId + "'";
                cmdAccess.Connection = sqlCon;

                sqlCon.Open();
                cmdAccess.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                sqlCon.Close();

                lblOutput.Text = "Album and corresponding images deleted!";
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewAlbumName.Text != "")
                {
                    System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Update [Albums] SET AlbumName ='" + txtNewAlbumName.Text + "'";
                    cmd.Connection = sqlCon;
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    lblOutput.Text = "Album updated!";
                }
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }

        protected void btnGiveAccess_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmdAlbum = new SqlCommand("SELECT Id FROM [Albums] WHERE AlbumName = '" + ddAlbums.SelectedValue + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                cmdAlbum.Connection.Open();
                string albumId = cmdAlbum.ExecuteScalar().ToString();
                cmdAlbum.Connection.Close();
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT [AlbumAccess] (AlbumId, UserId) VALUES ('" + albumId + "', '" + txtUserId.Text + "')";
                cmd.Connection = sqlCon;
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                lblOutput.Text = "User access created!";
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }
    }
}