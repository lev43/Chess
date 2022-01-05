using System;
using Chess;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new ChessBoard();
            // b.PlacePieces();
            // b.PrintBoard(ChessColor.White);
            // Console.WriteLine();
            // b.PrintBoard(ChessColor.Black);
            char a = Console.ReadKey().KeyChar, b = Console.ReadKey().KeyChar;
            Console.WriteLine($"\nChar1: {a}\nChar2: {b}");
            // Console.WriteLine($"{ChessBoard.ConvertPosition(a, b)[0]}-{ChessBoard.ConvertPosition(a, b)[1]}");
            // Console.WriteLine($"{ChessBoard.ConvertPosition(ChessBoard.ConvertPosition(a, b))}");
        }
    }
}
