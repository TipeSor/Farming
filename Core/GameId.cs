namespace Farming.Core
{
    public record GameId(string Value)
    {
        public override string ToString() => Value;
        public static implicit operator string(GameId gameId)
        {
            return gameId.ToString();
        }
    }
}
