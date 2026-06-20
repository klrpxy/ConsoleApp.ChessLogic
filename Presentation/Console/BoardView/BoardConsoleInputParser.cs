using ConsoleApp.ChessLogic.Application.Commands;
using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Presentation.Console.BoardView;

public sealed class BoardConsoleInputParser {
    public ViewInputResult Parse(string? input) {
        if (input is null ||
            input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase)) {
            return ViewInputResult.Quit();
        }

        var values = input.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (values.Length != 4 ||
            !int.TryParse(values[0], out var fromColumn) ||
            !int.TryParse(values[1], out var fromRow) ||
            !int.TryParse(values[2], out var toColumn) ||
            !int.TryParse(values[3], out var toRow) ||
            fromColumn is < 1 or > 9 ||
            toColumn is < 1 or > 9 ||
            fromRow is < 0 or > 9 ||
            toRow is < 0 or > 9) {
            return ViewInputResult.Invalid();
        }

        return ViewInputResult.Move(new MoveChessIntent(
            new BoardPosition(fromColumn - 1, fromRow),
            new BoardPosition(toColumn - 1, toRow)));
    }
}
