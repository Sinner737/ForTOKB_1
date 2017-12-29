using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ForTOKB_1_1;
using ForTOKB_1;
using ForTOKB_2;

namespace ForTOKB_3
{  
    public partial class Form3 : Form
    {
        public Form3()
        { 
            InitializeComponent();
            
        }
        
        public void button1_Click(object sender, EventArgs e)
        {   
           Form2 newForm2 = new Form2();
           newForm2.Show();
         
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
