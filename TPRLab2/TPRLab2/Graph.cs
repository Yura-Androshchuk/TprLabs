using System;
using System.Collections.Generic;
using System.Text;

namespace TPRLab2
{
    using System;
    using System.Collections.Generic;

    public class Graph
    {

        private readonly int V;
        private readonly List<List<int>> adj;

        public Graph(int V)
        {
            this.V = V;
            adj = new List<List<int>>(V);

            for (int i = 0; i < V; i++)
                adj.Add(new List<int>());
        }

        // This function is a variation of DFSUtil() in  
        // https://www.geeksforgeeks.org/archives/18212  
        private bool isCyclicUtil(int i, bool[] visited,
                                        bool[] recStack)
        {

            // Mark the current node as visited and  
            // part of recursion stack  
            if (recStack[i])
                return true;

            if (visited[i])
                return false;

            visited[i] = true;

            recStack[i] = true;
            List<int> children = adj[i];

            foreach (int c in children)
                if (isCyclicUtil(c, visited, recStack))
                    return true;

            recStack[i] = false;

            return false;
        }

        private void addEdge(int sou, int dest)
        {
            adj[sou].Add(dest);
        }

        // Returns true if the graph contains a  
        // cycle, else false.  
        // This function is a variation of DFS() in  
        // https://www.geeksforgeeks.org/archives/18212  
        private bool isCyclic()
        {

            // Mark all the vertices as not visited and  
            // not part of recursion stack  
            bool[] visited = new bool[V];
            bool[] recStack = new bool[V];


            // Call the recursive helper function to  
            // detect cycle in different DFS trees  
            for (int i = 0; i < V; i++)
                if (isCyclicUtil(i, visited, recStack))
                    return true;

            return false;
        }
        public static bool TakeMatrixAndCheck(int[,] arr)
        {
            Graph graph = new Graph(15);
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        graph.addEdge(i, j);
                    }
                }
            }
            if (graph.isCyclic())
                return false;
            else
            {
                return true;
            }

        }
    }

}
