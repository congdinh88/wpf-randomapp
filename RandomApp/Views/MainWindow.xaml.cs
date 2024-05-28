﻿using RandomApp.Pages;
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
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Navigate(new Page1());

        }

        public void NavigateToPage2()
        {
            MainContent.Navigate(new Page2());
        }

        public void NavigateToPage3()
        {
            MainContent.Navigate(new Page3());
        }
    }
}