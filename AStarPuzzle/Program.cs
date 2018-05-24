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
            //Heuristic mHeuristic;
            PuzzleStrategy mStrategy = new PuzzleStrategy();
            //var t = new FileInfo(Directory.GetCurrentDirectory() + "\\length25data.csv");
            //var textWriter = t.CreateText();
            //var csv = new CsvWriter(textWriter);
            using (TextWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "\\length25data.csv"))
            {
                var csv = new CsvWriter(writer);
                //var worksheet = workbook.Worksheets.Add("Sheet1");
                var text = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\25.pl");

                csv.WriteField("Algorithm");
                csv.WriteField("Heuristic");
                csv.WriteField("ProblemType");
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
                //csv.WriteRecord(spaceArray);
                //var recordHeadingEvalLevel = new { }
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
                            //var blah = initialState.Substring(1, 2);
                            var position = initialState.Substring(i, 1);
                            if (i == 0)
                            {
                                mInitialState[int.Parse(position)] = -1;
                            }
                            else
                            {
                                mInitialState[int.Parse(position)] = i;
                            }
                            //int blah = initialState[1];
                            //var blah1 = blah - 1;

                        }
                        //Console.WriteLine(mInitialState);
                        var spaceArray = new string[problemType];
                        var recordHeading = new[]
                        {

                                "Algorithm",
                                "Heuristic",
                                "ProblemType",
                                "CPU Time",
                                "Initial state(-1 indicate 0)",
                                "Goal state",
                                "Optimal solution cost",
                                "Nodes generated from level 0 to level of optimal cost"


                        };

                        //spaceArray,
                        //    new[] { "Nodes evaluated from level 0 to level of optimal cost" },
                        //    spaceArray,
                        //    new[] { "Nodes expanded from level 0 to level of optimal cost" }

                        var record = new { Algorithm = "A*", Heuristic = mHeuristic.ToString(), Problem = problemType };
                        csv.WriteRecord(record);
                        mStrategy.Solve(mInitialState, mHeuristic, csv, problemType);

                    }
                }
            }
            //var mInitialState = new int[] { 5, 4, 2, 7, 1, 3, -1, 8, 6 };


            //mStrategy.Solve(mInitialState, mHeuristic);


        }
    }
}
