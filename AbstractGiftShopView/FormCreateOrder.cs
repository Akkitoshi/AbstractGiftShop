using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace AbstractGiftShopView
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ISClientService serviceC;
        private readonly IGiftService serviceP;
        private readonly IMainService serviceM;
        public FormCreateOrder(ISClientService serviceC, IGiftService serviceP,
       IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<SClientViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxClient.DisplayMember = "SClientFIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }
                List<GiftViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBoxGift.DisplayMember = "GiftName";
                    comboBoxGift.ValueMember = "Id";
                    comboBoxGift.DataSource = listP;
                    comboBoxGift.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxGift.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxGift.SelectedValue);
                    GiftViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
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
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxGift.SelectedValue == null)
            {
                MessageBox.Show("Выберите подарок", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new SOrderBindingModel
                {
                    SClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    GiftId = Convert.ToInt32(comboBoxGift.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}