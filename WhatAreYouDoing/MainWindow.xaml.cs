using System.Windows;
using System.Windows.Input;

namespace WhatAreYouDoing
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                
                ((MainWindowViewModel) ViewModel).CloseWindow();
        }
    }
}