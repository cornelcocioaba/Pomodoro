using System;
using System.Windows;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            tsPomodoro.Value = Properties.Settings.Default.Pomodoro;
            tsShortBreak.Value = Properties.Settings.Default.ShortBreak;
            tsLongBreak.Value = Properties.Settings.Default.LongBreak;
            cbAlwaysOnTop.IsChecked = Properties.Settings.Default.AlwaysOnTop;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveSettings()
        {

            Properties.Settings.Default.Pomodoro = tsPomodoro.Value ?? tsPomodoro.DefaultValue ?? new TimeSpan();
            Properties.Settings.Default.ShortBreak = tsShortBreak.Value ?? tsShortBreak.DefaultValue ?? new TimeSpan();
            Properties.Settings.Default.LongBreak = tsLongBreak.Value ?? tsLongBreak.DefaultValue ?? new TimeSpan();
            Properties.Settings.Default.AlwaysOnTop = cbAlwaysOnTop.IsChecked ?? false;

            Properties.Settings.Default.Save();
        }
    }
}
