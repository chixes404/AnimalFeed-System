using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalFood
{
    public partial class Bills : Form
    {
        public Bills()
        {
            InitializeComponent();
            Load += Bills_Load; // Assign the event handler

            GetCustomers();
            DisplayProduct();
            DisplayTransaction();
        }
        private static string employeeName;

        public static string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }



        private void Bills_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                EmpNameLbl.Text = "Employee: ";
                EmpNameLbl.Text = $"{EmployeeName}";
            }
        }

        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        private void GetCustomers()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select Custid From Customers ", conn);
            SqlDataReader Rdr;
            Rdr= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Custid",typeof(int));
            dt.Load(Rdr);
            CustIDCB.ValueMember = "Custid";
            CustIDCB.DataSource = dt;
            conn.Close();
         }

        private void DisplayProduct()
        {
            conn.Open();
            string Query = "Select * From Product ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet(); 
            sda.Fill(ds);
            ProductViews.DataSource = ds.Tables[0];
            conn.Close();

        }
        private void DisplayTransaction()
        {
            conn.Open();
            string Query = "Select * From Bills ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            TransactionViews.DataSource = ds.Tables[0];
            conn.Close();

        }

        private void GetCustName()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SWproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE Custid = @Custid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Custid", CustIDCB.SelectedValue.ToString());
                DataTable dt = new DataTable();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    CustName.Text = dt.Rows[0]["CustName"].ToString();
                }
            }
        }


        int Stock =0 , Key =0;
        private void UpdateStock()
        {
            try
            {
                int NewQnt = Stock - Convert.ToInt32(ProQnt.Text);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update Product set ProQnt=@PQnt where Proid=@PKey ", conn);
                cmd.Parameters.AddWithValue("@PQnt", NewQnt);
                cmd.Parameters.AddWithValue("@PKey", Key);
                cmd.ExecuteNonQuery();
                //message,Box(Product Updated);
                conn.Close();
                DisplayProduct();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        int n = 0 , GrdTotal=0;
        private void Reset()
        {
            ProName.Text="";
            ProQnt.Text="";
            Stock=0;
            Key=0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ProductViews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProName.Text = ProductViews.SelectedRows[0].Cells[1].Value.ToString();
            //Proid.Text = ProductViews.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(ProductViews.SelectedRows[0].Cells[3].Value.ToString());
            ProPrice.Text = ProductViews.SelectedRows[0].Cells[4].Value.ToString();
            
            if(ProName.Text=="")
            {
                Key = 0;

            }
            else
            {
                Key=Convert.ToInt32(ProductViews.SelectedRows[0].Cells[0].Value.ToString());
            }
               
        }

        private void InsertBill()
        {

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Bills (Bill_Date,Cust_ID,Cust_Name,Emp_Name,Amount) values (@BDate,@Cid,@CName,@EName,@Amnt)", conn);
                cmd.Parameters.AddWithValue("@BDate", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@Cid", CustIDCB.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CName", CustName.Text);
                cmd.Parameters.AddWithValue("@EName", EmpNameLbl.Text);
                cmd.Parameters.AddWithValue("@Amnt", GrdTotal);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                conn.Close();
                DisplayTransaction();
                //Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }   
        }
        string prodname;
        

        private void button1_Click(object sender, EventArgs e) // Print Button
        {
           InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {

                printDocument1.Print();
            }
        }


        private void CustIDCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }

        int prodid, prodqty, proprice, tottal, pos = 60;

        private void label2_Click(object sender, EventArgs e)
        {
            Employee Obj2 = new Employee();
            Obj2.Show();
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home Obj1 = new Home();
            Obj1.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            login Obj6 = new login();
            Obj6.Show();
            this.Hide();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Product Obj4 = new Product();
            Obj4.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer Obj3 = new Customer();
            Obj3.Show();
            this.Hide();

        }

        private void ProductBillView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Animal Feed Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID Product Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            foreach (DataGridViewRow row in ProductBillView.Rows)
            {
                prodid = Convert.ToInt32(row.Cells[0].Value);
                prodname = "" + row.Cells[1].Value;
                proprice = Convert.ToInt32(row.Cells[2].Value);
                prodqty = Convert.ToInt32(row.Cells[3].Value);
                tottal = Convert.ToInt32(row.Cells[4].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + proprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand total : " + GrdTotal+"$", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("************ Chics for all Feed *************", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            ProductBillView.Rows.Clear();
            ProductBillView.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }


        private void RSlbl_Click(object sender, EventArgs e)
        {
               
        }

        private void button6_Click(object sender, EventArgs e) //ADD BiLLS
        {

            if (ProQnt.Text == "" || Convert.ToInt32(ProQnt.Text) > Stock)
            {
                MessageBox.Show("No Enough In Store");
            }
            else if (ProQnt.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                ProductBillView.ColumnCount = 5; // Set the number of columns to 5
                int total = Convert.ToInt32(ProQnt.Text) * Convert.ToInt32(ProPrice.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ProductBillView);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProName.Text;
                newRow.Cells[2].Value = ProQnt.Text;
                newRow.Cells[3].Value = ProPrice.Text;
                newRow.Cells[4].Value = total;
                GrdTotal = GrdTotal + total;
                ProductBillView.Rows.Add(newRow);
                n++;
                RSlbl.Text = $"Resukt{GrdTotal}";
                UpdateStock();
                Reset();
            }

        }
    }
}
