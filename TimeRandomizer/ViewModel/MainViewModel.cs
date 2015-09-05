using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using TimeRandomizer.Model.Interfaces;

namespace TimeRandomizer.ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly ITimeGenerator _timeGenerator;
        private ICommand _randomizeCommand;
        private DateTime _time;
        private bool _loaded;
        private bool _multipleOfFive;
        private string _timeAsText;

        public MainViewModel(ITimeGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            _timeGenerator = generator;

            Configuration = new GlobalConfig();
            Configuration.Loaded += (sender, args) =>
                {
                    MultipleOfFive = Configuration.Settings.MinutesMultipleOfFive;
                    GenerateTime();
                    SettingsLoaded = true;
                };
            Configuration.LoadAsync();
        }

        private void GenerateTime()
        {
            DateTime time = _timeGenerator.GenerateRandomTime(MultipleOfFive);
            Time = time;
            TimeAsText = _timeGenerator.GenerateText(time);
        }

        public ICommand RandomizeCommand
        {
            get
            {
                if (_randomizeCommand == null)
                {
                    _randomizeCommand = new RelayCommand(GenerateTime);
                }

                return _randomizeCommand;
            }
            set { _randomizeCommand = value; }
        }

        /// <summary>
        /// Время в виде текста (слов).
        /// </summary>
        public string TimeAsText
        {
            get { return _timeAsText; }
            private set
            {
                _timeAsText = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Время
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            private set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Кратно 5-ти
        /// </summary>
        public bool MultipleOfFive
        {
            get { return _multipleOfFive; }
            set
            {
                _multipleOfFive = value;
                Configuration.Settings.MinutesMultipleOfFive = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Возвращает или задаёт загружены ли настройки
        /// </summary>
        public bool SettingsLoaded
        {
            get { return _loaded; }
            set
            {
                _loaded = value;
                RaisePropertyChanged();
            }
        }

        internal GlobalConfig Configuration { get; private set; }
    }
}
