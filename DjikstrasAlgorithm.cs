using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class GraphAlgorithms
    {

        public static void PrintPath(int[,] adjacencyMatrix, Dictionary<int, int> previousPaths, int source, int destination)
        {
            Console.WriteLine("Path " + source + " to " + destination);
            int current = destination;
            int totalCost = 0;
            while (current != source)
            {
                int previous = previousPaths[current];
                Console.WriteLine("Vertex: " + current + "-> " + previous + " Cost:" + adjacencyMatrix[previous, current]);
                totalCost = totalCost + adjacencyMatrix[previous, current];
                current = previous;
                
            }
            Console.WriteLine("Total Cost for path: " + totalCost);
        }

        public static Dictionary<int, int> DjikstrasAlgorithm(int[,] adjacencyMatrix, int source)
        {

            Dictionary<int, int> previous = new Dictionary<int, int>(); // Previous dictionary holds the previous vertex that was used
            Dictionary<int, int> distances = new Dictionary<int, int>(); // Key is vertex. Value is distance


            // Initialization step
            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                if (i != source)
                {
                    distances.Add(i, int.MaxValue);
                    previous.Add(i, int.MaxValue);
                }
            }

            distances.Add(source, 0);

            // Main loop
            while (distances.Count > 0)
            {
                // OrderedList might make this line cleaner
                int currentVertex = distances.OrderBy(pair => pair.Value).Take(1).ToDictionary(pair => pair.Key, pair => pair.Value).Select(d => d.Key).First(); // Retrieve minimum value from sorted dictionary at first index.
                for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                {
                    if (currentVertex != i  // This represents a vertex's connection to itself in the adjacency matrix which is marked as zero but should not be explored.
                        && distances.ContainsKey(i) // Vertex must not have been explored already
                        && adjacencyMatrix[currentVertex, i] != int.MaxValue) // Max value represents an undefined edge
                    {
                        int alternateRoute = distances[currentVertex] + adjacencyMatrix[currentVertex, i];
                        if (alternateRoute < distances[i])
                        {
                            distances[i] = alternateRoute;
                            previous[i] = currentVertex;
                        }
                    }
                }
                distances.Remove(currentVertex);

            }

            return previous;

        }


        public static void Main(string[] args)
        {
            int[,] matrix = { { 0, 2, 4, int.MaxValue, int.MaxValue, int.MaxValue },
                              { int.MaxValue, 0, 1, 4, 2, int.MaxValue },
                              { int.MaxValue, int.MaxValue, 0, int.MaxValue, 3, int.MaxValue },
                              { int.MaxValue, int.MaxValue, int.MaxValue, 0, int.MaxValue, 2},
                              { int.MaxValue, int.MaxValue, int.MaxValue, 3, 0, 2 },
                              { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 0} };
            Dictionary<int, int> previousVertecies = DjikstrasAlgorithm(matrix, 0);

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                PrintPath(matrix, previousVertecies, 0 , i);
            }

        }
    }
}