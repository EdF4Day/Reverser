
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Reverser
{
    using IMutable = INotifyPropertyChanged;

    public class ViewModel : IMutable
    {
        #region Definitions, for IMutable

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Definitions, for IMutable


        #region Fields

        private ContentReverser _reverser;

        private ObservableCollection<FileDuples> _files = new ObservableCollection<FileDuples>();

        private bool _isChanging;
        private bool _didThrow;
        private bool _didChange;

        #endregion Fields


        #region Properties

        public ObservableCollection<FileDuples> Files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;

                // Setting this property is also setting the change sources.
                _reverser.ChangeSources = _files
                    .Select(x => new FileChangeSource(x.FullPath))
                    .ToList<IChangeSource>();

                WhenPropertyChanged(nameof(Files));
            }
        }

        #endregion Properties


        #region Command properties

        public Command ChangeForwardCommand { get; set; }
        public Command ChangeBackCommand { get; set; }
        public Command ExitCommand { get; set; }

        #endregion Command properties


        #region Result properties

        public bool DidThrow
        {
            get
            {
                return _didThrow;
            }
            set
            {
                _didThrow = value; 
                WhenPropertyChanged(nameof(DidThrow));
            }
        }

        public bool DidChange
        {
            get
            {
                return _didChange;
            }
            set
            {
                _didChange = value; 
                WhenPropertyChanged(nameof(DidChange));
            }
        }

        #endregion Result properties


        #region IMutable

        private void WhenPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(name);
                PropertyChanged(this, args);
            }
        }

        #endregion IMutable


        #region Constructors and dependencies

        public ViewModel()  /* verified */
        {
            _reverser = new ContentReverser();
            InitCommands();
        }

        private void InitCommands()  /* verified */
        {
            ChangeForwardCommand = new Command(
                TrueWhenNotChanging,
                ChangeForward
                );

            ChangeBackCommand = new Command(
                TrueWhenNotChanging,
                ChangeBack
                );

            ExitCommand = new Command(
                (_) => true,
                (_) => { App.Current.Shutdown(); }
                );
        }

        #endregion Constructors and dependencies


        #region Command implementation methods

        private bool TrueWhenNotChanging(object _)
        {
            return !_isChanging;
        }

        private void ChangeForward(object _)
        {
            // Resetting needed for result flashes and button availability.
            DidChange = false;
            DidThrow = false;
            _isChanging = false;

            // Changing forward, and triggering flash / availability.
            try
            {
                _isChanging = true;

                // Actually changing.
                _reverser.ChangeAllForward();

                DidChange = true;
            }
            catch
            {
                DidThrow = true;
            }

            // Making buttons available whether it succeeded or threw.
            _isChanging = false;
        }

        private void ChangeBack(object _)
        {
            // Resetting needed for result flashes and button availability.
            DidChange = false;
            DidThrow = false;
            _isChanging = false;

            // Changing forward, and triggering flash / availability.
            try
            {
                _isChanging = true;

                // Actually changing.
                _reverser.ChangeAllBack();

                DidChange = true;
            }
            catch
            {
                DidThrow = true;
            }

            // Making buttons available whether it succeeded or threw.
            _isChanging = false;
        }

        #endregion Command implementation methods

    }
}
