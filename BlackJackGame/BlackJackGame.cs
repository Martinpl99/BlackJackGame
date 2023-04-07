using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;
using BlackJackGame.Interfaces;

namespace BlackJackGame
{
    public class BlackJackGame : IBlackJackGame
    {
        
        Random random = new Random();
        private readonly IGenerateCards _generateCards;
        List<Tuple<string,int>> cards;
        List<Tuple<string, int>> ListOfCards;

        public BlackJackGame(IGenerateCards generateCards)
        {
            _generateCards = generateCards;
            cards = _generateCards.CreateRangeOfCards();
            ListOfCards=cards.ToList();
        }

        public Tuple<string,int> RandomCard()
        {
            System.Threading.Thread.Sleep(100);
            return ListOfCards[random.Next(ListOfCards.Count - 1)];
        }

        public void ResetGame()
        {
            ListOfCards = cards.ToList();
        }

        public CardStruct CroupierStartGame()
        {
            List<List<string>> builder_list = new List<List<string>>();
            List<Tuple<string,int>> cards_1 = new List<Tuple<string,int>>();
            StringBuilder builder = new StringBuilder();
            StringBuilder builder_2 = new StringBuilder();
            StringBuilder builder_3 = new StringBuilder();
            int CardValue = 0;
            int number_ofcards = 2;

            cards_1.Add(cards[cards.Count - 1]);
            ListOfCards.Remove(cards[cards.Count - 1]);
            builder_list.Add(new List<string>());

            for (int i = 0; i < number_ofcards; i++)
            {
                var random = RandomCard();
                cards_1.Add(random);
                ListOfCards.Remove(random);
                builder_list.Add(new List<string>());
            }

            number_ofcards++;

            for (int i = 0; i < number_ofcards; i++)
            {
                foreach (var x in cards_1[i].Item1)
                {
                    if (x == '\n')
                    {
                        builder.Append(" ");
                        builder_list[i].Add(builder.ToString());
                        builder.Clear();
                        continue;
                    }
                    else
                    {
                        builder.Append(x);
                    }
                }
            }

            for(int i=1;i<number_ofcards;i++)
            {
                CardValue = CardValue + cards_1[i].Item2;
            }

            for (int i = 0; i < builder_list[0].Count; i++)
            {
                for (int z = 0; z < builder_list.Count-1; z++)
                {
                    builder_3.Append(builder_list[z][i].ToString());
                }
                builder_3.Append('\n');
            }

            return new CardStruct()
            {
                Builder = builder_3,
                Builder_list = builder_list,
                Cards_1 = cards_1,
                NumberOfCards = number_ofcards,
                Value = CardValue
            };
           
        }

        public void ShowCroupierCards(CardStruct data)
        {
            var builder_3 = new StringBuilder();
            var builder_list=data.Builder_list;

            for (int i = 0; i < builder_list[0].Count; i++)
            {
                for (int z = 1; z < builder_list.Count; z++)
                {
                    //Value=Value+cards
                    builder_3.Append(builder_list[z][i].ToString());
                }
                builder_3.Append('\n');
            }
            Console.WriteLine("\n\nCroupier cards: ");
            Console.WriteLine(builder_3);
        }






        public CardStruct PlayerStartGame()
        {
            List<List<string>> builder_list = new List<List<string>>();
            List<Tuple<string,int>> cards_1 = new List<Tuple<string,int>>();
            StringBuilder builder = new StringBuilder();
            StringBuilder builder_2 = new StringBuilder();
            StringBuilder builder_3 = new StringBuilder();

            int CardValue = 0;

            int number_ofcards = 2;

            for (int i = 0; i < number_ofcards; i++)
            {
                var random = RandomCard();
                cards_1.Add(random);
                ListOfCards.Remove(random);
                builder_list.Add(new List<string>());
            }

            for (int i = 0; i < number_ofcards; i++)
            {
                foreach (var x in cards_1[i].Item1)
                {
                    if (x == '\n')
                    {
                        builder.Append(" ");
                        builder_list[i].Add(builder.ToString());
                        builder.Clear();
                        continue;
                    }
                    else
                    {
                        builder.Append(x);
                    }
                }
            }
            for (int i = 0; i < builder_list[0].Count; i++)
            {
                for (int z = 0; z < builder_list.Count; z++)
                {
                    builder_3.Append(builder_list[z][i].ToString());
                }
                builder_3.Append('\n');
            }

            for (int i = 0; i < number_ofcards; i++)
            {
                CardValue = CardValue + cards_1[i].Item2;
            }

            return new CardStruct()
            {
                Builder = builder_3,
                Builder_list = builder_list,
                Cards_1 = cards_1,
                NumberOfCards = number_ofcards,
                Value=CardValue
            };

        }


        public CardStruct AddCard(CardStruct data)
        {

            StringBuilder builder = new StringBuilder();
            StringBuilder builder_2 = new StringBuilder();
            StringBuilder builder_3 = new StringBuilder();

            int CardValue = data.Value;
            int number_ofcards = data.NumberOfCards;
            List<List<string>> builder_list = data.Builder_list;
            List<Tuple<string,int>> cards_1 = data.Cards_1;

            var random = RandomCard();
            cards_1.Add(random);
            ListOfCards.Remove(random);
            builder_list.Add(new List<string>());
            number_ofcards++;

            for (int i = number_ofcards - 1; i < number_ofcards; i++)
            {
                foreach (var x in cards_1[i].Item1)
                {
                    if (x == '\n')
                    {
                        builder.Append(" ");
                        builder_list[i].Add(builder.ToString());
                        builder.Clear();
                        continue;
                    }
                    else
                    {
                        builder.Append(x);
                    }
                }
            }

            for (int i = 0; i < builder_list[0].Count; i++)
            {
                for (int z = 0; z < builder_list.Count; z++)
                {
                    builder_3.Append(builder_list[z][i].ToString());
                }
                builder_3.Append('\n');
            }
            if ((random.Item2 == 11) && (CardValue > 15))
            {
                CardValue = CardValue + 1;
            }
            else
            {
                CardValue = CardValue + cards_1[cards_1.Count - 1].Item2;
            }


            return new CardStruct
            {
                Builder = builder_3,
                Cards_1 = cards_1,
                Builder_list = builder_list,
                NumberOfCards = number_ofcards,
                Value = CardValue
            };

        }
    }
}
