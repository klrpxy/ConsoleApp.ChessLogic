using ConsoleAppChessLogic.Application;
using ConsoleAppChessLogic.Presentation;
using ConsoleAppChessLogic.Presentation.Console.BoardView;

var engine = new GameEngine();

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

var application = new GameApplication(engine, view);
await application.RunAsync();
