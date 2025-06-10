using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void button1_Click(object sender, EventArgs e) // Insert
        {
            if (!ValidateInput()) return;

            try
            {
                int iid = int.Parse(textBox1.Text);
                string productid = textBox2.Text;
                int quantity = int.Parse(textBox3.Text);

                string query = "INSERT INTO Inventory (iid, productid, quantity) VALUES (@iid, @productid, @quantity)";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@iid", iid);
                        cmd.Parameters.AddWithValue("@productid", productid);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Inserted Successfully");
                ClearInputs();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Insert Error: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e) // Update
        {
            if (!ValidateInput()) return;

            try
            {
                int iid = int.Parse(textBox1.Text);
                string productid = textBox2.Text;
                int quantity = int.Parse(textBox3.Text);

                string query = "UPDATE Inventory SET productid = @productid, quantity = @quantity WHERE iid = @iid";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@iid", iid);
                        cmd.Parameters.AddWithValue("@productid", productid);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Updated Successfully");
                ClearInputs();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update Error: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e) // Delete
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("ID field is required to delete a record.");
                return;
            }

            try
            {
                int iid = int.Parse(textBox1.Text);
                string query = "DELETE FROM Inventory WHERE iid = @iid";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@iid", iid);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Record Deleted Successfully");
                ClearInputs();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete Error: {ex.Message}");
            }
        }

        private void LoadInventory()
        {
            try
            {
                string query = "SELECT * FROM Inventory";
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
                MessageBox.Show($"Load Error: {ex.Message}");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
