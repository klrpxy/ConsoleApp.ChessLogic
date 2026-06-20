using VitalRouter;
using ConsoleAppChessLogic.Application.Results;
using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Application.Commands;

public sealed class MoveChessIntent : ICommand {
    public MoveChessIntent(BoardPosition from, BoardPosition to) {
        From = from;
        To = to;
    }

    public BoardPosition From { get; }
    public BoardPosition To { get; }
    public GameResult? Result { get; private set; }

    internal void Complete(GameResult result) {
        if (Result is not null) {
            throw new InvalidOperationException("玩家意图不能被重复处理。");
        }

        Result = result;
    }
}
