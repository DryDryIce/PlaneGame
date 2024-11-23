using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PlaneGame
{
    public class Plane
    {
        public string ID { get; private set; }
        public List<AI_Module> AI_Modules { get; private set; } = new List<AI_Module>();
        public int Fuel { get; set; }
        public bool IsActive { get; set; }
        public Point CurrentPosition { get; set; }
        public Point TargetPosition { get; set; }
        public bool InTransit { get; set; }

        public Plane(Point startPosition)
        {
            ID = Guid.NewGuid().ToString();
            IsActive = true;
            Fuel = new Random().Next(50, 400);
            CurrentPosition = startPosition;
            TargetPosition = startPosition;
            InTransit = false;

            AI_Modules.Add(new AI_Module("Pilot"));
            AI_Modules.Add(new AI_Module("Copilot"));
            AI_Modules.Add(new AI_Module("Maintenance"));
            AI_Modules.Add(new AI_Module("Space Awareness"));
        }

        public bool MoveTo(Airport destination, int weight)
        {
            if (Fuel >= weight)
            {
                Fuel -= weight;
                InTransit = true;
                return true;
            }
            else
            {
                IsActive = false;
                return false;
            }
        }
    }

}
