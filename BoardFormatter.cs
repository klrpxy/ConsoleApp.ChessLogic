using System.Text;

namespace ConsoleAppChessLogic;

public static class BoardFormatter {
    public static string Format(GameSnapshot snapshot) {
        var piecesByPosition = snapshot.Pieces.ToDictionary(
            piece => piece.Position);
        var builder = new StringBuilder();

        builder.AppendLine("     1  2  3  4  5  6  7  8  9");

        for (var row = 0; row < 10; row++) {
            builder.Append($" {row}   ");

            for (var column = 0; column < 9; column++) {
                var position = new BoardPosition(column, row);
                builder.Append(
                    piecesByPosition.TryGetValue(position, out var piece)
                        ? FormatPiece(piece)
                        : "．");

                if (column < 8) {
                    builder.Append(' ');
                }
            }

            builder.AppendLine();

            if (row == 4) {
                builder.AppendLine("              楚河　汉界");
            }
        }

        builder.AppendLine();
        builder.AppendLine(FormatStatus(snapshot));

        if (snapshot.Status == GameStatus.Playing) {
            builder.Append("请输入：起点列 起点行 终点列 终点行（输入 quit 退出）");
        }

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

    private static string FormatStatus(GameSnapshot snapshot) =>
        snapshot.Status switch {
            GameStatus.Playing =>
                $"当前回合：{FormatColor(snapshot.CurrentTurn)}方",
            GameStatus.RedWon => "游戏结束：红方胜利",
            GameStatus.BlackWon => "游戏结束：黑方胜利",
            _ => throw new ArgumentOutOfRangeException(nameof(snapshot))
        };

    private static string FormatColor(PieceColor color) =>
        color == PieceColor.Red ? "红" : "黑";
}
