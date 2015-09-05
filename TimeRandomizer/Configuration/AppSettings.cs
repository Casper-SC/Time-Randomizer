using System;
using System.IO;
using System.Xml.Serialization;

namespace TimeRandomizer
{
    public class AppSettings
    {
        public AppSettings()
        {
            MinutesMultipleOfFive = false;
        }

        public bool MinutesMultipleOfFive { get; set; }

        /// <summary>
        /// Сохранить экземпляр класса настроек приложения
        /// </summary>
        /// <param name="settings">Экземпляр класса для сохранения</param>
        /// <param name="fileName">Путь к файлу, в который будут сохранены настройки</param>
        public static void Save(AppSettings settings, String fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (StreamWriter streamWriter = new StreamWriter(fs))
            {
                xmlSerializer.Serialize(streamWriter, settings);
            }
        }

        /// <summary>
        /// Загрузить настройки из файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Экземпляр класса настроек</returns>
        public static AppSettings Load(String fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                using (StreamReader streamReader = new StreamReader(fs))
                {
                    return (AppSettings)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            return null;
        }
    }
}
