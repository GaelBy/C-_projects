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
    public class SoloMove : IMove
    {
        private int xPos;
        private int yPos;
        private int direction;
        public PlayArea area;
        public Grid soloGrid;

        public SoloMove(int x, int y, int d, PlayArea playArea)
        {
            xPos = x;
            yPos = y;
            direction = d;
            area = playArea;
            soloGrid = area.SoloGrid;
        }

        private Button GetByCoordinates(int x, int y)
        {
            foreach (Button button in soloGrid.Children)
            {
                if (Grid.GetColumn(button) == x && Grid.GetRow(button) == y)
                    return button;
            }
            return null;
        }

        public bool IsValid()
        {
            if (!area.IsInArea(xPos, yPos) || GetByCoordinates(xPos, yPos).Background.Equals(Brushes.Gray))
                return false;
            switch (direction)
            {
                case (int)Direction.W:
                    if (area.IsInArea(xPos - 2, yPos) && GetByCoordinates(xPos - 2, yPos) != null
                        && GetByCoordinates(xPos - 1, yPos).Background.Equals(Brushes.Black) 
                        && GetByCoordinates(xPos - 2, yPos).Background.Equals(Brushes.Gray))
                        return true;
                    return false;
                case (int)Direction.N:
                    if (area.IsInArea(xPos, yPos - 2) && GetByCoordinates(xPos, yPos - 2) != null
                        && GetByCoordinates(xPos, yPos - 1).Background.Equals(Brushes.Black)
                        && GetByCoordinates(xPos, yPos - 2).Background.Equals(Brushes.Gray))
                        return true;
                    return false;
                case (int)Direction.E:
                    if (area.IsInArea(xPos + 2, yPos) && GetByCoordinates(xPos + 2, yPos) != null
                        && GetByCoordinates(xPos + 1, yPos).Background.Equals(Brushes.Black)
                        && GetByCoordinates(xPos + 2, yPos).Background.Equals(Brushes.Gray))
                        return true;
                    return false;
                case (int)Direction.S:
                    if (area.IsInArea(xPos, yPos + 2) && GetByCoordinates(xPos, yPos + 2) != null
                        && GetByCoordinates(xPos, yPos + 1).Background.Equals(Brushes.Black)
                        && GetByCoordinates(xPos, yPos + 2).Background.Equals(Brushes.Gray))
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        public void ApplyMove()
        {
            GetByCoordinates(xPos, yPos).Background = Brushes.Gray;
            switch (direction)
            {
                case (int)Direction.W:
                    GetByCoordinates(xPos - 1, yPos).Background = Brushes.Gray;
                    GetByCoordinates(xPos - 2, yPos).Background = Brushes.Black;
                    break;
                case (int)Direction.N:
                    GetByCoordinates(xPos, yPos - 1).Background = Brushes.Gray;
                    GetByCoordinates(xPos, yPos - 2).Background = Brushes.Black;
                    break;
                case (int)Direction.E:
                    GetByCoordinates(xPos + 1, yPos).Background = Brushes.Gray;
                    GetByCoordinates(xPos + 2, yPos).Background = Brushes.Black;
                    break;
                case (int)Direction.S:
                    GetByCoordinates(xPos, yPos + 1).Background = Brushes.Gray;
                    GetByCoordinates(xPos, yPos + 2).Background = Brushes.Black;
                    break;
            }
        }
    }
    public enum Direction { N, W, S, E };
}
