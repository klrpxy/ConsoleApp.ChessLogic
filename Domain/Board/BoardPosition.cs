namespace ConsoleAppChessLogic.Domain.Board;

public readonly record struct BoardPosition(int X, int Y) {
    public bool IsInsideBoard => X is >= 0 and < 9 && Y is >= 0 and < 10;
}
