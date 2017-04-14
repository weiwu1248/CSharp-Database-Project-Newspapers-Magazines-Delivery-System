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
    public partial class Form1 : Form
    {
        //use to connect to Sql database
        SqlConnection connection;
        string connectionString;

        public Form1()
        {
            InitializeComponent();

         //The connection string
            connectionString = ConfigurationManager.ConnectionStrings["Program.Properties.Settings.DatabaseConnectionString"].ConnectionString;

          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //enable full row select.
            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;
            listView3.FullRowSelect = true;
            listView4.FullRowSelect = true;
            
            //Sets the size of each column in listview.
            for (int i = 0; i < listView1.Columns.Count;i++ )
            {
                listView1.Columns[i].Width = 110;
            }
            for (int i = 0; i < listView2.Columns.Count; i++)
            {
                listView2.Columns[i].Width = 110;
            }
            for (int i = 0; i < listView3.Columns.Count; i++)
            {
                listView3.Columns[i].Width = 110;
            }
            for (int i = 0; i < listView4.Columns.Count; i++)
            {
                listView4.Columns[i].Width = 110;
            }
           
            //Call functions to pull data from database
            PopulateHouseholds();
            PopulatePublications();
            PopulateCarriers();
            PopulateSubscriptions();

        }
        private void PopulateHouseholds()
        {
            //clears the listview
            listView1.Items.Clear();
            //sets listview to view details
            listView1.View = View.Details;
            using (connection = new SqlConnection(connectionString))  //connects to sql database and opens it, will auto close.
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM HouseHolds", connection)) //the sql command
            {

                //fills a datatable with the data from the sql qury
                DataTable Table = new DataTable();
                adapter.Fill(Table);

                //loop to add the data to the listview
                for (int i = 0; i < Table.Rows.Count; i++ )
                {
                    DataRow dr = Table.Rows[i];
                    ListViewItem listItem = new ListViewItem(dr["Id"].ToString());
                    listItem.SubItems.Add(dr["FirstName"].ToString());
                    listItem.SubItems.Add(dr["LastName"].ToString());
                    listItem.SubItems.Add(dr["HouseNumber"].ToString());
                    listItem.SubItems.Add(dr["ApartmentNumber"].ToString());
                    listItem.SubItems.Add(dr["Street"].ToString());
                    listItem.SubItems.Add(dr["ZipCode"].ToString());
                    listItem.SubItems.Add(dr["PhoneNumber"].ToString());
                    listItem.SubItems.Add(dr["OutstandingBalance"].ToString());
                    listItem.SubItems.Add(dr["Status"].ToString());
                    listView1.Items.Add(listItem);
                }


            }
        }
        private void PopulatePublications() {
            //clears the listview
            listView2.Items.Clear();
            //sets listview to view details
            listView2.View = View.Details;
            using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Publications", connection))//the sql command
            {

                //fills a datatable with the data from the sql qury
                DataTable Table = new DataTable();
                adapter.Fill(Table);
                //loop to add the data to the listview
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    DataRow dr = Table.Rows[i];

                    ListViewItem listItem = new ListViewItem(dr["Id"].ToString());
                    listItem.SubItems.Add(dr["Name"].ToString());
                    listItem.SubItems.Add(dr["Street"].ToString());
                    listItem.SubItems.Add(dr["AddressNumber"].ToString());
                    listItem.SubItems.Add(dr["ZipCode"].ToString());
                    listItem.SubItems.Add(dr["PhoneNumber"].ToString());
                    listItem.SubItems.Add(dr["Cost"].ToString());
                    listItem.SubItems.Add(dr["AmountOwed"].ToString());


                    listView2.Items.Add(listItem);
                }


            }
        }
        private void PopulateCarriers() 
        {
            //clears the listview
            listView3.Items.Clear();
            //sets listview to view details
            listView3.View = View.Details;
            using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Carriers", connection))//the sql command
            {
                //fills a datatable with the data from the sql qury
                DataTable Table = new DataTable();
                adapter.Fill(Table);
                //loop to add the data to the listview
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    DataRow dr = Table.Rows[i];
                    ListViewItem listItem = new ListViewItem(dr["Id"].ToString());
                    listItem.SubItems.Add(dr["FirstName"].ToString());
                    listItem.SubItems.Add(dr["LastName"].ToString());
                    listItem.SubItems.Add(dr["Street"].ToString());
                    listItem.SubItems.Add(dr["HouseNumber"].ToString());
                    listItem.SubItems.Add(dr["ApartmentNumber"].ToString());
                    listItem.SubItems.Add(dr["ZipCode"].ToString());
                    listItem.SubItems.Add(dr["PhoneNumber"].ToString());
                    listItem.SubItems.Add(dr["Cost"].ToString());
                    listItem.SubItems.Add(dr["AmountOwed"].ToString());

                    listView3.Items.Add(listItem);
                }


            }
        }
        private void PopulateSubscriptions() 
        {
            //clears the listview
            listView4.Items.Clear();
            //sets listview to view details
            listView4.View = View.Details;

            using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Subscriptions", connection))//the sql command
            {
                //fills a datatable with the data from the sql qury
                DataTable Table = new DataTable();
                adapter.Fill(Table);
                //loop to add the data to the listview
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    DataRow dr = Table.Rows[i];
                    ListViewItem listItem = new ListViewItem(dr["Id"].ToString());
                    listItem.SubItems.Add(dr["HouseholdsID"].ToString());
                    listItem.SubItems.Add(dr["PublicationsID"].ToString());
                    listItem.SubItems.Add(dr["CarriersID"].ToString());


                    listView4.Items.Add(listItem);
                }


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            //warrning box
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //set status to active by default
                textBox10.Text = "Active";

                using (connection = new SqlConnection(connectionString))//connects to sql database and opens it, will auto close.
                //The sql command
                using (SqlDataAdapter adapter = new SqlDataAdapter("INSERT INTO Households(FirstName,LastName,Street,HouseNumber,ApartmentNumber,ZipCode,PhoneNumber,OutstandingBalance,Status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','"
                    + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox10.Text + "')", connection))
                {
                    connection.Open();//opens connection
                    adapter.SelectCommand.ExecuteNonQuery(); //exectes the query
                    connection.Close();
                }
                PopulateHouseholds();//refresh the listview
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //Fills the textboxes with the row of data selected
            string ID = listView1.SelectedItems[0].SubItems[0].Text;
            string firstName = listView1.SelectedItems[0].SubItems[1].Text;
            string lastName = listView1.SelectedItems[0].SubItems[2].Text;
            string houseNumber = listView1.SelectedItems[0].SubItems[3].Text;
            string apartmentNumber = listView1.SelectedItems[0].SubItems[4].Text;
            string street = listView1.SelectedItems[0].SubItems[5].Text;
            string zipCode = listView1.SelectedItems[0].SubItems[6].Text;
            string phoneNumber = listView1.SelectedItems[0].SubItems[7].Text;
            string outstandindBalance = listView1.SelectedItems[0].SubItems[8].Text;
            string status = listView1.SelectedItems[0].SubItems[9].Text;

           
            textBox1.Text = firstName;
            textBox2.Text = lastName;
            textBox3.Text = houseNumber;
            textBox4.Text = apartmentNumber;
            textBox5.Text = street;
            textBox6.Text = zipCode;
            textBox7.Text = phoneNumber;
            textBox8.Text = outstandindBalance;
            textBox9.Text = ID;
            textBox10.Text = status;

         
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //warrning box
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                using (connection = new SqlConnection(connectionString))//handles the connection
                //The sql query
                using (SqlDataAdapter adapter = new SqlDataAdapter("DELETE FROM Households WHERE Id = '" + textBox9.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery(); //exectues the query
                    connection.Close();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                PopulateHouseholds(); //Fills the textboxes with the row of data selected
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //warrning box
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))//handles the connection
                //The sql query
                using (SqlDataAdapter adapter = new SqlDataAdapter("UPDATE Households SET FirstName = '" + textBox1.Text + "'," + "LastName = '" + textBox2.Text + "'," + "Street = '" + textBox5.Text + "'," + "HouseNumber = '" + textBox3.Text + "'," + "ApartmentNumber = '" + textBox4.Text + "'," + "ZipCode = '" + textBox6.Text + "'," + "PhoneNumber = '" + textBox7.Text + "'," + "OutstandingBalance = '" + textBox8.Text + "'," + "Status = '" + textBox10.Text + "' WHERE Id = '" + textBox9.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulateHouseholds(); //Fills the textboxes with the row of data selected
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //changes the Status field to active or suspend.
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (textBox10.Text == "Active")
                {
                    textBox10.Text = "Suspended";
                }
                else
                {
                    textBox10.Text = "Active";
                }

                using (connection = new SqlConnection(connectionString))//handles the connection
                //the sql query
                using (SqlDataAdapter adapter = new SqlDataAdapter("UPDATE Households SET Status = '" + textBox10.Text + "' WHERE Id = '" + textBox9.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulateHouseholds();//Fills the textboxes with the row of data selected
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("INSERT INTO Publications (Name, Street, AddressNumber, ZipCode, PhoneNumber, Cost, AmountOwed) VALUES ('" + textBox20.Text + "','" + textBox19.Text + "','" + textBox18.Text + "','" + textBox17.Text + "','" + textBox16.Text + "','" + textBox15.Text + "','" + textBox14.Text + "')", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulatePublications();//Fills the textboxes with the row of data selected
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            //sets the textboxs with the row of data selected
            string ID = listView2.SelectedItems[0].SubItems[0].Text;
            string Name = listView2.SelectedItems[0].SubItems[1].Text;
            string Street = listView2.SelectedItems[0].SubItems[2].Text;
            string AddressNumber = listView2.SelectedItems[0].SubItems[3].Text;
            string ZipCode = listView2.SelectedItems[0].SubItems[4].Text;
            string PhoneNumber = listView2.SelectedItems[0].SubItems[5].Text;
            string Cost = listView2.SelectedItems[0].SubItems[6].Text;
            string AmountOwed = listView2.SelectedItems[0].SubItems[7].Text;



           
            textBox20.Text = Name;
            textBox19.Text = Street;
            textBox18.Text = AddressNumber;
            textBox17.Text = ZipCode;
            textBox16.Text = PhoneNumber;
            textBox15.Text = Cost;
            textBox14.Text = AmountOwed;
            textBox12.Text = ID;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("DELETE FROM Publications WHERE Id = '" + textBox12.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                //clears textboxs
                textBox20.Text = "";
                textBox19.Text = "";
                textBox18.Text = "";
                textBox17.Text = "";
                textBox16.Text = "";
                textBox15.Text = "";
                textBox14.Text = "";
                textBox12.Text = "";
                PopulatePublications();//Fills the textboxes with the row of data selected
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("UPDATE Publications SET Name = '" + textBox20.Text + "'," + "Street = '" + textBox19.Text + "'," + "AddressNumber = '" + textBox18.Text + "'," + "ZipCode = '" + textBox17.Text + "'," + "PhoneNumber = '" + textBox16.Text + "'," + "Cost = '" + textBox15.Text + "'," + "AmountOwed = '" + textBox14.Text + "' WHERE Id = '" + textBox12.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulatePublications();//Fills the textboxes with the row of data selected
            }
        }



        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            //sets the textboxs to the row of data selected
            string ID = listView3.SelectedItems[0].SubItems[0].Text;
            string FirstName = listView3.SelectedItems[0].SubItems[1].Text;
            string Lastname = listView3.SelectedItems[0].SubItems[2].Text;
            string Street = listView3.SelectedItems[0].SubItems[3].Text;
            string HouseNumber = listView3.SelectedItems[0].SubItems[4].Text;
            string ApartmentNumber = listView3.SelectedItems[0].SubItems[5].Text;
            string ZipCode = listView3.SelectedItems[0].SubItems[6].Text;
            string PhoneNumber = listView3.SelectedItems[0].SubItems[7].Text;
            string Cost = listView3.SelectedItems[0].SubItems[8].Text;
            string AmountOwed = listView3.SelectedItems[0].SubItems[9].Text;


            textBox28.Text = FirstName;
            textBox27.Text = Lastname;
            textBox26.Text = Street;
            textBox25.Text = HouseNumber;
            textBox24.Text = ApartmentNumber;
            textBox23.Text = ZipCode;
            textBox22.Text = PhoneNumber;
            textBox21.Text = Cost;
            textBox29.Text = AmountOwed;
            textBox13.Text = ID;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("INSERT INTO Carriers (FirstName, LastName, Street, HouseNumber, ApartmentNumber, ZipCode, PhoneNumber, Cost, AmountOwed) VALUES ('" + textBox28.Text + "','" + textBox27.Text + "','" + textBox26.Text + "','" + textBox25.Text + "','" + textBox24.Text + "','" + textBox23.Text + "','" + textBox22.Text + "','" + textBox21.Text + "','" + textBox29.Text + "')", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
               
                PopulateCarriers();//Fills the textboxes with the row of data selected
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("DELETE FROM Carriers WHERE Id = '" + textBox13.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                //clears textboxes
                textBox28.Text = "";
                textBox27.Text = "";
                textBox26.Text = "";
                textBox25.Text = "";
                textBox24.Text = "";
                textBox23.Text = "";
                textBox22.Text = "";
                textBox21.Text = "";
                textBox29.Text = "";
                textBox13.Text = "";
                PopulateCarriers();//Fills the textboxes with the row of data selected
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("UPDATE Carriers SET FirstName = '" + textBox28.Text + "'," + "LastName = '" + textBox27.Text + "'," + "Street = '" + textBox26.Text + "'," + "HouseNumber = '" + textBox25.Text + "'," + "ApartmentNumber = '" + textBox24.Text + "'," + "ZipCode = '" + textBox23.Text + "'," + "PhoneNumber = '" + textBox22.Text + "'," + "Cost = '" + textBox21.Text + "'," + "AmountOwed = '" + textBox29.Text + "' WHERE Id = '" + textBox13.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulateCarriers();//Fills the textboxes with the row of data selected
            }
        }



        private void listView4_MouseClick(object sender, MouseEventArgs e)
        {
            //sets the textboxs with the row of data selcected
            string ID = listView4.SelectedItems[0].SubItems[0].Text;
            string HouseHoldsID = listView4.SelectedItems[0].SubItems[1].Text;
            string PublicationsID = listView4.SelectedItems[0].SubItems[2].Text;
            string CarriersID = listView4.SelectedItems[0].SubItems[3].Text;



            textBox11.Text = ID;
            textBox36.Text = HouseHoldsID;
            textBox35.Text = PublicationsID;
            textBox34.Text = CarriersID;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("INSERT INTO Subscriptions (HouseholdsID, PublicationsID, CarriersID) VALUES ('" + textBox36.Text + "','" + textBox35.Text + "','" + textBox34.Text + "')", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulateSubscriptions();//Fills the textboxes with the row of data selected
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("DELETE FROM Subscriptions WHERE Id = '" + textBox11.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                //clears textboxs
                textBox11.Text = "";
                textBox36.Text = ""; ;
                textBox35.Text = ""; ;
                textBox34.Text = ""; ;
                PopulateSubscriptions();//Fills the textboxes with the row of data selected
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("UPDATE Subscriptions SET HouseholdsID = '" + textBox36.Text + "'," + "PublicationsID = '" + textBox35.Text + "'," + "CarriersID = '" + "' WHERE Id = '" + textBox11.Text + "'", connection))
                {
                    connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    connection.Close();
                }
                PopulateSubscriptions();//Fills the textboxes with the row of data selected
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox30.Text != "")
            {
                string FirstName = "", LastName = "", Street = "", HouseNumber = "",
                    ApartmentNumber = "", ZipCode = "", PhoneNumber = "", OutstandingBalance = "";
                string PublicationsName = "", Cost = "";

                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Households WHERE Id = " + textBox30.Text, connection))
                {



                    DataTable Table = new DataTable();
                    adapter.Fill(Table);

                    for (int i = 0; i < Table.Rows.Count; i++)
                    {
                        DataRow dr = Table.Rows[i];

                        FirstName = dr["FirstName"].ToString();
                        LastName = dr["LastName"].ToString();
                        Street = dr["Street"].ToString();
                        HouseNumber = dr["HouseNumber"].ToString();
                        ApartmentNumber = dr["ApartmentNumber"].ToString();
                        ZipCode = dr["ZipCode"].ToString();
                        PhoneNumber = dr["PhoneNumber"].ToString();
                        OutstandingBalance = dr["OutstandingBalance"].ToString();

                    }


                }
                textBox31.Text = "Name: " + FirstName + " " + LastName +
                     "\r\nStreet: " + Street +
                     "\r\nHouse Number: " + HouseNumber +
                     "\r\nAparment Number: " + ApartmentNumber +
                     "\r\nZip Code: " + ZipCode +
                     "\r\nPhone Number: " + PhoneNumber +
                     "\r\nOutstanding Balance: $" + OutstandingBalance +
                     "\r\n\r\n\rSubscriptions: ";

                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT Name, Cost FROM Publications WHERE Id IN (SELECT PublicationsID FROM Subscriptions WHERE HouseholdsID = " + textBox30.Text + ")", connection))
                {



                    DataTable Table = new DataTable();
                    adapter2.Fill(Table);

                    for (int i = 0; i < Table.Rows.Count; i++)
                    {
                        DataRow dr = Table.Rows[i];
                        PublicationsName = dr["Name"].ToString();
                        Cost = dr["Cost"].ToString();

                        textBox31.Text = textBox31.Text + "\r\n(" + (i + 1) + ") " + "Name: " + PublicationsName + "  Price: " + Cost;
                    }


                }


            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string ID = "", name = "", subs = "", totalOwed = "";

            textBox31.Text = "Publications Summary Sheet: \r\n";

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Name, AmountOwed FROM Publications", connection))
            {



                DataTable Table = new DataTable();
                adapter.Fill(Table);

                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    DataRow dr = Table.Rows[i];
                    ID = dr["Id"].ToString();
                    name = dr["Name"].ToString();
                    totalOwed = dr["AmountOwed"].ToString();
                     using (connection = new SqlConnection(connectionString))
                     using (SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT COUNT(*) FROM Subscriptions WHERE PublicationsID = " + ID, connection))
                     {
                         DataTable Table2 = new DataTable();
                         adapter2.Fill(Table2);
 
                      for (int j = 0; j < Table.Rows.Count; j++)
                      {
                      DataRow dr2 = Table.Rows[j];
                      subs = dr2[0].ToString();
                      }
                      textBox31.Text = textBox31.Text + "(" +(i + 1) + ")"+ " Name: " + name + "   Total Owed: $" + totalOwed + "   Number Of Subscriptions: " + subs + "\r\n";
                     }

                }


            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warrning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
          
        }

        

        

    }
}
