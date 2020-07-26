using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace greenVS.red
{
    public partial class create : Form
    {
        private readonly Random _random = new Random();
        public create()
        {
            InitializeComponent();
        }

        void otherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox2.Text) >= 5 && Convert.ToInt32(textBox1.Text) >= 5)
            {
                game otherForm = new game();
                otherForm.textBox1.Text = textBox1.Text;
                otherForm.textBox2.Text = textBox2.Text;
                otherForm.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
                this.Hide();
                otherForm.Show(); errorProvider2.Clear(); errorProvider1.Clear(); button1.Enabled = true;
            }
            else
            {
                errorProvider2.SetError(this.textBox2, "Add number equal or higher than 5!");
                errorProvider1.SetError(this.textBox1, "Add number equal or higher than 5!"); button1.Enabled = false;

            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void create_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.All(char.IsDigit)&& textBox1.Text != null) { errorProvider1.Clear(); button1.Enabled = true; }
            else { errorProvider1.SetError(this.textBox1, "Add number equal or higher than 5!"); button1.Enabled = false; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.All(char.IsDigit) && textBox2.Text != null) { errorProvider2.Clear(); button1.Enabled = true; }
            else { errorProvider2.SetError(this.textBox2, "Add number equal or higher than 5!"); button1.Enabled = false; }
        }
    }
}
