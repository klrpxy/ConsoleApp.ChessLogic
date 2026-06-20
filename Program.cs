using ConsoleAppChessLogic;
using VitalRouter;

var engine = new GameEngine();
var router = Router.Default;

// Replace this instance to use another input/output view.
IGameView view = new EventConsoleView(
    Console.In,
    Console.Out,
    new ConsoleMoveInputParser());

using var subscription = engine.MapTo(router);

var application = new GameApplication(router, engine, view);
await application.RunAsync();
