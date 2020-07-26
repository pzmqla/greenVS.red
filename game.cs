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
    public partial class game : Form

    {
        private readonly Random _random = new Random(); int turn = 1;
        public game()
        {
            InitializeComponent();
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string MakeRow(string makerow)
        {
            List<int> randomRow = new List<int>();
            string row = "";
            char ch = '"';

            for (int j = 0; j < 3; j++)
            {
                int randomNumber = RandomNumber(0, 2);
                randomRow.Add(randomNumber);
                row += ch.ToString() + randomRow[j].ToString() + ch.ToString() + ",";
            }

            for (int j = 0; j < row.Length - 1; j++)
            {
                makerow += row[j];
            }

            return makerow;
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label2.Text = (turn++).ToString();
            textBox1.Hide();
            textBox2.Hide();
            dataGridView1.Refresh();
            dataGridView1.ColumnCount = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {
                int rowId = i = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];
                for (int j = 0; j < Convert.ToInt32(textBox1.Text); j++)
                {
                    row.Cells[j].Value = RandomNumber(0, 2);
                    if (Convert.ToInt32(row.Cells[j].Value) == 0) { row.Cells[j].Style.BackColor = Color.FromArgb(127, 80, 255); }
                    else { row.Cells[j].Style.BackColor = Color.FromArgb(80, 255, 127); }
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;
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

        private void button2_Click(object sender, EventArgs e)
        {

            label2.Text = (turn++).ToString();
            var array = new object[dataGridView1.RowCount, dataGridView1.ColumnCount];
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    array[j.RowIndex, j.ColumnIndex] = j.Value;
                }
            }

            int r = array.GetLength(0) - 2;
            int c = array.GetLength(1) - 1;

            for (int row = 1; row < r; row++)
            {
                if (Convert.ToInt32(array[row, 0]) + Convert.ToInt32(array[row - 1, 0]) + Convert.ToInt32(array[row + 1, 0]) + Convert.ToInt32(array[row, 1]) == 0 ||
                    Convert.ToInt32(array[row, 0]) + Convert.ToInt32(array[row - 1, 0]) + Convert.ToInt32(array[row + 1, 0]) + Convert.ToInt32(array[row - 1, 1]) == 0 ||
                    Convert.ToInt32(array[row, 0]) + Convert.ToInt32(array[row - 1, 0]) + Convert.ToInt32(array[row + 1, 0]) + Convert.ToInt32(array[row + 1, 1]) == 0 ||
                    Convert.ToInt32(array[row - 1, 1]) + Convert.ToInt32(array[row, 0]) + Convert.ToInt32(array[row + 1, 1]) + Convert.ToInt32(array[row, 1]) == 0)
                {
                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.FromArgb(80, 255, 127);
                    dataGridView1.Rows[row].Cells[0].Value = 1;
                }
                if (Convert.ToInt32(array[row, c]) + Convert.ToInt32(array[row - 1, c]) + Convert.ToInt32(array[row + 1, c]) + Convert.ToInt32(array[row, c - 1]) == 0 ||
                    Convert.ToInt32(array[row, c]) + Convert.ToInt32(array[row - 1, c]) + Convert.ToInt32(array[row + 1, c]) + Convert.ToInt32(array[row - 1, c - 1]) == 0 ||
                    Convert.ToInt32(array[row, c]) + Convert.ToInt32(array[row - 1, c]) + Convert.ToInt32(array[row + 1, c]) + Convert.ToInt32(array[row + 1, c - 1]) == 0 ||
                    Convert.ToInt32(array[row - 1, c - 1]) + Convert.ToInt32(array[row, c]) + Convert.ToInt32(array[row + 1, c - 1]) + Convert.ToInt32(array[row, c - 1]) == 0)
                {
                    dataGridView1.Rows[row].Cells[c].Style.BackColor = Color.FromArgb(80, 255, 127);
                    dataGridView1.Rows[row].Cells[c].Value = 1;
                }
            }

            for (int column = 1; column < c; column++)
            {
                if (Convert.ToInt32(array[0, column]) + Convert.ToInt32(array[0, column - 1]) + Convert.ToInt32(array[0, column + 1]) + Convert.ToInt32(array[1, column]) == 0 ||
                    Convert.ToInt32(array[0, column]) + Convert.ToInt32(array[0, column - 1]) + Convert.ToInt32(array[0, column + 1]) + Convert.ToInt32(array[1, column - 1]) == 0 ||
                    Convert.ToInt32(array[0, column]) + Convert.ToInt32(array[0, column - 1]) + Convert.ToInt32(array[0, column + 1]) + Convert.ToInt32(array[1, column + 1]) == 0 ||
                    Convert.ToInt32(array[1, column - 1]) + Convert.ToInt32(array[0, column]) + Convert.ToInt32(array[1, column + 1]) + Convert.ToInt32(array[1, column]) == 0)
                {
                    dataGridView1.Rows[0].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                    dataGridView1.Rows[0].Cells[column].Value = 1;
                }
                if (Convert.ToInt32(array[r, column]) + Convert.ToInt32(array[r, column - 1]) + Convert.ToInt32(array[r, column + 1]) + Convert.ToInt32(array[r - 1, column]) == 0 ||
                    Convert.ToInt32(array[r, column]) + Convert.ToInt32(array[r, column - 1]) + Convert.ToInt32(array[r, column + 1]) + Convert.ToInt32(array[r - 1, column - 1]) == 0 ||
                    Convert.ToInt32(array[r, column]) + Convert.ToInt32(array[r, column - 1]) + Convert.ToInt32(array[r, column + 1]) + Convert.ToInt32(array[r - 1, column + 1]) == 0 ||
                    Convert.ToInt32(array[r - 1, column - 1]) + Convert.ToInt32(array[r, column]) + Convert.ToInt32(array[r - 1, column + 1]) + Convert.ToInt32(array[r - 1, column]) == 0)
                {
                    dataGridView1.Rows[r].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                    dataGridView1.Rows[r].Cells[column].Value = 1;
                }
            }

            if (Convert.ToInt32(array[0, 0]) + Convert.ToInt32(array[0, 1]) + Convert.ToInt32(array[1, 0]) + Convert.ToInt32(array[1, 1]) == 0)
            {
                dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(80, 255, 127);
                dataGridView1.Rows[0].Cells[0].Value = 1;
            }
            if (Convert.ToInt32(array[r, c]) + Convert.ToInt32(array[r - 1, c]) + Convert.ToInt32(array[r - 1, c - 1]) + Convert.ToInt32(array[r, c - 1]) == 0)
            {
                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(80, 255, 127);
                dataGridView1.Rows[r].Cells[c].Value = 1;
            }
            if (Convert.ToInt32(array[r, 0]) + Convert.ToInt32(array[r, 1]) + Convert.ToInt32(array[r - 1, 0]) + Convert.ToInt32(array[r - 1, 1]) == 0)
            {
                dataGridView1.Rows[r].Cells[0].Style.BackColor = Color.FromArgb(80, 255, 127);
                dataGridView1.Rows[r].Cells[0].Value = 1;
            }
            if (Convert.ToInt32(array[0, c]) + Convert.ToInt32(array[0, c - 1]) + Convert.ToInt32(array[1, c]) + Convert.ToInt32(array[1, c - 1]) == 0)
            {
                dataGridView1.Rows[0].Cells[c].Style.BackColor = Color.FromArgb(80, 255, 127);
                dataGridView1.Rows[0].Cells[c].Value = 1;
            }

            for (int row = 1; row < r; row++)
            {
                for (int column = 1; column < c; column++)
                {
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 3)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                        dataGridView1.Rows[row].Cells[column].Value = 1;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 6)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                        dataGridView1.Rows[row].Cells[column].Value = 1;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 0)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 1)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 2)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 4)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 5)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 7)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 0 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
    Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
    Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 8)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 0)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 1)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 4)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 5)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 7)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 8)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(127, 80, 255);
                        dataGridView1.Rows[row].Cells[column].Value = 0;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 6)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                        dataGridView1.Rows[row].Cells[column].Value = 1;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 2)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                        dataGridView1.Rows[row].Cells[column].Value = 1;
                    }
                    if (Convert.ToInt32(array[row, column]) == 1 && +Convert.ToInt32(array[row - 1, column - 1]) + Convert.ToInt32(array[row - 1, column]) +
   Convert.ToInt32(array[row - 1, column + 1]) + Convert.ToInt32(array[row, column - 1]) + Convert.ToInt32(array[row, column + 1]) +
   Convert.ToInt32(array[row + 1, column - 1]) + Convert.ToInt32(array[row + 1, column]) + Convert.ToInt32(array[row + 1, column + 1]) == 3)
                    {
                        dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(80, 255, 127);
                        dataGridView1.Rows[row].Cells[column].Value = 1;
                    }

                }

            }
        }
    }
}

