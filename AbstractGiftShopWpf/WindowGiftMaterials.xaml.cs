using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowBlankCraft.xaml
    /// </summary>
    public partial class WindowGiftMaterials : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public GiftMaterialsViewModel Model { set { model = value; } get { return model; } }

        private readonly IMaterialsService service;

        private GiftMaterialsViewModel model;

        public WindowGiftMaterials(IMaterialsService service)
        {
            InitializeComponent();
            Loaded += WindowGiftMaterials_Load;
            this.service = service;
        }

        private void WindowGiftMaterials_Load(object sender, EventArgs e)
        {
            List<MaterialsViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBoxMaterials.DisplayMemberPath = "MaterialsName";
                    comboBoxMaterials.SelectedValuePath = "Id";
                    comboBoxMaterials.ItemsSource = list;
                    comboBoxMaterials.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model != null)
            {
                comboBoxMaterials.IsEnabled = false;
                foreach (MaterialsViewModel item in list)
                {
                    if (item.MaterialsName == model.MaterialsName)
                    {
                        comboBoxMaterials.SelectedItem = item;
                    }
                }
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxMaterials.SelectedItem == null)
            {
                MessageBox.Show("Выберите заготовку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new GiftMaterialsViewModel
                    {
                        MaterialsId = Convert.ToInt32(comboBoxMaterials.SelectedValue),
                        MaterialsName = comboBoxMaterials.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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