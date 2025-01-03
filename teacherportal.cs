﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Oracle.ManagedDataAccess.Client;

namespace DB_Project
{

    public partial class teacherportal : Form
    {
        OracleConnection con;
        public teacherportal()
        {
            InitializeComponent();
        }

        private void teacherportal_Load(object sender, EventArgs e)
        {

            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            con.Open();
            string input = userName.Text;

            if (int.TryParse(input, out int intValue))
            {
                OracleCommand command = new OracleCommand("SELECT password FROM teacherlogin WHERE teacher_id = :username", con);
                command.Parameters.Add(":username", OracleDbType.Varchar2).Value = userName.Text.ToString();
                string pass = command.ExecuteScalar()?.ToString();

                if (pass == Hash.Hash_SHA1(password.Text))
                {
                    this.Hide();
                    teacherworking tw = new teacherworking();
                    tw.tidd = int.Parse(userName.Text);
                    tw.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Incorrect password or username");
                }
            }
            else
            {
                MessageBox.Show("Invalid format for Teacher login");
            }
            con.Close();


        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstpage fp=new firstpage();
            fp.ShowDialog();

        }
    }
}
