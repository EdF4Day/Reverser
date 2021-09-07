
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ViewModel
    {
        public Command ChangeForwardCommand { get; set; }
        public Command ChangeBackCommand { get; set; }
        public Command ExitCommand { get; set; }

        public ViewModel()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            ExitCommand = new Command((_) => true, (_) => { App.Current.Shutdown(); });
        }
    }
}
