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
            config.AddRootPath(@"C:\ProgramData\MediaBrowser\StartupFolder");
            
            library = Library.Initialize(config);

            foreach (var item in library.GetRootItems()) {
                currentFolder.Items.Add(item);
            }

        }
    }
}
