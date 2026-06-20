namespace ConsoleAppChessLogic.Presentation.Console.Board;

public readonly record struct AnsiColor(byte Red, byte Green, byte Blue);

public sealed record BoardConsoleTheme(
    AnsiColor? Background,
    AnsiColor Text,
    AnsiColor Numbers,
    AnsiColor EmptyPositions,
    AnsiColor River,
    AnsiColor RedPieces,
    AnsiColor BlackPieces,
    bool ColorsEnabled = true) {
    public static BoardConsoleTheme Default { get; } = new(
        Background: null,
        Text: new AnsiColor(255, 255, 255),
        Numbers: new AnsiColor(218, 165, 32),
        EmptyPositions: new AnsiColor(128, 128, 128),
        River: new AnsiColor(128, 128, 128),
        RedPieces: new AnsiColor(255, 60, 60),
        BlackPieces: new AnsiColor(255, 255, 255));

    public static BoardConsoleTheme Plain { get; } =
        Default with { ColorsEnabled = false };

    public string Begin() =>
        ColorsEnabled
            ? $"{FormatBackground()}{ForegroundCode(Text)}"
            : string.Empty;

    public string Foreground(AnsiColor color) =>
        ColorsEnabled ? ForegroundCode(color) : string.Empty;

    public string RestoreText() => Foreground(Text);

    public string Reset() =>
        ColorsEnabled ? "\u001b[0m" : string.Empty;

    private static string ForegroundCode(AnsiColor color) =>
        $"\u001b[38;2;{color.Red};{color.Green};{color.Blue}m";

    private static string BackgroundCode(AnsiColor color) =>
        $"\u001b[48;2;{color.Red};{color.Green};{color.Blue}m";

    private string FormatBackground() =>
        Background is { } color
            ? BackgroundCode(color)
            : string.Empty;
}
