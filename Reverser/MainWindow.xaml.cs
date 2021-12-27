
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        FileSource _fileSource;

        #endregion Fields


        #region Constructors and dependencies

        public MainWindow()  /* verified */
        {
            // For any files dropped now or later.
            _fileSource = new FileSource();

            // View-model before view, so that onscreen 
            // components can use its content if needed.
            _vm = new ViewModel();
            this.DataContext = _vm;

            // Any files dropped now.
            ApplyAnyStartArgFiles();

            // Boilerplate.
            InitializeComponent();
        }

        private void ApplyAnyStartArgFiles() {
            List<string> argFiles = _fileSource.FilesFromOpeningArgs();

            if (argFiles.Count > 0)
            {
                ReplaceFilesWithFiles(argFiles);
            }
        }

        #endregion Constructors and dependencies


        #region Drag and drop eventing

        private void WhenDraggedOverFilesListBox(object sender, DragEventArgs e)
        {
            if (!CtrlKeyIsPressed(e))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void WhenDroppedOnFilesListBox(object sender, DragEventArgs e)
        {
            List<string> files = _fileSource.FilesFromDragEvent(e);

            if (!CtrlKeyIsPressed(e))
            {
                ReplaceFilesWithFiles(files);
            }
            else
            {
                AddFilesToFiles(files);
            }
        }

        private void AddFilesToFiles(List<string> files)
        {
            List<FileDuple> duples = FileDuple.InstancesFromPaths(files);
            _vm.Files.AddRange(duples);
        }

        private void ReplaceFilesWithFiles(List<string> files)
        {
            List<FileDuple> duples = FileDuple.InstancesFromPaths(files);
            _vm.Files = new ObservableCollection<FileDuple>(duples);
        }

        private bool CtrlKeyIsPressed(DragEventArgs e)
        {
            return (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey;
        }

        #endregion Drag and drop eventing

    }
}
