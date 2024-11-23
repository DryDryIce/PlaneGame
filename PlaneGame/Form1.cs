using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneGame
{
    public partial class Form1 : Form
    {
        private Graph graph;
        private List<Rectangle> bullets = new List<Rectangle>();
        private Rectangle gun;
        private Timer animationTimer;
        private Timer bulletTimer;
        private Timer planeSpawnTimer; // Agrega esta línea
        private Random random = new Random();
        private int score = 0; // Puntaje del jugador
        private Timer gameTimer; // Timer para controlar el tiempo del juego
        private int elapsedTime = 0; // Tiempo transcurrido en segundos


        public Form1()
        {
            InitializeComponent();

            gun = new Rectangle(this.ClientSize.Width / 2 - 20, this.ClientSize.Height - 50, 40, 20);

            graph = new Graph();
            GenerateRandomGraph();

            animationTimer = new Timer { Interval = 20 };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            bulletTimer = new Timer { Interval = 20 };
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();

            planeSpawnTimer = new Timer { Interval = 3000 }; // Generar aviones cada 3 segundos
            planeSpawnTimer.Tick += PlaneSpawnTimer_Tick;
            planeSpawnTimer.Start();

            gameTimer = new Timer { Interval = 1000 }; // Control del tiempo global (1 segundo)
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += Form1_KeyDown;
        }

        private void PlaneSpawnTimer_Tick(object sender, EventArgs e)
        {
            var airportsWithCapacity = graph.Nodes.OfType<Airport>().Where(a => a.Planes.Count < a.HangarCapacity).ToList();
            if (airportsWithCapacity.Any())
            {
                var selectedAirport = airportsWithCapacity[random.Next(airportsWithCapacity.Count)];

                try
                {
                    selectedAirport.BuildPlane();
                    lstOutput.Items.Add($"Nuevo avión generado en {selectedAirport.Name}. Aviones actuales: {selectedAirport.Planes.Count}");
                }
                catch (Exception ex)
                {
                    lstOutput.Items.Add($"Error al generar avión en {selectedAirport.Name}: {ex.Message}");
                }
            }

            this.Invalidate(); // Redibuja el formulario
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && gun.X > 0)
                gun.X -= 10;
            else if (e.KeyCode == Keys.Right && gun.X < this.ClientSize.Width - gun.Width)
                gun.X += 10;

            this.Invalidate();
        }
        private Dictionary<Node, Point> nodePositions = new Dictionary<Node, Point>();

        private void GenerateRandomGraph()
        {
            nodePositions.Clear();

            for (int i = 0; i < 5; i++)
            {
                Point position;
                do
                {
                    position = new Point(random.Next(50, 500), random.Next(50, 500));
                } while (nodePositions.Values.Any(p => Math.Abs(p.X - position.X) < 50 && Math.Abs(p.Y - position.Y) < 50));

                var airport = new Airport(
                    $"Aeropuerto {i}",
                    random.Next(2, 5), // Capacidad de hangares
                    position,
                    random.Next(100, 500) // Capacidad de combustible
                );

                graph.AddNode(airport);
                nodePositions[airport] = position;
                airport.Position = position; // Asignar la posición al aeropuerto
            }

            foreach (var node in graph.Nodes)
            {
                foreach (var otherNode in graph.Nodes)
                {
                    if (node != otherNode)
                    {
                        graph.AddEdge(node, otherNode, random.Next(10, 50));
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            // Dibujar la pistola
            g.FillRectangle(Brushes.Gray, gun);

            // Dibujar balas
            foreach (var bullet in bullets)
                g.FillRectangle(Brushes.Red, bullet);

            // Dibujar aeropuertos y aviones
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    var position = nodePositions[airport];
                    g.FillEllipse(Brushes.Blue, position.X - 15, position.Y - 15, 30, 30);

                    foreach (var plane in airport.Planes)
                    {
                        g.FillRectangle(Brushes.Green, plane.CurrentPosition.X - 10, plane.CurrentPosition.Y - 10, 20, 20);
                    }
                }
            }
        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i] = new Rectangle(bullets[i].X, bullets[i].Y - 10, bullets[i].Width, bullets[i].Height);
                if (bullets[i].Y < 0) bullets.RemoveAt(i);
            }
            CheckCollisions();
            this.Invalidate();
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    foreach (var plane in airport.Planes.ToList())
                    {
                        if (!plane.InTransit)
                        {
                            // Elegir un aeropuerto aleatorio como destino
                            var destination = graph.Nodes.OfType<Airport>()
                                .Where(a => a != airport)
                                .OrderBy(_ => random.Next())
                                .FirstOrDefault();

                            if (destination != null)
                            {
                                plane.TargetPosition = destination.Position; // Asignar posición destino
                                plane.InTransit = true;
                                lstOutput.Items.Add($"Avión {plane.ID} despegó de {airport.Name} hacia {destination.Name}.");
                            }
                        }

                        if (plane.InTransit)
                        {
                            // Consumir combustible durante el vuelo
                            plane.Fuel -= 1;
                            if (plane.Fuel <= 0)
                            {
                                lstOutput.Items.Add($"Avión {plane.ID} se quedó sin combustible y cayó.");
                                airport.Planes.Remove(plane);
                                continue; // Detener el procesamiento de este avión
                            }

                            // Mover avión más lentamente
                            int dx = (plane.TargetPosition.X - plane.CurrentPosition.X) / 20; // Reducir paso
                            int dy = (plane.TargetPosition.Y - plane.CurrentPosition.Y) / 20;

                            plane.CurrentPosition = new Point(
                                plane.CurrentPosition.X + dx,
                                plane.CurrentPosition.Y + dy
                            );

                            // Verificar si llegó al destino
                            if (Math.Abs(plane.CurrentPosition.X - plane.TargetPosition.X) < 5 &&
                                Math.Abs(plane.CurrentPosition.Y - plane.TargetPosition.Y) < 5)
                            {
                                plane.CurrentPosition = plane.TargetPosition;
                                plane.InTransit = false;

                                // El avión llegó a su destino
                                var destination = graph.Nodes.OfType<Airport>()
                                    .FirstOrDefault(a => a.Position == plane.TargetPosition);

                                if (destination != null)
                                {
                                    if (destination.Fuel >= 20)
                                    {
                                        plane.Fuel += 20;
                                        destination.Fuel -= 20;
                                        lstOutput.Items.Add($"Avión {plane.ID} aterrizó en {destination.Name} y recargó combustible.");
                                    }
                                    else
                                    {
                                        lstOutput.Items.Add($"Avión {plane.ID} aterrizó en {destination.Name}, pero no pudo recargar por falta de combustible.");
                                    }

                                    destination.Planes.Add(plane);
                                    airport.Planes.Remove(plane);
                                }
                            }
                        }
                    }
                }
            }

            this.Invalidate(); // Redibuja el formulario
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;

            // Verificar si el tiempo excede 30 segundos
            if (elapsedTime > 30)
            {
                EndGame("Se acabó el tiempo. ¡Juego terminado!");
                return;
            }

            // Verificar si todos los aeropuertos se quedaron sin combustible
            bool allAirportsOutOfFuel = graph.Nodes.OfType<Airport>().All(a => a.Fuel <= 0);
            if (allAirportsOutOfFuel)
            {
                EndGame("Todos los aeropuertos se quedaron sin combustible. ¡Juego terminado!");
                return;
            }
        }


        private void CheckCollisions()
        {
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    foreach (var plane in airport.Planes.ToList())
                    {
                        var planeBounds = new Rectangle(
                            plane.CurrentPosition.X - 15,
                            plane.CurrentPosition.Y - 15,
                            30,
                            30 // Tamaño del avión actualizado
                        );

                        foreach (var bullet in bullets.ToList())
                        {
                            if (planeBounds.IntersectsWith(bullet))
                            {
                                bullets.Remove(bullet);
                                airport.Planes.Remove(plane);

                                score++; // Incrementar el puntaje
                                lstOutput.Items.Add($"¡Avión {plane.ID} derribado! Puntaje: {score}");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void EndGame(string message)
        {
            animationTimer.Stop();
            bulletTimer.Stop();
            planeSpawnTimer.Stop();
            gameTimer.Stop();

            lstOutput.Items.Add(message);
            MessageBox.Show(message + $"\nPuntaje final: {score}", "Juego Terminado");

            // Reiniciar juego
            bullets.Clear();
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    airport.Planes.Clear();
                    airport.Fuel = airport.FuelCapacity; // Recargar combustible
                }
            }
            score = 0;
            elapsedTime = 0;
            animationTimer.Start();
            bulletTimer.Start();
            planeSpawnTimer.Start();
            gameTimer.Start();
            this.Invalidate();
        }



        private void btnShoot_Click(object sender, EventArgs e)
        {
            bullets.Add(new Rectangle(gun.X + gun.Width / 2 - 5, gun.Y - 10, 10, 10)); // Crear bala
            lstOutput.Items.Add("¡Bala disparada!");
            this.Invalidate(); // Redibuja el formulario
        }

        private void btnGenerateGraph_Click(object sender, EventArgs e)
        {
            if (graph.Nodes.Count > 0)
            {
                lstOutput.Items.Add("El grafo ya está generado.");
                return; // No regenerar el grafo
            }

            GenerateRandomGraph();
            lstOutput.Items.Add("¡Grafo generado con éxito!");
            this.Invalidate(); // Redibuja el formulario
        }

        private void btnBuildPlanes_Click(object sender, EventArgs e)
        {
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    airport.BuildPlane();
                    lstOutput.Items.Add($"Avión construido en {airport.Name}");
                }
            }
            this.Invalidate(); // Redibuja el formulario para mostrar aviones
        }

        private void btnStartMovement_Click(object sender, EventArgs e)
        {
            animationTimer.Start(); // Inicia el Timer de animación
            lstOutput.Items.Add("¡Movimiento de aviones iniciado!");
        }

        private void btnPauseMovement_Click(object sender, EventArgs e)
        {
            animationTimer.Stop(); // Detiene el Timer de animación
            lstOutput.Items.Add("Movimiento de aviones pausado.");
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            bullets.Clear(); // Limpia las balas
            foreach (var node in graph.Nodes)
            {
                if (node is Airport airport)
                {
                    airport.Planes.Clear(); // Limpia los aviones en cada aeropuerto
                }
            }
            lstOutput.Items.Clear(); // Limpia la lista de mensajes
            lstOutput.Items.Add("Juego reiniciado, pero el grafo permanece igual.");
            this.Invalidate(); // Redibuja el formulario
        }

        private void lstOutput_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
