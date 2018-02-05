using System;
using System.Collections.Generic;
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

        private readonly Dictionary<DateTime, string> _words;

        public MainViewModel(ITimeGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            _words = new Dictionary<DateTime, string>(1440); // 24 часа * 60 минут
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

            if (!_words.ContainsKey(time))
            {
                string timeAsText = _timeGenerator.GenerateText(time);
                _words.Add(time, timeAsText);
            }

            TimeAsText = _words[time];
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
