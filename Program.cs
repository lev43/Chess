using System;
using Chess;

class Game
{
    static void Main(string[] args)
    {
        var board = new ChessBoard();
        ChessColor turn = ChessColor.White;
        // board.PlacePieces();
        board.Board[0, 0] = new King(ChessColor.Black);
        board.Board[0, 7] = new King(ChessColor.White);
        // board.Board[1, 7] = new Pawn(ChessColor.White);
        // board.Board[1, 6] = new Pawn(ChessColor.White);
        board.Board[0, 6] = new Pawn(ChessColor.White);
        board.Board[7, 7] = new Rook(ChessColor.Black);
        // board.Board[0, 1] = new Rook(ChessColor.Black);
        board.Board[7, 0] = new Bishop(ChessColor.Black);
        Console.WriteLine("Game has started");
        board.PrintBoard();
        VictoryStatus v = board.CheckVictory();
        Console.WriteLine($"Victory: {v.Victory}\nStatus: {v.Status}\nReason: {v.Reason}\nColor: {v.Color}");
    }
}