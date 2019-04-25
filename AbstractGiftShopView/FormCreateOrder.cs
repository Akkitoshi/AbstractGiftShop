using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormCreateOrder : Form
    {
        public FormCreateOrder()
        {
            InitializeComponent();
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {

        }
        private void CalcSum()
        {
            if (comboBoxGift.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxGift.SelectedValue);
                    GiftViewModel item = APIClient.GetRequest<GiftViewModel>("api/Gift/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * item.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_CountChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxGift_SumChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxGift.SelectedValue == null)
            {
                MessageBox.Show("Выберите подарок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<SOrderBindingModel, bool>("api/Main/CreateOrder", new SOrderBindingModel
                {
                    SClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    GiftId = Convert.ToInt32(comboBoxGift.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
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