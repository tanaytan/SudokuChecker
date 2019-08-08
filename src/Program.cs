using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            int[][] badSudoku3 = {
                new int[] {7,8,4,  0,5,1,  3,2,6},
                new int[] {5,3,1,  6,7,2,  8,4,0},
                new int[] {6,0,2,  4,3,8,  7,5,1},

                new int[] {1,2,8,  7,0,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  0,1,2},
                new int[] {4,6,0,  1,2,3,  5,8,7},

                new int[] {8,7,6,  3,1,4,  2,0,5},
                new int[] {2,4,3,  5,6,0,  1,7,8},
                new int[] {0,1,5,  2,8,7,  6,3,4}
            };

            SudokuChecker(goodSudoku1);

            SudokuChecker(goodSudoku2);

            SudokuChecker(badSudoku1);

            SudokuChecker(badSudoku2);

            SudokuChecker(badSudoku3);


            // The method
            bool SudokuChecker(int[][] grid)
             {
                // RULES FOR VALIDATION:
                foreach(var row in grid)
                {
                    foreach(var num in row)
                    {
                        if (num == 0)
                        {
                            Console.WriteLine("Bad Sudoku");
                            return false;                            
                        }
                        
                    }
                }

                // Checks W x L is same
                foreach (var i in grid)
                {
                    if (i.Length != grid.Length)
                    {
                        Console.WriteLine("Bad Sudoku");
                        return false;
                    }
                }

                //Checks N is a squarable number
                var sqrLength = Math.Pow(grid.Length, 0.5);
                var roundedSqrLength = Math.Round(sqrLength);

                if (sqrLength != roundedSqrLength)
                {
                    Console.WriteLine("Bad Sudoku");
                    return false;
                }

                // FUNCTION TO CHECK FOR DUPLICATES: 
                bool DupCheckerArray(IEnumerable<int> arrayList)
                {
                    List<int> vals = new List<int>();
                    bool returnValue = false;
                    foreach (int s in arrayList)
                    {
                        if (vals.Contains(s))
                        {
                            returnValue = true;
                            break;
                        }
                        vals.Add(s);
                    }
                    return returnValue;
                }

                foreach (var row in grid)
                {
                    if (DupCheckerArray(row))
                    {
                        Console.WriteLine("Bad Sudoku");
                        return false;
                    }
                }

                foreach (var col in Helper.Range(grid.Length))                         
                {
                    int sideSize = grid.Length;
                    var column = new List<int>();
                    foreach (var row in Helper.Range(grid.Length))                     
                    {
                        int val = grid[row][col];
                        column.Add(val);
                    }

                    if (DupCheckerArray(column))
                    {
                        Console.WriteLine("Bad Sudoku");
                        return false;
                    }
                }

                
                bool BoxCheck(int[][] gridy)
                {
                    int boxSize = Convert.ToInt32(Math.Pow(gridy.Length, 0.5));
                    int sideSize = gridy.Length;

                    foreach (int i in Helper.Range(boxSize, sideSize + 1, boxSize))
                    {
                        foreach (int j in Helper.Range(boxSize, sideSize + 1, boxSize))
                        {
                            var box = new List<int>();
                            //foreach (int[] row in gridy[i - boxSize..i])
                            foreach (int[] row in gridy.Skip(i - boxSize).Take(boxSize))
                            {
                                box.AddRange(row.Skip(j - boxSize).Take(boxSize));                          
                            }
                            if (DupCheckerArray(box))
                            {
                                Console.WriteLine("Bad Sudoku");
                                return false;
                            }
                        }
                    }
                    return true;
                }

                BoxCheck(grid);

                Console.WriteLine("Good Sudoku");
                return true;


             }
        }
    }
}
