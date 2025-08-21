using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace POS_system_myowndesign.Views.UserControls
{
    public partial class LeftSideBar : UserControl
    {
        // Store reference to the main window
        private MainWindow mainWindow;

        public LeftSideBar()
        {
            InitializeComponent();
            Loaded += LeftSideBar_Loaded;
        }

        private void LeftSideBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Get reference to the main window when loaded
            mainWindow = Window.GetWindow(this) as MainWindow;

            // Set initial active button
            SetActiveButton(HomeBtn);
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons to default style
            HomeBtn.Style = (Style)Resources["SearchButtonStyle"];
            TablesBtn.Style = (Style)Resources["SearchButtonStyle"];
            OrdersBtn.Style = (Style)Resources["SearchButtonStyle"];
            ReportsBtn.Style = (Style)Resources["SearchButtonStyle"];

            // Set the active button style
            activeButton.Style = (Style)Resources["ActiveButtonStyle"];
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(HomeBtn);
            if (mainWindow != null)
            {
                // Show products view, hide others
                mainWindow.ProductsScrollViewControl.Visibility = Visibility.Visible;
                mainWindow.TablesViewControl.Visibility = Visibility.Collapsed;
                mainWindow.OrdersViewControl.Visibility = Visibility.Collapsed;
            }
        }

        private void TablesBtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(TablesBtn);
            if (mainWindow != null)
            {
                // Show tables view, hide others
                mainWindow.ProductsScrollViewControl.Visibility = Visibility.Collapsed;
                mainWindow.TablesViewControl.Visibility = Visibility.Visible;
                mainWindow.OrdersViewControl.Visibility = Visibility.Collapsed;
                mainWindow.ReportsViewControl.Visibility = Visibility.Collapsed;
            }
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(OrdersBtn);
            if (mainWindow != null)
            {
                // Show orders view, hide others
                mainWindow.ProductsScrollViewControl.Visibility = Visibility.Collapsed;
                mainWindow.TablesViewControl.Visibility = Visibility.Collapsed;
                mainWindow.OrdersViewControl.Visibility = Visibility.Visible;
                mainWindow.ReportsViewControl.Visibility = Visibility.Collapsed;
            }
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(ReportsBtn);
            if (mainWindow != null)
            {
                // Show reports view, hide others
                mainWindow.ProductsScrollViewControl.Visibility = Visibility.Collapsed;
                mainWindow.TablesViewControl.Visibility = Visibility.Collapsed;
                mainWindow.OrdersViewControl.Visibility = Visibility.Collapsed;
                mainWindow.ReportsViewControl.Visibility = Visibility.Visible;
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?",
                                                     "Logout Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Perform logout actions here
                MessageBox.Show("Logged out successfully!");

                // You might want to close the application or show a login screen
                // Application.Current.Shutdown();
            }
        }
    }
}