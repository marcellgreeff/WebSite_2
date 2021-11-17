using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;

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

                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();

                if (datareader.HasRows)
                {
                    lblOutput.Text = "Security Question = " + datareader[0].ToString();
                    cmd.Connection.Close();

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
                    lblOutput.Text = "No data found!";
                }
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

                    string pwd = txtPassword.Text;
                    string salt = Register.GenerateSalt(70);
                    string pwdHashed = Register.HashPassword(pwd, salt, 10101, 70);
                    using (SqlConnection conn = new SqlConnection(constr))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = new SqlCommand("UPDATE [User] SET Password = '" + pwdHashed + "' WHERE UserId = '" + txtId.Text + "'", conn))
                            {
                                cmd1.Parameters.AddWithValue("@Password", pwdHashed);
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

        public static string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }
    }
}