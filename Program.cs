using ConsoleAppChessLogic;

var engine = new GameEngine();

while (true)
{
    var input = Console.ReadLine();
    if (input is null || input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    var values = input
        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    if (values.Length != 4 ||
        !int.TryParse(values[0], out var fromX) ||
        !int.TryParse(values[1], out var fromY) ||
        !int.TryParse(values[2], out var toX) ||
        !int.TryParse(values[3], out var toY))
    {
        Console.WriteLine("输入非法");
        continue;
    }

    var result = engine.Execute(new MovePieceCommand(
        new BoardPosition(fromX, fromY),
        new BoardPosition(toX, toY)));

    if (result.Result is MoveResult.InvalidInput or MoveResult.GameAlreadyEnded)
    {
        Console.WriteLine(
            result.Result == MoveResult.InvalidInput
                ? "输入非法"
                : "游戏已经结束");
        continue;
    }

    foreach (var gameEvent in result.Events)
    {
        Console.WriteLine(FormatEvent(gameEvent));
    }
}

static string FormatEvent(IGameEvent gameEvent) =>
    gameEvent switch
    {
        PieceMovedEvent e =>
            $"{FormatColor(e.Color)}方{FormatPiece(e.PieceType)}从" +
            $"({e.From.X},{e.From.Y})移动到({e.To.X},{e.To.Y})",
        PieceCapturedEvent e =>
            $"{FormatColor(e.Color)}方{FormatPiece(e.PieceType)}被吃",
        CheckEvent e =>
            $"{FormatColor(e.CheckedColor)}方被将军",
        GameLostEvent e =>
            $"{FormatColor(e.LosingColor)}方输了",
        _ => throw new ArgumentOutOfRangeException(nameof(gameEvent))
    };

static string FormatColor(PieceColor color) =>
    color == PieceColor.Red ? "红" : "黑";

static string FormatPiece(PieceType pieceType) =>
    pieceType switch
    {
        PieceType.General => "将",
        PieceType.Advisor => "士",
        PieceType.Elephant => "象",
        PieceType.Horse => "马",
        PieceType.Chariot => "车",
        PieceType.Cannon => "炮",
        PieceType.Soldier => "兵",
        _ => throw new ArgumentOutOfRangeException(nameof(pieceType))
    };
