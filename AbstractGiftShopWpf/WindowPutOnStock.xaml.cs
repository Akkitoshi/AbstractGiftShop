using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowPutOnStock.xaml
    /// </summary>
    /// <summary>
    /// Логика взаимодействия для PutOnStock.xaml
    /// </summary>
    public partial class WindowPutOnStock : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ISStockService serviceS;

        private readonly IMaterialsService serviceI;

        private readonly IMainService serviceM;

        public WindowPutOnStock(ISStockService serviceS, IMaterialsService serviceI, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += WindowPutOnStock_Load;
            this.serviceS = serviceS;
            this.serviceI = serviceI;
            this.serviceM = serviceM;
        }

        private void WindowPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> listI = serviceI.GetList();
                if (listI != null)
                {
                    comboBoxMaterials.DisplayMemberPath = "MaterialsName";
                    comboBoxMaterials.SelectedValuePath = "Id";
                    comboBoxMaterials.ItemsSource = listI;
                    comboBoxMaterials.SelectedItem = null;
                }
                List<SStockViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxSStock.DisplayMemberPath = "SStockName";
                    comboBoxSStock.SelectedValuePath = "Id";
                    comboBoxSStock.ItemsSource = listS;
                    comboBoxSStock.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (comboBoxSStock.SelectedItem == null)
            {
                MessageBox.Show("Выберите базу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.PutMaterialsOnStock(new StockMaterialsBindingModel
                {
                    MaterialsId = Convert.ToInt32(comboBoxMaterials.SelectedValue),
                    SStockId = Convert.ToInt32(comboBoxSStock.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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