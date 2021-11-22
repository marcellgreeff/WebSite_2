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
            string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
            // connection string  
            SqlConnection con = new SqlConnection(sCon);
            con.Open();

            SqlCommand com = new SqlCommand("SELECT Image.Id FROM [Image] INNER JOIN [Access] on Access.ImageId = Image.Id WHERE Access.UserId = '" + Session["Id"] + "'", con);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  
            ddShare.DataTextField = ds.Tables[0].Columns["Id"].ToString(); // text field name of table dispalyed in dropdown       
            ddShare.DataValueField = ds.Tables[0].Columns["Id"].ToString();
            // to retrive specific  textfield name   
            ddShare.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist  
            ddShare.DataBind();  //binding dropdownlist

            Label3.Visible = false;
            Label4.Visible = false;
            txtLocation.Visible = false;
            txtType.Visible = false;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            txtLocation.Attributes.Add("autocomplete", "off");
            txtType.Attributes.Add("autocomplete", "off");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddShare.SelectedIndex > -1)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Image.Id FROM [Image] INNER JOIN [Access] on Access.ImageId = Image.Id WHERE Image.Id = '" + ddShare.SelectedValue + "' AND Access.UserId = '" + Session["Id"] + "'", new SqlConnection(constr));
                    cmd.Connection.Open();
                    password = cmd.ExecuteScalar().ToString();
                    if (password != "" && ddShare.SelectedIndex >-1)
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
                    }

                }
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Id FROM [Image] WHERE Id = '" + ddShare.SelectedValue + "'", new SqlConnection(constr));
                cmd.Connection.Open();
                if (ddShare.SelectedValue == cmd.ExecuteScalar().ToString())
                {
                    cmd.Connection.Close();
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = new SqlCommand("UPDATE [Image] SET Location = '" + txtLocation.Text + "', Type = '" + txtType.Text + "' WHERE Id = '" + ddShare.SelectedValue + "'", conn))
                        {
                            cmd1.Parameters.AddWithValue("@Location", txtLocation.Text.ToString());
                            cmd1.Parameters.AddWithValue("@Type", txtType.Text.ToString());
                            int rows = cmd1.ExecuteNonQuery();
                            lblOutput.Text = "Data has been updated successfully!";
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
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmdAccess = new SqlCommand("DELETE FROM [Access] WHERE ImageId = '" + ddShare.SelectedValue + "'", new SqlConnection(constr));
                SqlCommand cmdName = new SqlCommand("SELECT Name FROM [Image] WHERE Id = '" + ddShare.SelectedValue + "'", new SqlConnection(constr));
                SqlCommand cmd = new SqlCommand("DELETE FROM [Image] WHERE Id = '" + ddShare.SelectedValue + "'", new SqlConnection(constr));
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
                txtLocation.Text = "";
                txtType.Text = "";
                Label3.Visible = false;
                Label4.Visible = false;
                txtLocation.Visible = false;
                txtType.Visible = false;
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
            }
            catch (SqlException ex)
            {
                lblOutput.Text = ("Something Went Wrong. Please restart!");
            }
        }
    }
}