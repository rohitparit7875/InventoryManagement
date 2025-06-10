using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            Load += Order_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                int orderid = int.Parse(textBox1.Text);
                string productid = textBox2.Text;
                int customerid = int.Parse(textBox3.Text);
                int quantity = int.Parse(textBox4.Text);
                string orderdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                string query = "INSERT INTO orders (OrderId, ProductId, Quantity, OrderDate, CustomerId) VALUES (@OrderId, @ProductId, @Quantity, @OrderDate, @CustomerId)";
                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderid);
                        cmd.Parameters.AddWithValue("@ProductId", productid);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@OrderDate", orderdate);
                        cmd.Parameters.AddWithValue("@CustomerId", customerid);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Inserted Successfully");
                OrderLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
            }
        }

        private void Order_Load(object sender, EventArgs e)
        {
            OrderLoad();
        }

        private void OrderLoad()
        {
            try
            {
                string query = "SELECT * FROM orders";
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
    }
}
