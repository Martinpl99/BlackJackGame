using System.Text;

namespace BlackJackGame.Interfaces
{
    public interface IBlackJackGame
    {
        Tuple<string,int> RandomCard();
        CardStruct CroupierStartGame();
        CardStruct PlayerStartGame();
        CardStruct AddCard(CardStruct data);
        public void ResetGame();
        public void ShowCroupierCards(CardStruct data);
    }
}