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

namespace Solo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayArea playArea;
        public MainWindow()
        {
            InitializeComponent();
            playArea = new PlayArea(SoloPlayArea, this);
        }
        public void Restart_Click(object sender, RoutedEventArgs e)
        {
            Status.Text = "";
            foreach (Button button in playArea.SoloGrid.Children)
            {
                if (Grid.GetRow(button) == 3 && Grid.GetColumn(button) == 3)
                    button.Background = Brushes.Gray;
                else
                    button.Background = Brushes.Black;
            }
        }
        public void DisplayWin()
        {
            Status.Text = "You win!";
        }
        public void DisplayLost()
        {
            Status.Text = "You lose!";
        }
    }
}
