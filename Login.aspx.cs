﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebSite_2
{
    public partial class Login : System.Web.UI.Page
    {
        static string sCon = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
        SqlConnection con = new SqlConnection(sCon);
        public static string ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Attributes.Add("autocomplete", "off");
            txtPassword.Attributes.Add("autocomplete", "off");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recovery.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            con.Open();
            string sql = @"SELECT UserId, Password FROM [User] WHERE UserId = @UserId";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@UserId", txtId.Text);
            SqlDataReader datareader = command.ExecuteReader();
            datareader.Read();
            if (txtId.Text != "" && txtPassword.Text != "")
            {
                if (datareader.HasRows)
                {
                    if (datareader["UserId"].ToString() == txtId.Text && datareader["Password"].ToString() == txtPassword.Text)
                    {
                        txtId.Text = "";
                        txtPassword.Text = "";
                        Response.Redirect("Homepage.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Username and Password does not match. Please retry!";
                        txtId.Text = "";
                        txtPassword.Text = "";
                        txtId.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Username does not exist. Please retry!";
                    txtId.Text = "";
                    txtPassword.Text = "";
                    txtId.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Please enter a username AND password!";
                txtId.Focus();
            }
            con.Close();
            datareader.Close();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}