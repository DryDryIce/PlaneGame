using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGame
{
    public class AI_Module
    {
        private static Random random = new Random();

        public string ID { get; private set; }
        public string Role { get; private set; }
        public int FlightHours { get; set; }

        public AI_Module(string role)
        {
            ID = new string(Enumerable.Range(0, 3).Select(_ => (char)('A' + random.Next(26))).ToArray());
            Role = role;
            FlightHours = random.Next(0, 500);
        }
    }

}
