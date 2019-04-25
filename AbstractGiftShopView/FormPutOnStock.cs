using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormPutOnStock : Form
    {
        public FormPutOnStock()
        {
            InitializeComponent();
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите материалы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<StockMaterialsBindingModel, bool>("api/Main/PutMaterialOnStock", new StockMaterialsBindingModel
                {
                    MaterialsId = Convert.ToInt32(comboBoxMaterials.SelectedValue),
                    SStockId = Convert.ToInt32(comboBoxStocks.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
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