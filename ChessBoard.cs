namespace ConsoleAppChessLogic;

public sealed class ChessBoard {
    private readonly ChessPiece?[,] pieces = new ChessPiece?[9, 10];

    public ChessPiece? this[BoardPosition position] {
        get => pieces[position.X, position.Y];
        private set => pieces[position.X, position.Y] = value;
    }

    public static ChessBoard CreateInitial() {
        var board = new ChessBoard();

        board.PlaceBackRank(PieceColor.Black, 0);
        board.Place(PieceColor.Black, PieceType.Cannon, 1, 2);
        board.Place(PieceColor.Black, PieceType.Cannon, 7, 2);
        board.PlaceSoldiers(PieceColor.Black, 3);

        board.PlaceBackRank(PieceColor.Red, 9);
        board.Place(PieceColor.Red, PieceType.Cannon, 1, 7);
        board.Place(PieceColor.Red, PieceType.Cannon, 7, 7);
        board.PlaceSoldiers(PieceColor.Red, 6);

        return board;
    }

    public static ChessBoard Create(
        params (BoardPosition Position, ChessPiece Piece)[] pieces) {
        var board = new ChessBoard();

        foreach (var (position, piece) in pieces) {
            if (!position.IsInsideBoard) {
                throw new ArgumentOutOfRangeException(nameof(pieces));
            }

            board[position] = piece;
        }

        return board;
    }

    public ChessBoard Clone() {
        var clone = new ChessBoard();
        Array.Copy(pieces, clone.pieces, pieces.Length);
        return clone;
    }

    public void Move(BoardPosition from, BoardPosition to) {
        this[to] = this[from];
        this[from] = null;
    }

    public BoardPosition? FindGeneral(PieceColor color) {
        foreach (var (position, piece) in GetPieces(color)) {
            if (piece.Type == PieceType.General) {
                return position;
            }
        }

        return null;
    }

    public IEnumerable<(BoardPosition Position, ChessPiece Piece)> GetPieces(PieceColor color) {
        for (var x = 0; x < 9; x++) {
            for (var y = 0; y < 10; y++) {
                var piece = pieces[x, y];
                if (piece is { } value && value.Color == color) {
                    yield return (new BoardPosition(x, y), value);
                }
            }
        }
    }

    public IEnumerable<(BoardPosition Position, ChessPiece Piece)> GetAllPieces() {
        foreach (var color in Enum.GetValues<PieceColor>()) {
            foreach (var piece in GetPieces(color)) {
                yield return piece;
            }
        }
    }

    public int CountPiecesBetween(BoardPosition from, BoardPosition to) {
        var stepX = Math.Sign(to.X - from.X);
        var stepY = Math.Sign(to.Y - from.Y);
        var x = from.X + stepX;
        var y = from.Y + stepY;
        var count = 0;

        while (x != to.X || y != to.Y) {
            if (pieces[x, y] is not null) {
                count++;
            }

            x += stepX;
            y += stepY;
        }

        return count;
    }

    private void PlaceBackRank(PieceColor color, int y) {
        var types = new[] {
            PieceType.Chariot,
            PieceType.Horse,
            PieceType.Elephant,
            PieceType.Advisor,
            PieceType.General,
            PieceType.Advisor,
            PieceType.Elephant,
            PieceType.Horse,
            PieceType.Chariot
        };

        for (var x = 0; x < types.Length; x++) {
            Place(color, types[x], x, y);
        }
    }

    private void PlaceSoldiers(PieceColor color, int y) {
        for (var x = 0; x < 9; x += 2) {
            Place(color, PieceType.Soldier, x, y);
        }
    }

    private void Place(PieceColor color, PieceType type, int x, int y) {
        pieces[x, y] = new ChessPiece(color, type);
    }
}
