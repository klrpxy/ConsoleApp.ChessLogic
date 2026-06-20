namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class PieceMoveStrategyRegistry {
    private readonly IReadOnlyDictionary<PieceType, IPieceMoveStrategy> strategies =
        new Dictionary<PieceType, IPieceMoveStrategy> {
            [PieceType.General] = new GeneralMoveStrategy(),
            [PieceType.Advisor] = new AdvisorMoveStrategy(),
            [PieceType.Elephant] = new ElephantMoveStrategy(),
            [PieceType.Horse] = new HorseMoveStrategy(),
            [PieceType.Chariot] = new ChariotMoveStrategy(),
            [PieceType.Cannon] = new CannonMoveStrategy(),
            [PieceType.Soldier] = new SoldierMoveStrategy()
        };

    public IPieceMoveStrategy Get(PieceType pieceType) => strategies[pieceType];
}
