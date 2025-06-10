using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product pt = new Product();
            pt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer cr = new Customer();
            cr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Order or = new Order();
            or.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier(); 
            supplier.Show();    
        }

      
    }
}
