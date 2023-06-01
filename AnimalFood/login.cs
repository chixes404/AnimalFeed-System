using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;



namespace AnimalFood
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.Silver;
            panel4.BackColor = Color.Silver;
            panel3.BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;

        }

    
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Silver;
            panel3.BackColor = Color.Silver;
            panel4.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;

        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;

        }

        private void button1_Click(object sender, EventArgs e)
{
    string enteredName = textBox1.Text;
    string enteredPassword = textBox2.Text;

    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT COUNT(*) FROM Employees WHERE EmpName = @Name AND EmpPass = @Password";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", enteredName);
                command.Parameters.AddWithValue("@Password", enteredPassword);

                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Access approved!");

                    // Hide the current login form
                    this.Hide();

                    // Set the employee name in each form
                    Employee.EmployeeName = EncodeHtml(enteredName);
                    Customer.EmployeeName = EncodeHtml(enteredName);
                    Home.EmployeeName = EncodeHtml(enteredName);
                    Product.EmployeeName = EncodeHtml(enteredName);
                    Bills.EmployeeName = EncodeHtml(enteredName);

                    // Create and show the home form
                    Home homeForm = new Home();
                    homeForm.Show();
                }
                else
                {
                    MessageBox.Show("Access denied!");
                }
            }
        }
    }
    catch (SqlException ex)
    {
        // Handle SQL exceptions (e.g., syntax errors, connection errors)
        MessageBox.Show("An error occurred while accessing the database: " + ex.Message);
    }
    catch (Exception ex)
    {
        // Handle other exceptions
        MessageBox.Show("An error occurred: " + ex.Message);
    }
}

private string EncodeHtml(string input)
{
    // Encode special characters to prevent XSS attacks
    return System.Net.WebUtility.HtmlEncode(input);
}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    }
    
