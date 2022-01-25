using System;

namespace Chess {
    struct ChessBoardStatus {
        public ChessBoardStatus(int status) {
            Status = status;
            Victory = status > 2;
            Color = ChessColor.Null;
            switch (status) {
                case 0:  Reason = ""; 
                    break;
                case 1:  Reason = "Check"; Color = ChessColor.White;
                    break;
                case 2:  Reason = "Check"; Color = ChessColor.Black;
                    break;
                case 3:  Reason = "Checkmate"; Color = ChessColor.White;
                    break;
                case 4:  Reason = "Checkmate"; Color = ChessColor.Black;
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
            return Move(x1+1, y1, x2, y2) || Move(x1-1, y1, x2, y2);
        }
        public Pawn(ChessColor c) : base(c) {}
        public override String ToString() {
            return Convert.ToString(Simbol(Color));
        }
    }
    
    class ChessBoard {
        public ChessPiece[,] Board = new ChessPiece[8, 8];
        public void CheckPosition(int x, int y) {
            Console.WriteLine("CheckPosition{" + $"{ConvertPosition(x, y)}" + "}" + $": ({Board[x, y]?.ToString() ?? " "}) [{Board[x, y]?.Color.ToString() ?? "     "}]");
        }

        public void CheckPosition(int x1, int y1, int x2, int y2) {
            Console.WriteLine("CheckPosition{" + $"{ConvertPosition(x1, y1)}" + "}" + $": ({Board[x1, y1]?.ToString() ?? " "}) [{Board[x1, y1]?.Color.ToString() ?? "     "}] Attack to <{ConvertPosition(x2, y2)}> is {Board[x1, y1]?.Attack(x1, y1, x2, y2).ToString() ?? "Unknow"}");
        }
        public bool CheckAttackTo(int px, int py, ChessColor c) {
            if(px < 0 || px > 7) throw new ArgumentOutOfRangeException("Position X", px, "Going beyond the board");
            if(py < 0 || py > 7) throw new ArgumentOutOfRangeException("Position Y", py, "Going beyond the board");
            
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
            
            if(px-2 >= 0 && py-1 >= 0 && Board[px-2, py-1] != null && Board[px-2, py-1].Color != c) w = w || Board[px-2, py-1].Attack(px-2, py-1, px, py);
            if(px-1 >= 0 && py-2 >= 0 && Board[px-1, py-2] != null && Board[px-1, py-2].Color != c) w = w || Board[px-1, py-2].Attack(px-1, py-2, px, py);
            if(px+1 <= 7 && py-2 >= 0 && Board[px+1, py-2] != null && Board[px+1, py-2].Color != c) w = w || Board[px+1, py-2].Attack(px+1, py-2, px, py);
            if(px+2 <= 7 && py-1 >= 0 && Board[px+2, py-1] != null && Board[px+2, py-1].Color != c) w = w || Board[px+2, py-1].Attack(px+2, py-1, px, py);
            if(px+2 <= 7 && py+1 <= 7 && Board[px+2, py+1] != null && Board[px+2, py+1].Color != c) w = w || Board[px+2, py+1].Attack(px+2, py+1, px, py);
            if(px+1 <= 7 && py+2 <= 7 && Board[px+1, py+2] != null && Board[px+1, py+2].Color != c) w = w || Board[px+1, py+2].Attack(px+1, py+2, px, py);
            if(px-1 >= 0 && py+2 <= 7 && Board[px-1, py+2] != null && Board[px-1, py+2].Color != c) w = w || Board[px-1, py+2].Attack(px-1, py+2, px, py);
            if(px-2 >= 0 && py+1 <= 7 && Board[px-2, py+1] != null && Board[px-2, py+1].Color != c) w = w || Board[px-2, py+1].Attack(px-2, py+1, px, py);
            
            return w;
        }
        
