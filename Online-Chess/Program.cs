using System;

namespace Online_Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] chessBoard = new char[8, 8]
            {
                {'R' , 'N', 'B', 'K', 'Q', 'B', 'N', 'R'},
                {'P' , 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
                {'p' , 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                {'r' , 'n', 'b', 'k', 'q', 'b', 'n', 'r'}
            };

            while (true)
            {
                string startPosition = Console.ReadLine();
                string finalPosition = Console.ReadLine();

                int startRow = int.Parse(startPosition[1].ToString()) - 1;
                int startCol = 7 - ((int)startPosition[0] - 97);
                int finalRow = int.Parse(finalPosition[1].ToString()) - 1;
                int finalCol = 7 - ((int)finalPosition[0] - 97);

                if(!AreCoordinatesCorect(chessBoard, startRow, startCol, finalRow, finalCol))
                {
                    Console.WriteLine("You cannot move there!");
                }
                
                else if (!IsChess(chessBoard, startRow, startCol, finalRow, finalCol) && CanMove(chessBoard, startRow, startCol, finalRow, finalCol))
                {
                    chessBoard[finalRow, finalCol] = chessBoard[startRow, startCol];
                    chessBoard[startRow, startCol] = ' ';
                }
                else
                {
                    Console.WriteLine("You cannot move there!");
                }
                
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Console.Write(chessBoard[i, j]+" ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static bool CanMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if(chessBoard[startRow, startCol] == 'P')
            {
                return CanWhitePawnMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'p')
            {
                return CanBlackPawnMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'R')
            {
                return CanWhiteRookMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'r')
            {
                return CanBlackRookMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'B')
            {
                return CanWhiteBishopMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'b')
            {
                return CanBlackBishopMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'Q')
            {
                return CanWhiteQueenMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'q')
            {
                return CanBlackQueenMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'N')
            {
                return CanWhiteKnightMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'n')
            {
                return CanBlackKnightMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'K')
            {
                return CanWhiteKingMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }
            if (chessBoard[startRow, startCol] == 'k')
            {
                return CanBlackKingMove(chessBoard, startRow, startCol, finalRow, finalCol);
            }

            return true;
        }

        static bool IsChess(char[,] chessBoard, int startRow, int StartCol, int finalRow, int finalCol)
        {
            //TODO
            return false;
        }

        static bool CanWhitePawnMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if(((finalCol==startCol && finalRow == startRow+1) || ((finalCol == startCol+1 || finalCol == startCol-1) && finalRow == startRow+1 && chessBoard[finalRow, finalCol] != ' ')) ||
                (startRow == 1 && finalRow == startRow + 2 && finalCol == startCol && chessBoard[startRow + 1, finalCol] == ' '))
            {
                return true;
            }

            return false;
        }

        static bool CanBlackPawnMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (((finalCol == startCol && finalRow == startRow - 1) || ((finalCol == startCol + 1 || finalCol == startCol - 1) && finalRow == startRow - 1 && chessBoard[finalRow, finalCol] != ' ')) ||
                (startRow == 6 && finalRow == startRow - 2 && finalCol == startCol && chessBoard[startRow - 1, finalCol] == ' '))
            {
                return true;
            }

            return false;
        }

        static bool AreCoordinatesCorect(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if(startRow>=0 && startRow<=7 && startCol>=0 && startCol<=7 && finalRow>=0 && finalRow<=7 && finalCol>= 0 && finalCol <= 7 && chessBoard[startRow, startCol] != ' ')
            {
                return true;
            }
            return false;
        }

        static bool CanWhiteRookMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (!(startCol==finalCol || startRow == finalRow) || chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
            {
                return false;
            }
            else
            {
                int currentRow = startRow;
                int currentCol = startCol;
                while (currentRow != finalRow || currentCol != finalCol)
                {
                    if (chessBoard[currentRow, currentCol] != ' ' && (currentRow != startRow || currentCol != startCol))
                    {
                        return false;
                    }
                    if (currentCol > finalCol)
                    {
                        currentCol--;
                    }
                    else if(currentCol < finalCol)
                    {
                        currentCol++;
                    }
                    if (currentRow > finalRow)
                    {
                        currentRow--;
                    }
                    else if(currentRow < finalRow)
                    {
                        currentRow++;
                    }
                }
                return true;
            }
        }

        static bool CanBlackRookMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (!(startCol == finalCol || startRow == finalRow) || chessBoard[finalRow, finalCol] == 'p' || chessBoard[finalRow, finalCol] == 'r' || chessBoard[finalRow, finalCol] == 'n' || chessBoard[finalRow, finalCol] == 'b' || chessBoard[finalRow, finalCol] == 'q' || chessBoard[finalRow, finalCol] == 'k' || chessBoard[finalRow, finalCol] == 'K')
            {
                return false;
            }
            else
            {
                int currentRow = startRow;
                int currentCol = startCol;
                while (currentRow != finalRow || currentCol != finalCol)
                {
                    if (chessBoard[currentRow, currentCol] != ' ' && (currentRow != startRow || currentCol != startCol))
                    {
                        return false;
                    }
                    if (currentCol > finalCol)
                    {
                        currentCol--;
                    }
                    else if (currentCol < finalCol)
                    {
                        currentCol++;
                    }
                    if (currentRow > finalRow)
                    {
                        currentRow--;
                    }
                    else if (currentRow < finalRow)
                    {
                        currentRow++;
                    }
                }
                return true;
            }
        }

        static bool CanWhiteBishopMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if(Math.Abs(startRow - finalRow) != Math.Abs(startCol - finalCol) || chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
            {
                return false;
            }
            else
            {
                int currentRow = startRow;
                int currentCol = startCol;

                while (currentRow != finalRow && currentCol != finalCol)
                {
                    if(chessBoard[currentRow, currentCol] != ' ' && (currentRow != startRow || currentCol!=startCol))
                    {
                        Console.WriteLine(chessBoard[currentRow, currentCol]);
                        return false;
                    }
                    if (currentCol > finalCol)
                    {
                        currentCol--;
                    }
                    else
                    {
                        currentCol++;
                    }
                    if (currentRow > finalRow)
                    {
                        currentRow--;
                    }
                    else
                    {
                        currentRow++;
                    }
                }
                return true;
            }
        }

        static bool CanBlackBishopMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (Math.Abs(startRow - finalRow) != Math.Abs(startCol - finalCol) || chessBoard[finalRow, finalCol] == 'p' || chessBoard[finalRow, finalCol] == 'r' || chessBoard[finalRow, finalCol] == 'n' || chessBoard[finalRow, finalCol] == 'b' || chessBoard[finalRow, finalCol] == 'q' || chessBoard[finalRow, finalCol] == 'k' || chessBoard[finalRow, finalCol] == 'K')
            {
                return false;
            }
            else
            {
                int currentRow = startRow;
                int currentCol = startCol;

                while (currentRow != finalRow && currentCol != finalCol)
                {
                    if (chessBoard[currentRow, currentCol] != ' ' && (currentRow != startRow || currentCol != startCol))
                    {
                        Console.WriteLine(chessBoard[currentRow, currentCol]);
                        return false;
                    }
                    if (currentCol > finalCol)
                    {
                        currentCol--;
                    }
                    else
                    {
                        currentCol++;
                    }
                    if (currentRow > finalRow)
                    {
                        currentRow--;
                    }
                    else
                    {
                        currentRow++;
                    }
                }
                return true;
            }
        }

        static bool CanWhiteQueenMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            return (CanWhiteBishopMove(chessBoard, startRow, startCol, finalRow, finalCol) || CanWhiteRookMove(chessBoard, startRow, startCol, finalRow, finalCol));
        }

        static bool CanBlackQueenMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            return (CanBlackBishopMove(chessBoard, startRow, startCol, finalRow, finalCol) || CanBlackRookMove(chessBoard, startRow, startCol, finalRow, finalCol));
        }

        static bool CanWhiteKnightMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if(chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
            {
                return false;
            }

            if ((startRow + 2 == finalRow && startCol + 1 == finalCol) ||
                (startRow + 2 == finalRow && startCol - 1 == finalCol) ||
                (startRow + 1 == finalRow && startCol - 2 == finalCol) ||
                (startRow + 1 == finalRow && startCol + 2 == finalCol) ||
                (startRow - 2 == finalRow && startCol + 1 == finalCol) ||
                (startRow - 2 == finalRow && startCol - 1 == finalCol) ||
                (startRow - 1 == finalRow && startCol + 2 == finalCol) ||
                (startRow - 1 == finalRow && startCol - 2 == finalCol))
            {
                return true;
            }

            return false;
        }

        static bool CanBlackKnightMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (chessBoard[finalRow, finalCol] == 'p' || chessBoard[finalRow, finalCol] == 'r' || chessBoard[finalRow, finalCol] == 'n' || chessBoard[finalRow, finalCol] == 'b' || chessBoard[finalRow, finalCol] == 'q' || chessBoard[finalRow, finalCol] == 'k' || chessBoard[finalRow, finalCol] == 'K')
            {
                return false;
            }

            if ((startRow + 2 == finalRow && startCol + 1 == finalCol) ||
                (startRow + 2 == finalRow && startCol - 1 == finalCol) ||
                (startRow + 1 == finalRow && startCol - 2 == finalCol) ||
                (startRow + 1 == finalRow && startCol + 2 == finalCol) ||
                (startRow - 2 == finalRow && startCol + 1 == finalCol) ||
                (startRow - 2 == finalRow && startCol - 1 == finalCol) ||
                (startRow - 1 == finalRow && startCol + 2 == finalCol) ||
                (startRow - 1 == finalRow && startCol - 2 == finalCol))
            {
                return true;
            }

            return false;
        }

        static bool CanWhiteKingMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
            {
                return false;
            }

            if((startRow != finalRow || startCol != finalCol) && Math.Abs(finalCol-startCol) <= 1 && Math.Abs(finalRow - startRow)<= 1)
            {
                return true;
            }

            return false;
        }

        static bool CanBlackKingMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (chessBoard[finalRow, finalCol] == 'p' || chessBoard[finalRow, finalCol] == 'r' || chessBoard[finalRow, finalCol] == 'n' || chessBoard[finalRow, finalCol] == 'b' || chessBoard[finalRow, finalCol] == 'q' || chessBoard[finalRow, finalCol] == 'k' || chessBoard[finalRow, finalCol] == 'K')
            {
                return false;
            }

            if ((startRow != finalRow || startCol != finalCol) && Math.Abs(finalCol - startCol) <= 1 && Math.Abs(finalRow - startRow) <= 1)
            {
                return true;
            }

            return false;
        }
    }
}

/*
    TODO:
    - mate
    - pat
    - an-pasan
    -check
 */
 