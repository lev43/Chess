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
        public static char Simbol(ChessColor c = ChessColor.White) { return ' '; }
        public abstract bool move(int x1, int y1, int x2, int y2);
        public ChessPiece(ChessColor c = ChessColor.White) {Color = c;}
        public override String ToString() {
            return Convert.ToString(Simbol());
        }
    }
    
    class King : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♔' : '♚'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) <= 1 && Math.Abs(y2 - y1) <= 1;
        }
        public King(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Queen : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♕' : '♛'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2 || Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public Queen(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Rook : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♖' : '♜'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2;
        }
        public Rook(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Bishop : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♗' : '♝'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public Bishop(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Horse : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♘' : '♞'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == 2 && Math.Abs(y2 - y1) == 1 || Math.Abs(x2 - x1) == 1 && Math.Abs(y2 - y1) == 2;
        }
        public Horse(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Pawn : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♙' : '♟'; }
        public override bool move(int x1, int y1, int x2, int y2)
        {
            return Color == ChessColor.White ? (y1 + 1 == y2) : (y1 - 1 == y2);
        }
        public Pawn(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    
    class ChessBoard {
        public ChessPiece[,] Board = new ChessPiece[8, 8];
        public void PlacePieces() {
            // Place pawns
            for(int i = 0; i < 8; i++) {
                Board[1, i] = new Pawn(ChessColor.Black);
                Board[6, i] = new Pawn(ChessColor.White);
            }
            // Place rooks
            Board[7, 0] = new Rook(ChessColor.White); Board[7, 7] = new Rook(ChessColor.White);
            Board[0, 0] = new Rook(ChessColor.Black); Board[0, 7] = new Rook(ChessColor.Black);
            // Place horses
            Board[7, 1] = new Horse(ChessColor.White); Board[7, 6] = new Horse(ChessColor.White);
            Board[0, 1] = new Horse(ChessColor.Black); Board[0, 6] = new Horse(ChessColor.Black);
            // Place bishops
            Board[7, 2] = new Bishop(ChessColor.White); Board[7, 5] = new Bishop(ChessColor.White);
            Board[0, 2] = new Bishop(ChessColor.Black); Board[0, 5] = new Bishop(ChessColor.Black);
            // Place king and queen
            Board[7, 3] = new King(ChessColor.White); Board[7, 4] = new Queen(ChessColor.White);
            Board[0, 4] = new King(ChessColor.Black); Board[0, 3] = new Queen(ChessColor.Black);
        }
        public void PrintBoard(ChessColor color = ChessColor.White) {
            if(color == ChessColor.White) {
                Console.WriteLine("   a b c d e f g h");
                for(int i = 0; i < 8; i++) {
                    Console.Write($"{i+1}: ");
                    for(int j = 0; j < 8; j++)
                        if(Board[i, j] == null) Console.Write( (i + j) % 2 == 0 ? "+ " : "# ");
                        else Console.Write($"{Board[i, j]} ");
                    Console.WriteLine();
                }
            } else {
                Console.WriteLine("   a b c d e f g h");
                for(int i = 7; i >= 0; i--) {
                    Console.Write($"{i+1}: ");
                    for(int j = 7; j >= 0; j--)
                        if(Board[i, j] == null) Console.Write( (i + j) % 2 == 0 ? "+ " : "# ");
                        else Console.Write($"{Board[i, j]} ");
                    Console.WriteLine();
                }
            }
        }
    }
}
