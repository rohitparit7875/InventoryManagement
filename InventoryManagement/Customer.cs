using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            Load += OnLoad;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                int customerid = int.Parse(textBox1.Text);
                string customername = textBox2.Text;
                string contact = textBox3.Text;

                string query = "INSERT INTO customers (CustomerId, CustomerName, Contact) VALUES (@CustomerId, @CustomerName, @Contact)";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerid);
                        cmd.Parameters.AddWithValue("@CustomerName", customername);
                        cmd.Parameters.AddWithValue("@Contact", contact);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Inserted Successfully");
                customerLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            customerLoad();
        }

        private void customerLoad()
        {
            try
            {
                string query = "SELECT * FROM customers";
                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int customerid = int.Parse(textBox1.Text);
            string customername = textBox2.Text;
            string contact = textBox3.Text;

            string query = "UPDATE customers SET CustomerName=@CustomerName,Contact=@Contact WHERE CustomerId=@CustomerId";

            using (MySqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerid);
                    cmd.Parameters.AddWithValue("@CustomerName", customername);
                    cmd.Parameters.AddWithValue("@Contact", contact);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Record Updated Successfully");
            customerLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int customerid = int.Parse(textBox1.Text);
          
            string query = "DELETE FROM Customers WHERE CustomerId=@CustomerId";

            using (MySqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerid);
                    

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Record Deleted Successfully");
            customerLoad();
        }
    }
}
