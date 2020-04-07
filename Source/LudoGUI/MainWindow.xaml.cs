using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EngineClasses;

namespace LudoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameEngine gameEngine;
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            CreateGUI(gameEngine.GameBoard);

            gameEngine.CreatePlayer("Mirko", "Red");
            gameEngine.CreatePlayer("Anas", "Blue");
            
        }

        public void CreateGUI(GameBoard gameBoard)
        {
            Grid grid = (Grid)Content;

            for (int i = 0; i < 11; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

            }
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            StackPanel stackPanel = new StackPanel
            {
                Background = Brushes.LightPink,
            };
            grid.Children.Add(stackPanel);
            Grid.SetColumn(stackPanel, 11);
            Label user1 = new Label
            {
                Content = "Skriv in ditt användarnamn!",
                FontSize = 30,
                Height = 1000,
                Width = 500,
            };
            TextBlock tb1 = new TextBlock();
            stackPanel.Children.Add(user1);
            stackPanel.Children.Add(tb1);

            for (int row = 0; row < 11; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Button button = new Button
                    {
                        Content = row + "." + column,
                    };
                    if (row == 1 && column == 1 || (row == 2 && column == 2) || (row == 1 && column == 2) || (row == 2 && column == 1) || (row == 4 && column == 0) || (row == 5 && column >= 0 && column < 5))
                    {
                        button.Background = Brushes.Red;
                    }
                    else if (row == 1 && column == 8 || (row == 1 && column == 9) || (row == 2 && column == 8) || (row == 2 && column == 9) || (row == 0 && column == 6) || (row < 5 && column == 5))
                    {
                        button.Background = Brushes.Blue;
                        button.Content = 
                    }
                    else if (row == 8 && column == 1 || (row == 8 && column == 2) || (row == 9 && column == 1) || (row == 9 && column == 2) || (row == 10 && column == 4) || (row < 11 && row > 5 && column == 5))
                    {
                        button.Background = Brushes.Green;
                    }
                    else if (row == 8 && column == 8 || (row == 8 && column == 9) || (row == 9 && column == 8) || (row == 9 && column == 9) || (row == 6 && column == 10) || (row == 5 && column > 5))
                    {
                        button.Background = Brushes.Yellow;
                    }
                    else if (row == 5 && column == 5)
                    {
                        button.Background = Brushes.Gold;
                        button.Content = "GOAL";
                    }
                    else if (row == 4 && column > 0 || (column < 5 && column > 5) || (row == 6 && column >= 0) || (column < 5 && column > 5) || (column == 4 && row >= 0 && row <= 10) || (column == 6 && row >= 0 && row <= 10))
                    {
                        button.Background = Brushes.Tomato;
                    }
                    else
                    {
                        button.Background = Brushes.Black;
                    }
                    grid.Children.Add(button);
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);

                    button.Click += HandleButtonClick;

                }
            }
        }

        private void HandleButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("You clicked " + button.Content);
        }
    }
}
