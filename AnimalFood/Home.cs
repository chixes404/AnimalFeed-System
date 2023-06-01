using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalFood
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            Load += Home_Load; // Assign the event handler

        }
        private static string employeeName;

        public static string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }


        private void Home_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                label6.Text = "Employee: ";
                label6.Text = $"{EmployeeName}";
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
              
            

        }



        private void label2_Click(object sender, EventArgs e)
        {
            Employee Obj2 = new Employee();
            Obj2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer Obj2 = new Customer();
            Obj2.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Product Obj3 = new Product();
            Obj3.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Bills Obj5 = new Bills();
            Obj5.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
