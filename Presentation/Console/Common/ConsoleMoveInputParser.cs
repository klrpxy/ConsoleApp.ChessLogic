using ConsoleApp.ChessLogic.Application.Commands;
using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Presentation.Console.Common;

public sealed class ConsoleMoveInputParser {
    public ViewInputResult Parse(string? input) {
        if (input is null ||
            input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase)) {
            return ViewInputResult.Quit();
        }

        var values = input.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (values.Length != 4 ||
            !int.TryParse(values[0], out var fromX) ||
            !int.TryParse(values[1], out var fromY) ||
            !int.TryParse(values[2], out var toX) ||
            !int.TryParse(values[3], out var toY)) {
            return ViewInputResult.Invalid();
        }

        return ViewInputResult.Move(new MoveChessIntent(
            new BoardPosition(fromX, fromY),
            new BoardPosition(toX, toY)));
    }
}
