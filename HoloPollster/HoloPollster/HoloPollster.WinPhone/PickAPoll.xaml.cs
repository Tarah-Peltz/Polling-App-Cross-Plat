using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HoloPollster.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PickAPoll : Page
    {
        int rowIndex;
        static public string picked;
        public PickAPoll()
        {
            rowIndex = 0;
            this.InitializeComponent();


            try {
                foreach (PollsWithMetaData poll in MainPage.polls.CreatedPolls)
                {
                    RowDefinition newRow = new RowDefinition();
                    rowIndex += 1;
                    newRow.Height = GridLength.Auto;
                    newRow.MinHeight = 30;

                    grid.RowDefinitions.Add(newRow);
                    var MyButton = new Button();
                    /* MyButton.Content = new TextBlock()
                     {
                         FontSize = 25,
                         TextAlignment = TextAlignment.Center,
                         TextWrapping = TextWrapping.Wrap,
                         Text = "Name: " + poll.PollName + " Created By: " + poll.PollCreator+ " Created At: " + poll.CreationTime.ToString()
                     };*/
                    MyButton.Content = poll.PollName;
                    MyButton.Click += navigation;
                    Grid.SetRow(MyButton, rowIndex);
                    grid.Children.Add(MyButton);

                }
            }
           catch {
                var tex = new TextBlock();
                tex.FontSize = 24;
                tex.TextWrapping = TextWrapping.WrapWholeWords;
                tex.HorizontalAlignment = HorizontalAlignment.Center;
                tex.Text = "No Polls Created Yet!";
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
                Grid.SetRow(tex, 1);
                grid.Children.Add(tex);
            }

        }




        private void navigation(object sender, RoutedEventArgs e)
        {

            var selection = sender as Button;
            picked = selection.Content.ToString();
            Frame.Navigate(typeof(TakeAPoll));
                
        }


    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
    }
}
  }


