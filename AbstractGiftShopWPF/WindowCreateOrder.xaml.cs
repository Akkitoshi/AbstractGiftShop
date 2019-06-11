using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
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
    /// Логика взаимодействия для WindowCreateOrder.xaml
    /// </summary>
    public partial class WindowCreateOrder : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ISClientService serviceC;

        private readonly IGiftService serviceCF;

        private readonly IMainService serviceM;


        public WindowCreateOrder(ISClientService serviceC, IGiftService serviceCF, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += WindowCreateOrder_Load;
            comboBoxGift.SelectionChanged += comboBoxGift_SelectedIndexChanged;
            comboBoxGift.SelectionChanged += new SelectionChangedEventHandler(comboBoxGift_SelectedIndexChanged);
            this.serviceC = serviceC;
            this.serviceCF = serviceCF;
            this.serviceM = serviceM;
        }

        private void WindowCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<SClientViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxSClient.DisplayMemberPath = "SClientFIO";
                    comboBoxSClient.SelectedValuePath = "Id";
                    comboBoxSClient.ItemsSource = listC;
                    comboBoxGift.SelectedItem = null;
                }
                List<GiftViewModel> listCF = serviceCF.GetList();
                if (listCF != null)
                {
                    comboBoxGift.DisplayMemberPath = "GiftName";
                    comboBoxGift.SelectedValuePath = "Id";
                    comboBoxGift.ItemsSource = listCF;
                    comboBoxGift.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxGift.SelectedItem != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = ((GiftViewModel)comboBoxGift.SelectedItem).Id;
                    GiftViewModel product = serviceCF.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxGift_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxSClient.SelectedItem == null)
            {
                MessageBox.Show("Выберите получателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxGift.SelectedItem == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new SOrderBindingModel
                {
                    SClientId = ((SClientViewModel)comboBoxSClient.SelectedItem).Id,
                    GiftId = ((GiftViewModel)comboBoxGift.SelectedItem).Id,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
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