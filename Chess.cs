using System;
namespace Chess {
    enum ChessColor {
        White,
        Black
    }
    enum ChessSimbol {
        King   =  '♔',
        Queen  =  '♕',
        Rook   =  '♖',
        Bishop =  '♗',
        Horse  =  '♘',
        Pawn   =  '♙'
    }
    abstract class ChessPiece {
        public ChessColor Color; // 1 == White; 0 == Black;
        public char Simbol = ' ';
        public abstract bool move(int x1, int y1, int x2, int y2);
        public ChessPiece(ChessColor c = ChessColor.White) {Color = c;}
    }
    
    class King : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♔' : '♚'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) <= 1 && Math.Abs(y2 - y1) <= 1;
        }
        public King(ChessColor c) : base(c) {}
    }
    class Queen : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♕' : '♛'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2 || Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public Queen(ChessColor c) : base(c) {}
    }
    class Rook : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♖' : '♜'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2;
        }
        public Rook(ChessColor c) : base(c) {}
    }
    class Bishop : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♗' : '♝'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public Bishop(ChessColor c) : base(c) {}
    }
    class Horse : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♘' : '♞'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == 2 && Math.Abs(y2 - y1) == 1 || Math.Abs(x2 - x1) == 1 && Math.Abs(y2 - y1) == 2;
        }
        public Horse(ChessColor c) : base(c) {}
    }
    class Pawn : ChessPiece {
        public new char Simbol { get { return Color == ChessColor.White ? '♙' : '♟'; } }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return y1 + 1 == y2;
        }
        public Pawn(ChessColor c) : base(c) {}
    }
    
    class ChessBoard {
        public ChessPiece[,] Board = new ChessPiece[8, 8];
        public void PlacePieces() {
            for(int i = 0; i < 8; i++) {
                Board[i, 1] = new Pawn(ChessColor.Black);
                Board[i, 6] = new Pawn(ChessColor.White);
            }
        }
        public void PrintBoard() {
            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++)
                    if(Board[i, j] == null) Console.Write("🙾 ");
                    else Console.Write($"{Board[i, j].Simbol} ");
                Console.WriteLine();
            }
        }
    }
}
