// Lab 6
// Student name:Tingwei Zhu
// Student number: 13860812

using System;
using System.Collections.Generic;

Console.WriteLine("CPEN223 Lab 6");

// Example graph from Section 1 of the handout. You may use it in your tests.
Dictionary<string, Dictionary<string, double>> scaffold = new()
{
    ["A"] = new() { ["B"] = 2.0, ["C"] = 5.0 },
    ["B"] = new() { ["A"] = 2.0, ["C"] = 1.0, ["D"] = 4.0 },
    ["C"] = new() { ["A"] = 5.0, ["B"] = 1.0, ["D"] = 1.5 },
    ["D"] = new() { ["B"] = 4.0, ["C"] = 1.5, ["E"] = 3.0 },
    ["E"] = new() { ["D"] = 3.0 },
    ["F"] = new()
};

//Testing: Write test cases that exercise ShortestDistancesFrom
//(and ShortestPath, if you attempt the optional challenge).
//Compare finite distances using a small tolerance, and check unreachable
//vertices with double.IsPositiveInfinity.
//TODO
Dictionary<string, double> distances =
    GraphLibrary.ShortestDistancesFrom(scaffold, "A");

double tolerance = 0.000001;

if (distances.Count == 6 &&
    Math.Abs(distances["A"] - 0.0) < tolerance &&
    Math.Abs(distances["B"] - 2.0) < tolerance &&
    Math.Abs(distances["C"] - 3.0) < tolerance &&
    Math.Abs(distances["D"] - 4.5) < tolerance &&
    Math.Abs(distances["E"] - 7.5) < tolerance &&
    double.IsPositiveInfinity(distances["F"]))
{
    Console.WriteLine("Test 1 passed");
}
else
{
    Console.WriteLine("Test 1 failed");
}

Dictionary<string, double> isolated =
    GraphLibrary.ShortestDistancesFrom(scaffold, "F");

if (isolated.Count == 6 &&
    Math.Abs(isolated["F"] - 0.0) < tolerance &&
    double.IsPositiveInfinity(isolated["A"]) &&
    double.IsPositiveInfinity(isolated["B"]) &&
    double.IsPositiveInfinity(isolated["C"]) &&
    double.IsPositiveInfinity(isolated["D"]) &&
    double.IsPositiveInfinity(isolated["E"]))
{
    Console.WriteLine("Test 2 passed");
}
else
{
    Console.WriteLine("Test 2 failed");
}

Dictionary<string, Dictionary<string, double>> zeroCostGraph = new()
{
    ["A"] = new() { ["B"] = 0.0, ["C"] = 5.0 },
    ["B"] = new() { ["A"] = 0.0, ["C"] = 2.0 },
    ["C"] = new() { ["A"] = 5.0, ["B"] = 2.0 }
};

Dictionary<string, double> zeroDistances =
    GraphLibrary.ShortestDistancesFrom(zeroCostGraph, "A");

if (zeroDistances.Count == 3 &&
    Math.Abs(zeroDistances["A"] - 0.0) < tolerance &&
    Math.Abs(zeroDistances["B"] - 0.0) < tolerance &&
    Math.Abs(zeroDistances["C"] - 2.0) < tolerance)
{
    Console.WriteLine("Test 3 passed");
}
else
{
    Console.WriteLine("Test 3 failed");
}

try
{
    GraphLibrary.ShortestDistancesFrom(scaffold, "Z");
    Console.WriteLine("Test 4 failed");
}
catch (ArgumentException)
{
    Console.WriteLine("Test 4 passed");
}
//
//
//end Testing code

//Do not change the program skeleton: keep the class name, method names,
//parameters, and return types exactly as given.
//Do not use LINQ or recursion, and do not use Console inside the GraphLibrary methods.
//You may add private helper methods.

