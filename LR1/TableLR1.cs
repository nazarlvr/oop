using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace excel
{
    public partial class TableLR1 : Form
    {
        bool formula_value; 
        string myownpath = ""; // строка для зберігання шляху збереження таблиці

        public TableLR1(string[] args)
        {
            UnitTest.UnitTests();
            InitializeComponent();
            table.Controls[0].Enabled = true;
            table.Controls[1].Enabled = true;
            SetupTableSize(15, 15); // Задаємо початковий розмір таблиці 15 на 15
            formula_value = false;
            if (args.Length == 1)
            {
                OpenTable(args[0]);
            }

            Manage_Cell.Rebuildtable(table); // оновлюємо таблицю та масив комірок після кожної внесеної зміни у таблицю
        }


        private void SetupTableSize(int colnum, int rownum) // задає початкову таблицю
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.SetProperty, null, table, new object[] { true });
            table.AllowUserToAddRows = false;
            table.ColumnCount = colnum;
            table.RowCount = rownum;
            UpdateTable();
        }

        private void Savetable(string path) // зберігання таблиці
        {
            myownpath = path;
            DataSet dataset = new DataSet();
            DataTable datatab = new DataTable("base");
            table.EndEdit();
            foreach (DataGridViewColumn col in table.Columns)
            {
                datatab.Columns.Add(col.Index.ToString());
            }
            foreach (DataGridViewRow row in table.Rows)
            {
                datatab.Rows.Add(row.Index.ToString());
                datatab.NewRow();
            }
            for (int i = 0; i < table.RowCount; ++i)
            {
                for (int j = 0; j < table.ColumnCount; ++j)
                {
                    if (Manage_Cell.GetCellbyindex(j, i).Formula == null)
                        Manage_Cell.GetCellbyindex(j, i).Formula = "";
                    datatab.Rows[i][j] = Manage_Cell.GetCellbyindex(j, i).Formula;
                }
            }
            dataset.Tables.Add(datatab);
            dataset.WriteXml(path);
        }
        private void OpenTable(string path) // завантаження раніше збереженої таблиці
        {
            myownpath = path;
            DataSet dataset = new DataSet();
            dataset.ReadXml(path);
            table.ColumnCount = dataset.Tables[0].Columns.Count;
            table.RowCount = dataset.Tables[0].Rows.Count;
            Manage_Cell.Rebuildtable(table);

            for (int i = 0; i < table.ColumnCount; ++i)
            {
                for (int j = 0; j < table.RowCount; ++j)
                {
                    Manage_Cell.GetCellbyindex(i, j).Formula = dataset.Tables[0].Rows[j][i].ToString(); 
                }
            }

            UpdateTable();
            UpdateCellsValue();
        }

        private void ChangeViewFormula_Value() // метод зміни значення у комірці(Формула або саме значення)
        {
            formula_value = !formula_value;
            UpdateCellsValue();
        }


        public void UpdateCellsValue() // Після внесення змін оновлює значення в комірках
        {
            for (int i = 0; i < table.ColumnCount; ++i)
                for (int j = 0; j < table.RowCount; ++j)
                    Manage_Cell.GetCellbyindex(i, j).Counted = false;

            for (int i = 0; i < table.ColumnCount; ++i)
            {
                for (int j = 0; j < table.RowCount; ++j)
                {
                    Cell cell = Manage_Cell.GetCellbyindex(i, j);

                    try
                    {
                        cell.process();

                        if (!formula_value)
                        {
                            if (cell.Formula == null || cell.Formula == "")
                                table[i, j].Value = "";
                            else if (cell.Value is Parser.ErrorCell)
                                table[i, j].Value = "error";
                            else
                                table[i, j].Value = cell.Value;
                        }
                        else
                        {
                            table[i, j].Value = cell.Formula;
                        }
                    }
                    catch (Exception e)
                    {
                        if (!formula_value)
                            table[i, j].Value = "error";
                        else
                            table[i, j].Value = cell.Formula;
                    }

                }
            }
        }

        private void UpdateTable()
        {
            foreach(DataGridViewColumn col in table.Columns)
            {
                col.HeaderText = "C" + (col.Index + 1);
                col.MinimumWidth = 80;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach(DataGridViewRow row in table.Rows)
            {
                row.HeaderCell.Value = "R" + (row.Index + 1);
            }
        }
        private void Row_Add() //додає рядок до таблиці
        {
            table.RowCount++;
            bool tmp = formula_value;
            formula_value = true;
            Manage_Cell.Rebuildtable(table);
            formula_value = tmp;
            UpdateTable();
        }
        private void Row_Delete() // видаляє рядок з таблиці
        {
            if (table.ColumnCount > 0 && table.RowCount > 0)
            {
                DialogResult res = MessageBox.Show("Ви дійсно хочете видалити цей рядок?", "Підтвердження видалення", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    DataGridViewCell tablecell = table.SelectedCells[0];
                    table.Rows.RemoveAt(tablecell.RowIndex);
                    bool tmp = formula_value;
                    formula_value = true;
                    Manage_Cell.Rebuildtable(table);
                    formula_value = tmp;
                    UpdateTable();
                    UpdateCellsValue();
                }
            }
        }

        public void Column_Add() // додає стовпчик до таблиці
        {
            table.ColumnCount++;
            bool tmp = formula_value;
            formula_value = true;
            Manage_Cell.Rebuildtable(table);
            formula_value = tmp;
            UpdateTable();
        }

        private void Column_Delete() //видаляє стовпчик з таблиці
        {
            if (table.ColumnCount > 0 && table.RowCount > 0)
            {
                DialogResult res = MessageBox.Show("Ви дійсно хочете видалити стовпчик?", "Підтвердження видалення", MessageBoxButtons.YesNo);
                
                if (res == DialogResult.Yes)
                {
                    DataGridViewCell tablecell = table.SelectedCells[0];
                    table.Columns.RemoveAt(tablecell.ColumnIndex);
                    bool tmp = formula_value;
                    formula_value = true;
                    Manage_Cell.Rebuildtable(table);
                    formula_value = tmp;
                    UpdateTable();
                    UpdateCellsValue();
                }
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {}

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {}

        private void infoToolStripMenuItem_Click(object sender, EventArgs e) // натискання на кнопку Інформація
        {
            MessageBox.Show("Данний проект розроблений студентом Лаврентюком Назаром. Підтримує операції:'+','-','*','/','^', " +
                "унарні операції, max(n values), min(n values), not, логічні операції тощо." +
                "Значення комірок має тип динамік, тому може зберігати як логічні так і числові значення, проте намагання зробити арифметичні дії з логічними аргументами приведе до помилки" +
                "", "Інформація про проект");
        }

        private void TableLR1_FormClosing(object sender, FormClosingEventArgs e) // натискання на хрестик
        {
            DialogResult result = MessageBox.Show("Ви хочете зберегти таблицю?", "Збереження Таблиці", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (myownpath != "")
                {
                    Savetable(myownpath);
                }
                else
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Savetable(saveFileDialog1.FileName);
                    };
                }
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {}
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Row_Add();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Row_Delete();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Column_Add();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Column_Delete();
        }

        private void toolStripButton0_Click(object sender, EventArgs e)
        {
            ChangeViewFormula_Value();
        }

        public void tableCellendendit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            Cell cell = Manage_Cell.GetCellbyindex(e.ColumnIndex, e.RowIndex);
            DataGridViewCell tablecell = cell.Example;
            string firstexpression = cell.Formula;
            if (tablecell.Value != null)
            {
                cell.Formula = tablecell.Value.ToString();
                try 
                {
                    UpdateCellsValue();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.GetType().ToString());
                    cell.Formula = firstexpression;
                    UpdateCellsValue();
                }
            }
            else
            {
                cell.Formula = "";
            }
        }


        private void tablecelldoubleclick(object sender, DataGridViewCellEventArgs e)
        {
            table.BeginEdit(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenTable(openFileDialog1.FileName);
            };
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (myownpath != "")
            {
                Savetable(myownpath);
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Savetable(saveFileDialog1.FileName); 
                };
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.Width > SystemInformation.VirtualScreen.Width)
            {
                this.Width = SystemInformation.VirtualScreen.Width;
                this.Left = 0;
            }
            if (this.Height > (SystemInformation.VirtualScreen.Height * 9) / 10)
            {
                this.Height = (SystemInformation.VirtualScreen.Height * 9) / 10;
                this.Top = 0;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви хочете зберегти зміни?", "Збреження Таблиці", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (myownpath != "")
                {
                    Savetable(myownpath);
                }
                else
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Savetable(saveFileDialog1.FileName);
                    };
                }
            };
        }

        private void tableCellbeginedit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cell cell = Manage_Cell.GetCellbyindex(e.ColumnIndex, e.RowIndex);
            DataGridViewCell tablecell = cell.Example;
            tablecell.Value = cell.Formula;
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("{ '~', '|', '&', '=', '!', '>', '<', '}', '{', '%', '\\', '*', '/', '^', ',' '+', '-', 'R', 'mmax', 'mmin', 'C', цифри 0-9}", "Дозволені символи");
        }
    }
}
