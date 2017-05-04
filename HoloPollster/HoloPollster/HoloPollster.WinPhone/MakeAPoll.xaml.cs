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
    public sealed partial class MakeAPoll : Page
    {
        int rowIndex;
        static public List<PollData> questions;
        public MakeAPoll()
        {
            this.InitializeComponent();
            questions = new List<PollData>();
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
            RowDefinition newRow2 = new RowDefinition();
            //RowDefinition newRow3 = new RowDefinition();
            rowIndex += 2;
            /*newRow.Height = GridLength.Auto;
            newRow.MinHeight = 30;*/
            grid.RowDefinitions.Add(newRow);
            grid.RowDefinitions.Add(newRow2);
            //grid.RowDefinitions.Add(newRow3);
            AdderSet.SetValue(Grid.RowProperty, rowIndex);
            //ToggleSwitch newBut = new ToggleSwitch();
            TextBox text = new TextBox();
            Button close = new Button();
            close.Height = 50;
            close.Width = 50;
            close.Content = "X";
            close.Click += this.RemoveQuestion;
            //close.Background.SetValue(SolidColorBrush.ColorProperty, Windows.UI.Colors.Red);
            //close.Foreground.SetValue(SolidColorBrush.ColorProperty, Windows.UI.Colors.White);
            close.HorizontalAlignment = HorizontalAlignment.Right;
            close.VerticalAlignment = VerticalAlignment.Top;
            //newBut.OnContent = "TextBox";
            //newBut.OffContent = "Radio Buttons";
            //newBut.IsOn = true;
            text.Text = "Your Question Here";
            Grid.SetRow(close, rowIndex - 2);
            //Grid.SetRow(newBut, rowIndex - 2);
            Grid.SetRow(text, rowIndex - 1);
            grid.Children.Add(close);
            //grid.Children.Add(newBut);
            grid.Children.Add(text);



            //Name each of these programmatically and add to a list of structs
        }
        void RemoveQuestion(object sender, RoutedEventArgs e)
        {
            rowIndex -= 2;
            var rm = sender as Button;

            int rmindex = (int)rm.GetValue(Grid.RowProperty);
            for (int i = 0; i < 2; i++)
            {
                foreach (UIElement control in grid.Children)
                {
                    // var usercontrol = control as StackPanel;
                    //if (usercontrol != null)
                    //{
                    //  int childrowindex = (int)usercontrol.GetValue(Grid.RowProperty);
                    int childrowindex = (int)control.GetValue(Grid.RowProperty);
                    if (childrowindex == rmindex)
                    {
                        grid.Children.Remove(control);
                        grid.RowDefinitions.RemoveAt(childrowindex);
                        break;
                    }

                    /*  */
                    // }

                }
                foreach (FrameworkElement control in grid.Children)
                {
                    int childrowindex = (int)control.GetValue(Grid.RowProperty);
                    if (childrowindex > rmindex)
                    {
                        Grid.SetRow(control, childrowindex - 1);
                    }
                }
            }
        }

        private void FinalizePoll(object sender, RoutedEventArgs e)
        {

            foreach (TextBox tb in FindVisualChildren<TextBox>(grid))
            {
                PollData elem = new PollData();
                elem.QuestionText = tb.Text;
                int tbrowindex = (int)tb.GetValue(Grid.RowProperty);
                foreach (ToggleSwitch toggle in FindVisualChildren<ToggleSwitch>(grid))
                {

                    int childrowindex = (int)toggle.GetValue(Grid.RowProperty);
                    if (childrowindex == tbrowindex-1)
                    {
                        if (toggle.IsOn)
                        {
                            elem.type = PollData.AnswerType.TextBox;
                        }
                        else elem.type = PollData.AnswerType.RadioButton;
                        break;
                    }

                }
                questions.Add(elem);
            }

            this.Frame.Navigate(typeof(NamePoll));
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomeScreen));
        }
    }
}