public static class GraphLibrary
{
    /// <summary>
    /// Computes the minimum total edge cost from <paramref name="source"/> to every
    /// vertex of an undirected weighted graph, using the algorithm in Section 3
    /// of the handout (Dijkstra's algorithm).
    /// </summary>
    /// <param name="graph">
    /// The graph, represented as a nested mapping: each key is a vertex, and its
    /// associated dictionary maps each neighbour to the cost of the connecting edge.
    /// </param>
    /// <param name="source">
    /// The vertex from which distances are measured.
    /// </param>
    /// <returns>
    /// A new dictionary with exactly one entry for every vertex in <paramref name="graph"/>:
    /// <paramref name="source"/> maps to 0.0, each reachable vertex maps to its minimum
    /// total path cost, and each unreachable vertex maps to double.PositiveInfinity.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="source"/> is not a vertex in the graph.
    /// </exception>
    /// <remarks>
    /// Assumes that <paramref name="graph"/> is a valid undirected weighted graph:
    /// every neighbour appearing in an inner dictionary is also a key in the graph;
    /// if v lists w with cost c, then w lists v with the same cost c;
    /// every edge cost is finite and greater than or equal to 0;
    /// and there are no self-loops.
    /// The method does not modify the graph, including the inner dictionaries.
    ///
    /// Implementation requirements: use a <see cref="PriorityQueue{TElement, TPriority}"/>
    /// as the worklist and a <see cref="HashSet{T}"/> to track finalized vertices,
    /// as described in steps 4 and 5. Do not detect stale queue entries by comparing
    /// priorities to stored distances instead.
    /// </remarks>
    public static Dictionary<string, double> ShortestDistancesFrom(
        IReadOnlyDictionary<string, Dictionary<string, double>> graph,
        string source)
    {
        //TODO
        if (!graph.ContainsKey(source))
        {
            throw new ArgumentException("Source vertex not found.");
        }

        Dictionary<string, double> distances = new();

        foreach (string vertex in graph.Keys)
        {
            distances[vertex] = double.PositiveInfinity;
        }

        distances[source] = 0.0;

// Process vertices in order of their shortest known distance
        PriorityQueue<string, double> queue = new();
        queue.Enqueue(source, 0.0);

        HashSet<string> finalized = new();

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();

            if (finalized.Contains(current))
            {
                continue;
            }

            finalized.Add(current);
// Check whether going through the current vertex gives a shorter path
            foreach (KeyValuePair<string, double> edge in graph[current])
            {
                string neighbor = edge.Key;
                double edgeCost = edge.Value;

                double candidateDist = distances[current] + edgeCost;

// Update the distance and enqueue the improved path
                if (candidateDist < distances[neighbor])
                {
                    distances[neighbor] = candidateDist;
                    queue.Enqueue(neighbor, candidateDist);
                }
            }
        }

        return distances;
    }


    // ---------------------------------------------------------------------
    // Optional challenge (Section 7). Leave the NotImplementedException in
    // place if you do not attempt it.
    // ---------------------------------------------------------------------

    /// <summary>
    /// Finds one minimum-cost route from <paramref name="source"/> to
    /// <paramref name="target"/> in an undirected weighted graph.
    /// </summary>
    /// <param name="graph">
    /// The graph, represented as in <see cref="ShortestDistancesFrom"/>.
    /// </param>
    /// <param name="source">The vertex where the route starts.</param>
    /// <param name="target">The vertex where the route ends.</param>
    /// <returns>
    /// The vertices of one minimum-cost route, in order from <paramref name="source"/>
    /// to <paramref name="target"/>, including both endpoints. If several minimum-cost
    /// routes exist, any one is acceptable. If <paramref name="source"/> equals
    /// <paramref name="target"/>, the list contains only <paramref name="source"/>.
    /// If <paramref name="target"/> is unreachable, the list is empty.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="source"/> or <paramref name="target"/> is not a vertex
    /// in the graph.
    /// </exception>
    /// <remarks>
    /// Same assumptions about <paramref name="graph"/> as <see cref="ShortestDistancesFrom"/>.
    /// The method does not modify the graph.
    /// Hint: to avoid duplicating Dijkstra's algorithm, write a private helper that
    /// computes both the distance and predecessor mappings.
    /// </remarks>
    public static List<string> ShortestPath(
        IReadOnlyDictionary<string, Dictionary<string, double>> graph,
        string source,
        string target)
    {
        //TODO (optional)
        throw new NotImplementedException();
    }
}
