﻿using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowGift.xaml
    /// </summary>
    public partial class WindowGift : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IGiftService service;

        private int? id;

        private List<GiftMaterialsViewModel> giftMaterials;

        public WindowGift(IGiftService service)
        {
            InitializeComponent();
            Loaded += WindowGift_Load;
            this.service = service;
        }

        private void WindowGift_Load(object sender, EventArgs e)
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
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                giftMaterials = new List<GiftMaterialsViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (giftMaterials != null)
                {
                    dataGridView.ItemsSource = null;
                    dataGridView.ItemsSource = giftMaterials;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[2].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowGiftMaterials>();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.GiftId = id.Value;
                    giftMaterials.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                var form = Container.Resolve<WindowGiftMaterials>();
                form.Model = giftMaterials[dataGridView.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    giftMaterials[dataGridView.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        giftMaterials.RemoveAt(dataGridView.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (giftMaterials == null || giftMaterials.Count == 0)
            {
                MessageBox.Show("Заполните заготовки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<GiftMaterialsBindingModel> giftMaterialBM = new List<GiftMaterialsBindingModel>();
                for (int i = 0; i < giftMaterials.Count; ++i)
                {
                    giftMaterialBM.Add(new GiftMaterialsBindingModel
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
                        GiftMaterialss = giftMaterialBM
                    });
                }
                else
                {
                    service.AddElement(new GiftBindingModel
                    {
                        GiftName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GiftMaterialss = giftMaterialBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}