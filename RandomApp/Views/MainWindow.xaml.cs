using RandomApp.Pages;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> item {get;set;}
        public static MainWindow Instance { get; private set; }
        public string s { get; set;}
        public bool b = true;
        public MainWindow()
        {
            InitializeComponent();
            
            Instance = this;
            MainContent.Navigate(new Page1());
            comboBox.ItemsSource = PageShow();
            comboBox.Text=PageShow().FirstOrDefault()?.ToString();
           
            BackgroundMusic.Play();
        }



        public void Page2ToPage3(string message)
        {
            MainContent.Navigate(new Page3(message));
            s = message;
        }

        private string[] PageShow()
        {
            string[] strArray =
            {
                "Page1",
                "Page2",
                "Page3",
            };
            return strArray;
        }
        public void NavigateToPage2()
        {
            MainContent.Navigate(new Page2());
            comboBox.Text = PageShow()[1].ToString();
        }

        public void NavigateToPage3()
        {
            MainContent.Navigate(new Page3(s));
            comboBox.Text = PageShow()[2].ToString();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string str = comboBox.SelectedValue.ToString();
            if (str != null)
            {
                switch (str)
                {
                    case "Page1":
                        MainContent.Navigate(new Page1());
                        b = false;
                        break;
                    case "Page2":
                        MainContent.Navigate(new Page2());
                        b = true;
                        break;
                    case "Page3":
                        if (s != null)
                        {
                            MainContent.Navigate(new Page3(s));
                        }
                        else
                        {
                            if (b == false)
                            {
                                b = true;
                                MessageBox.Show("Mời chọn trang tiếp theo sau đó chọn đoàn du lịch để tiếp tục!");
                                comboBox.SelectedValue = PageShow()[0];
                            }
                            else
                            {
                                MessageBox.Show("Mời chọn đoàn du lịch để tiếp tục!");
                                comboBox.SelectedValue = PageShow()[1];
                            }
                        }
                        break;
                }
            }
        }


        private void togbtn_Checked(object sender, RoutedEventArgs e)
        {
            txt1.Text = "\xE74F";
            BackgroundMusic.Pause();
        }

        private void togbtn_Unchecked(object sender, RoutedEventArgs e)
        {
            txt1.Text = "\xE995";
            BackgroundMusic.Play();
            
        }

        private void BackgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundMusic.Position = TimeSpan.Zero;
            BackgroundMusic.Play();
        }
    }
}