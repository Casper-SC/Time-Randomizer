using System;

namespace TimeRandomizer.Model.Interfaces
{
    public interface ITimeGenerator
    {
        /// <summary>
        /// Генерировать случайное время
        /// </summary>
        /// <param name="multipleOfFive">Генерируемые минуты кратны пяти.</param>
        /// <returns>Время</returns>
        DateTime GenerateRandomTime(bool multipleOfFive);

        /// <summary>
        /// Генерировать текст.
        /// </summary>
        /// <param name="time">Время, на основе которого будет сгенерирован текст</param>
        string GenerateText(DateTime time);

        /// <summary>
        /// Конвертирует число от 0 до 59 в текстовое представление на английском языке
        /// </summary>
        /// <param name="number">число от 0 до 59</param>
        /// <returns>Текстовое представление числа</returns>
        string ConvertNumberToText(int number);
    }
}