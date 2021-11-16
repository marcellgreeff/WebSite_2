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
                SqlCommand cmd = new SqlCommand("SELECT Id FROM [Image] WHERE Id = '" + txtImageId.Text + "'", new SqlConnection(constr));
                cmd.Connection.Open();
                password = cmd.ExecuteScalar().ToString();
                if (password != "" && txtImageId.Text !="")
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

        }
    }
}