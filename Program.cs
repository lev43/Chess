using System;
using Chess;

class Game
{
    static void Main(string[] args)
    {
        var board = new ChessBoard();
        ChessColor turn = ChessColor.White;
        board.PlacePieces();
        Console.WriteLine("Game has started");
        VictoryStatus v = board.CheckVictory();
        Console.WriteLine($"Victory: {v.Victory}\nStatus: {v.Status}\nReason: {v.Reason}\nColor: {v.Color}");
    }
}