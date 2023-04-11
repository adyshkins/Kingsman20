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

namespace Kingsman20.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {

        DB.Service editService = null;

        private bool isEdit = false;

        public EditServiceWindow()
        {
            InitializeComponent();
            isEdit = false;
        }

        public EditServiceWindow(DB.Service service)
        {
            InitializeComponent();

            isEdit = true; 

            editService = service;

            // Заполнение типа услуги

            CmbTypeService.ItemsSource = ClassHelper.EF.Context.ServiceType.ToList();
            CmbTypeService.DisplayMemberPath = "TypeName";

            // выгрузка изображения 
            ImgImageService.Source = new BitmapImage(new Uri(service.Image));

            // заполнение полей
            TbNameService.Text = service.ServiceName;
            TbDiscService.Text = service.Description;
            TbPriceService.Text = Convert.ToString(service.Price);

            // заполнение типа услуги
            CmbTypeService.SelectedItem = ClassHelper.EF.Context.ServiceType.Where(i => i.ID == service.ServiceTypeID).FirstOrDefault();

        }

        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {

           
            // валидация

            editService.ServiceName = TbNameService.Text;
            editService.Description = TbDiscService.Text;
            editService.Price = Convert.ToDecimal(TbPriceService.Text);
            editService.ServiceTypeID = (CmbTypeService.SelectedItem as DB.ServiceType).ID;

            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Данные успешно сохранны");

            this.Close();
        }
    }
}
