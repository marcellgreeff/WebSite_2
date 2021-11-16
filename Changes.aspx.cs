using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace WebSite_2
{
    public partial class Changes : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Attributes.Add("autocomplete", "off");
            txtLocation.Attributes.Add("autocomplete", "off");
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
                    System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT [Image] (Name, Location, UserUpload, Type, Date) VALUES ('" + FileUpload1.FileName + "', '" + txtLocation.Text + "', '" + Login.ID + "', '" + ddType.SelectedValue + "', '" + txtDate.Text + "')";
                    cmd.Connection = sqlCon;
                    
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
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
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/App_Data/ImageData/")))
            {
                FileInfo fi = new FileInfo(strFile);
                dt.Rows.Add(fi.Name, fi.Length, fi.Extension);
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