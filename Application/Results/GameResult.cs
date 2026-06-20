using ConsoleAppChessLogic.Application.Events;

namespace ConsoleAppChessLogic.Application.Results;

public sealed record GameResult(
    MoveResult Result,
    IReadOnlyList<IGameEvent> Events) {
    public static GameResult WithoutEvents(MoveResult result) =>
        new(result, Array.Empty<IGameEvent>());
}
