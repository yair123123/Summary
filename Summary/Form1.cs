using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Data;

namespace Summary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBcontext dbContext = new DBcontext(DBcontext.GetConnString());
            DataTable dt = dbContext.MakeQuery("select * from users");            
            dataGridView1.DataSource = dt;
            
        }
    }
}
