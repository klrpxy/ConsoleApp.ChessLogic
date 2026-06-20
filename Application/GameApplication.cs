using ConsoleApp.ChessLogic.Presentation;

namespace ConsoleApp.ChessLogic.Application;

public sealed class GameApplication {
    private readonly GameEngine engine;
    private readonly IGameView view;

    public GameApplication(GameEngine engine, IGameView view) {
        this.engine = engine;
        this.view = view;
    }

    public async ValueTask RunAsync(CancellationToken cancellationToken = default) {
        view.ShowInitial(engine.GetSnapshot());

        while (!cancellationToken.IsCancellationRequested) {
            var input = await view.ReadInputAsync(cancellationToken);

            if (input.Kind == ViewInputKind.Quit) {
                return;
            }

            if (input.Kind == ViewInputKind.Invalid || input.Intent is null) {
                view.ShowInvalidInput();
                continue;
            }

            var result = engine.Execute(input.Intent);

            view.ShowResult(result, engine.GetSnapshot());
        }
    }
}
