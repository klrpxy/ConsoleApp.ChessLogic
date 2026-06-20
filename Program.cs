using ConsoleAppChessLogic.Application;
using ConsoleAppChessLogic.Presentation;
using ConsoleAppChessLogic.Presentation.Console.Board;
using VitalRouter;

var engine = new GameEngine();
var router = Router.Default;

// Adjust all board colors here.
var boardTheme = BoardConsoleTheme.Default with {
    // Background = new AnsiColor(0, 0, 0), // null uses the console default.
};

// Replace this instance to use another input/output view.
IGameView view = new BoardConsoleView(
    Console.In,
    Console.Out,
    new BoardConsoleInputParser(),
    boardTheme);

using var subscription = engine.MapTo(router);

var application = new GameApplication(router, engine, view);
await application.RunAsync();
