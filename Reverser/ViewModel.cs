
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
        #region Fields

        ContentReverser _reverser;

        #endregion Fields


        #region Command properties

        public Command ChangeForwardCommand { get; set; }
        public Command ChangeBackCommand { get; set; }
        public Command ExitCommand { get; set; }

        #endregion Command properties


        #region Constructors and dependencies

        public ViewModel()  /* verified */
        {
            _reverser = new ContentReverser();
            InitCommands();
        }

        private void InitCommands()  /* verified */
        {
            ChangeForwardCommand = new Command(
                (_) => true,
                (_) => { _reverser.ChangeAllForward(); }
                );

            ChangeBackCommand = new Command(
                (_) => true,
                (_) => { _reverser.ChangeAllBack(); }
                );

            ExitCommand = new Command(
                (_) => true,
                (_) => { App.Current.Shutdown(); }
                );
        }

        #endregion Constructors and dependencies
    }
}
