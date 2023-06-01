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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            DisplayProducts();
           Load += Product_Load; // Assign the event handler
        }
    private static string employeeName;

    public static string EmployeeName
    {
        get { return employeeName; }
        set { employeeName = value; }
    }

        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void DisplayProducts()
        {
            conn.Open();
            string Query = "Select * From Product ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ProViews.DataSource = ds.Tables[0];
            conn.Close();

        }

        private void Clear()
        {
            ProName.Text = "";
            CateCB.SelectedIndex =0;
            ProQnt.Text = "";
            ProPrice.Text = "";

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Product_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                label6.Text = "Employee: ";
                label6.Text = $"{EmployeeName}";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home Obj3 = new Home();
            Obj3.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee Obj2 = new Employee();
            Obj2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer Obj1 = new Customer();
            Obj1.Show();
            this.Hide();
        
    }

        private void button3_Click(object sender, EventArgs e)  //SAVE BUTTON
        {
            if (ProName.Text == "" || CateCB.SelectedIndex == -1 || ProQnt.Text == "" || ProPrice.Text == "")

            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Product (ProName,ProCatog,ProQnt,ProPrice) values (@PName,@PCate,@PQnt,@Pprice)", conn);
                    cmd.Parameters.AddWithValue("@PName", ProName.Text);
                    cmd.Parameters.AddWithValue("@PCate", CateCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQnt", ProQnt.Text);
                    cmd.Parameters.AddWithValue("@Pprice", ProPrice.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        int Key = 0;
        private void ProViews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            ProName.Text = ProViews.SelectedRows[0].Cells[1].Value.ToString();
            CateCB.Text = ProViews.SelectedRows[0].Cells[2].Value.ToString();
            ProQnt.Text = ProViews.SelectedRows[0].Cells[3].Value.ToString();
            ProPrice.Text = ProViews.SelectedRows[0].Cells[4].Value.ToString();

            if (ProName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProViews.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)

            {
                MessageBox.Show("Please Select A Product");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete From Product where ProID=@EKey ", conn);
                    cmd.Parameters.AddWithValue("@EKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)  //EDIT PRODUCt
        {
            if (ProName.Text == "" || CateCB.SelectedIndex == -1 || ProQnt.Text == "" || ProPrice.Text == "")

            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Product set ProName = @PName,ProCatog=@PCatog,ProQnt=@PQnt,ProPrice =@Pprice Where ProID=@Pkey", conn);
                    cmd.Parameters.AddWithValue("@PName", ProName.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.Parameters.AddWithValue("@PCatog", CateCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQnt", ProQnt.Text);
                    cmd.Parameters.AddWithValue("@Pprice", ProPrice.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Bills  Obj5 = new Bills();
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
