using System;

namespace Chess {
    struct VictoryStatus {
        public VictoryStatus(int status) {
            Status = status;
            Victory = status > 0;
            Color = ChessColor.Null;
            switch (status) {
                case 0:  Reason = ""; 
                    break;
                case 1:  Reason = "Checkmate"; Color = ChessColor.White;
                    break;
                case 2:  Reason = "Checkmate"; Color = ChessColor.Black;
                    break;
                default: Reason = "Unknown"; break;
            }
        }
        public bool Victory { get; }
        public int Status { get; }
        public string Reason { get; }
        public ChessColor Color { get; }
    }
    enum ChessColor {
        Null,
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
        public abstract bool Move(int x1, int y1, int x2, int y2);
        public abstract bool Attack(int x1, int y1, int x2, int y2);
        public ChessPiece(ChessColor c = ChessColor.White) {Color = c;}
        public override String ToString() {
            return Convert.ToString(Simbol());
        }
    }
    
    class King : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♔' : '♚'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) <= 1 && Math.Abs(y2 - y1) <= 1;
        }
        public override bool Attack(int x1, int y1, int x2, int y2) { return Move(x1, y1, x2, y2); }
        public King(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Queen : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♕' : '♛'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2 || Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public override bool Attack(int x1, int y1, int x2, int y2) { return Move(x1, y1, x2, y2); }
        public Queen(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Rook : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♖' : '♜'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return y1 == y2 || x1 == x2;
        }
        public override bool Attack(int x1, int y1, int x2, int y2) { return Move(x1, y1, x2, y2); }
        public Rook(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Bishop : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♗' : '♝'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == Math.Abs(y2 - y1);
        }
        public override bool Attack(int x1, int y1, int x2, int y2) { return Move(x1, y1, x2, y2); }
        public Bishop(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Horse : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♘' : '♞'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) == 2 && Math.Abs(y2 - y1) == 1 || Math.Abs(x2 - x1) == 1 && Math.Abs(y2 - y1) == 2;
        }
        public override bool Attack(int x1, int y1, int x2, int y2) { return Move(x1, y1, x2, y2); }
        public Horse(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    class Pawn : ChessPiece {
        public new static char Simbol(ChessColor c = ChessColor.White) { return c == ChessColor.Black ? '♙' : '♟'; }
        public override bool Move(int x1, int y1, int x2, int y2)
        {
            return Color == ChessColor.White ? (y1 + 1 == y2) || (y1 == 6 ? y1 + 2 == y2 : false) : (y1 - 1 == y2) || (y1 == 1 ? y1 - 2 == y2 : false) ;
        }
        public override bool Attack(int x1, int y1, int x2, int y2) {
            return Move(x1, y1, x2, y2) && (x1 + 1 == x2 || x1 - 1 == x2);
        }
        public Pawn(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    
    class ChessBoard {
        public ChessPiece[,] Board = new ChessPiece[8, 8];
        private void CheckPosition(int x, int y) {
            Console.WriteLine("CheckPosition{" + $"{ConvertPosition(x, y)}" + "}" + $": ({Board[x, y]?.ToString() ?? " "}) [{Board[x, y]?.Color.ToString() ?? "     "}]");
        }

        public void CheckPosition(int x1, int y1, int x2, int y2) {
            Console.WriteLine("CheckPosition{" + $"{ConvertPosition(x1, y1)}" + "}" + $": ({Board[x1, y1]?.ToString() ?? " "}) [{Board[x1, y1]?.Color.ToString() ?? "     "}] Attack to <{ConvertPosition(x2, y2)}> is {Board[x1, y1]?.Attack(x1, y1, x2, y2).ToString() ?? "Unknow"}");
        }
        public bool CheckAttackTo(int px, int py, ChessColor c) {
            bool w = false;
            for(int x = px+1; x < 8; x++) {
                // CheckPosition(x, py, px, py);
                if(Board[x, py] != null) {
                    if(Board[x, py].Color != c)
                        w = w || Board[x, py].Attack(x, py, px, py);
                    break;
                }
            }
            for(int x = px-1; x >= 0; x--) {
                // CheckPosition(x, py, px, py);
                if(Board[x, py] != null) {
                    if(Board[x, py].Color != c)
                        w = w || Board[x, py].Attack(x, py, px, py);
                    break;
                }
            }

            for(int y = py+1; y < 8; y++) {
                // CheckPosition(px, y, px, py);
                if(Board[px, y] != null) {
                    if(Board[px, y].Color != c)
                        w = w || Board[px, y].Attack(px, y, px, py);
                    break;
                }
            }
            for(int y = py-1; y >= 0; y--) {
                // CheckPosition(px, y, px, py);
                if(Board[px, y] != null) {
                    if(Board[px, y].Color != c)
                        w = w || Board[px, y].Attack(px, y, px, py);
                    break;
                }
            }

            for(int x = px+1, y = py+1; x < 8 && y < 8; x++, y++) {
                // CheckPosition(x, y, px, py);
                if(Board[x, y] != null) {
                    if(Board[x, y].Color != c)
                        w = w || Board[x, y].Attack(x, y, px, py);
                    break;
                }
            }
            for(int x = px-1, y = py-1; x >= 0 && y >= 0; x--, y--) {
                // CheckPosition(x, y, px, py);
                if(Board[x, y] != null) {
                    if(Board[x, y].Color != c)
                        w = w || Board[x, y].Attack(x, y, px, py);
                    break;
                }
            }
            for(int x = px+1, y = py-1; x < 8 && y >= 0; x++, y--) {
                // CheckPosition(x, y, px, py);
                if(Board[x, y] != null) {
                    if(Board[x, y].Color != c)
                        w = w || Board[x, y].Attack(x, y, px, py);
                    break;
                }
            }
            for(int x = px-1, y = py+1; x >= 0 && y < 8; x--, y++) {
                // CheckPosition(x, y, px, py);
                if(Board[x, y] != null) {
                    if(Board[x, y].Color != c)
                        w = w || Board[x, y].Attack(x, y, px, py);
                    break;
                }
            }
            return w;
        }
        public VictoryStatus CheckVictory() {
            int[] WhiteKing = null, BlackKing = null;
            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    if(Board[i, j] != null && Board[i, j].GetType().Name == "King") {
                        if(Board[i, j].Color == ChessColor.White) 
                            WhiteKing = new int[2] {i, j};
                        else
                            BlackKing = new int[2] {i, j};
                    }
                }
            }
            if(WhiteKing == null) return new VictoryStatus(2);
            if(BlackKing == null) return new VictoryStatus(1);

            bool w = CheckAttackTo(WhiteKing[0], WhiteKing[1], ChessColor.White);

            Console.WriteLine(w);
            for(int x = WhiteKing[0]-1; x <= WhiteKing[0]+1 && w == true; x++)
                for(int y = WhiteKing[1]-1; y <= WhiteKing[1]+1 && w == true; y++)
                    if( x >= 0 && x < 8 && y >= 0 && y < 8 
                        && !(x == WhiteKing[0] && y == WhiteKing[1])
                        && (Board[x, y] == null || Board[x, y].Color == ChessColor.Black)
                        && !CheckAttackTo(x, y, ChessColor.White)
                    ) w = false;

            Console.WriteLine(w);
            if(w == true) return new VictoryStatus(2);

            bool b = CheckAttackTo(BlackKing[0], BlackKing[1], ChessColor.Black);

            Console.WriteLine(b);
            for(int x = BlackKing[0]-1; x <= BlackKing[0]+1 && w == true; x++)
                for(int y = BlackKing[1]-1; y <= BlackKing[1]+1 && w == true; y++)
                    if( x >= 0 && x < 8 && y >= 0 && y < 8 
                        && !(x == BlackKing[0] && y == BlackKing[1])
                        && (Board[x, y] == null || Board[x, y].Color == ChessColor.White)
                        && !CheckAttackTo(x, y, ChessColor.Black)
                    ) b = false;

            Console.WriteLine(b);
            if(b == true) return new VictoryStatus(1);

            return new VictoryStatus(0);
        }
        public static int[] ConvertPosition(char x, char y) {
            var res = new int[2] {x - '0' - 1, y - 'a'};
            if(res[0] < 0 || res[0] > 7) throw new ArgumentOutOfRangeException("Position X", x, "Going beyond the board");
            if(res[1] < 0 || res[1] > 7) throw new ArgumentOutOfRangeException("Position Y", y, "Going beyond the board");
            return res;
        }
        public static int[] ConvertPosition(string str) {
            return ConvertPosition(str[0], str[1]);
        }
        public static string ConvertPosition(int x, int y) {
            if(x < 0 || x > 7) throw new ArgumentOutOfRangeException("Position X", x, "Going beyond the board");
            if(y < 0 || y > 7) throw new ArgumentOutOfRangeException("Position Y", y, "Going beyond the board");
            return $"{(char)('0'+x+1)}{(char)('a'+y)}";
        }
        public static string ConvertPosition(int[] p) {
            return ConvertPosition(p[0], p[1]);
        }
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
