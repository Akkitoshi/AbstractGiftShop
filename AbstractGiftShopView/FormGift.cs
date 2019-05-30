using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormGift : Form
{
    public int Id { set { id = value; } }
    private int? id;
    private List<GiftMaterialsViewModel> giftMaterialss;
    public FormGift()
    {
        InitializeComponent();
    }
    private void FormGift_Load(object sender, EventArgs e)
    {
        if (id.HasValue)
        {
            try
            {
                GiftViewModel view = APIClient.GetRequest<GiftViewModel>("api/Gift/Get/" + id.Value);
                if (view != null)
                {
                    textBoxName.Text = view.GiftName;
                    textBoxPrice.Text = view.Price.ToString();
                    giftMaterialss = view.GiftMaterials;
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
            giftMaterialss = new List<GiftMaterialsViewModel>();
        }
    }
    private void LoadData()
    {
        try
        {
            if (giftMaterialss != null)
            {
                dataGridView.DataSource = null;
                dataGridView.DataSource = giftMaterialss;
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
    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var form = new FormGiftMaterials();
        if (form.ShowDialog() == DialogResult.OK)
        {
            if (form.Model != null)
            {
                if (id.HasValue)
                {
                    form.Model.GiftId = id.Value;
                }
                    giftMaterialss.Add(form.Model);
            }
            LoadData();
        }
    }
    private void buttonUpd_Click(object sender, EventArgs e)
    {
        if (dataGridView.SelectedRows.Count == 1)
        {
            var form = new FormGiftMaterials();
            form.Model = giftMaterialss[dataGridView.SelectedRows[0].Cells[0].RowIndex];
            if (form.ShowDialog() == DialogResult.OK)
            {
                    giftMaterialss[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                LoadData();
            }
        }
    }
    private void buttonDel_Click(object sender, EventArgs e)
    {
        if (dataGridView.SelectedRows.Count == 1)
        {
            if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                        giftMaterialss.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (string.IsNullOrEmpty(textBoxPrice.Text))
        {
            MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (giftMaterialss == null || giftMaterialss.Count == 0)
        {
            MessageBox.Show("Заполните материалы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            List<GiftMaterialsBindingModel> productComponentBM = new List<GiftMaterialsBindingModel>();
            for (int i = 0; i < giftMaterialss.Count; ++i)
            {
                productComponentBM.Add(new GiftMaterialsBindingModel
                {
                    Id = giftMaterialss[i].Id,
                    GiftId = giftMaterialss[i].GiftId,
                    MaterialsId = giftMaterialss[i].MaterialsId,
                    Count = giftMaterialss[i].Count
                });
            }
            if (id.HasValue)
            {
                APIClient.PostRequest<GiftBindingModel, bool>("api/Gift/UpdElement", new GiftBindingModel
                {
                    Id = id.Value,
                    GiftName = textBoxName.Text,
                    Price = Convert.ToInt32(textBoxPrice.Text),
                    GiftMaterialss = productComponentBM
                });
            }
            else
            {
                APIClient.PostRequest<GiftBindingModel, bool>("api/Gift/AddElement", new GiftBindingModel
                {
                    GiftName = textBoxName.Text,
                    Price = Convert.ToInt32(textBoxPrice.Text),
                    GiftMaterialss = productComponentBM
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
