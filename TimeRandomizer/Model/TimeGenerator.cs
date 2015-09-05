using System;
using System.Collections.Generic;
using System.Text;
using TimeRandomizer.Model.Interfaces;

namespace TimeRandomizer.Model
{
    internal class TimeGenerator : ITimeGenerator
    {
        static readonly Random _random = new Random();
        private readonly StringBuilder _sb;
        private readonly Dictionary<int, string> _dictionary;

        #region Constants

        private const string Its = "It's";
        private const string To = "to";
        private const string Past = "past";
        private const string Half = "half";
        private const string Exactly = "exactly";

        private const string One = "one";     //1
        private const string Two = "two";     //2
        private const string Three = "three"; //3
        private const string Four = "four";   //4
        private const string Five = "five";   //5
        private const string Six = "six";     //6
        private const string Seven = "seven"; //7
        private const string Eight = "eight"; //8
        private const string Nine = "nine";   //9
        private const string Ten = "ten";     //10
        private const string Eleven = "eleven"; //11
        private const string Twelve = "twelve"; //12
        private const string Thirteen = "thirteen"; //13
        private const string Fourteen = "fourteen"; //14
        private const string Fifteen = "fifteen";   //15
        private const string Sixteen = "sixteen";     //16
        private const string Seventeen = "seventeen"; //17
        private const string Eighteen = "eighteen";   //18
        private const string Nineteen = "nineteen";   //19
        private const string Twenty = "twenty"; //20
        private const string Thirty = "thirty"; //30
        private const string Forty = "forty";   //40
        private const string Fifty = "fifty";   //50
        private const string Sixty = "sixty";   //60 
        #endregion

        public TimeGenerator()
        {
            _sb = new StringBuilder(30);
            _dictionary = new Dictionary<int, string>
            {
                { 1, One},
                { 2, Two},
                { 3, Three},
                { 4, Four},
                { 5, Five},
                { 6, Six},
                { 7, Seven},
                { 8, Eight},
                { 9, Nine},
                { 10, Ten},
                { 11, Eleven},
                { 12, Twelve},
                { 13, Thirteen},
                { 14, Fourteen},
                { 15, Fifteen},
                { 16, Sixteen},
                { 17, Seventeen},
                { 18, Eighteen},
                { 19, Nineteen},
                { 20, Twenty},
                { 30, Thirty},
                { 40, Forty},
                { 50, Fifty},
                { 60, Sixty}
            };
        }

        /// <summary>
        /// Генерировать случайное время
        /// </summary>
        /// <param name="multipleOfFive">Генерируемые минуты кратны пяти.</param>
        /// <returns>Время</returns>
        public DateTime GenerateRandomTime(bool multipleOfFive)
        {
            int hour = _random.Next(1, 24);
            int minute = _random.Next(0, 60);
            if (multipleOfFive)
                minute = RoundMinuteMultipleOfFive(minute);

            DateTime time = new DateTime(1, 1, 1, hour, minute, 0);
            return time;
        }

        /// <summary>
        /// Генерировать текст.
        /// </summary>
        /// <param name="time">Время, на основе которого будет сгенерирован текст</param>
        public string GenerateText(DateTime time)
        {
            if (_sb.Length > 0)
                _sb.Remove(0, _sb.Length);

            _sb.Append(Its).Append(" ");

            int hour = time.Hour < 13 ? time.Hour : time.Hour - 12;
            string hoursAsText = null;
            if (time.Minute <= 30)
                hoursAsText = ConvertNumberToText(hour);
            string minutesAsText = null;
            if (time.Minute < 30)
                minutesAsText = ConvertNumberToText(time.Minute);


            if (time.Minute == 0)
            {
                _sb.Append(Exactly).Append(" ");
                _sb.Append(hoursAsText);
            }
            else if (time.Minute == 30)
            {
                _sb.AppendFormat("{0} {1} {2}", Half, Past, hoursAsText);
            }
            else if (time.Minute < 30)
            {
                _sb.AppendFormat("{0} {1} {2}", minutesAsText, Past, hoursAsText);
            }
            else if (time.Minute > 30)
            {
                minutesAsText = ConvertNumberToText(60 - time.Minute);
                time = time.AddHours(1d);
                hour = time.Hour < 13 ? time.Hour : time.Hour - 12;
                if (hour == 0)
                    hour = 12;

                hoursAsText = ConvertNumberToText(hour);
                _sb.AppendFormat("{0} {1} {2}", minutesAsText, To, hoursAsText);
            }

            return _sb.ToString();
        }

        /// <summary>
        /// Конвертирует число от 0 до 59 в текстовое представление на английском языке
        /// </summary>
        /// <param name="number">число от 0 до 59</param>
        /// <returns>Текстовое представление числа</returns>
        public string ConvertNumberToText(int number)
        {
            if (number < 0 || number > 59)
                throw new ArgumentOutOfRangeException(string.Format(
                    "number выходит за диапазон допустимых значений. Передано значение: {0}", number));

            string tensWord = string.Empty;  //десятки
            string unitsWord = string.Empty; //единицы

            if (number < 20 && number > 0)
            {
                return _dictionary[number];
            }
            else if (number != 0)
            {
                int tensPlace = (number / 10) * 10;
                tensWord = _dictionary[tensPlace];

                int unitsPlace = number % 10;
                if (unitsPlace != 0)
                {
                    unitsWord = _dictionary[unitsPlace];
                }
            }

            if (unitsWord != string.Empty)
            {
                return tensWord + " " + unitsWord;
            }

            return tensWord;
        }

        private int RoundMinuteMultipleOfFive(int minute)
        {
            return (minute / 5) * 5;
        }
    }
}
