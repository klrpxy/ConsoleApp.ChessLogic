namespace ConsoleAppChessLogic;

public enum PieceColor {
    Red,
    Black
}

public enum PieceType {
    General,
    Advisor,
    Elephant,
    Horse,
    Chariot,
    Cannon,
    Soldier
}

public enum GameStatus {
    Playing,
    RedWon,
    BlackWon
}

public enum MoveResult {
    Success,
    InvalidInput,
    Check,
    RedWins,
    BlackWins,
    GameAlreadyEnded
}

public readonly record struct BoardPosition(int X, int Y) {
    public bool IsInsideBoard => X is >= 0 and < 9 && Y is >= 0 and < 10;
}

public readonly record struct ChessPiece(PieceColor Color, PieceType Type);