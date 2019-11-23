using System;

namespace Online_Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] chessBoard = new char[8, 8]
            {
                {'R' , 'K', 'B', 'K', 'Q', 'B', 'K', 'R'},
                {'P' , 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
                {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
                {'p' , 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                {'r' , 'k', 'b', 'k', 'q', 'b', 'k', 'r'}
            };

            while (true)
            {
                string startPosition = Console.ReadLine();
                string finalPosition = Console.ReadLine();

                int startRow = int.Parse(startPosition[1].ToString()) - 1;
                int startCol = 7 - ((int)startPosition[0] - 97);
                int finalRow = int.Parse(finalPosition[1].ToString()) - 1;
                int finalCol = 7 - ((int)finalPosition[0] - 97);

                chessBoard[finalRow, finalCol] = chessBoard[startRow, startCol];
                chessBoard[startRow, startCol] = ' ';

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
    }
}
