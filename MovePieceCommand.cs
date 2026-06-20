using VitalRouter;

namespace ConsoleAppChessLogic;

public sealed record MovePieceCommand(BoardPosition From, BoardPosition To);

public sealed class MoveChessIntent : ICommand {
    public MoveChessIntent(MovePieceCommand command) {
        Command = command;
    }

    public MovePieceCommand Command { get; }

    public GameResult? Result { get; private set; }

    internal void Complete(GameResult result) {
        if (Result is not null) {
            throw new InvalidOperationException("玩家意图不能被重复处理。");
        }

        Result = result;
    }
}

public sealed record GameResult(
    MoveResult Result,
    IReadOnlyList<IGameEvent> Events) {
    public static GameResult WithoutEvents(MoveResult result) =>
        new(result, Array.Empty<IGameEvent>());
}
