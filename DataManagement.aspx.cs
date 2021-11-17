using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;

namespace WebSite_2
{
    public partial class DataManagement : System.Web.UI.Page
    {
        string password = "";
        public SqlConnection sqlCon;
        public SqlDataAdapter da;
        public SqlCommand comm;
        public DataTable dt;
        public SqlDataReader dbReader;
        public string constr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
        static readonly string rootFolder = @"C:/Users/marce/source/repos/WebSite_2/App_Data/ImageData";
        string authorsFile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Visible = false;
            Label4.Visible = false;
            txtLocation.Visible = false;
            txtType.Visible = false;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            txtImageId.Attributes.Add("autocomplete", "off");
            txtLocation.Attributes.Add("autocomplete", "off");
            txtType.Attributes.Add("autocomplete", "off");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtImageId.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Image.Id FROM [Image] INNER JOIN [Access] on Access.ImageId = Image.Id WHERE Image.Id = '" + txtImageId.Text + "' AND Access.UserId = '" + Session["Id"] + "'", new SqlConnection(constr));
                cmd.Connection.Open();
                password = cmd.ExecuteScalar().ToString();
                if (password != "" && txtImageId.Text != "")
                {
                    lblOutput.Text = "Data found! Choose to update or delete data!";

                    Label3.Visible = true;
                    Label4.Visible = true;
                    txtLocation.Visible = true;
                    txtType.Visible = true;
                    btnDelete.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    lblOutput.Text = "No such Data found! Please retry!";
                    txtImageId.Text = "";
                }

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Image] WHERE Id = '" + txtImageId.Text + "'", new SqlConnection(constr));
            cmd.Connection.Open();
            if (txtImageId.Text == cmd.ExecuteScalar().ToString())
            {
                cmd.Connection.Close();
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = new SqlCommand("UPDATE [Image] SET Location = '" + txtLocation.Text + "', Type = '" + txtType.Text + "' WHERE Id = '" + txtImageId.Text + "'", conn))
                    {
                        cmd1.Parameters.AddWithValue("@Location", txtLocation.Text.ToString());
                        cmd1.Parameters.AddWithValue("@Type", txtType.Text.ToString());
                        int rows = cmd1.ExecuteNonQuery();
                        lblOutput.Text = "Data has been updated successfully!";
                        txtImageId.Text = "";
                        txtLocation.Text = "";
                        txtType.Text = "";
                        Label3.Visible = false;
                        Label4.Visible = false;
                        txtLocation.Visible = false;
                        txtType.Visible = false;
                        btnDelete.Visible = false;
                        btnUpdate.Visible = false;
                    }
                    conn.Close();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmdAccess = new SqlCommand("DELETE FROM [Access] WHERE ImageId = '" + txtImageId.Text + "'", new SqlConnection(constr));
            SqlCommand cmdName = new SqlCommand("SELECT Name FROM [Image] WHERE Id = '" + txtImageId.Text + "'", new SqlConnection(constr));
            SqlCommand cmd = new SqlCommand("DELETE FROM [Image] WHERE Id = '" + txtImageId.Text + "'", new SqlConnection(constr));
            cmdName.Connection.Open();
            cmdName.ExecuteNonQuery();
            authorsFile = cmdName.ExecuteScalar().ToString();
            cmdName.Connection.Close();
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmdAccess.Connection.Open();
            cmdAccess.ExecuteNonQuery();
            cmdAccess.Connection.Close();
            if (File.Exists(Path.Combine(rootFolder, authorsFile)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(rootFolder, authorsFile));
            }
            lblOutput.Text = "Data has been updated successfully!";
            txtImageId.Text = "";
            txtLocation.Text = "";
            txtType.Text = "";
            Label3.Visible = false;
            Label4.Visible = false;
            txtLocation.Visible = false;
            txtType.Visible = false;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
        }
    }
}