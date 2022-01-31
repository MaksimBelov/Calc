using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calc
{
    internal class AppCommands
    {
        public static RoutedCommand Copy { get; set; }

        static AppCommands()
        {
            Copy = new RoutedCommand(); // Команда копирования в контекстном меню
        }
    }
}
