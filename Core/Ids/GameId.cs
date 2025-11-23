namespace Farming.Core
{
    public record GameId(string Id)
    {
        public override string ToString() => Id;
        public static implicit operator string(GameId gameId)
        {
            return gameId.ToString();
        }
    }
}
