using ConsoleAppChessLogic.Application.Results;
using ConsoleAppChessLogic.Application.Snapshots;

namespace ConsoleAppChessLogic.Presentation;

public interface IGameView {
    ValueTask<ViewInputResult> ReadInputAsync(CancellationToken cancellationToken = default);
    void ShowInitial(GameSnapshot snapshot);
    void ShowInvalidInput();
    void ShowResult(GameResult result, GameSnapshot snapshot);
}
