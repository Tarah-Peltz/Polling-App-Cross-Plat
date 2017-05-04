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
        { //Code that dynamically adds new questions when creating polls
            RowDefinition newRow = new RowDefinition();
            RowDefinition newRow2 = new RowDefinition(); 
            //Add row for "remove question" button and row for textbox to input question text
            rowIndex += 2; 
            grid.RowDefinitions.Add(newRow);
            //Add new rows to row definitions
            grid.RowDefinitions.Add(newRow2);
            AdderSet.SetValue(Grid.RowProperty, rowIndex); //Sets the number of rows on the page
            TextBox text = new TextBox(); //Create textbox for user to write question text
            Button close = new Button(); //Create button to delete a question
            close.Height = 50; //Set button height and width
            close.Width = 50;
            close.Content = "X"; //Populate button with an X
            close.Click += this.RemoveQuestion; //Subscribe button to Remove Question event handler on click
            close.HorizontalAlignment = HorizontalAlignment.Right; //Set location of new button within its row
            close.VerticalAlignment = VerticalAlignment.Top;
            text.Text = "Your Question Here"; //Set default text
            Grid.SetRow(close, rowIndex - 2); //Place rows on page in correct location
            Grid.SetRow(text, rowIndex - 1);
            grid.Children.Add(close); //Add the button and textbox as children of the row
            grid.Children.Add(text);

        }

        void RemoveQuestion(object sender, RoutedEventArgs e)
        { //Dynamically removes a question on click
            //We must remove the button and the textbox associated with that question
            rowIndex -= 2; //Decrement row index since we remove two rows
            var rm = sender as Button;

            int rmindex = (int)rm.GetValue(Grid.RowProperty); //Finds the row of the close button that was clicked
            for (int i = 0; i < 2; i++) //This will run twice. 
                //Since the button gets deleted first, the textbox will assume the same row index of the button
                //This means on the second iteration of the loop, we don't have to change the target row index since the textbox now assumes that row index
            { 
                //We must iterate through based on the row index because we can't dynamically name the close buttons as they are created
                //This means we can't delete based on name. We must iterate through and find the button ourselves
                foreach (UIElement control in grid.Children)
                {
                    int childrowindex = (int)control.GetValue(Grid.RowProperty); //Checks current index of row
                    if (childrowindex == rmindex) //We're on the proper row
                    {
                        grid.Children.Remove(control); //Removes element as child
                        grid.RowDefinitions.RemoveAt(childrowindex); //Removes row itself
                        break;
                    }
                }
                foreach (FrameworkElement control in grid.Children) 
                    //Since we don't know if we're moving a button or a textbox, we refer to it as FrameworkElement
                {
                    int childrowindex = (int)control.GetValue(Grid.RowProperty);
                    if (childrowindex > rmindex) //Ensures we only move up stuff below the deleted question and not stuff above it
                    {
                        Grid.SetRow(control, childrowindex - 1); //Moves each element up by one row
                    }
                }
            }
        }

        private void FinalizePoll(object sender, RoutedEventArgs e)
        {
            //Event handler to finalize the poll and upload it to the cloud
            foreach (TextBox tb in FindVisualChildren<TextBox>(grid)) 
            { //We don't know if we're looking at a textbox or a button. 
                //We can't run "foreach (element in the grid); upload element.text" because a button doesn't have a text field
                //We perform the FindVisualChildren search to create a list of only the textboxes
                PollData elem = new PollData(); //Creates a new PollData instance
                elem.QuestionText = tb.Text; //Since we know tb is a Textbox, we can pull the text now
                //Sets the questionText field of polldata
                int tbrowindex = (int)tb.GetValue(Grid.RowProperty);
                elem.type = PollData.AnswerType.TextBox; 
                //Sets type to textbox. Would allow for expansion to other question types in the future
                questions.Add(elem); //Adds the polldata to the list of polldatas.
            }

            this.Frame.Navigate(typeof(NamePoll)); //navitages to page to name poll
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        { //Pulls all the elements of a particular type from a grid
            //Pulled from the internet. Credit to: http://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type
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
        { //Allows user to cancel poll creation
            this.Frame.Navigate(typeof(HomeScreen));
            
        }
    }
}