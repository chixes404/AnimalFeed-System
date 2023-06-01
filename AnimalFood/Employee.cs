using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalFood
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            DisplayEmployees();
            Load += Employee_Load; // Assign the event handler

        }

        public static string EmployeeName { get; set; }

        private void label1_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void DisplayEmployees()
        {
            conn.Open();
            string Query = "Select * From Employees ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);    
            DataSet ds = new DataSet(); 
            sda.Fill(ds);
            EmpViews.DataSource= ds.Tables[0];
            conn.Close();   

        }

        private void Clear()
        {
            EmpName.Text = "";
            EmpPass.Text = "";
            EmpPhone.Text = "";
            EmpAge.Text = "";

        }
        int Key = 0;
        private void button4_Click(object sender, EventArgs e) // Save Button
        {
            if (string.IsNullOrEmpty(EmpName.Text) || string.IsNullOrEmpty(EmpPass.Text) || string.IsNullOrEmpty(EmpPhone.Text) || string.IsNullOrEmpty(EmpAge.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else if (!IsValidName(EmpName.Text))
            {
                MessageBox.Show("Invalid name. Name should contain only letters and spaces.");
            }
            else if (!IsValidPassword(EmpPass.Text))
            {
                MessageBox.Show("Invalid password. Password should be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");
            }
            else if (!IsValidPhone(EmpPhone.Text))
            {
                MessageBox.Show("Invalid phone number. Phone number should be a valid 10-digit number.");
            }
            else if (!IsValidAge(EmpAge.Text))
            {
                MessageBox.Show("Invalid age. Age should be a valid positive number.");
            }
            else
            {
                try
                {
                    // Save the employee information to the database
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Employees (EmpName,EmpPass,EmpPhone,EmpAge) values (@EName,@EPass,@EPhone,@EAge)", conn);
                    cmd.Parameters.AddWithValue("@EName", EmpName.Text);
                    cmd.Parameters.AddWithValue("@EPass", EmpPass.Text);
                    cmd.Parameters.AddWithValue("@EPhone", EmpPhone.Text);
                    cmd.Parameters.AddWithValue("@EAge", EmpAge.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added");
                    conn.Close();
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

        private bool IsValidPassword(string password)
        {
            // Password should be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, and one digit
            return password.Length >= 6 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
        }

        private bool IsValidPhone(string phone)
        {
            // Phone number should be a valid 10-digit number
            return phone.Length == 10 && phone.All(char.IsDigit);
        }

        private bool IsValidAge(string age)
        {
            // Age should be a valid positive number
            return int.TryParse(age, out int ageValue) && ageValue > 0;
        }


        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EmpName_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmpView_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void EmpViews_CellContentClick(object sender, DataGridViewCellEventArgs e) //VIEW , WHEN CLICK 
        {
            EmpName.Text = EmpViews.SelectedRows[0].Cells[1].Value.ToString();
            EmpPass.Text = EmpViews.SelectedRows[0].Cells[4].Value.ToString();
            EmpPhone.Text = EmpViews.SelectedRows[0].Cells[2].Value.ToString();
            EmpAge.Text = EmpViews.SelectedRows[0].Cells[3].Value.ToString();

            if (EmpName.Text == "")
            {
                Key = 0;     
            }
           else
            {
                Key = Convert.ToInt32(EmpViews.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void EmpPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Edit Button
        {
            if (EmpName.Text == "" || EmpPass.Text == "" || EmpPhone.Text == "" || EmpAge.Text == "")

            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Employees set EmpName = @EName,EmpPhone=@EPhone,EmpAge=@EAge,EmpPass =@EPass Where EmpId=@Ekey", conn);
                    cmd.Parameters.AddWithValue("@EName", EmpName.Text);
                    cmd.Parameters.AddWithValue("@EKey", Key);
                    cmd.Parameters.AddWithValue("@EPhone", EmpPhone.Text);
                    cmd.Parameters.AddWithValue("@EAge", EmpAge.Text);
                    cmd.Parameters.AddWithValue("@EPass", EmpPass.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated");
                    conn.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void button3_Click(object sender, EventArgs e) //DELETE 
        {

            if (Key==0)

            {
                MessageBox.Show("Please Select An Employee");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete From Employees where EmpId=@EKey ", conn);
                    cmd.Parameters.AddWithValue("@EKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted");
                    conn.Close();
                    DisplayEmployees();
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
            Customer Obj =new Customer();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void Employee_Load(object sender, EventArgs e)
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

        private void label13_Click(object sender, EventArgs e)
        {
            login Obj6 = new login();
            Obj6.Show();
            this.Hide();

        }
    }
}
