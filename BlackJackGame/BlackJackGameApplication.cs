using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;
using BlackJackGame.Interfaces;
using System.Diagnostics.Metrics;

namespace BlackJackGame
{
    public class BlackJackGameApplication : IBlackJackGameApplication
    {
        private readonly IBlackJackGame _blackJackGame;

        CardStruct Croupier= new CardStruct();
        CardStruct User= new CardStruct();
        int EnterMoney = 5000;
        int bet = 0;

        public BlackJackGameApplication(IBlackJackGame blackJackGame)
        {
            _blackJackGame = blackJackGame;
        }

        public void Run()
        {
            Console.WriteLine("\n\n ********* Welcome in Black Jack Game-Academic Version **********\n Author:" +
                " Martin Student\n Greetings to Kacper Szmid, Paweł Lenio, Kamil Mulawa" +
                ",Szwagier Pawełczak, Filip Nowicz and others\n\n\n Let's start !!!\n\n");

            Bet();
            StartGame();
            Console.WriteLine("\n");

            while(true)
            {
                if (User.Value == 21)
                {
                    EnterMoney += bet;
                    Console.WriteLine("Black Jack!!!\n You win the game\n");
                    Console.WriteLine("You win {0} euros!!!",bet);
                    Console.WriteLine("If you want try again please enter yes(y) or no(n)\n");
                    CheckAnswer();
                    continue;
                }

                Console.WriteLine("A-HIT, B-STAND, D-DOUBLE");
                var choice=Console.ReadLine();
                if(choice.ToLower()=="a")
                {
                    User = _blackJackGame.AddCard(User);
                    CroupierCards(Croupier);
                    PlayerCards(User);
                    Console.WriteLine("\n\n");
                }
                else if((choice.ToLower()=="b")) 
                {
                    CheckB();
                    CheckAnswer();
                    continue;
                }
                else if (choice.ToLower() == "d") 
                {
                    if (bet*2 > EnterMoney)
                    {
                        Console.WriteLine("\n****You can't use Double, beacuse you have not enough money!!! ****\n ");
                        continue;
                    }
                    else
                    {
                        bet = bet * 2;
                        Console.WriteLine($"Your bet is now: {bet}");
                        CheckB();
                        CheckAnswer();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("You gave unrecognized command, try again!");
                    continue;
                }


                if(User.Value>21)
                {
                    EnterMoney -= bet;
                    Console.WriteLine("\nYou lose game!!!\n Your score is grater than 21!!\n If" +
                        " you want try again please enter yes(y) or no(n)\n");
                    CheckAnswer();

                }
                else if(User.Value==21)
                {
                    EnterMoney += bet;
                    Console.WriteLine("Black Jack!!!\n You win the game\n");
                    Console.WriteLine("You win {0} euros!!!",2*bet);
                    Console.WriteLine("If you want try again please enter yes(y) or no(n)\n");
                    CheckAnswer();
                }
            }
        }


        private void CheckAnswer()
        {
            while (true)
            {
                var choice_2 = Console.ReadLine();
                if (choice_2.ToLower() == "y")
                {
                    _blackJackGame.ResetGame();
                    Bet();
                    StartGame();
                    break;
                }
                else if (choice_2.ToLower() == "n")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("You give wrong answer, try again.\n");
                    Console.WriteLine("If you want try again please enter yes(y) or no(n)\n");
                }
            }
        }

        private void CheckB()
        {
            while (true)
            {
                _blackJackGame.ShowCroupierCards(Croupier);
                PlayerCards(User);

                if (Croupier.Value < 16)
                {
                    Croupier = _blackJackGame.AddCard(Croupier);
                    Thread.Sleep(1000);
                }
                else if ((Croupier.Value < User.Value) || Croupier.Value > 21)
                {
                    EnterMoney += bet;
                    Console.WriteLine("You won the game");
                    Console.WriteLine("If you want try again please enter yes(y) or no(n)\n");
                    break;
                }
                else
                {
                    EnterMoney -= bet;
                    Console.WriteLine("You lose the game");
                    Console.WriteLine("If you want try again please enter yes(y) or no(n)\n");
                    break;
                }
            }
        }

        public void CroupierCards(CardStruct data)
        {
            Console.WriteLine("Croupier cards: \n");
            Console.WriteLine(data.Builder);
            Console.WriteLine("\n");
        }


        public void PlayerCards(CardStruct data)
        {
            Console.WriteLine("Player cards: \n");
            Console.WriteLine(data.Builder);
            Console.WriteLine("Your's cards sum is: {0}",data.Value);
        }

        public void StartGame()
        {
            Croupier = _blackJackGame.CroupierStartGame();
            User = _blackJackGame.PlayerStartGame();
            CroupierCards(Croupier);
            PlayerCards(User);
            Console.WriteLine("\nYour bet is: {0}", bet);
        }

        public void Bet()
        {
            if (EnterMoney <= 0)
            {
                Console.WriteLine("You dont' have money!!!\n Bye Bye");
                Environment.Exit(0);
            }

            
            while (true)
            {
                Console.WriteLine("You have {0} euros\n", EnterMoney);
                Console.WriteLine("How many you want to bet?\n");

                var Bet=Console.ReadLine();

                int bet_1;
                if (int.TryParse(Bet, out bet_1))
                {
                    if (bet_1 > EnterMoney)
                    {
                        Console.WriteLine("\n******** You don't have that much money! ******\n ");
                        Console.WriteLine("The maximum bet is: {0}", EnterMoney);
                        continue;
                    }
                    bet = bet_1;
                    break;
                }
                else
                {
                    Console.WriteLine("\n*********You gave wrong number !!! *********\n");
                    Console.WriteLine("****** TRY AGAIN *******\n");
                    continue;
                }
            }
           
            Console.WriteLine("\n");
        }

    }
}
