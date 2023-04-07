namespace BlackJackGame.Interfaces
{
    public interface IGenerateCards
    {
        List<Tuple<string, int>> CreateRangeOfCards();
    }
}