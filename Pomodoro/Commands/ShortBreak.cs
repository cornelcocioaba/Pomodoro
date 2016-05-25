using System.Windows;
using System.Windows.Input;

namespace Pomodoro.Commands
{

    public class ShortBreakCommand : CommandBase<ShortBreakCommand>
    {
        public override void Execute(object parameter)
        {
            MainWindow win = GetTaskbarWindow(parameter) as MainWindow;
            win?.SetType(TimeType.ShortBreak);
            CommandManager.InvalidateRequerySuggested();
        }


        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}
