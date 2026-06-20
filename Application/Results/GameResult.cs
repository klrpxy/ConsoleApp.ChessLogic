using ConsoleApp.ChessLogic.Application.Events;

namespace ConsoleApp.ChessLogic.Application.Results;

public sealed record GameResult(
    bool Success,
    IReadOnlyList<IGameEvent> Events) {
    public static GameResult Failed() => new(false, Array.Empty<IGameEvent>());
}
