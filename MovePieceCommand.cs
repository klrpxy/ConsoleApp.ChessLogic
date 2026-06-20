namespace ConsoleAppChessLogic;

public sealed record MovePieceCommand(BoardPosition From, BoardPosition To);

public sealed record GameResult(
    MoveResult Result,
    IReadOnlyList<IGameEvent> Events) {
    public static GameResult WithoutEvents(MoveResult result) =>
        new(result, Array.Empty<IGameEvent>());
}
