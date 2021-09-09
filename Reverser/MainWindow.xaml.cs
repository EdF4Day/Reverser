
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reverser
{
    public partial class MainWindow : Window
    {
        #region Fields

        ViewModel _vm;

        #endregion Fields


        #region Constructors

        public MainWindow()  /* verified */
        {
            // View-model first, so components
            // can use its content if needed.
            _vm = new ViewModel();
            this.DataContext = _vm;

            // Boilerplate.
            InitializeComponent();
        }

        #endregion Constructors
    }
}
