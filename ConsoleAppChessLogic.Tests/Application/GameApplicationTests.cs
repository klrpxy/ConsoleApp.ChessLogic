namespace ConsoleAppChessLogic.Tests.Application;

public sealed class GameApplicationTests {
    [Fact]
    public async Task RunAsync_UsesViewForInputAndOutput() {
        var engine = new GameEngine();
        var router = new VitalRouter.Router();
        using var subscription = engine.MapTo(router);
        var view = new RecordingGameView(
            ViewInputResult.Move(new MoveChessIntent(new MovePieceCommand(
                new BoardPosition(1, 9),
                new BoardPosition(2, 7)))),
            ViewInputResult.Quit());
        var application = new GameApplication(router, engine, view);

        await application.RunAsync();

        Assert.NotNull(view.InitialSnapshot);
        var shown = Assert.Single(view.Results);
        Assert.Equal(MoveResult.Success, shown.Result.Result);
        Assert.Equal(PieceColor.Black, shown.Snapshot.CurrentTurn);
    }

    [Fact]
    public async Task RunAsync_AsksViewToShowMalformedInput() {
        var engine = new GameEngine();
        var router = new VitalRouter.Router();
        using var subscription = engine.MapTo(router);
        var view = new RecordingGameView(
            ViewInputResult.Invalid(),
            ViewInputResult.Quit());
        var application = new GameApplication(router, engine, view);

        await application.RunAsync();

        Assert.Equal(1, view.InvalidInputCount);
        Assert.Empty(view.Results);
    }

    private sealed class RecordingGameView : IGameView {
        private readonly Queue<ViewInputResult> inputs;

        public RecordingGameView(params ViewInputResult[] inputs) {
            this.inputs = new Queue<ViewInputResult>(inputs);
        }

        public GameSnapshot? InitialSnapshot { get; private set; }

        public int InvalidInputCount { get; private set; }

        public List<(GameResult Result, GameSnapshot Snapshot)> Results { get; } = [];

        public ValueTask<ViewInputResult> ReadInputAsync(
            CancellationToken cancellationToken = default) =>
            ValueTask.FromResult(inputs.Dequeue());

        public void ShowInitial(GameSnapshot snapshot) {
            InitialSnapshot = snapshot;
        }

        public void ShowInvalidInput() {
            InvalidInputCount++;
        }

        public void ShowResult(GameResult result, GameSnapshot snapshot) {
            Results.Add((result, snapshot));
        }
    }
}
