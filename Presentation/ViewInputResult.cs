using ConsoleAppChessLogic.Application.Commands;

namespace ConsoleAppChessLogic.Presentation;

public sealed record ViewInputResult(
    ViewInputKind Kind,
    MoveChessIntent? Intent) {
    public static ViewInputResult Move(MoveChessIntent intent) =>
        new(ViewInputKind.Move, intent);

    public static ViewInputResult Quit() =>
        new(ViewInputKind.Quit, null);

    public static ViewInputResult Invalid() =>
        new(ViewInputKind.Invalid, null);
}
