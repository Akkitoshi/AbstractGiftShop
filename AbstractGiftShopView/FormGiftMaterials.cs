using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormGiftMaterials : Form
    {
        public GiftMaterialsViewModel Model
        {
            set { model = value; }
            get { return model; }
        }
        private GiftMaterialsViewModel model;
        public FormGiftMaterials()
        {
            InitializeComponent();
        }
        private void FormGiftMaterials_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> list = APIClient.GetRequest<List<MaterialsViewModel>>("api/Material/GetList");
                if (list != null)
                {
                    comboBoxGiftMaterials.DisplayMember = "MaterialsName";
                    comboBoxGiftMaterials.ValueMember = "Id";
                    comboBoxGiftMaterials.DataSource = list;
                    comboBoxGiftMaterials.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxGiftMaterials.Enabled = false;
                comboBoxGiftMaterials.SelectedValue = model.MaterialsId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxGiftMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new GiftMaterialsViewModel
                    {
                        MaterialsId = Convert.ToInt32(comboBoxGiftMaterials.SelectedValue),
                        MaterialsName = comboBoxGiftMaterials.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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