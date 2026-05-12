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

            SetImportanceColor(r.Importance);

            Opacity = 0;
            var delay = System.TimeSpan.FromSeconds(index * 0.15);
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = delay
            };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                var sb = (Storyboard)Resources["FadeInRecord"];
                sb.Begin(this);
            };
            timer.Start();
        }

        private void SetImportanceColor(Importance importance)
        {
            SolidColorBrush color;
            string badgeText;

            switch (importance)
            {
                case Importance.Critical:
                    color = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                    badgeText = "⚠️ Срочная / Критическая";
                    break;
                case Importance.Warning:
                    color = new SolidColorBrush(Color.FromRgb(243, 156, 18));
                    badgeText = "🔶 Требует внимания";
                    break;
                default:
                    color = new SolidColorBrush(Color.FromRgb(74, 144, 217));
                    badgeText = "✅ Обычная запись";
                    break;
            }

            ImportanceIndicator.Background = color;
            ImportanceBadge.Background = color;
            ImportanceText.Text = badgeText;
        }

        private void Record_Click(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
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