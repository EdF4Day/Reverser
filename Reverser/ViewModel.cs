
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
        ContentReverser _reverser;

        public Command ChangeForwardCommand { get; set; }
        public Command ChangeBackCommand { get; set; }
        public Command ExitCommand { get; set; }

        public ViewModel()
        {
            _reverser = new ContentReverser();
            InitCommands();
        }

        private void InitCommands()
        {
            ChangeForwardCommand = new Command((_) => true, (_) => { System.Windows.MessageBox.Show("Forward"); });
            ChangeBackCommand = new Command((_) => true, (_) => { System.Windows.MessageBox.Show("Back"); });

            ExitCommand = new Command((_) => true, (_) => { App.Current.Shutdown(); });
        }
    }
}
