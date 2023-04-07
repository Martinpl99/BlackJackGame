namespace BlackJackGame.Interfaces
{
    public interface IBlackJackGameApplication
    {
        void Run();
        void CroupierCards(CardStruct data);
        void PlayerCards(CardStruct data);
    }
}