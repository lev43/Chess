using System;
using Chess;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new ChessBoard();
            b.PlacePieces();
            b.PrintBoard(ChessColor.White);
            Console.WriteLine();
            b.PrintBoard(ChessColor.Black);
            // Console.WriteLine(Pawn.Simbol(ChessColor.White));
        }
    }
}
