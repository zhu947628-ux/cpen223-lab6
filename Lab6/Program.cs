// Lab 6
// Student name:
// Student number:

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
        throw new NotImplementedException();
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
