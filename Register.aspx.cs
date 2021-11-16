﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace WebSite_2
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Attributes.Add("autocomplete", "off");
            txtAnswer.Attributes.Add("autocomplete", "off");
            txtPassword.Attributes.Add("autocomplete", "off");
        }

        protected void Button1_Click(object sender, EventArgs e)
        { 
            if (txtId.Text != "" && txtPassword.Text != "" && txtAnswer.Text != "")
            {
                    if (ddlQuestion.SelectedIndex > -1)                                        
                        {
                     System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");
                     System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                     cmd.CommandType = System.Data.CommandType.Text;
                    string pwd = txtPassword.Text;
                    string salt = Register.GenerateSalt(70);
                    string pwdHashed = Register.HashPassword(pwd, salt, 10101, 70);
                    string password = Hascode(txtPassword.Text);

                    cmd.CommandText = "INSERT [User] (UserId, Password, Question, Answer) VALUES ('" + txtId.Text + "', '" + password + "', '" + ddlQuestion.SelectedItem + "', '" + txtAnswer.Text + "')";
                    cmd.Connection = sqlCon;

                     sqlCon.Open();
                     cmd.ExecuteNonQuery();
                     sqlCon.Close();

                    lblRegister.Text = "User Registered!";
                            txtAnswer.Text = "";
                            txtId.Text = "";
                            txtPassword.Text = "";
                            ddlQuestion.SelectedValue.FirstOrDefault();
                        }
                    else
                    {
                        lblRegister.Text = "Please select a value in die drop down list";
                    }
                }
                else
                {
                    lblRegister.Text = "Please insert a value at all boxes!";
                }
            }
    

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

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
        public static string Hascode(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

    }
}