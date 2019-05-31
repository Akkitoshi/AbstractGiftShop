using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopView;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormStock : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<StockMaterialsViewModel> stockMaterialss;

        public FormStock()
        {
            InitializeComponent();
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SStockViewModel view = APIClient.GetRequest<SStockViewModel>("api/Stock/Get/" + id.Value);
                    if (view != null)
                    {
                        TextBoxName.Text = view.SStockName;
                        stockMaterialss = view.StockMaterialss;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                stockMaterialss = new List<StockMaterialsViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (stockMaterialss != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = stockMaterialss;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<SStockBindingModel, bool>("api/Stock/UpdElement", new SStockBindingModel
                    {
                        Id = id.Value,
                        SStockName = TextBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<SStockBindingModel, bool>("api/Stock/AddElement", new SStockBindingModel
                    {
                        SStockName = TextBoxName.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}