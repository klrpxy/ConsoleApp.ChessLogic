using VitalRouter;
using ConsoleAppChessLogic.Application.Results;

namespace ConsoleAppChessLogic.Application.Commands;

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
