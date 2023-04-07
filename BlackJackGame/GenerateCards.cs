using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;
using BlackJackGame.Interfaces;

namespace BlackJackGame
{
    public class GenerateCards : IGenerateCards
    {

        //Function returned Card Collection write in string;
        public List<Tuple<string,int>> CreateRangeOfCards()
        {
            var cards_tuple_list = ReturnCards();
            List<Tuple<string,int>> full_cards = new List<Tuple<string,int>>();

            var card = CardSkeleton();//Loading CardSkeleton

            int rows = card.GetLength(0);
            int column = card.GetLength(1);

            var builder = new StringBuilder();

            foreach (var g in cards_tuple_list)
            {
                var created_card = CardSkeleton();
                if (g.Item1.Length == 2)
                {
                    char[] figure = g.Item1.ToCharArray();

                    created_card[1, 1] = Convert.ToChar(figure[0]);
                    created_card[1, 2] = Convert.ToChar(figure[1]);
                    created_card[2, 2] = Convert.ToChar(g.Item2);
                    created_card[3, 2] = Convert.ToChar(figure[0]);
                    created_card[3, 3] = Convert.ToChar(figure[1]);
                }
                else
                {
                    created_card[1, 1] = Convert.ToChar(g.Item1);
                    created_card[2, 2] = Convert.ToChar(g.Item2);
                    created_card[3, 3] = Convert.ToChar(g.Item1);
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int z = 0; z < column; z++)
                    {
                        builder.Append(created_card[i, z]);
                    }
                    builder.Append("\n");
                }
                var tuple=Tuple.Create(builder.ToString(),g.Item3);
                full_cards.Add(tuple);
                //Console.WriteLine(builder.ToString());
                builder.Clear();

            }

            //Adding cover Card
            {
                var cover_card = CardSkeleton();

                cover_card[1, 1] = '#';
                cover_card[1, 2] = '#';
                cover_card[2, 1] = '#';
                cover_card[2, 2] = '#';
                cover_card[2, 3] = '#';
                cover_card[3, 2] = '#';
                cover_card[3, 3] = '#';

                for (int i = 0; i < rows; i++)
                {
                    for (int z = 0; z < column; z++)
                    {
                        builder.Append(cover_card[i, z]);
                    }
                    //if ((i == rows - 1))
                    //{
                    //    continue;
                    //}
                    builder.Append("\n");
                }
                var tuple = Tuple.Create(builder.ToString(),0);
                full_cards.Add(tuple);
                builder.Clear();

            }
            return full_cards;

        }

        private char[,] CardSkeleton()
        {
            char[,] card = new char[5, 5];

            //Skeleton of card
            card[0, 0] = ' ';
            card[1, 0] = '|';
            card[2, 0] = '|';
            card[3, 0] = '|';
            card[4, 0] = ' ';

            card[0, 1] = '-';
            card[1, 1] = ' ';
            card[2, 1] = ' ';
            card[3, 1] = ' ';
            card[4, 1] = '-';

            card[0, 2] = '-';
            card[1, 2] = ' ';
            card[2, 2] = ' ';
            card[3, 2] = ' ';
            card[4, 2] = '-';

            card[0, 3] = '-';
            card[1, 3] = ' ';
            card[2, 3] = ' ';
            card[3, 3] = ' ';
            card[4, 3] = '-';

            card[0, 4] = ' ';
            card[1, 4] = '|';
            card[2, 4] = '|';
            card[3, 4] = '|';
            card[4, 4] = ' ';

            return card;
        }

        private List<Tuple<string, string,int>> ReturnCards()
        {
            List<Tuple<string, string,int>> tuple_list = new List<Tuple<string, string,int>>();
            List<string> numerousCard = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            List<string> cardFigures = new List<string>() { "J", "Q", "K", "A" };
            List<string> stringList = new List<string>() { "\u2660", "\u2663", "\u2665", "\u2666" };
            Tuple<string, string,int> tuple;

            try
            {
                foreach (var x in numerousCard)
                {
                    foreach (var z in stringList)
                    {
                        tuple = Tuple.Create(x, z,Convert.ToInt32(x));
                        tuple_list.Add(tuple);
                    }
                }

                foreach (var x in cardFigures)
                {
                    foreach (var z in stringList)
                    {
                        if (x == "A")
                        {
                            tuple = Tuple.Create(x, z,11);
                            tuple_list.Add(tuple);
                        }
                        else
                        {
                            tuple = Tuple.Create(x, z,10);
                            tuple_list.Add(tuple);
                        }
                    }
                }

                return tuple_list;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
