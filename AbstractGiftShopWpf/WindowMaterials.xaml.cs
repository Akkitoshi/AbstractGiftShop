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
    /// Логика взаимодействия для WindowMaterialss.xaml
    /// </summary>
    public partial class WindowMaterialss : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IMaterialsService service;

        public WindowMaterialss(IMaterialsService service)
        {
            InitializeComponent();
            Loaded += WindowMaterialss_Load;
            this.service = service;
        }

        private void WindowMaterialss_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<MaterialsViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewMaterialss.ItemsSource = list;
                    dataGridViewMaterialss.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewMaterialss.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowMaterials>();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterialss.SelectedItem != null)
            {
                var form = Container.Resolve<WindowMaterials>();
                form.Id = ((MaterialsViewModel)dataGridViewMaterialss.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterialss.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((MaterialsViewModel)dataGridViewMaterialss.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
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
    }
}