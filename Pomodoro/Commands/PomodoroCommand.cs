using System.Windows;
using System.Windows.Input;

namespace Pomodoro.Commands
{

    public class PomodoroCommand : CommandBase<PomodoroCommand>
    {
        public override void Execute(object parameter)
        {
            MainWindow win = GetTaskbarWindow(parameter) as MainWindow;
            win?.SetType(TimeType.Pomodoro);
            CommandManager.InvalidateRequerySuggested();
        }


        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}
