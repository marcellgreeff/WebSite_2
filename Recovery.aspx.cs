using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebSite_2
{
    public partial class Recovery : System.Web.UI.Page
    {

        public SqlConnection sqlCon;
        public SqlDataAdapter da;
        public SqlCommand comm;
        public DataTable dt;
        public SqlDataReader dbReader;

        public string constr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Attributes.Add("autocomplete", "off");
            txtPassword.Attributes.Add("autocomplete", "off");
            txtAnswer.Attributes.Add("autocomplete", "off");
            txtConfirmPassword.Attributes.Add("autocomplete", "off");
            txtPassword.Visible = false;
            txtConfirmPassword.Visible = false;
            txtAnswer.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            btnChange.Visible = false;
            lblOutput.Text = "";
            
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Question FROM [User] WHERE UserId = '" + txtId.Text + "'", new SqlConnection(constr));
                cmd.Connection.Open();
                lblOutput.Text = "Security Question = " + cmd.ExecuteScalar().ToString();

                if (lblOutput.Text != "")
                {
                    txtPassword.Visible = true;
                    txtConfirmPassword.Visible = true;
                    txtAnswer.Visible = true;
                    Label3.Visible = true;
                    Label4.Visible = true;
                    Label5.Visible = true;
                    btnChange.Visible = true;
                }
                cmd.Connection.Close();
            }
            else
            {
                lblOutput.Text = "Insert a value at Id!";
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Answer FROM [User] WHERE UserId = '" + txtId.Text + "'", new SqlConnection(constr));
            cmd.Connection.Open();
            if(txtAnswer.Text == cmd.ExecuteScalar().ToString())
            {
                cmd.Connection.Close();
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                   using (SqlConnection conn = new SqlConnection(constr))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = new SqlCommand("UPDATE [User] SET Password = '" + txtPassword.Text + "' WHERE UserId = '" + txtId.Text + "'", conn))
                            {
                                cmd1.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
                                int rows = cmd1.ExecuteNonQuery();
                                lblOutput.Text = "Password has been changed successfully!";
                                txtId.Text = "";
                                txtAnswer.Text = "";
                                txtPassword.Text = "";
                                txtConfirmPassword.Text = "";
                                txtPassword.Visible = false;
                                txtConfirmPassword.Visible = false;
                                txtAnswer.Visible = false;
                                Label3.Visible = false;
                                Label4.Visible = false;
                                Label5.Visible = false;
                                btnChange.Visible = false;
                        }
                        conn.Close();
                        }
                }
            }
        }
    }
}