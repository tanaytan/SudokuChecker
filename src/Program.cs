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
            if(SudokuChecker(goodSudoku1))
            {
                Console.WriteLine("Good Sudoku");
            }
            else
            {
                Console.WriteLine("Bad Sudoku");
            }

            if(SudokuChecker(goodSudoku2))
            {
                Console.WriteLine("Good Sudoku");
            }
            else
            {
                Console.WriteLine("Bad Sudoku");
            }

            if (SudokuChecker(badSudoku1))
            {
                Console.WriteLine("Good Sudoku");
            }
            else
            {
                Console.WriteLine("Bad Sudoku");
            }

            if(SudokuChecker(badSudoku2))
            {
                Console.WriteLine("Good Sudoku");
            }
            else
            {
                Console.WriteLine("Bad Sudoku");
            }


            // The method
            bool SudokuChecker(int[][] grid)
             {
                // RULES FOR VALIDATION:
                if (grid.Length < 1)
                {
                    return false;
                }

                // Checks W x L is same
                foreach (var i in grid)
                {
                    if (i.Length != grid.Length)
                    {
                        return false;
                    }
                }

                //Checks N is a squarable number
                if ((Math.Pow(grid.Length, 0.5) != (int)(Math.Pow(grid.Length, 0.5))))
                {
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
                        return false;
                    }
                }

                
                bool BoxCheck(int[][] gridy)
                {
                    int boxSize = Convert.ToInt32(Math.Pow(gridy.Length, 0.5));
                    int sideSize = Convert.ToInt32(Math.Pow(gridy.Length, 2));

                    foreach (int i in Helper.Range(boxSize, sideSize + 1, boxSize))
                    {
                        foreach (int j in Helper.Range(boxSize, sideSize + 1, boxSize))
                        {
                            var box = new List<int>();
                            //foreach (int[] row in gridy[i - boxSize..i])
                            foreach (int[] row in gridy.Skip(i - boxSize).Take(i))
                            {
                                box.AddRange(row.Skip(j - boxSize).Take(j));                          
                            }
                            if (DupCheckerArray(box))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                BoxCheck(grid);
                
                return true;


             }
        }
    }
}
