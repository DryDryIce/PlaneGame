using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGame
{
    public class Airport : Node
    {
        public int HangarCapacity { get; set; } // Capacidad máxima de aviones en el hangar
        public int CurrentPlaneCount => Planes.Count; // Cantidad actual de aviones
        public Point Position { get; set; } // Posición gráfica del aeropuerto
        public int Fuel { get; set; } // Cantidad actual de combustible disponible
        public int FuelCapacity { get; set; } // Capacidad máxima de combustible
        public List<Plane> Planes { get; set; } = new List<Plane>(); // Lista de aviones en el aeropuerto

        public Airport(string name, int hangarCapacity, Point position, int fuelCapacity) : base(name)
        {
            HangarCapacity = hangarCapacity;
            Position = position;
            FuelCapacity = fuelCapacity;
            Fuel = fuelCapacity; // Inicialmente el aeropuerto tiene la máxima cantidad de combustible
        }

        // Método para construir un avión
        public void BuildPlane()
        {
            if (Planes.Count < HangarCapacity)
            {
                if (Fuel >= 50) // Supongamos que construir un avión consume 50 unidades de combustible
                {
                    // Asignar la posición inicial del avión según la posición del aeropuerto
                    var startPosition = new Point(Position.X, Position.Y);
                    var newPlane = new Plane(startPosition);
                    Planes.Add(newPlane);
                    Fuel -= 50; // Reducir el combustible del aeropuerto
                }
                else
                {
                    throw new Exception($"No hay suficiente combustible en {Name} para construir un avión.");
                }
            }
            else
            {
                throw new Exception($"El hangar de {Name} está lleno. No se pueden construir más aviones.");
            }
        }

        // Método para recargar combustible al aeropuerto
        public void Refuel(int amount)
        {
            if (Fuel + amount > FuelCapacity)
            {
                Fuel = FuelCapacity; // No exceder la capacidad máxima
            }
            else
            {
                Fuel += amount;
            }
        }

        // Método para consumir combustible al aterrizar un avión
        public bool ConsumeFuel(int amount)
        {
            if (Fuel >= amount)
            {
                Fuel -= amount;
                return true;
            }
            else
            {
                return false; // No hay suficiente combustible
            }
        }
    }
}