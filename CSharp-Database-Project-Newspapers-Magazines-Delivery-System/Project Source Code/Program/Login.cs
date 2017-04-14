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
    public partial class Login : Form
    {
        //use to connect to Sql database
        SqlConnection connection;
        string connectionString;

        public Login()
        {
            InitializeComponent();

            //The connection string
            connectionString = ConfigurationManager.ConnectionStrings["Program.Properties.Settings.DatabaseConnectionString"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
             using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(*) FROM Logins WHERE Username = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'" , connection))//the sql command
             {
                 DataTable Table = new DataTable();
                 adapter.Fill(Table);
                 if (Table.Rows[0][0].ToString() == "1")
                 {
                     this.Hide();
                     Form1 mainWindow = new Form1();
                     mainWindow.Show();
                    
                 }
                 else {
                     MessageBox.Show("Incorrect username or password");
                 }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signup signupWindow = new Signup();
            signupWindow.Show();
        }


    }
}
