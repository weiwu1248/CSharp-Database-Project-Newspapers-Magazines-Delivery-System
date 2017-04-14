using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Program
{
    public partial class Signup : Form
    {

        //use to connect to Sql database
        SqlConnection connection;
        string connectionString;

        public Signup()
        {
            InitializeComponent();
            //The connection string
            connectionString = ConfigurationManager.ConnectionStrings["Program.Properties.Settings.DatabaseConnectionString"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a username");
            }
            else if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a password");
            }

            else if (!usernameExist(textBox1.Text) && passwordsMatch())
            {
                using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
                //The sql command
                using (SqlDataAdapter adapter = new SqlDataAdapter("INSERT INTO Logins(Username,Password) VALUES('" + textBox1.Text + "','" + textBox2.Text + "')", connection))
                {
                    connection.Open();//opens connection
                    adapter.SelectCommand.ExecuteNonQuery(); //exectes the query
                    connection.Close();
                }
                MessageBox.Show("Successfully signed up");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (usernameExist(textBox1.Text))
            {
                label4.Visible = true;
            }
            else {
                label4.Visible = false;
            }
        }
        private bool usernameExist(string username)
        {

            using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(*) FROM Logins WHERE Username = '" + username + "'", connection))//the sql command
            {
                DataTable Table = new DataTable();
                adapter.Fill(Table);
                if (Table.Rows[0][0].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool passwordsMatch() {
            if (textBox2.Text == textBox3.Text)
            {
                return true;
            }
            else {
                return false;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox3.ReadOnly = true;
                textBox3.Clear();
            }
            else
            {
                textBox3.ReadOnly = false; 
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (passwordsMatch())
            {
                label5.Visible = false;
            }
            else {
                label5.Visible = true;
            }
        }


    }
}
