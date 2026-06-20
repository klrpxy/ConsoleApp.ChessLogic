using ConsoleAppChessLogic.Application.Results;
using ConsoleAppChessLogic.Application.Snapshots;
using ConsoleAppChessLogic.Presentation.Console.Common;

namespace ConsoleAppChessLogic.Presentation.Console.EventView;

public sealed class EventConsoleView : IGameView {
    private readonly TextReader input;
    private readonly TextWriter output;
    private readonly ConsoleMoveInputParser inputParser;

    public EventConsoleView(
        TextReader input,
        TextWriter output,
        ConsoleMoveInputParser inputParser) {
        this.input = input;
        this.output = output;
        this.inputParser = inputParser;
    }

    public async ValueTask<ViewInputResult> ReadInputAsync(
        CancellationToken cancellationToken = default) {
        var line = await input.ReadLineAsync(cancellationToken);
        return inputParser.Parse(line);
    }

    public void ShowInitial(GameSnapshot snapshot) {
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
            output.WriteLine(GameEventFormatter.Format(gameEvent));
        }
    }
}
