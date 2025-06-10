using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Supplier : Form
    {
        public Supplier()
        {
            InitializeComponent();
            SupplierLoad(); // Load supplier data when the form initializes
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                int supplierId = int.Parse(textBox1.Text);
                string supplierName = textBox2.Text;
                string contact = textBox3.Text;

                string query = "INSERT INTO Suppliers (sid, name, contact) VALUES (@sid, @name, @contact)";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sid", supplierId);
                        cmd.Parameters.AddWithValue("@name", supplierName);
                        cmd.Parameters.AddWithValue("@contact", contact);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Inserted Successfully");
                SupplierLoad(); // Refresh the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
            }
        }

        private void SupplierLoad()
        {
            try
            {
                string query = "SELECT * FROM Suppliers";
                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable; // Assuming `dataGridView1` is your UI component
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
                // Input validation
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                int supplierId = int.Parse(textBox1.Text);
                string supplierName = textBox2.Text;
                string contact = textBox3.Text;

                string query = "UPDATE Suppliers SET name = @name, contact = @contact WHERE sid = @sid";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sid", supplierId);
                        cmd.Parameters.AddWithValue("@name", supplierName);
                        cmd.Parameters.AddWithValue("@contact", contact);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Updated Successfully");
                SupplierLoad(); // Refresh the DataGridView
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
                // Input validation
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Supplier ID is required to delete a record.");
                    return;
                }

                int supplierId = int.Parse(textBox1.Text);

                string query = "DELETE FROM Suppliers WHERE sid = @sid";

                using (MySqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sid", supplierId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Deleted Successfully");
                SupplierLoad(); // Refresh the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }

        }
    }
}
