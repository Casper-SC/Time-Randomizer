using System;

namespace TimeRandomizer.Model.Interfaces
{
    public interface ITimeGenerator
    {
        /// <summary>
        /// ������������ ��������� �����
        /// </summary>
        /// <param name="multipleOfFive">������������ ������ ������ ����.</param>
        /// <returns>�����</returns>
        DateTime GenerateRandomTime(bool multipleOfFive);

        /// <summary>
        /// ������������ �����.
        /// </summary>
        /// <param name="time">�����, �� ������ �������� ����� ������������ �����</param>
        string GenerateText(DateTime time);

        /// <summary>
        /// ������������ ����� �� 0 �� 59 � ��������� ������������� �� ���������� �����
        /// </summary>
        /// <param name="number">����� �� 0 �� 59</param>
        /// <returns>��������� ������������� �����</returns>
        string ConvertNumberToText(int number);
    }
}