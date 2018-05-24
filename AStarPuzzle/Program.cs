using CsvHelper;
using Puzzle;
using System;
using System.IO;

namespace ASharpPuzzle
{
    class Program
    {

        static void Main(string[] args)
        {
            PuzzleStrategy mStrategy = new PuzzleStrategy();
            using (TextWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "\\length10data.csv"))
            {
                var csv = new CsvWriter(writer);
                var text = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\10.pl");

                csv.WriteField("Algorithm");
                csv.WriteField("Heuristic");
                csv.WriteField("Time taken to solve the problem(ms)");
                csv.WriteField("Initial state(-1 indicate 0)");
                csv.WriteField("Goal state");
                csv.WriteField("Optimal solution cost");
                csv.WriteField("Nodes generated from level 0 to optimal solution path length ");
                csv.WriteField("Nodes evaluated from level 0 to optimal solution path length ");
                csv.WriteField("Nodes expanded from level 0 to optimal solution path length ");
                csv.WriteField("Total nodes generated");
                csv.WriteField("Total nodes evaluated");
                csv.WriteField("Total nodes expanded");
                csv.NextRecord();

                foreach (Heuristic mHeuristic in Enum.GetValues(typeof(Heuristic)))
                {
                    foreach (var problem in text)
                    {
                        var bracketIndex = problem.IndexOf("(");
                        var problemType = int.Parse(problem.Substring(bracketIndex + 1, 2));
                        var spaceIndex = problem.IndexOf(" ");
                        var initialState = problem.Substring(spaceIndex + 1, 9);

                        var mInitialState = new int[9];
                        for (int i = 0; i < initialState.Length; i++)
                        {
                            var position = initialState.Substring(i, 1);
                            if (i == 0)
                            {
                                mInitialState[int.Parse(position)] = -1;
                            }
                            else
                            {
                                mInitialState[int.Parse(position)] = i;
                            }
                        }

                        var record = new { Algorithm = "A*", Heuristic = mHeuristic.ToString() };
                        csv.WriteRecord(record);
                        mStrategy.Solve(mInitialState, mHeuristic, csv, problemType);
                        Console.WriteLine("Heuristic: " + mHeuristic + " " + problem + " Finished");
                    }
                }
            }
        }
    }
}
