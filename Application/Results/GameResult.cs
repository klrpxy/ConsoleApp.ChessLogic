using ConsoleAppChessLogic.Application.Events;

namespace ConsoleAppChessLogic.Application.Results;

public sealed record GameResult(
    bool Success,
    IReadOnlyList<IGameEvent> Events) {
    public static GameResult Failed() => new(false, Array.Empty<IGameEvent>());
}
