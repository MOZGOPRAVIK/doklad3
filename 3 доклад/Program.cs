using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_доклад
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Edge>> graph = new Dictionary<string, List<Edge>>
            {
                { "A", new List<Edge> { new Edge { Target = "B", Weight = 1 }, new Edge { Target = "C", Weight = 4 } } },
                { "B", new List<Edge> { new Edge { Target = "C", Weight = 2 }, new Edge { Target = "D", Weight = 5 } } },
                { "C", new List<Edge> { new Edge { Target = "D", Weight = 1 } } },
                { "D", new List<Edge>() }
            };

            var distances = Dijkstra(graph, "A");

            foreach (var vertex in distances)
            {
                if (vertex.Value == int.MaxValue)
                {
                    Console.WriteLine($"Distance from A to {vertex.Key}: no way");
                }
                else
                {
                    Console.WriteLine($"Distance from A to {vertex.Key}: {vertex.Value}");
                }
            }
        }
        class Edge
        {
            public string Target { get; set; }
            public int Weight { get; set; }
        }

        static Dictionary<string, int> Dijkstra(Dictionary<string, List<Edge>> graph, string start)
        {
            Dictionary<string, int> distances = graph.Keys.ToDictionary(vertex => vertex, vertex => int.MaxValue);
            distances[start] = 0;

            List<KeyValuePair<string, int>> priorityQueue = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>(start, 0) };

            while (priorityQueue.Count > 0)
            {
                priorityQueue.Sort((x, y) => x.Value.CompareTo(y.Value));

                var current = priorityQueue[0];
                priorityQueue.RemoveAt(0);

                string currentVertex = current.Key;
                int currentDistance = current.Value;

                if (currentDistance > distances[currentVertex])
                    continue;

                foreach (var edge in graph[currentVertex])
                {
                    int newDistance = currentDistance + edge.Weight;

                    if (newDistance < distances[edge.Target])
                    {
                        distances[edge.Target] = newDistance;
                        priorityQueue.Add(new KeyValuePair<string, int>(edge.Target, newDistance));
                    }
                }
            }

            return distances;
        }
    }
}
