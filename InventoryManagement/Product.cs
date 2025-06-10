using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            this.Load += Product_Load;  // Hook form load event
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int productid = int.Parse(textBox1.Text);
                string productname = textBox2.Text;
                int price = int.Parse(textBox3.Text);
                string quantity = textBox4.Text;

                string query = "INSERT INTO Products (ProductId, ProductName, Price, Quantity) VALUES (@ProductId, @ProductName, @Price, @Quantity)";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productid);
                        cmd.Parameters.AddWithValue("@ProductName", productname);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Inserted Successfully");
                ProductLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
            }
        }

        private void Product_Load(object sender, EventArgs e)
        {
            ProductLoad();
        }

        private void ProductLoad()
        {
            try
            {
                string query = "SELECT * FROM Products";
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
            try
            {
                int productid = int.Parse(textBox1.Text);
                string productname = textBox2.Text;
                int price = int.Parse(textBox3.Text);
                string quantity = textBox4.Text;

                string query = "UPDATE Products SET ProductName=@ProductName, Price=@Price, Quantity=@Quantity WHERE ProductId=@ProductId";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productid);
                        cmd.Parameters.AddWithValue("@ProductName", productname);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Updated Successfully");
                ProductLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int productid = int.Parse(textBox1.Text);
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productid);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Deleted Successfully");
                ProductLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        private void Product_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'inventoryDbDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.inventoryDbDataSet.Products);

        }
    }
}
