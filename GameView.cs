namespace ConsoleAppChessLogic;

public interface IGameView {
    ValueTask<ViewInputResult> ReadInputAsync(
        CancellationToken cancellationToken = default);

    void ShowInitial(GameSnapshot snapshot);

    void ShowInvalidInput();

    void ShowResult(GameResult result, GameSnapshot snapshot);
}

public enum ViewInputKind {
    Move,
    Quit,
    Invalid
}

public sealed record ViewInputResult(
    ViewInputKind Kind,
    MoveChessIntent? Intent) {
    public static ViewInputResult Move(MoveChessIntent intent) =>
        new(ViewInputKind.Move, intent);

    public static ViewInputResult Quit() =>
        new(ViewInputKind.Quit, null);

    public static ViewInputResult Invalid() =>
        new(ViewInputKind.Invalid, null);
}
