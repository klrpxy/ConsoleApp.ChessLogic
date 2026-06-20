using ConsoleApp.ChessLogic.Application.Commands;
using ConsoleApp.ChessLogic.Application.Events;
using ConsoleApp.ChessLogic.Application.Results;
using ConsoleApp.ChessLogic.Application.Snapshots;
using ConsoleApp.ChessLogic.Domain.Board;
using ConsoleApp.ChessLogic.Domain.Game;
using ConsoleApp.ChessLogic.Domain.Pieces;
using ConsoleApp.ChessLogic.Domain.Pieces.Strategies;
using ConsoleApp.ChessLogic.Domain.Rules;

namespace ConsoleApp.ChessLogic.Application;

public sealed class GameEngine {
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

    public GameResult Execute(MoveChessIntent intent) {
        if (State.Status != GameStatus.Playing) {
            return GameResult.Failed();
        }

        if (!moveValidator.IsLegalMove(
                State.Board,
                intent.From,
                intent.To,
                State.CurrentTurn)) {
            return GameResult.Failed();
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

            events.Add(new GameEndedEvent(movingColor));

            return new GameResult(true, events);
        }

        State.CurrentTurn = opponent;

        if (moveValidator.IsInCheck(State.Board, opponent)) {
            events.Add(new CheckEvent(opponent));
            return new GameResult(true, events);
        }

        return new GameResult(true, events);
    }
}
