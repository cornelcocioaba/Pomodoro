using System.Windows;
using System.Windows.Input;

namespace Pomodoro.Commands
{

    public class ShowSettingCommand : CommandBase<ShowSettingCommand>
    {
        object parameter = null;
        public override void Execute(object parameter)
        {
            this.parameter = parameter;

            SettingsWindow settings = new SettingsWindow();
            settings.Show();
            settings.btnOK.Click += BtnOK_Click;
            CommandManager.InvalidateRequerySuggested();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = GetTaskbarWindow(parameter) as MainWindow;

            win?.LoadSettings();
        }

        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}
