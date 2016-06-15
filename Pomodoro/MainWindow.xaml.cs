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
            TimeType lastType = (TimeType)Enum.Parse(TimeType.GetType(), Properties.Settings.Default.LastType);
            SetType(lastType);

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

                if (TimeType == TimeType.Pomodoro)
                {
                    SetType(TimeType.ShortBreak);
                }
                else
                {
                    SetType(TimeType.Pomodoro);
                }
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
                    Background = this.FindResource("Background.Pomodoro") as SolidColorBrush;
                    btnStartStop.Foreground = this.FindResource("Foreground.Pomodoro") as SolidColorBrush;
                    btnStartStop.Style = this.FindResource("PomodoroButtonStyle") as Style;
                    break;
                case TimeType.LongBreak:
                case TimeType.ShortBreak:
                    Background = this.FindResource("Background.Break") as SolidColorBrush;
                    btnStartStop.Foreground = this.FindResource("Foreground.Break") as SolidColorBrush;
                    btnStartStop.Style = this.FindResource("BreakButtonStyle") as Style;
                    break;
            }

            ResetTimer();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastType = TimeType.ToString() ?? "Pomodoro";

            Properties.Settings.Default.Save();
        }
    }
}
