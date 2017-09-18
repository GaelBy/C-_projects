using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Solo
{
    public class PlayArea
    {
        private int length;
        private int height;
        private Button selected;

        public int Length { get; private set; }
        public int Height { get; private set; }
        public MainWindow Window { get; private set; }
        public Grid SoloGrid { get; set; }
        
        public PlayArea(Grid SoloPlayArea, MainWindow window)
        {
            length = SoloPlayArea.ColumnDefinitions.Count;
            height = SoloPlayArea.RowDefinitions.Count;
            Window = window;
            SoloGrid = SoloPlayArea;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if ((i > 1 && i < 5) || (j > 1 && j < 5))
                    {   
                        Button button = new Button() { Background = Brushes.Black, Height = 20, Width = 20 };
                        SoloGrid.Children.Add(button);
                        Grid.SetColumn(button, i);
                        Grid.SetRow(button, j);
                        button.Click += Button_Click;
                    }   
                }
            }
            foreach (Button btn in SoloGrid.Children)
            {
                if (Grid.GetColumn(btn) == 3 && Grid.GetRow(btn) == 3)
                    btn.Background = Brushes.Gray;
            }
        }

        public bool IsInArea(int i, int j)
        {
            if (i >= 0 && i < length && j >= 0 && j < height)
                return true;
            else
                return false;
        }

        public void CheckWin()
        {
            int count = 0;
            foreach (Button button in SoloGrid.Children)
            {
                if (button.Background.Equals(Brushes.Black))
                    count++;
            }
            if (count == 1)
                Window.DisplayWin();
        }
        public void CheckLoss()
        {
            List<SoloMove> moveList = generateMoveList();
            if (moveList.Count == 0)
                Window.DisplayLost();
        }

        public List<SoloMove> generateMoveList()
        {
            List<SoloMove> MoveList = new List<SoloMove>();
            foreach (Button button in SoloGrid.Children)
            {
                int x = Grid.GetColumn(button);
                int y = Grid.GetRow(button);
                foreach (int d in Enum.GetValues(typeof(Direction)))
                {
                    SoloMove move = new SoloMove(x, y, d, this);
                    if (move.IsValid())
                        MoveList.Add(move);
                }
            }
            return MoveList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (selected == null)
            {
                if (button.Background.Equals(Brushes.Black))
                {
                    button.Width = 25;
                    button.Height = 25;
                    selected = button;
                }
            }
            else if (selected == button)
            {
                button.Width = 20;
                button.Height = 20;
                selected = null;
            }
            else
            {
                int x = Grid.GetColumn(selected);
                int y = Grid.GetRow(selected);
                int direction = -1;
                if (Grid.GetColumn(button) == x + 2)
                    direction = (int)Direction.E;
                else if (Grid.GetColumn(button) == x - 2)
                    direction = (int)Direction.W;
                else if (Grid.GetRow(button) == y + 2)
                    direction = (int)Direction.S;
                else if (Grid.GetRow(button) == y - 2)
                    direction = (int)Direction.N;
                if (direction != -1)
                {
                    SoloMove move = new SoloMove(x, y, direction, this);
                    if (move.IsValid())
                    {
                        move.ApplyMove();
                        selected.Height = 20;
                        selected.Width = 20;
                        selected = null;
                        CheckWin();
                        CheckLoss();
                    }
                }
            }
        }
    }
}
