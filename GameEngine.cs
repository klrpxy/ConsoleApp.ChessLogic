namespace ConsoleAppChessLogic;

public sealed class GameEngine {
    private readonly MoveValidator moveValidator;

    public GameEngine(GameState? initialState = null) {
        State = initialState ?? new GameState(ChessBoard.CreateInitial());
        moveValidator = new MoveValidator(new PieceMoveStrategyRegistry());
    }

    public GameState State { get; }

    public GameResult Execute(MovePieceCommand cmd) {
        if (State.Status != GameStatus.Playing) {
            return GameResult.WithoutEvents(MoveResult.GameAlreadyEnded);
        }

        if (!moveValidator.IsLegalMove(
                State.Board,
                cmd.From,
                cmd.To,
                State.CurrentTurn)) {
            return GameResult.WithoutEvents(MoveResult.InvalidInput);
        }

        var movingPiece = State.Board[cmd.From]!.Value;
        var capturedPiece = State.Board[cmd.To];
        var events = new List<IGameEvent> {
            new PieceMovedEvent(
                movingPiece.Color,
                movingPiece.Type,
                cmd.From,
                cmd.To)
        };

        State.Board.Move(cmd.From, cmd.To);

        var movingColor = State.CurrentTurn;
        var opponent = MoveValidator.OpponentOf(movingColor);

        if (capturedPiece is { } captured) {
            events.Add(new PieceCapturedEvent(captured.Color, captured.Type));
        }

        if (State.Board.FindGeneral(opponent) is null ||
            !moveValidator.HasAnyLegalMove(State.Board, opponent)) {
            State.Status = movingColor == PieceColor.Red
                ? GameStatus.RedWon
                : GameStatus.BlackWon;

            events.Add(new GameLostEvent(opponent));

            return new GameResult(
                movingColor == PieceColor.Red
                    ? MoveResult.RedWins
                    : MoveResult.BlackWins,
                events);
        }

        State.CurrentTurn = opponent;

        if (moveValidator.IsInCheck(State.Board, opponent)) {
            events.Add(new CheckEvent(opponent));
            return new GameResult(MoveResult.Check, events);
        }

        return new GameResult(MoveResult.Success, events);
    }
}
