using VitalRouter;
using ConsoleAppChessLogic.Application.Commands;
using ConsoleAppChessLogic.Application.Events;
using ConsoleAppChessLogic.Application.Results;
using ConsoleAppChessLogic.Application.Snapshots;
using ConsoleAppChessLogic.Domain.Board;
using ConsoleAppChessLogic.Domain.Game;
using ConsoleAppChessLogic.Domain.Pieces;
using ConsoleAppChessLogic.Domain.Pieces.Strategies;
using ConsoleAppChessLogic.Domain.Rules;

namespace ConsoleAppChessLogic.Application;

[Routes]
public sealed partial class GameEngine {
    private readonly MoveValidator moveValidator;

    public GameEngine(GameState? initialState = null) {
        State = initialState ?? new GameState(ChessBoard.CreateInitial());
        moveValidator = new MoveValidator(new PieceMoveStrategyRegistry());
    }

    public GameState State { get; }

    public GameSnapshot GetSnapshot() {
        var pieces = State.Board
            .GetAllPieces()
            .Select(x => new PieceSnapshot(
                x.Piece.Color,
                x.Piece.Type,
                x.Position))
            .ToArray();

        return new GameSnapshot(
            State.CurrentTurn,
            State.Status,
            pieces);
    }

    [Route]
    private void Handle(MoveChessIntent intent) {
        intent.Complete(Execute(intent));
    }

    public GameResult Execute(MoveChessIntent intent) {
        if (State.Status != GameStatus.Playing) {
            return GameResult.WithoutEvents(MoveResult.GameAlreadyEnded);
        }

        if (!moveValidator.IsLegalMove(
                State.Board,
                intent.From,
                intent.To,
                State.CurrentTurn)) {
            return GameResult.WithoutEvents(MoveResult.InvalidInput);
        }

        var movingPiece = State.Board[intent.From]!.Value;
        var capturedPiece = State.Board[intent.To];
        var events = new List<IGameEvent> {
            new PieceMovedEvent(
                movingPiece.Color,
                movingPiece.Type,
                intent.From,
                intent.To)
        };

        State.Board.Move(intent.From, intent.To);

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
