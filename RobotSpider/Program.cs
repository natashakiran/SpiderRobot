using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider
{
    public class Coordinates<T1, T2>
    {
        public T1 x { get; set; }
        public T2 y { get; set; }
    }

    class RobotSpider
    {
        private Tuple<int, int> gridStart = new Tuple<int, int>(0,0);
        private Tuple<int, int> gridEnd = new Tuple<int, int>(0,0);
        private Coordinates<int, int> currentPosition = new Coordinates<int, int>();
        private int currentOrientation = 90;
        private int angleSweep = 90;
        private char[] moveCommands ={'F'};
        private char[] orientationCommands = {'L','R'};

        public RobotSpider()
        {
            this.gridStart = new Tuple<int, int>(0, 0);
            this.gridEnd = new Tuple<int, int>(7, 15);
            this.currentOrientation = 90;
        }
        public void SetGridStart(Tuple<int, int> position)
        {
            this.gridStart = position;
        }

        public void SetGridEnd(Tuple<int, int> position)
        {
            this.gridEnd = position;
        }
        
        public void SetCurrentPosition(Coordinates<int, int> position)
        {
            this.currentPosition = position;
        }

        public Coordinates<int, int> GetCurrentPosition()
        {
            return currentPosition;
        }

        public void SetCurrentOrientation(int orientation)
        {
            this.currentOrientation = orientation;
        }

        public string GetCurrentOrientation()
        {
            if (this.currentOrientation == 0)
            {
                return "Right";
            }
            else if(this.currentOrientation==180){
                return "Left";
            }
            else if (this.currentOrientation == 90)
            {
                return "Up";
            }
            else if (this.currentOrientation == 270)
            {
                return "Down";
            }
            return "Invaid" ;
        }

        public void Move(char command)
        {
            if (command == 'F')
            {
                switch (this.currentOrientation)
                {
                    case 0:
                        if (this.currentPosition.x < this.gridEnd.Item1)
                            this.currentPosition.x += 1;
                        else
                            Console.WriteLine("Grid Boundary Reached");
                        break;
                    case 90:
                        if (this.currentPosition.y < this.gridEnd.Item2)
                            this.currentPosition.y += 1;
                        else
                            Console.WriteLine("Grid Boundary Reached");
                        break;
                    case 180:

                        if(this.currentPosition.x >0)
                            this.currentPosition.x -= 1;
                        else
                            Console.WriteLine("Grid Boundary Reached");
                        break;
                    case 270:
                        if (this.currentPosition.y >0)
                            this.currentPosition.y -= 1;
                        else
                            Console.WriteLine("Grid Boundary Reached");
                        break;
                    default:
                        
                        break;
                }
            }
        }

        public void ChangeOrientation(char command)
        {
            switch (command)
            {
                case 'L':
                    this.currentOrientation += this.angleSweep;
                    this.currentOrientation = this.currentOrientation % 360;
                    break;
                case 'R':
                    this.currentOrientation -= this.angleSweep;
                    if (this.currentOrientation < 0)
                    {
                        this.currentOrientation += 360; 
                    }
                    
                    break;
                default:
                    break;
            }
        }

        public void SendCommand(char command)
        {
            if(moveCommands.Contains(command)){
                Move(command);
            }
            else if (orientationCommands.Contains(command))
            {
                ChangeOrientation(command);
            }
            else
            {
                Console.WriteLine("Invalid Command");
            }
        }

        public void GetCurrentState()
        {
            Console.WriteLine("{0} {1} {2}", this.currentPosition.x.ToString(), this.currentPosition.y.ToString(), this.GetCurrentOrientation());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Test data
            char[] commands = { 'F', 'L', 'F', 'L', 'F', 'R', 'F', 'F', 'L', 'F'};
            Coordinates<int, int> spiderStartPoint = new Coordinates<int, int>();
            spiderStartPoint.x = 2;
            spiderStartPoint.y = 4;
            int startOrientation = 180;

            RobotSpider spider = new RobotSpider();
            spider.SetCurrentPosition(spiderStartPoint);
            spider.SetCurrentOrientation(startOrientation);
            spider.GetCurrentState();
            foreach (char command in commands)
            {
                spider.SendCommand(command);
                spider.GetCurrentState();
            }
            Console.WriteLine("\nPress Enter Key to Exit..");

            Console.ReadLine();
            
        }
    }
}
