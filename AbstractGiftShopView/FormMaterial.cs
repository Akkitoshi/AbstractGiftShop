using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormMaterial : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormMaterial()
        {
            InitializeComponent();
        }
        private void FormMaterial_Load(object sender, EventArgs e)
        {

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<MaterialsBindingModel, bool>("api/Material/UpdElement", new MaterialsBindingModel
                    {
                        Id = id.Value,
                        MaterialsName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<MaterialsBindingModel, bool>("api/Material/AddElement", new MaterialsBindingModel
                    {
                        MaterialsName = textBoxName.Text
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