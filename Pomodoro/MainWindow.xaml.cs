using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro
{
    public enum TimeType
    {
        Pomodoro,
        ShortBreak,
        LongBreak
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan currentTimeSpan;
        private TimeSpan pomodoro;
        private TimeSpan shortBreak;
        private TimeSpan longBreak;
        private bool alwaysOnTop;

        public TimeType TimeType { get; set; }

        public TimeSpan Time
        {
            get
            {
                switch (TimeType)
                {
                    case TimeType.Pomodoro: return pomodoro;
                    case TimeType.ShortBreak: return shortBreak;
                    case TimeType.LongBreak: return longBreak;
                    default: return pomodoro;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            LoadSettings();

            //Init Timer
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        public void LoadSettings()
        {
            pomodoro = Properties.Settings.Default.Pomodoro;
            shortBreak = Properties.Settings.Default.ShortBreak;
            longBreak = Properties.Settings.Default.LongBreak;
            alwaysOnTop = Properties.Settings.Default.AlwaysOnTop;

            Topmost = alwaysOnTop;
            ResetTimer();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTimeSpan = currentTimeSpan.Subtract(timer.Interval);

            if (currentTimeSpan > TimeSpan.Zero)
            {
                lblTime.Content = currentTimeSpan.ToString(@"mm\:ss");
            }
            else
            {
                StopTimer();
            }
        }

        private void ResetTimer()
        {
            currentTimeSpan = Time;
            lblTime.Content = currentTimeSpan.ToString(@"mm\:ss");
        }

        private void StartTimer()
        {
            timer.Start();
            btnStartStop.Content = "STOP";
        }

        private void StopTimer()
        {
            timer.Stop();
            btnStartStop.Content = "START";
            ResetTimer();
        }

        public void SetType(TimeType type)
        {
            TimeType = type;
            // update the layout
            switch (type)
            {
                case TimeType.Pomodoro:
                    Background = Brushes.Red;
                    break;
                case TimeType.ShortBreak:
                    Background = Brushes.Green;
                    break;
                case TimeType.LongBreak:
                    Background = Brushes.Blue;
                    break;
            }

            ResetTimer();
        }
    }
}