        public ChessBoardStatus GetStatus() {
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
            if(WhiteKing == null) return new ChessBoardStatus(4);
            if(BlackKing == null) return new ChessBoardStatus(3);

            bool w = CheckAttackTo(WhiteKing[0], WhiteKing[1], ChessColor.White);

            // Console.WriteLine(w);
            for(int x = WhiteKing[0]-1; x <= WhiteKing[0]+1 && w == true; x++)
                for(int y = WhiteKing[1]-1; y <= WhiteKing[1]+1 && w == true; y++)
                    if( x >= 0 && x < 8 && y >= 0 && y < 8 
                        && !(x == WhiteKing[0] && y == WhiteKing[1])
                        && (Board[x, y] == null || Board[x, y].Color == ChessColor.Black)
                        && !CheckAttackTo(x, y, ChessColor.White)
                    ) w = false;

            // Console.WriteLine(w);
            if(w == true) return new ChessBoardStatus(2);

            bool b = CheckAttackTo(BlackKing[0], BlackKing[1], ChessColor.Black);

            // Console.WriteLine(b);
            for(int x = BlackKing[0]-1; x <= BlackKing[0]+1 && w == true; x++)
                for(int y = BlackKing[1]-1; y <= BlackKing[1]+1 && w == true; y++)
                    if( x >= 0 && x < 8 && y >= 0 && y < 8 
                        && !(x == BlackKing[0] && y == BlackKing[1])
                        && (Board[x, y] == null || Board[x, y].Color == ChessColor.White)
                        && !CheckAttackTo(x, y, ChessColor.Black)
                    ) b = false;

            // Console.WriteLine(b);
            if(b == true) return new ChessBoardStatus(1);

            return new ChessBoardStatus(0);
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
        public void Turn(ChessColor c, int x1, int y1, int x2, int y2) {
            if(x1 < 0 || x1 > 7) throw new ArgumentOutOfRangeException("Position X1", x1, "Going beyond the board");
            if(y1 < 0 || y1 > 7) throw new ArgumentOutOfRangeException("Position Y1", y1, "Going beyond the board");
            if(x2 < 0 || x2 > 7) throw new ArgumentOutOfRangeException("Position X2", x2, "Going beyond the board");
            if(y2 < 0 || y2 > 7) throw new ArgumentOutOfRangeException("Position Y2", y2, "Going beyond the board");
            
            if(Board[x1, y1] == null) throw new ArgumentException("Tile is null");
            if(Board[x1, y1].Color != c) throw new ArgumentException("A chess piece of a different color");
            if(Board[x2, y2] == null) { 
                if(!Board[x1, y1].Move(x1, y1, x2, y2)) throw new ArgumentException("A chess piece cannot move to this position");
            } else if(!Board[x1, y1].Attack(x1, y1, x2, y2)) throw new ArgumentException("A chess piece cannot attack this position");
            else if(Board[x2, y2].Color == c) throw new ArgumentException("Cannot attack a chess piece of the same color");
            
            Board[x2, y2] = Board[x1, y1];
            Board[x1, y1] = null;
        }
    }
    class ChessBoardInterface {
        private ChessBoard B;
        public ChessBoardInterface(ChessBoard _B) {
            if(_B == null) throw new ArgumentNullException();
            B = _B;
        }
        public void PrintBoard(ChessColor color = ChessColor.White) {
            ChessBoardInterface.PrintBoard(B, color);
        }
        public static void PrintBoard(ChessBoard B, ChessColor color = ChessColor.White) {
            if(color == ChessColor.White) {
                Console.WriteLine("   a b c d e f g h");
                for(int i = 0; i < 8; i++) {
                    Console.Write($"{i+1}: ");
                    for(int j = 0; j < 8; j++)
                        if(B.Board[i, j] == null) Console.Write( (i + j) % 2 == 0 ? "+ " : "# ");
                        else Console.Write($"{B.Board[i, j]} ");
                    Console.WriteLine();
                }
            } else {
                Console.WriteLine("   a b c d e f g h");
                for(int i = 7; i >= 0; i--) {
                    Console.Write($"{i+1}: ");
                    for(int j = 7; j >= 0; j--)
                        if(B.Board[i, j] == null) Console.Write( (i + j) % 2 == 0 ? "+ " : "# ");
                        else Console.Write($"{B.Board[i, j]} ");
                    Console.WriteLine();
                }
            }
        }
        public static int[] ReadPosition(string text = "") {
            int x, y;
            while(true) {
                Console.Write($"Enter position{text}: ");
                x = -2; y = -2;
                while(x == -2) x = Convert.ToInt32(Console.Read()) - '0' - 1;
                while(y == -2) y = Convert.ToInt32(Console.Read()) - 'a';
                Console.WriteLine();
                if(x >= 0 && x <= 7 && y >= 0 && y <= 7)break;
                else Console.WriteLine("Incorrect position");
            }
            return new int[2] {x, y};
        }
    }
    class ChessGame {
        
    }
}
