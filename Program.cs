using ConsoleAppChessLogic.Application;
using ConsoleAppChessLogic.Presentation;
using ConsoleAppChessLogic.Presentation.Console.BoardView;

var engine = new GameEngine();

// Replace this instance to use another input/output view.
IGameView view = new BoardConsoleView(
    Console.In,
    Console.Out,
    new BoardConsoleInputParser(),
    BoardConsoleTheme.Default);

var application = new GameApplication(engine, view);
await application.RunAsync();
