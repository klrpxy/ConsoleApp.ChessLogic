using VitalRouter;
using ConsoleAppChessLogic.Presentation;

namespace ConsoleAppChessLogic.Application;

public sealed class GameApplication {
    private readonly Router router;
    private readonly GameEngine engine;
    private readonly IGameView view;

    public GameApplication(Router router, GameEngine engine, IGameView view) {
        this.router = router;
        this.engine = engine;
        this.view = view;
    }

    public async ValueTask RunAsync(
        CancellationToken cancellationToken = default) {
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

            await router.PublishAsync(input.Intent, cancellationToken);

            var result = input.Intent.Result ??
                         throw new InvalidOperationException(
                             "玩家意图没有被 GameEngine 处理。");

            view.ShowResult(result, engine.GetSnapshot());
        }
    }
}
