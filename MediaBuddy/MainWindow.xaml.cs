using System;
using System.Collections.Generic;
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
using MediaLibrary;

namespace MediaBuddy {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Library library; 

        public MainWindow() {
            InitializeComponent();

            var config = Configuration.DefaultVideoLibraryConfig;
            config.RootPath = @"C:\ProgramData\MediaBrowser\StartupFolder";
            
            library = Library.Initialize(config);
            var rootItem = library.GetRootItems()[0] as Folder;
            currentFolder.ItemsSource = rootItem.Children;
            currentFolder.DisplayMemberPath = "Name";

        }

        private void currentFolder_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                var folder = currentFolder.SelectedItem as Folder;
                if (folder != null) {
                    currentFolder.ItemsSource = folder.Children;
                }
            } else if (e.Key == Key.Back) { 
                var item = currentFolder.SelectedItem as Item;
                if (item.Parent != null && item.Parent.Parent != null) {
                    var folder = item.Parent.Parent as Folder;
                    if (folder != null) {
                        currentFolder.ItemsSource = folder.Children;
                    }
                }
            }
        }
    }
}
