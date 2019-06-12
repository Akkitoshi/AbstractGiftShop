using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowStock.xaml
    /// </summary>
    /// 

    public partial class WindowStock : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ISStockService service;

        private int? id;

        public WindowStock(ISStockService service)
        {
            InitializeComponent();
            Loaded += WindowStock_Load;
            this.service = service;
        }

        private void WindowStock_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SStockViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SStockName;
                        dataGridViewSStock.ItemsSource = view.StockMaterialss;
                        dataGridViewSStock.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewSStock.Columns[1].Visibility = Visibility.Hidden;
                        dataGridViewSStock.Columns[2].Visibility = Visibility.Hidden;
                        dataGridViewSStock.Columns[3].Width = DataGridLength.Auto;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new SStockBindingModel
                    {
                        Id = id.Value,
                        SStockName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new SStockBindingModel
                    {
                        SStockName = textBoxName.Text
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