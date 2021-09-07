
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace Reverser
{
    /* A class like this provides interaction and control for arbitrary processes provided as delegates, 
     * with simple Binding syntax in XAML once an instance is inited and exposed as a public property. */

    public class Command : ICommand
    {
        #region Definitions, including ICommand events

        /* Any handler that would be wired to CanExecuteChanged to respond to it is instead wired (by WPF code) 
         * to CommandManager.RequerySuggested, so that it is raised as frequently as for built-in commands.  
         * This has the effect of calling CanExecute() to get state as often as needed for full statefulness.
         * This code was discovered at StackOverflow, and though obscure, is better than more obvious options. */

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion Definitions, including ICommand events


        #region Delegate properties

        public Func<object, bool> CanEnact { get; set; }

        public Action<object> Enact { get; set; }

        #endregion Delegate properties


        #region Constructors and dependencies

        /* these ctors name hard delegate types in their parameter lists, but lambdas can be used */

        public Command(Func<object, bool> setCanEnact, Action<object> enactor)
        {
            this.CanEnact = setCanEnact;
            this.Enact = enactor;
        }

        #endregion Constructors and dependencies


        #region Public methods, defined by ICommand

        /* This `parameter` is not the one used by Execute() / provided in XAML as CommandParameter. */

        public bool CanExecute(object parameter)
        {
            /* invokes the delegate to get the value */
            bool canEnact = this.CanEnact(parameter);
            return canEnact;
        }

        /* This `parameter` is the same as that used in a RoutedCommand / RoutedUICommand, 
         * so you can set its value using CommandParameter in XAML */

        public void Execute(object parameter)
        {
            /* invokes the delegate */
            this.Enact(parameter);
        }

        #endregion Public methods, defined by ICommand
    }
}
