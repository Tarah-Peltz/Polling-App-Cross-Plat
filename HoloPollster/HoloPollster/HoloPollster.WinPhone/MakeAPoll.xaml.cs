using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class MakeAPage : Page
    {
        int rowIndex;
        public MakeAPage()
        {
            this.InitializeComponent();
            rowIndex = 0;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void AddQuestion(object sender, RoutedEventArgs e)
        {
            RowDefinition newRow = new RowDefinition();
            rowIndex += 1;
            newRow.Height = GridLength.Auto;
            newRow.MinHeight = 30;
 
            grid.RowDefinitions.Add(newRow);
            Adder.SetValue(Grid.RowProperty, rowIndex);
            StackPanel stack = new StackPanel();
            Button newBut = new Button();
            TextBox text = new TextBox();
            newBut.Height = 100;
            newBut.Content = "Hi!";
            Grid.SetRow(stack,rowIndex-1);
            grid.Children.Add(stack);
            stack.Children.Add(text);
            stack.Children.Add(newBut);

        }
    }
}
