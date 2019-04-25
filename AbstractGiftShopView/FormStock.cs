﻿using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopView;
using System;
using System.Windows.Forms;

namespace AbstractGiftShopView
{
    public partial class FormStock : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormStock()
        {
            InitializeComponent();
        }
        private void FormStock_Load(object sender, EventArgs e)
        {

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
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
                        SStockName = nameTextBox.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<SStockBindingModel, bool>("api/Stock/AddElement", new SStockBindingModel
                    {
                        SStockName = nameTextBox.Text
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