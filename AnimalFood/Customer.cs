using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalFood
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            DisplayCustomers();
            Load += Customer_Load; // Assign the event handler

        }
        private static string employeeName;

        public static string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }



        private void CustName_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void DisplayCustomers()
        {
            conn.Open();
            string Query = "Select * From Customers ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            CustViews.DataSource = ds.Tables[0];
            conn.Close();

        }

        private void Clear()
        {
            CustName.Text = "";
            CustPhone.Text = "";
            CustEmail.Text = "";
            CustAdd.Text = "";

        }
        int Key = 0;

        private void button2_Click(object sender, EventArgs e) // Save button
        {
            if (CustName.Text == "" || CustPhone.Text == "" || CustEmail.Text == "" || CustAdd.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else if (!IsValidName(CustName.Text))
            {
                MessageBox.Show("Invalid name. Name should contain only letters and spaces.");
            }
            else if (!IsValidPhone(CustPhone.Text))
            {
                MessageBox.Show("Invalid phone number. Phone number should be a valid 10-digit number.");
            }
            else if (!IsValidEmail(CustEmail.Text))
            {
                MessageBox.Show("Invalid email address.");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customers (CustName,CustPhone,CustEmail,CustAddr) values (@CName,@CPhone,@Cemail,@CAdd)", conn);
                    cmd.Parameters.AddWithValue("@CName", CustName.Text);
                    cmd.Parameters.AddWithValue("@CPhone", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@Cemail", CustEmail.Text);
                    cmd.Parameters.AddWithValue("@CAdd", CustAdd.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    conn.Close();
                    DisplayCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // Edit button
        {
            if (CustName.Text == "" || CustPhone.Text == "" || CustEmail.Text == "" || CustAdd.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else if (!IsValidName(CustName.Text))
            {
                MessageBox.Show("Invalid name. Name should contain only letters and spaces.");
            }
            else if (!IsValidPhone(CustPhone.Text))
            {
                MessageBox.Show("Invalid phone number. Phone number should be a valid 10-digit number.");
            }
            else if (!IsValidEmail(CustEmail.Text))
            {
                MessageBox.Show("Invalid email address.");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Customers set CustName = @CName,CustPhone=@CPhone,CustEmail=@Cemail,CustAddr =@CAdd Where Custid=@Ckey", conn);
                    cmd.Parameters.AddWithValue("@CName", CustName.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.Parameters.AddWithValue("@CPhone", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@Cemail", CustEmail.Text);
                    cmd.Parameters.AddWithValue("@CAdd", CustAdd.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    conn.Close();
                    DisplayCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool IsValidName(string name)
        {
            // Name should contain only letters and spaces
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private bool IsValidPhone(string phone)
        {
            // Phone number should be a valid 10-digit number
            return phone.Length == 10 && phone.All(char.IsDigit);
        }

        private bool IsValidEmail(string email)
        {
            // Email address validation using a simple regular expression
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }




        private void CustViews_CellContentClick(object sender, DataGridViewCellEventArgs e)  // view , when click
        {
            CustName.Text = CustViews.SelectedRows[0].Cells[1].Value.ToString();
            CustPhone.Text = CustViews.SelectedRows[0].Cells[2].Value.ToString();
            CustEmail.Text = CustViews.SelectedRows[0].Cells[4].Value.ToString();
            CustAdd.Text = CustViews.SelectedRows[0].Cells[3].Value.ToString();

            if (CustName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustViews.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)

            {
                MessageBox.Show("Please Select An Customer");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete From Customers where Custid=@CKey ", conn);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    conn.Close();
                    DisplayCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee Obj = new Employee();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                label6.Text = "Employee: ";
                label6.Text = $"{EmployeeName}";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Product Obj4 = new Product();
            Obj4.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Bills Obj5 = new Bills();
            Obj5.Show();
            this.Hide();

        }

        private void label11_Click(object sender, EventArgs e)
        {
            login Obj6 = new login();
            Obj6.Show();
            this.Hide();

        }
    }
}
