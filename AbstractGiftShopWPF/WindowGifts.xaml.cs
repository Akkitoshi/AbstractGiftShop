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
    /// Логика взаимодействия для WindowGifts.xaml
    /// </summary>
    public partial class WindowGifts : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IGiftService service;

        public WindowGifts(IGiftService service)
        {
            InitializeComponent();
            Loaded += WindowGifts_Load;
            this.service = service;
        }

        private void WindowGifts_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<GiftViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewGifts.ItemsSource = list;
                    dataGridViewGifts.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewGifts.Columns[1].Width = DataGridLength.Auto;
                    dataGridViewGifts.Columns[3].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowGift>();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewGifts.SelectedItem != null)
            {
                var form = Container.Resolve<WindowGift>();
                form.Id = ((GiftViewModel)dataGridViewGifts.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewGifts.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((GiftViewModel)dataGridViewGifts.SelectedItem).Id;
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