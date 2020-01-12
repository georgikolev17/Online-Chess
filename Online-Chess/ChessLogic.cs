using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class ChessLogic
    {
        static List<Char> WhiteFigures = new List<char>()
        {
            'K',
            'Q',
            'B',
            'N',
            'R',
            'P'
        };
        static List<Char> BlackFigures = new List<char>()
        {
            'k',
            'q',
            'b',
            'n',
            'r',
            'p'
        };
        static int moves = 1;
        static int lastMoveStartRow = 0, lastMoveStartCol = 0;
        static int lastMoveFinalRow = 0, lastMoveFinalCol = 0;
        public char[,] Controller(char[,] chessBoard)
        {
            if (IsChess(chessBoard) && !IsTherePossibleMoveForWhite(chessBoard) && moves % 2 == 1)
            {
                Console.WriteLine("Black won by checkmate!");
                return chessBoard;
            }
            if (!IsChess(chessBoard) && !IsTherePossibleMoveForWhite(chessBoard) && moves % 2 == 1)
            {
                Console.WriteLine("Draw!");
                return chessBoard;
            }
            if (IsChess(chessBoard) && !IsTherePossibleMoveForBlack(chessBoard) && moves % 2 == 0)
            {
                Console.WriteLine("White won by checkmate!");
                return chessBoard;
            }
            if (!IsChess(chessBoard) && !IsTherePossibleMoveForBlack(chessBoard) && moves % 2 == 0)
            {
                Console.WriteLine("Draw!");
                return chessBoard;
            }
            if (IsChess(chessBoard))
            {
                Console.WriteLine("Check!");
            }
            string startPosition = Console.ReadLine();
            string finalPosition = Console.ReadLine();

            int startRow = int.Parse(startPosition[1].ToString()) - 1;
            int startCol = 7 - ((int)startPosition[0] - 97);
            int finalRow = int.Parse(finalPosition[1].ToString()) - 1;
            int finalCol = 7 - ((int)finalPosition[0] - 97);

            if (!AreCoordinatesCorect(chessBoard, startRow, startCol, finalRow, finalCol))
            {
                Console.WriteLine("You cannot move there!");
            }

            else if (CanMove(chessBoard, startRow, startCol, finalRow, finalCol))
            {
                CanPromote(chessBoard, startRow, startCol, finalRow, finalCol);
                chessBoard[finalRow, finalCol] = chessBoard[startRow, startCol];
                chessBoard[startRow, startCol] = ' ';
                moves++;
            }
            else
            {
                Console.WriteLine("You cannot move there!");
            }

            lastMoveStartRow = startRow;
            lastMoveStartCol = startCol;
            lastMoveFinalRow = finalRow;
            lastMoveFinalCol = finalCol;

            return chessBoard;
        }

        static bool IsTherePossibleMoveForWhite(char[,] chessBoard)
        {
            int countedFigures = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (WhiteFigures.Contains(chessBoard[i, j]))
                    {
                        countedFigures++;
                        for (int x = 0; x < 7; x++)
                        {
                            for (int k = 0; k < 7; k++)
                            {
                                if (x == i && k == j)
                                {
                                    continue;
                                }
                                if (CanMove(chessBoard, i, j, x, k))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    if (countedFigures == 16)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        static bool IsTherePossibleMoveForBlack(char[,] chessBoard)
        {
            int countedFigures = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (BlackFigures.Contains(chessBoard[i, j]))
                    {
                        countedFigures++;
                        for (int x = 0; x < 7; x++)
                        {
                            for (int k = 0; k < 7; k++)
                            {
                                if (x == i && k == j)
                                {
                                    continue;
                                }
                                if (CanMove(chessBoard, i, j, x, k))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    if (countedFigures == 16)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        static bool CanMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            char[,] boardAfterMove = new char[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardAfterMove[i, j] = chessBoard[i, j];
                }
            }
            boardAfterMove[finalRow, finalCol] = boardAfterMove[startRow, startCol];
            boardAfterMove[startRow, startCol] = ' ';
            if (IsChess(boardAfterMove))
            {
                return false;
            }

            if (moves % 2 == 1 && !WhiteFigures.Contains(chessBoard[startRow, startCol]))
            {
                return false;
            }
            if (moves % 2 == 0 && !BlackFigures.Contains(chessBoard[startRow, startCol]))
            {
                return false;
            }
            if (chessBoard[startRow, startCol] == 'P')
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

            return false;
        }

        static bool IsChess(char[,] chessBoard)
        {
            if (moves % 2 == 1)
            {
                int row = 0, col = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (chessBoard[i, j] == 'K')
                        {
                            row = i;
                            col = j;
                        }
                    }
                }

                #region ChessWithKnight
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 2, col + 1) && chessBoard[row + 2, col + 1] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 2, col - 1) && chessBoard[row + 2, col - 1] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 2, col + 1) && chessBoard[row - 2, col + 1] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 2, col - 1) && chessBoard[row - 2, col - 1] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 1, col + 2) && chessBoard[row + 1, col + 2] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 1, col - 2) && chessBoard[row + 1, col - 2] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 1, col + 2) && chessBoard[row - 1, col + 2] == 'n')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 1, col - 2) && chessBoard[row - 1, col - 2] == 'n')
                {
                    return true;
                }
                #endregion

                int currentRow = row + 1;
                int currentCol = col;
                while (currentRow <= 7 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'k' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'r')
                    {
                        return true;
                    }

                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }

                    currentRow++;
                }

                currentRow = row - 1;
                while (currentRow >= 0 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'k' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'r')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }
                    currentRow--;
                }

                currentRow = row;
                currentCol = col + 1;
                while (currentCol <= 7 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'k' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'r')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }
                    currentCol++;
                }

                currentCol = col - 1;
                while (currentCol >= 0 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'k' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'r')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }
                    currentCol--;
                }

                currentRow = row - 1;
                currentCol = col + 1;
                while (currentCol <= 7 && currentRow >= 0 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if (((chessBoard[currentRow, currentCol] == 'k') && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'b')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }
                    currentCol++;
                    currentRow--;
                }

                currentRow = row + 1;
                currentCol = col + 1;
                while (currentCol <= 7 && currentRow <= 7 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if (((chessBoard[currentRow, currentCol] == 'k' || chessBoard[currentRow, currentCol] == 'p') && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'b')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }
                    currentCol++;
                    currentRow++;
                }

                currentRow = row + 1;
                currentCol = col - 1;
                while (currentCol >= 0 && currentRow <= 7 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if (((chessBoard[currentRow, currentCol] == 'k' || chessBoard[currentRow, currentCol] == 'p') && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'b')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }

                    currentCol--;
                    currentRow++;
                }

                currentRow = row - 1;
                currentCol = col - 1;
                while (currentCol >= 0 && currentRow >= 0 && !WhiteFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'k' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'q' || chessBoard[currentRow, currentCol] == 'b')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'p' || chessBoard[currentRow, currentCol] == 'n' || chessBoard[currentRow, currentCol] == 'b' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'k'))
                    {
                        break;
                    }

                    currentCol--;
                    currentRow--;
                }
            }
            else if (moves % 2 == 0)
            {
                int row = 0, col = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (chessBoard[i, j] == 'k')
                        {
                            row = i;
                            col = j;
                        }
                    }
                }

                #region ChessWithKnight
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 2, col + 1) && chessBoard[row + 2, col + 1] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 2, col - 1) && chessBoard[row + 2, col - 1] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 2, col + 1) && chessBoard[row - 2, col + 1] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 2, col - 1) && chessBoard[row - 2, col - 1] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 1, col + 2) && chessBoard[row + 1, col + 2] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row + 1, col - 2) && chessBoard[row + 1, col - 2] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 1, col + 2) && chessBoard[row - 1, col + 2] == 'N')
                {
                    return true;
                }
                if (AreCoordinatesCorect(chessBoard, 0, 0, row - 1, col - 2) && chessBoard[row - 1, col - 2] == 'N')
                {
                    return true;
                }
                #endregion

                int currentRow = row + 1;
                int currentCol = col;

                while (currentRow <= 7 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'R')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentRow++;
                }

                currentRow = row - 1;
                while (currentRow >= 0 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'R')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentRow--;
                }

                currentRow = row;
                currentCol = col + 1;
                while (currentCol <= 7 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'R')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol++;
                }

                currentCol = col - 1;
                while (currentCol >= 0 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'R')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol--;
                }

                currentRow = row - 1;
                currentCol = col + 1;
                while (currentCol <= 7 && currentRow >= 0 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if (((chessBoard[currentRow, currentCol] == 'K' || chessBoard[currentRow, currentCol] == 'P') && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'B')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol++;
                    currentRow--;
                }

                currentRow = row + 1;
                currentCol = col + 1;
                while (currentCol <= 7 && currentRow <= 7 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'B')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol++;
                    currentRow++;
                }

                currentRow = row + 1;
                currentCol = col - 1;
                while (currentCol >= 0 && currentRow <= 7 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if ((chessBoard[currentRow, currentCol] == 'K' && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'B')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol--;
                    currentRow++;
                }

                currentRow = row - 1;
                currentCol = col - 1;
                while (currentCol >= 0 && currentRow >= 0 && !BlackFigures.Contains(chessBoard[currentRow, currentCol]))
                {
                    if (((chessBoard[currentRow, currentCol] == 'K' || chessBoard[currentRow, currentCol] == 'P') && Math.Abs(currentRow - row) == 1) || chessBoard[currentRow, currentCol] == 'Q' || chessBoard[currentRow, currentCol] == 'B')
                    {
                        return true;
                    }
                    if (chessBoard[currentRow, currentCol] == 'P' || chessBoard[currentRow, currentCol] == 'N' || chessBoard[currentRow, currentCol] == 'B' || (Math.Abs(currentCol - col) > 1 && chessBoard[currentRow, currentCol] == 'K'))
                    {
                        break;
                    }
                    currentCol--;
                    currentRow--;
                }
            }
            return false;
        }

        static bool CanWhitePawnMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            Console.WriteLine(chessBoard[finalRow, finalCol] == ' ');
            Console.WriteLine(chessBoard[finalRow - 1, finalCol] == 'p');
            Console.WriteLine(lastMoveStartCol == finalCol);
            Console.WriteLine(lastMoveFinalCol == finalCol);
            Console.WriteLine(lastMoveFinalRow == startRow);
            Console.WriteLine(lastMoveStartRow == startRow + 2);
            if ((finalCol == startCol && finalRow == startRow + 1 && (chessBoard[finalRow, finalCol] == ' '))
               || ((finalCol == startCol + 1 || finalCol == startCol - 1) && finalRow == startRow + 1 && chessBoard[finalRow, finalCol] != ' ')
               || (chessBoard[finalRow, finalCol] == ' ' && chessBoard[finalRow - 1, finalCol] == 'p' && lastMoveStartCol == finalCol && lastMoveFinalCol == finalCol && lastMoveFinalRow == startRow && lastMoveStartRow == startRow + 2)
               || (startRow == 1 && finalRow == startRow + 2 && finalCol == startCol && chessBoard[startRow + 1, finalCol] == ' ' && chessBoard[finalRow, finalCol] == ' '))
            {
                if (chessBoard[finalRow, finalCol] == ' ' && chessBoard[finalRow - 1, finalCol] == 'p' && lastMoveStartCol == finalCol && lastMoveFinalCol == finalCol && lastMoveFinalRow == startRow && lastMoveStartRow == startRow + 2)
                {
                    chessBoard[lastMoveFinalRow, lastMoveFinalCol] = ' ';
                    chessBoard[finalRow, finalCol] = 'P';
                }
                return true;
            }

            return false;
        }

        static bool CanBlackPawnMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if ((finalCol == startCol && finalRow == startRow - 1 && chessBoard[finalRow, finalCol] == ' ')
                || ((finalCol == startCol + 1 || finalCol == startCol - 1) && finalRow == startRow - 1 && chessBoard[finalRow, finalCol] != ' ') || (chessBoard[finalRow, finalCol] == ' ' && chessBoard[finalRow + 1, finalCol] == 'P' && lastMoveStartCol == finalCol && lastMoveFinalCol == finalCol && lastMoveFinalRow == startRow && lastMoveStartRow == startRow - 2)
                || (startRow == 6 && finalRow == startRow - 2 && finalCol == startCol && chessBoard[startRow - 1, finalCol] == ' ' && chessBoard[finalRow, finalCol] == ' '))
            {
                if (chessBoard[finalRow, finalCol] == ' ' && chessBoard[finalRow + 1, finalCol] == 'P' && lastMoveStartCol == finalCol && lastMoveFinalCol == finalCol && lastMoveFinalRow == startRow && lastMoveStartRow == startRow - 2)
                {
                    chessBoard[lastMoveFinalRow, lastMoveFinalCol] = ' ';
                    chessBoard[finalRow, finalCol] = 'p';
                }
                return true;
            }

            return false;
        }

        static bool AreCoordinatesCorect(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (startRow >= 0 && startRow <= 7 && startCol >= 0 && startCol <= 7 && finalRow >= 0 && finalRow <= 7 && finalCol >= 0 && finalCol <= 7 && chessBoard[startRow, startCol] != ' ')
            {
                return true;
            }
            return false;
        }

        static bool CanWhiteRookMove(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (!(startCol == finalCol || startRow == finalRow) || chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
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
            if (Math.Abs(startRow - finalRow) != Math.Abs(startCol - finalCol) || chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
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
            if (chessBoard[finalRow, finalCol] == 'P' || chessBoard[finalRow, finalCol] == 'R' || chessBoard[finalRow, finalCol] == 'N' || chessBoard[finalRow, finalCol] == 'B' || chessBoard[finalRow, finalCol] == 'Q' || chessBoard[finalRow, finalCol] == 'K' || chessBoard[finalRow, finalCol] == 'k')
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

            if ((startRow != finalRow || startCol != finalCol) && Math.Abs(finalCol - startCol) <= 1 && Math.Abs(finalRow - startRow) <= 1)
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

        static void CanPromote(char[,] chessBoard, int startRow, int startCol, int finalRow, int finalCol)
        {
            if (chessBoard[startRow, startCol] == 'P' && finalRow == 7)
            {
                Console.Write("Choose what you want to promote (Q-queen, N-knight, B-bishop, R-rook): ");
                string piece = Console.ReadLine();
                chessBoard[startRow, startCol] = char.Parse(piece.ToUpper());
                Console.WriteLine();
            }
            else if (chessBoard[startRow, startCol] == 'p' && finalRow == 0)
            {
                Console.Write("Choose what you want to promote (Q-queen, N-knight, B-bishop, R-rook): ");
                string piece = Console.ReadLine();
                chessBoard[startRow, startCol] = char.Parse(piece.ToLower());
                Console.WriteLine();
            }
        }
    }
}
