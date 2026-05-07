using System.Windows;
using System.Windows.Controls;
using MedClinic.Models;
using MedClinic.ViewModels;

namespace MedClinic
{
    public partial class MainWindow : Window
    {
        private MedicalRecordsViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MedicalRecordsViewModel();
            DataContext = viewModel;

            viewModel.ShowAddPatientWindow = OpenAddPatient;
            viewModel.ShowAddRecordWindow = OpenAddRecord;
            viewModel.ShowEditRecordWindow = OpenEditRecord;
        }

        private void OpenAddPatient()
        {
            var win = new PatientDialog { Owner = this };
            if (win.ShowDialog() == true)
                viewModel.AddPatient(win.Result);
        }

        private async void OpenAddRecord()
        {
            var win = new RecordDialog { Owner = this };
            if (win.ShowDialog() == true)
                await viewModel.AddRecordAsync(win.Result);
        }

        private async void OpenEditRecord()
        {
            if (viewModel.SelectedRecord == null) return;
            var win = new RecordDialog(viewModel.SelectedRecord) { Owner = this };
            if (win.ShowDialog() == true)
                await viewModel.UpdateRecordAsync(win.Result);
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void About_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Программа учёта медицинских записей\nВерсия 2.0 (MVVM)",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}