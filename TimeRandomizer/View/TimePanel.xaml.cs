using System.Windows.Controls;
using System.Windows.Input;

namespace TimeRandomizer.View
{
    public partial class TimePanel : UserControl
    {
        public TimePanel()
        {
            InitializeComponent();
        }

        private void EnglishTextTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            englishTextTextBox.SelectAll();
        }
    }
}
