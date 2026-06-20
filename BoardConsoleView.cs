namespace ConsoleAppChessLogic;

public sealed class BoardConsoleView : IGameView {
    private readonly TextReader input;
    private readonly TextWriter output;
    private readonly BoardConsoleInputParser inputParser;

    public BoardConsoleView(
        TextReader input,
        TextWriter output,
        BoardConsoleInputParser inputParser) {
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
        output.WriteLine(BoardFormatter.Format(snapshot));
    }

    public void ShowInvalidInput() {
        output.WriteLine("输入非法");
    }

    public void ShowResult(GameResult result, GameSnapshot snapshot) {
        if (result.Result == MoveResult.InvalidInput) {
            output.WriteLine("输入非法");
            return;
        }

        if (result.Result == MoveResult.GameAlreadyEnded) {
            output.WriteLine("游戏已经结束");
            return;
        }

        foreach (var gameEvent in result.Events) {
            output.WriteLine(GameEventFormatter.Format(gameEvent, columnOffset: 1));
        }

        output.WriteLine();
        output.WriteLine(BoardFormatter.Format(snapshot));
    }
}
