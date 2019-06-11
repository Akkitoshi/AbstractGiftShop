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
    /// Логика взаимодействия для WindowMaterials.xaml
    /// </summary>
    public partial class WindowMaterials : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IMaterialsService service;

        public WindowMaterials(IMaterialsService service)
        {
            InitializeComponent();
            Loaded += WindowMaterials_Load;
            this.service = service;
        }

        private void WindowMaterials_Load(object sender, EventArgs e)
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
                    dataGridViewMaterials.ItemsSource = list;
                    dataGridViewMaterials.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewMaterials.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowMaterial>();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterials.SelectedItem != null)
            {
                var form = Container.Resolve<WindowMaterial>();
                form.Id = ((MaterialsViewModel)dataGridViewMaterials.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterials.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((MaterialsViewModel)dataGridViewMaterials.SelectedItem).Id;
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