using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TimeRandomizer
{
    public sealed class GlobalConfig
    {
        private readonly SynchronizationContext _context;

        public GlobalConfig()
        {
            _context = SynchronizationContext.Current ?? new SynchronizationContext();
            Folders = new AppFolders(Constants.AppName);
        }

        public AppFolders Folders { get; protected set; }

        public AppSettings Settings { get; protected set; }

        public event EventHandler Loaded;

        public event EventHandler Saved;

        public Task SaveAsync()
        {
            return Task.Factory.StartNew(() =>
             {
                 AppSettings.Save(Settings, Path.Combine(Folders.GetPath(Folder.AppData), Constants.SettingsFileName));
                 OnSaved();
             });
        }

        public Task LoadAsync()
        {
            return Task.Factory.StartNew(() =>
             {
                 Folders.CheckFolders();
                 string settingsFileName = Path.Combine(Folders.GetPath(Folder.AppData), Constants.SettingsFileName);
                 Settings = AppSettings.Load(settingsFileName) ?? new AppSettings();
                 OnLoaded();
             });
        }

        private void OnLoaded()
        {
            EventHandler handler = Loaded;
            if (handler != null)
            {
                _context.Post(args =>
                {
                    handler(this, EventArgs.Empty);
                }, null);
            }
        }

        private void OnSaved()
        {
            EventHandler handler = Saved;
            if (handler != null)
            {
                _context.Post(args =>
                {
                    handler(this, EventArgs.Empty);
                }, null);
            }
        }
    }
}
