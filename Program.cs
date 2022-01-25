using System;
using Chess;

class Game
{
    static void Main(string[] args)
    {
        try {
            var board = new ChessBoard();
            var board_interface = new ChessBoardInterface(board);
            
            int[] p = ChessBoardInterface.ReadPosition();
            Console.WriteLine(ChessBoard.ConvertPosition(p));
            
            // // board.PlacePieces();
            // board.Board[0, 0] = new King(ChessColor.Black);
            // board.Board[0, 7] = new King(ChessColor.White);
            // board.Board[1, 7] = new Pawn(ChessColor.White);
            // board.Board[1, 6] = new Pawn(ChessColor.White);
            // board.Board[0, 6] = new Pawn(ChessColor.White);
            // // board.Board[7, 7] = new Rook(ChessColor.Black);
            // // board.Board[0, 1] = new Rook(ChessColor.Black);
            // board.Board[3, 6] = new Bishop(ChessColor.White);
            // board.Board[1, 5] = new Horse(ChessColor.Black);
            // Console.WriteLine("Game has started");
            // board_interface.PrintBoard();
            // board.Turn(ChessColor.Black, 1, 5, 0, 7);
            // board_interface.PrintBoard();
            // ChessBoardStatus v = board.GetStatus();
            // Console.WriteLine($"Victory: {v.Victory}\nStatus: {v.Status}\nReason: {v.Reason}\nColor: {v.Color}");
        } catch(Exception e) {
            Console.WriteLine(e.ToString());
        }
    }
}
