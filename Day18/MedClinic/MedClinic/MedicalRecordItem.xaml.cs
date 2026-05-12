using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MedClinic.Models;

namespace MedClinic
{
    public partial class MedicalRecordItem : UserControl
    {
        private bool isExpanded = false;
        private MedicalRecord record;

        public MedicalRecordItem()
        {
            InitializeComponent();
        }

        public void SetRecord(MedicalRecord r, int index)
        {
            record = r;

            DiagnosisText.Text = r.Diagnosis;
            DoctorText.Text = $"Врач: {r.Doctor}";
            DateText.Text = r.Date.ToString("dd.MM.yyyy");
            DescriptionText.Text = r.Description;

            SetImportanceColor(r);

            // Анимация появления с задержкой по индексу
            var sb = (Storyboard)Resources["FadeInRecord"];
            var delay = System.TimeSpan.FromSeconds(index * 0.15);

            Opacity = 0;
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = delay
            };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                sb.Begin(this);
            };
            timer.Start();
        }

        private void SetImportanceColor(MedicalRecord r)
        {
            string diagnosis = r.Diagnosis?.ToLower() ?? "";

            bool isCritical = diagnosis.Contains("инфаркт") ||
                              diagnosis.Contains("инсульт") ||
                              diagnosis.Contains("перелом") ||
                              diagnosis.Contains("операция");

            bool isWarning = diagnosis.Contains("грипп") ||
                              diagnosis.Contains("ангина") ||
                              diagnosis.Contains("бронхит") ||
                              diagnosis.Contains("пневмония");

            if (isCritical)
            {
                ImportanceIndicator.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                ImportanceBadge.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                ImportanceText.Text = "⚠️ Важная запись";
            }
            else if (isWarning)
            {
                ImportanceIndicator.Background = new SolidColorBrush(Color.FromRgb(243, 156, 18));
                ImportanceBadge.Background = new SolidColorBrush(Color.FromRgb(243, 156, 18));
                ImportanceText.Text = "🔶 Требует внимания";
            }
            else
            {
                ImportanceIndicator.Background = new SolidColorBrush(Color.FromRgb(74, 144, 217));
                ImportanceBadge.Background = new SolidColorBrush(Color.FromRgb(74, 144, 217));
                ImportanceText.Text = "✅ Обычная запись";
            }
        }

        private void Record_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isExpanded)
            {
                var sb = (Storyboard)Resources["CollapseDetails"];
                sb.Begin(this);
                ArrowText.Text = "▼";
            }
            else
            {
                var sb = (Storyboard)Resources["ExpandDetails"];
                sb.Begin(this);
                ArrowText.Text = "▲";
            }

            isExpanded = !isExpanded;
        }
    }
}