using System.Diagnostics;
using System.Windows;
using TimeRandomizer.ViewModel;

namespace TimeRandomizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Exit"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs"/> that contains the event data.</param>
        protected override async void OnExit(ExitEventArgs e)
        {
            var locator = Resources["Locator"] as ViewModelLocator;
            Debug.Assert(locator != null, "locator == null");
            await locator.Main.Configuration.SaveAsync();
            base.OnExit(e);
        }
    }
}
