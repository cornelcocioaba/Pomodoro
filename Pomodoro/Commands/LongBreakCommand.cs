using System.Windows;
using System.Windows.Input;

namespace Pomodoro.Commands
{

    public class LongBreakCommand : CommandBase<LongBreakCommand>
    {
        public override void Execute(object parameter)
        {
            MainWindow win = GetTaskbarWindow(parameter) as MainWindow;
            win?.SetType(TimeType.LongBreak);
            CommandManager.InvalidateRequerySuggested();
        }


        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}
