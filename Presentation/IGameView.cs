using ConsoleApp.ChessLogic.Application.Results;
using ConsoleApp.ChessLogic.Application.Snapshots;

namespace ConsoleApp.ChessLogic.Presentation;

public interface IGameView {
    ValueTask<ViewInputResult> ReadInputAsync(CancellationToken cancellationToken = default);
    void ShowInitial(GameSnapshot snapshot);
    void ShowInvalidInput();
    void ShowResult(GameResult result, GameSnapshot snapshot);
}
