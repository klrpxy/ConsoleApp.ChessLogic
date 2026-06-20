namespace ConsoleApp.ChessLogic.Tests.Application;

public sealed class GameEngineTests {
    [Fact]
    public void GetSnapshot_ReturnsCurrentReadOnlyState() {
        var engine = new GameEngine();

        var snapshot = engine.GetSnapshot();

        Assert.Equal(PieceColor.Red, snapshot.CurrentTurn);
        Assert.Equal(GameStatus.Playing, snapshot.Status);
        Assert.Equal(32, snapshot.Pieces.Count);
        Assert.Contains(
            snapshot.Pieces,
            piece =>
                piece.Color == PieceColor.Red &&
                piece.Type == PieceType.General &&
                piece.Position == new BoardPosition(4, 9));
    }

    [Fact]
    public void LegalMove_ChangesTurnAndReturnsMoveEvent() {
        var engine = new GameEngine();

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(1, 9),
            new BoardPosition(2, 7)));

        Assert.True(result.Success);
        Assert.Equal(PieceColor.Black, engine.State.CurrentTurn);

        var moved = Assert.IsType<PieceMovedEvent>(Assert.Single(result.Events));
        Assert.Equal(PieceColor.Red, moved.Color);
        Assert.Equal(PieceType.Horse, moved.PieceType);
        Assert.Equal(new BoardPosition(1, 9), moved.From);
        Assert.Equal(new BoardPosition(2, 7), moved.To);
    }

    [Fact]
    public void IllegalMove_DoesNotChangeBoardOrTurn() {
        var engine = new GameEngine();

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(1, 9),
            new BoardPosition(3, 7)));

        Assert.False(result.Success);
        Assert.Empty(result.Events);
        Assert.Equal(PieceColor.Red, engine.State.CurrentTurn);
        Assert.Equal(
            TestBoard.Red(PieceType.Horse),
            engine.State.Board[new BoardPosition(1, 9)]);
        Assert.Null(engine.State.Board[new BoardPosition(3, 7)]);
    }

    [Fact]
    public void Capture_ReturnsMoveThenCaptureEvent() {
        var engine = new GameEngine();

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(1, 7),
            new BoardPosition(1, 0)));

        Assert.Collection(
            result.Events,
            gameEvent => {
                var moved = Assert.IsType<PieceMovedEvent>(gameEvent);
                Assert.Equal(PieceType.Cannon, moved.PieceType);
            },
            gameEvent => {
                var captured = Assert.IsType<PieceCapturedEvent>(gameEvent);
                Assert.Equal(PieceColor.Black, captured.Color);
                Assert.Equal(PieceType.Horse, captured.PieceType);
            });
    }

    [Fact]
    public void MoveThatChecksOpponent_ReturnsCheckEventLast() {
        var board = TestBoard.WithGenerals(
            (new BoardPosition(0, 1), TestBoard.Red(PieceType.Chariot)));
        var engine = new GameEngine(new GameState(board));

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(0, 1),
            new BoardPosition(4, 1)));

        Assert.True(result.Success);
        Assert.Collection(
            result.Events,
            gameEvent => Assert.IsType<PieceMovedEvent>(gameEvent),
            gameEvent => {
                var check = Assert.IsType<CheckEvent>(gameEvent);
                Assert.Equal(PieceColor.Black, check.CheckedColor);
            });
    }

    [Fact]
    public void CapturingGeneral_ReturnsCaptureAndLossEvents() {
        var board = ChessBoard.Create(
            (new BoardPosition(4, 9), TestBoard.Red(PieceType.General)),
            (new BoardPosition(4, 0), TestBoard.Black(PieceType.General)),
            (new BoardPosition(4, 1), TestBoard.Red(PieceType.Chariot)));
        var engine = new GameEngine(new GameState(board));

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(4, 1),
            new BoardPosition(4, 0)));

        Assert.True(result.Success);
        Assert.Equal(GameStatus.RedWon, engine.State.Status);
        Assert.Collection(
            result.Events,
            gameEvent => Assert.IsType<PieceMovedEvent>(gameEvent),
            gameEvent => {
                var captured = Assert.IsType<PieceCapturedEvent>(gameEvent);
                Assert.Equal(PieceType.General, captured.PieceType);
                Assert.Equal(PieceColor.Black, captured.Color);
            },
            gameEvent => {
                var ended = Assert.IsType<GameEndedEvent>(gameEvent);
                Assert.Equal(PieceColor.Red, ended.Winner);
            });
    }

    [Fact]
    public void GameAlreadyEnded_RejectsFurtherMoves() {
        var state = new GameState(
            ChessBoard.CreateInitial(),
            PieceColor.Red,
            GameStatus.BlackWon);
        var engine = new GameEngine(state);

        var result = engine.Execute(new MoveChessIntent(
            new BoardPosition(1, 9),
            new BoardPosition(2, 7)));

        Assert.False(result.Success);
        Assert.Empty(result.Events);
    }
}
