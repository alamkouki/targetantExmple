using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace reflectionExmple
{
    class Program
    { 
        class position
        {
            public int X;
            public int Y;
            public int score1;
            public int score2;
            public int score3;
            public position pere;
        }

        public static void Main(string[] args)
        {
            string[] map = new string[]
            {
                "+----------------+",
                "|A               |",
                "|XXXXXXXXXXXXX   |",
                "|                |",
                "|      XXXXXXXXXX|",
                "|XXX          XXX|",
                "|xx             B|",
                "+----------------+"
            };
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach(var line in map)
            {
                Console.WriteLine(line);
            }
            position current = null;
            position start = new position { X = 1, Y = 1 };
            position traget = new position { X = 16, Y = 6 };
            List<position> openList = new List<position>();
            List<position> closeList = new List<position>();
            int spot = 0;
            openList.Add(start);
            while (openList.Count > 0)
            {
                int min = openList.Min(l => l.score1);
                current = openList.First(l => l.score1 == min);
                closeList.Add(current);
                Console.SetCursorPosition(current.X, current.Y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('.');
                Console.SetCursorPosition(current.X, current.Y);
                System.Threading.Thread.Sleep(1000);
                openList.Remove(current);
                if(closeList.FirstOrDefault(l=>l.X==traget.X &&
                                            l.Y == traget.Y) != null)
                {
                    break;
                }
                List<position> voisinposition = GetmoveAdjacentSequare(current.X, current.Y, map);
                spot++;
                foreach(position pos in voisinposition)
                {
                    if(closeList.FirstOrDefault(l=>l.X==pos.X && l.Y == pos.Y) != null)
                    {
                        continue;
                    }
                    if (openList.FirstOrDefault(l => l.X == pos.X && l.Y == pos.Y) == null)
                    {
                        pos.score2 = spot;
                        pos.score3 = nombrespot(pos.X, pos.Y, traget.X, traget.Y);
                        pos.score1 = pos.score2 + pos.score3;
                        pos.pere = current;
                        openList.Insert(0, pos);
                    }
                    else
                    {
                        if(spot+pos.score3<pos.score1)
                        {
                            pos.score2 = spot;
                            pos.score1 = pos.score2 + pos.score3;
                            pos.pere = current;
                        }
                    }
                }
            }
            while (current != null)
            {
                Console.SetCursorPosition(current.X, current.Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('o');
                Console.SetCursorPosition(current.X, current.Y);
                current = current.pere;
                System.Threading.Thread.Sleep(1000);

            }
            Console.ReadLine();


        }
      static List<position> GetmoveAdjacentSequare(int x, int y,string[] map)
      {
            List<position> positionestime = new List<position>()
            {
                new position{X=x,Y=y-1},
                new position{X=x,Y=y+1},
                new position{X=x-1,Y=y},
                new position{X=x+1,Y=y}
            };

            return positionestime.Where(l => map[l.Y][l.X] == ' ' || map[l.Y][l.X] == 'B').ToList();
      }
        static int nombrespot(int x,int y, int tragetx , int tragety)
        {
            return Math.Abs(tragetx-x) + Math.Abs(tragety-y);
        }

    }
}
