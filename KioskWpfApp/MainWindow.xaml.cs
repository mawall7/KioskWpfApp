using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KioskWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// This part represents a simple customer ordering kiosk. 
    /// TODO: order number is now simply increasing every time! and also IsReady has to be hardcoded set. by 1 but the app is working and can send orders to the api PostOrder Azure Function and
    /// the javascript orderdisplay app is showing new orders and if IsReady is change to true the order will be moved to the ready orders in the js app.
    /// I should also be able to make a kitchen microapp where orders can be set as ready if I want. Simulating payment is not handled yet either. 
    public partial class MainWindow : Window
    {
        public ObservableCollection<ImageItem> ImageList { get; set; }

        private readonly HttpClient httpClient = new HttpClient();

        private int CurrentOrderNumber = 0;

        private Image _selectedImage;


        //private readonly HttpClient httpClient;
        public MainWindow()
        {
            InitializeComponent();

            ImageList = new ObservableCollection<ImageItem>
            {
                new ImageItem{Id= 1, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Cheese, IsReady=false},
                new ImageItem{Id= 2, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 3, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Fish, IsReady=false} ,
                new ImageItem{Id= 4, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem= FoodItems.Veggie, IsReady=true},
                new ImageItem{Id= 5, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 6, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 7, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 8, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 9, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 10, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 11, ImagePath="Images/andy-chilton-oljL3vFlV2g-unsplash.jpg", OrderItem = FoodItems.Hamburger},
                new ImageItem{Id= 12, ImagePath=  "Images/tim-mossholder-FH3nWjvia-U-unsplash.jpg", OrderItem = FoodItems.Hamburger},

            };

            //httpClient = new HttpClient();

            this.DataContext = this;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MainMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Clicked Main");
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            _selectedImage = sender as Image;
            if (sender is not Image clickedImage)
                return;

            Order.Visibility = Visibility.Visible;

            if (_selectedImage.DataContext is ImageItem clickedItem)
            {
                int clickedId = clickedItem.Id;
                string path = clickedItem.ImagePath;
                string orderitem = clickedItem.OrderItem.ToString();
                //MessageBox.Show($"Clicked image ID: {clickedId}\nPath: {path}");
                OrderBox.Text = $"Do you want to Order?\n #{orderitem } ?";
            }
            

                //// The DataContext of the image is the ImageItem bound to this item
                //if (image.DataContext is ImageItem clickedItem)
                //{
                //    int clickedId = clickedItem.Id;
                //    string path = clickedItem.ImagePath;
                //    string orderitem = clickedItem.OrderItem.ToString();
                //    //MessageBox.Show($"Clicked image ID: {clickedId}\nPath: {path}");
                //    OrderBox.Text = $"Do you want to Order?\n #{orderitem } ?";

                //    //TODO : logic to accept or decline change ordernumber to something where setting ordernumber follows a logic
                //    //then send: 
                //    var Order = new Order() { OrderName = clickedItem.OrderItem.ToString(), OrderNumber = 4, IsReady = clickedItem.IsReady, TakeAway = false };

                //    await SendOrder(Order);


                //}
            }

            private async Task SendOrder(Order orderitem)
        {
            //setting name to localrun for local manual testing
            string apiUrl = "http://localhost:7071/api/PostOrder?name=localrun";

            try
            {

                var response = await httpClient.PostAsJsonAsync(apiUrl, orderitem);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void OrderNo_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Order.Visibility = Visibility.Hidden;
            _selectedImage = default;
            e.Handled = true;
        }

        private async void OrderYes_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            // The DataContext of the image is the ImageItem bound to this item
           

            if (_selectedImage.DataContext is ImageItem clickedItem)
            {
                int clickedId = clickedItem.Id;
                string path = clickedItem.ImagePath;
                string orderitem = clickedItem.OrderItem.ToString();
                //MessageBox.Show($"Clicked image ID: {clickedId}\nPath: {path}");
                OrderBox.Text = $"Do you want to Order?\n #{orderitem } ?";
                
                var Order = new Order() { OrderName = clickedItem.OrderItem.ToString(), OrderNumber = 4, IsReady = clickedItem.IsReady, TakeAway = false };
                _selectedImage = default;
                this.Order.Visibility = Visibility.Hidden;

                await SendOrder(Order);
             }
        }
    }
}
