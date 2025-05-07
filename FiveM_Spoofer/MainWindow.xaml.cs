using System;
using System.Windows;
using FiveM_Spoofer.Services;

namespace FiveM_Spoofer
{
    public partial class MainWindow : Window
    {
        private readonly CleanerService _cleanerService;

        public MainWindow()
        {
            InitializeComponent();
            _cleanerService = new CleanerService();
        }

        private void SpoofButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement HWID spoofing logic here
            MessageBox.Show("HWID Spoofing functionality is not yet implemented.", "Spoof", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CleanerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _cleanerService.Clean();
                MessageBox.Show("Cleaning completed successfully.", "Cleaner", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during cleaning: {ex.Message}", "Cleaner", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
