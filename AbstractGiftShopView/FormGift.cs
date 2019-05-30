using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace AbstractGiftShopView
{
    public partial class FormGift : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IGiftService service;
        private int? id;
        private List<GiftMaterialsViewModel> giftMaterials;
        public FormGift(IGiftService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormGift_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    GiftViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.GiftName;
                        textBoxPrice.Text = view.Price.ToString();
                        giftMaterials = view.GiftMaterials;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                giftMaterials = new List<GiftMaterialsViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (giftMaterials != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = giftMaterials;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormGiftMaterials>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.GiftId = id.Value;
                    }
                    giftMaterials.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormGiftMaterials>();
                form.Model =
               giftMaterials[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    giftMaterials[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        giftMaterials.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (giftMaterials == null || giftMaterials.Count == 0)
            {
                MessageBox.Show("Заполните материалы", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<GiftMaterialsBindingModel> giftMateialsBM = new
               List<GiftMaterialsBindingModel>();
                for (int i = 0; i < giftMaterials.Count; ++i)
                {
                    giftMateialsBM.Add(new GiftMaterialsBindingModel
                    {
                        Id = giftMaterials[i].Id,
                        GiftId = giftMaterials[i].GiftId,
                        MaterialsId = giftMaterials[i].MaterialsId,
                        Count = giftMaterials[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new GiftBindingModel
                    {
                        Id = id.Value,
                        GiftName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GiftMaterialss = giftMateialsBM
                    });
                }
                else
                {
                    service.AddElement(new GiftBindingModel
                    {
                        GiftName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GiftMaterialss = giftMateialsBM
                    });
                }
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