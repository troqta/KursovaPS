using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        private static readonly Random random = new Random();
        int time = 0;
        int pointsint = 0;
        List<Button> buttons = new List<Button>();
        public SolidColorBrush getRandomColor()
        {
            if (Difficulty.SelectedItem.Equals("Easy"))
            {
                return getRandomColorEasy();
            }
            else if (Difficulty.SelectedItem.Equals("Medium"))
            {
                return getRandomColorMeidum();
            }
            else
            {
                return getRandomColorHard();
            }
        }
        public SolidColorBrush getRandomColorEasy()
        {
           
            int randomNumber = random.Next(0, 1000);
            if (randomNumber < 250)
            {
                return new SolidColorBrush(Colors.Green);

            }
            else if(randomNumber>250 && randomNumber < 500)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (randomNumber > 500 && randomNumber < 750)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Yellow);
            }


        }
        public SolidColorBrush getRandomColorMeidum()
        {

            int randomNumber = random.Next(0, 1250);
            if (randomNumber < 250)
            {
                return new SolidColorBrush(Colors.Green);

            }
            else if (randomNumber > 250 && randomNumber < 500)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (randomNumber > 500 && randomNumber < 750)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if (randomNumber > 750 && randomNumber < 1000)
            {
                return new SolidColorBrush(Colors.Purple);
            }
            else
            {
                return new SolidColorBrush(Colors.Yellow);
            }


        }
        public SolidColorBrush getRandomColorHard()
        {

            int randomNumber = random.Next(0, 1500);
            if (randomNumber < 250)
            {
                return new SolidColorBrush(Colors.Green);

            }
            else if (randomNumber > 250 && randomNumber < 500)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (randomNumber > 500 && randomNumber < 750)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if (randomNumber > 750 && randomNumber < 1000)
            {
                return new SolidColorBrush(Colors.Purple);
            }
            else if (randomNumber > 1000 && randomNumber < 1250)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else
            {
                return new SolidColorBrush(Colors.Yellow);
            }


        }
        public MainWindow()
        {
            
            InitializeComponent();
            List<string> difficulties = new List<string>();
            difficulties.Add("Easy");
            difficulties.Add("Medium");
            difficulties.Add("Hard");
            Difficulty.ItemsSource = difficulties;
            Difficulty.SelectedIndex = 0;
            for (int i = 0; i < 15; ++i)
             {
                 for (int j = 0; j < 15; j++)
                 {
                        Button button = new Button();
                        button.Height = 30;
                        button.Width = 30;
                        button.SetValue(Grid.RowProperty, i);
                        button.SetValue(Grid.ColumnProperty, j);
                        button.Click += boxClick;
                        button.IsEnabled = false;
                        
                    
                        gameGrid.Children.Add(button);

                 }
            }
            
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
            
            

        }
        
        private void Dt_Tick(object sender, EventArgs e)
        {
            time++;
            gameTime.Content = time.ToString();
            if (time == 60)
            {
                dt.Stop();
                GameOver();
                time = 0;
                gameTime.Content = 0;

            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Difficulty.IsEnabled = false;
            StartGame.IsEnabled = false;
            dt.Start();
            points.Content = 0;
            pointsint = 0;

                foreach (Control ctrl in gameGrid.Children)
                {
                    if (ctrl is Button)
                    {
                    ctrl.Background = getRandomColor();
                   
                        ctrl.IsEnabled = true; ;
                    }
                }
            
        }
        private void GameOver()
        {
            foreach (Control ctrl in gameGrid.Children)
            {
                if (ctrl is Button)
                {
                    ctrl.Background = getRandomColor();
                    ctrl.IsEnabled = false; 
                }
            }
            MessageBox.Show("Game over!... Points: "+ pointsint);
            Difficulty.IsEnabled = true;
            StartGame.IsEnabled = true;
        }
        public void boxClick(object sender, RoutedEventArgs e)
        {
            HashSet<Button> used = new HashSet<Button>();
            buttons.Add(((Button)sender));
            used.Add(((Button)sender));


            fillListByColor(((Button)sender), used);
            if (buttons.Count >= 3) {
                foreach (Button button in buttons)
                {
                    button.Background = getRandomColor();
                }
                pointsint += buttons.Count * 10;
                points.Content = pointsint;
            }
            buttons.Clear();
            

        }
        public void fillListByColor(Button button , HashSet<Button> used)
        {
            
            int col = (int)button.GetValue(Grid.ColumnProperty);
            int row = (int)button.GetValue(Grid.RowProperty);
            
           
            if (col+1 >= 0 && col+1<15)
            {
                if(getButtonByCoordinates(row, col + 1).Background.ToString().Equals(button.Background.ToString()) && !used.Contains(getButtonByCoordinates(row, col + 1)))
                {
                   
                    buttons.Add(getButtonByCoordinates(row, col + 1));
                    used.Add(getButtonByCoordinates(row, col + 1));
                    fillListByColor(getButtonByCoordinates(row, col + 1), used);
                }
            }
            if (col - 1 >= 0 && col - 1 < 15)
            {
                
                if (getButtonByCoordinates(row, col - 1).Background.ToString().Equals(button.Background.ToString()) && !used.Contains(getButtonByCoordinates(row, col - 1)))
                {
                    buttons.Add(getButtonByCoordinates(row, col - 1));
                    used.Add(getButtonByCoordinates(row, col - 1));
                    fillListByColor(getButtonByCoordinates(row, col - 1), used);
                }
            }
            if (row + 1 >= 0 && row + 1 < 15)
            {
              
                if (getButtonByCoordinates(row +1, col).Background.ToString().Equals(button.Background.ToString()) && !used.Contains(getButtonByCoordinates(row + 1, col)))
                {
                    buttons.Add(getButtonByCoordinates(row +1, col));
                    used.Add(getButtonByCoordinates(row + 1, col));
                    fillListByColor( getButtonByCoordinates(row +1, col), used);
                }
            }
            if (row - 1 >= 0 && row - 1 < 15)
            {
              
                if (getButtonByCoordinates(row -1, col).Background.ToString().Equals(button.Background.ToString()) && !used.Contains(getButtonByCoordinates(row - 1, col)))
                {
                    buttons.Add(getButtonByCoordinates(row -1, col));
                    used.Add(getButtonByCoordinates(row - 1, col));
                    fillListByColor( getButtonByCoordinates(row -1, col), used);
                }
            }

        }
        public Button getButtonByCoordinates(int row, int col)
        {
            foreach(Control ctrl in gameGrid.Children)
            {
                if(ctrl is Button)
                {
                    if((int)ctrl.GetValue(Grid.RowProperty)==row && (int)ctrl.GetValue(Grid.ColumnProperty) == col)
                    {
 
                        return(Button)ctrl;
                    }
                }
            }
            return null;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to quit ?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                base.OnClosing(e);
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
                e.Cancel = false;
            }
        }
    }
    
}
