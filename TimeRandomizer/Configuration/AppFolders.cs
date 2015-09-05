using System;
using System.Collections.Generic;
using System.IO;
using TimeRandomizer.Configuration;

namespace TimeRandomizer
{
    public enum Folder
    {
        /// <summary>
        /// Корневая папка для всех сохраняемых элементов программы
        /// </summary>
        AppData
    }

    public class AppFolders : AppFolderBase<Folder>
    {
        /// <summary>
        /// Инициализирует пути к данным приложения
        /// </summary>
        /// <param name="rootDirectoryName">Название корневой папки для хранения данных приложения</param>
        public AppFolders(string rootDirectoryName)
        {
            AppDataFolderName = rootDirectoryName;

            string appData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                rootDirectoryName);

            _folders = new Dictionary<Folder, string>
            {
                { Folder.AppData, appData }
            };
        }

        /// <summary>
        /// Название папки для данных лаунчера
        /// </summary>
        public string AppDataFolderName { get; private set; }
    }
}
