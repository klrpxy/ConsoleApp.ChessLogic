using ConsoleApp.ChessLogic.Application.Results;
using ConsoleApp.ChessLogic.Application.Snapshots;
using ConsoleApp.ChessLogic.Presentation.Console.Common;

namespace ConsoleApp.ChessLogic.Presentation.Console.BoardView;

public sealed class BoardConsoleView : IGameView {
    private readonly TextReader input;
    private readonly TextWriter output;
    private readonly BoardConsoleInputParser inputParser;
    private readonly BoardConsoleTheme theme;

    public BoardConsoleView(
        TextReader input,
        TextWriter output,
        BoardConsoleInputParser inputParser,
        BoardConsoleTheme theme) {
        this.input = input;
        this.output = output;
        this.inputParser = inputParser;
        this.theme = theme;
    }

    public async ValueTask<ViewInputResult> ReadInputAsync(
        CancellationToken cancellationToken = default) {
        var line = await input.ReadLineAsync(cancellationToken);
        return inputParser.Parse(line);
    }

    public void ShowInitial(GameSnapshot snapshot) {
        output.WriteLine(BoardFormatter.Format(snapshot, theme));
    }

    public void ShowInvalidInput() {
        output.WriteLine("输入非法");
    }

    public void ShowResult(GameResult result, GameSnapshot snapshot) {
        if (!result.Success) {
            output.WriteLine("输入非法");
            return;
        }

        foreach (var gameEvent in result.Events) {
            output.WriteLine(GameEventFormatter.Format(gameEvent, columnOffset: 1));
        }

        output.WriteLine();
        output.WriteLine(BoardFormatter.Format(snapshot, theme));
    }
}
