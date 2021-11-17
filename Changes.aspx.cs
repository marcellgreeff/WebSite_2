﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace WebSite_2
{
    public partial class Changes : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Attributes.Add("autocomplete", "off");
            txtLocation.Attributes.Add("autocomplete", "off");

            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));


            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/App_Data/ImageData/")))
            {
                FileInfo fi = new FileInfo(strFile);
                dt.Rows.Add(fi.Name);
            }

            gvDelete.DataSource = dt;
            gvDelete.DataBind();

            string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
            // connection string  
            SqlConnection con = new SqlConnection(sCon);
            con.Open();

            SqlCommand com = new SqlCommand("SELECT AlbumName FROM [Albums]", con);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  
            ddAlbum.DataTextField = ds.Tables[0].Columns["AlbumName"].ToString(); // text field name of table dispalyed in dropdown       
            ddAlbum.DataValueField = ds.Tables[0].Columns["AlbumName"].ToString();
            // to retrive specific  textfield name   
            ddAlbum.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
            ddAlbum.DataBind();  //binding dropdownlist
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtDate.Text != "" && txtLocation.Text != "" && FileUpload1.HasFile)
            {
                if (ddType.SelectedIndex > -1)
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/App_Data/ImageData/") + FileUpload1.FileName);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT [Image] (Name, Location, UserUpload, Type, Date) VALUES ('" + FileUpload1.FileName + "', '" + txtLocation.Text + "', '" + Session["Id"].ToString() + "', '" + ddType.SelectedValue + "', '" + txtDate.Text + "')";
                    cmd.Connection = sqlCon;
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdimage = new SqlCommand("SELECT Id FROM [Image] WHERE Name = '" + FileUpload1.FileName + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                    cmdimage.Connection.Open();
                    string imageId = cmdimage.ExecuteScalar().ToString();
                    cmdimage.Connection.Close();

                    SqlCommand cmdAlbum = new SqlCommand("SELECT Id FROM [Albums] WHERE AlbumName = '" + ddAlbum.SelectedValue + "';", new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True"));
                    cmdAlbum.Connection.Open();
                    string AlbumId = cmdAlbum.ExecuteScalar().ToString();
                    cmdimage.Connection.Close();

                    SqlCommand cmdAccess = new SqlCommand();
                    cmdAccess.CommandType = CommandType.Text;
                    cmdAccess.CommandText = "INSERT [Access] (UserId, ImageId) VALUES ('" + Session["Id"].ToString() + "', '" + imageId + "')";
                    cmdAccess.Connection = sqlCon;

                    SqlCommand cmdAlbumImage = new SqlCommand();
                    cmdAlbumImage.CommandType = CommandType.Text;
                    cmdAlbumImage.CommandText = "INSERT [AlbumImages] (AlbumId, ImageId) VALUES ('" + AlbumId + "', '" + imageId + "')";
                    cmdAlbumImage.Connection = sqlCon;

                    cmdAlbumImage.ExecuteNonQuery();
                    cmdAccess.ExecuteNonQuery();
                    sqlCon.Close();

                    lblMessage.Text = "Image Uploaded!";
                    txtDate.Text = "";
                    txtLocation.Text = "";
                    ddType.SelectedValue.FirstOrDefault();
                }
                else
                {
                    lblMessage.Text = "Please select a value in die drop down list";
                }
            }
            else
            {
                lblMessage.Text = "Please insert a value at all boxes!";
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));

            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/App_Data/ImageData/")))
            {
                FileInfo fi = new FileInfo(strFile);
                dt.Rows.Add(fi.Name);
            }

            gvDelete.DataSource = dt;
            gvDelete.DataBind();

        }

        protected void gvDelete_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/App_Data/ImageData/") + e.CommandArgument);
                Response.End();
            }
        }
    }
}