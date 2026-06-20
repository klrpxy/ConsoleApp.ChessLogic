using System.Text;
using ConsoleApp.ChessLogic.Application.Snapshots;
using ConsoleApp.ChessLogic.Domain.Board;
using ConsoleApp.ChessLogic.Domain.Game;
using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Presentation.Console.BoardView;

public static class BoardFormatter {
    public static string Format(
        GameSnapshot snapshot,
        BoardConsoleTheme theme) {
        var piecesByPosition = snapshot.Pieces.ToDictionary(
            piece => piece.Position);
        var builder = new StringBuilder();

        builder.Append(theme.Begin());
        builder.Append("     ");
        AppendStyled(builder, "1  2  3  4  5  6  7  8  9", theme.Numbers, theme);
        builder.AppendLine();

        for (var row = 0; row < 10; row++) {
            builder.Append(' ');
            AppendStyled(builder, row.ToString(), theme.Numbers, theme);
            builder.Append("   ");

            for (var column = 0; column < 9; column++) {
                var position = new BoardPosition(column, row);
                if (piecesByPosition.TryGetValue(position, out var piece)) {
                    AppendStyled(
                        builder,
                        FormatPiece(piece),
                        piece.Color == PieceColor.Red
                            ? theme.RedPieces
                            : theme.BlackPieces,
                        theme);
                } else {
                    AppendStyled(
                        builder,
                        "．",
                        theme.EmptyPositions,
                        theme);
                }

                if (column < 8) {
                    builder.Append(' ');
                }
            }

            builder.AppendLine();

            if (row == 4) {
                builder.Append("              ");
                AppendStyled(builder, "楚河　汉界", theme.River, theme);
                builder.AppendLine();
            }
        }

        builder.AppendLine();
        AppendStatus(builder, snapshot);
        builder.AppendLine();

        if (snapshot.Status == GameStatus.Playing) {
            builder.Append("请输入：起点列 起点行 终点列 终点行（输入 quit 退出）");
        }

        builder.Append(theme.Reset());
        return builder.ToString();
    }

    private static string FormatPiece(PieceSnapshot piece) =>
        (piece.Color, piece.Type) switch {
            (PieceColor.Red, PieceType.General) => "帅",
            (PieceColor.Red, PieceType.Advisor) => "仕",
            (PieceColor.Red, PieceType.Elephant) => "相",
            (PieceColor.Red, PieceType.Horse) => "马",
            (PieceColor.Red, PieceType.Chariot) => "车",
            (PieceColor.Red, PieceType.Cannon) => "炮",
            (PieceColor.Red, PieceType.Soldier) => "兵",
            (PieceColor.Black, PieceType.General) => "将",
            (PieceColor.Black, PieceType.Advisor) => "士",
            (PieceColor.Black, PieceType.Elephant) => "象",
            (PieceColor.Black, PieceType.Horse) => "馬",
            (PieceColor.Black, PieceType.Chariot) => "車",
            (PieceColor.Black, PieceType.Cannon) => "砲",
            (PieceColor.Black, PieceType.Soldier) => "卒",
            _ => throw new ArgumentOutOfRangeException(nameof(piece))
        };

    private static void AppendStatus(
        StringBuilder builder,
        GameSnapshot snapshot) {
        switch (snapshot.Status) {
            case GameStatus.Playing:
                builder.Append(
                    snapshot.CurrentTurn == PieceColor.Red
                        ? "当前回合：红方"
                        : "当前回合：黑方");
                break;
            case GameStatus.RedWon:
                builder.Append("游戏结束：红方胜利");
                break;
            case GameStatus.BlackWon:
                builder.Append("游戏结束：黑方胜利");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(snapshot));
        }
    }

    private static void AppendStyled(
        StringBuilder builder,
        string text,
        AnsiColor color,
        BoardConsoleTheme theme) {
        builder.Append(theme.Foreground(color));
        builder.Append(text);
        builder.Append(theme.RestoreText());
    }
}
