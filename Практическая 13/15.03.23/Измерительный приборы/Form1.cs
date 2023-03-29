using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Измерительный_приборы
{
    public partial class Form1 : Form
    {
        private DataGridViewColumn dataGridViewColumn1 = null;
        private DataGridViewColumn dataGridViewColumn2 = null;
        private DataGridViewColumn dataGridViewColumn3 = null;
        private DataGridViewColumn dataGridViewColumn4 = null;
        private DataGridViewColumn dataGridViewColumn5 = null;
        private DataGridViewColumn dataGridViewColumn6 = null;

        private SortedList<uint, MeasuringDevice> devices = new SortedList<uint, MeasuringDevice>();
        public Form1()
        {
            InitializeComponent();
            initDataGridView();
        }

        //Инициализация таблицы
        private void initDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Add(getDataGridViewColumn1());
            dataGridView1.Columns.Add(getDataGridViewColumn2());
            dataGridView1.Columns.Add(getDataGridViewColumn3());
            dataGridView1.Columns.Add(getDataGridViewColumn4());
            dataGridView1.Columns.Add(getDataGridViewColumn5());
            dataGridView1.Columns.Add(getDataGridViewColumn6());
            dataGridView1.AutoResizeColumns();
        }
        private DataGridViewColumn getDataGridViewColumn1()
        {
            if (dataGridViewColumn1 == null)
            {
                dataGridViewColumn1 = new DataGridViewTextBoxColumn();
                dataGridViewColumn1.Name = "";
                dataGridViewColumn1.HeaderText = "Тип прибора";
                dataGridViewColumn1.ValueType = typeof(string);
                dataGridViewColumn1.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn1;
        }
        private DataGridViewColumn getDataGridViewColumn2()
        {
            if (dataGridViewColumn2 == null)
            {                     
                dataGridViewColumn2 = new DataGridViewTextBoxColumn();
                dataGridViewColumn2.Name = "";
                dataGridViewColumn2.HeaderText = "Фирма";
                dataGridViewColumn2.ValueType = typeof(string);
                dataGridViewColumn2.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn2;
        }
        private DataGridViewColumn getDataGridViewColumn3()
        {
            if (dataGridViewColumn3 == null)
            {                     
                dataGridViewColumn3 = new DataGridViewTextBoxColumn();
                dataGridViewColumn3.Name = "";
                dataGridViewColumn3.HeaderText = "Единица измерения";
                dataGridViewColumn3.ValueType = typeof(string);
                dataGridViewColumn3.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn3;
        }
        private DataGridViewColumn getDataGridViewColumn4()
        {
            if (dataGridViewColumn4 == null)
            {                     
                dataGridViewColumn4 = new DataGridViewTextBoxColumn();
                dataGridViewColumn4.Name = "";
                dataGridViewColumn4.HeaderText = "Точность";
                dataGridViewColumn4.ValueType = typeof(string);
                dataGridViewColumn4.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn4;
        }
        private DataGridViewColumn getDataGridViewColumn5()
        {
            if (dataGridViewColumn5 == null)
            {                     
                dataGridViewColumn5 = new DataGridViewTextBoxColumn();
                dataGridViewColumn5.Name = "";
                dataGridViewColumn5.HeaderText = "Максимальное значение";
                dataGridViewColumn5.ValueType = typeof(string);
                dataGridViewColumn5.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn5;
        }
        private DataGridViewColumn getDataGridViewColumn6()
        {
            if (dataGridViewColumn6 == null)
            {                     
                dataGridViewColumn6 = new DataGridViewTextBoxColumn();
                dataGridViewColumn6.Name = "";
                dataGridViewColumn6.HeaderText = "Минимальное значение";
                dataGridViewColumn6.ValueType = typeof(string);
                dataGridViewColumn6.Width = dataGridView1.Width / 6;
            }
            return dataGridViewColumn6;
        }

        //Обработчики событий
        private void addObject_Click(object sender, EventArgs e)
        {
            AddDevice(type.Text, producer.Text, accuracy.Text, maxMeasure.Text, minMeasure.Text, unit.Text);    
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (devices.Count > 0)
            {
                int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
                DialogResult dr = MessageBox.Show("Удалить прибор?", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDevice(selectedRow);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        private void change_Click(object sender, EventArgs e)
        {
            int sel = dataGridView1.SelectedCells[0].RowIndex;
            if (change.Text=="Изменить" && sel!=-1)
            {
                change.Text = "Подтвердить";
                type.Text = (string)dataGridView1[0,sel].Value;
                producer.Text = (string)dataGridView1[1, sel].Value;
                unit.Text = (string)dataGridView1[2, sel].Value;
                accuracy.Text = (string)dataGridView1[3, sel].Value;
                maxMeasure.Text = (string)dataGridView1[4, sel].Value;
                minMeasure.Text = (string)dataGridView1[5, sel].Value;
            }
            else if (type.Text != "" && producer.Text != "" && unit.Text != "" && accuracy.Text != "" && maxMeasure.Text != "" && minMeasure.Text != "")
            {
                change.Text = "Изменить";
                dataGridView1[0, sel].Value = type.Text;
                dataGridView1[1, sel].Value = producer.Text;
                dataGridView1[2, sel].Value = unit.Text;
                dataGridView1[3, sel].Value = accuracy.Text;
                dataGridView1[4, sel].Value = maxMeasure.Text;
                dataGridView1[5, sel].Value = minMeasure.Text;
            }
            else MessageBox.Show("Пустые поля недопустимы");
        }

        //Заполнение таблицы
        private void ShowListInGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (MeasuringDevice device in devices.Values)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cell1 = new
                DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new
                DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new
                DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new
                DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new
                DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell6 = new
                DataGridViewTextBoxCell();
                cell1.ValueType = typeof(string);
                cell1.Value = device.Type;
                cell2.ValueType = typeof(string);
                cell2.Value = device.Producer;
                cell3.ValueType = typeof(string);
                cell3.Value = device.Unit;
                cell4.ValueType = typeof(string);
                cell4.Value = device.Accuracy;
                cell5.ValueType = typeof(string);
                cell5.Value = device.MaxOfMeasure;
                cell6.ValueType = typeof(string);
                cell6.Value = device.MinOfMeasure;
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);
                dataGridView1.Rows.Add(row);
            }
        }

        //Функионал
        uint counter = 0;
        private void AddDevice(string type, string producer, string accuracy, string maxOfMeasure, string minOfMeasure, string unit)
        {
            counter++;
            if (this.type.Text != "" && this.producer.Text != "" && this.unit.Text != "" && this.accuracy.Text != "" && this.maxMeasure.Text != "" && this.minMeasure.Text != "")
            {
                MeasuringDevice s = new MeasuringDevice(type, producer, accuracy, maxOfMeasure, minOfMeasure, unit);
                devices.Add(counter, s);
                this.type.Text = "";
                this.producer.Text = "";
                this.unit.Text = "";
                this.accuracy.Text = "";
                this.maxMeasure.Text = "";
                this.minMeasure.Text = "";
                ShowListInGrid();
            }
            else
            {
                MessageBox.Show("Пустые поля недопустимы");
            }
        }
        private void DeleteDevice(int elementIndex)
        {
            devices.RemoveAt(elementIndex);
            ShowListInGrid();
        }

    }
}

