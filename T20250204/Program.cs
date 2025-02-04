using System;

namespace T20250204
{
    // 1 - 13 -> Heart, 1 -> A, 11 -> J, 12 -> Q, 13 -> K
    // 14 - 26 -> Diamond
    // 27 - 39 -> Clover
    // 40 - 52 -> Spade 
    internal class Program
    {
        static void Initialize(int[] deck)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = i + 1;
            }
        }

        static void Shuffle(int[] deck)
        {
            Random rand = new Random();

            for (int i = 0; i < deck.Length * 10; ++i)
            {
                int firstCardIndex = rand.Next(0, deck.Length);
                int secondCardIndex = rand.Next(0, deck.Length);

                int temp = deck[firstCardIndex];
                deck[firstCardIndex] = deck[secondCardIndex];
                deck[secondCardIndex] = temp;
            }
        }
        enum CardType
        {
            None = -1,
            Heart = 0,
            Diamond = 1,
            Clover = 2,
            Spade = 3,
            Max,
        }
        
        static int GetScore(int cardNumber)
        {
            int value = (((cardNumber - 1) % 13) + 1);
            return value > 10 ? 10 : value;
        }

        static void PrintCardList(int[] deck)
        {
            //computer
            Console.WriteLine("Computer");
            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine($"{CheckCardType(deck[i]).ToString()}{CheckCardName(deck[i])}");
            }
            Console.WriteLine("---------");
            //player
            Console.WriteLine("Player");
            for (int i = 3; i < 6; ++i)
            {
                Console.WriteLine($"{CheckCardType(deck[i]).ToString()}{CheckCardName(deck[i])}");
            }
            Console.WriteLine("---------");
        }


        static void Print(int[] deck)
        {
            PrintCardList(deck);

            //카드 3장 받고 합하기
            int computerScore = GetScore(deck[0]) + GetScore(deck[1]) + GetScore(deck[2]);
            int playerScore = GetScore(deck[3]) + GetScore(deck[4]) + GetScore(deck[5]);

            Console.WriteLine($"Computer score: {computerScore}, Player score: {playerScore}");

            if (playerScore >= 21 && computerScore < 21)
            {
                //computer win
                Console.WriteLine("Computer Win");

            }
            else if (playerScore <= 21 && computerScore >= 21)
            {
                //player win
                Console.WriteLine("Player Win");
            }
            else if (playerScore >= 21 && computerScore >= 21)
            {
                //player win
                Console.WriteLine("Player Win");
            }
            else if (computerScore <= playerScore)
            {
                //player win
                Console.WriteLine("Player Win");
            }
            else // (computerScore > playerScore)
            {
                //computer win
                Console.WriteLine("Computer Win");
            }

            //for (int i = 0; i < 4; ++i)
            //{
            //    Console.WriteLine($"{CheckCardType(deck[i]).ToString()}{CheckCardName(deck[i])}");
            //}

        }     
        

        //value 1 - 13
        static CardType CheckCardType(int CardNumber)
        {
            int valueType = (CardNumber - 1) / 13;
            //CardType returnCardType = (CardType)valueType;
            //switch((CardType)valueType)
            //{
            //    case CardType.Heart:
            //        returnCardType = CardType.Heart;
            //        break;
            //    case CardType.Diamond:
            //        returnCardType = CardType.Diamond;
            //        break;
            //    case CardType.Clover:
            //        returnCardType = CardType.Clover;
            //        break;
            //    case CardType.Spade:
            //        returnCardType = CardType.Spade;
            //        break;
            //    default:
            //        returnCardType = CardType.None;
            //        break;
            //}

            return (CardType)valueType;
        }

        static string CheckCardName(int cardNumber)
        {
            int cardValue = ((cardNumber - 1) % 13) + 1;
            string cardName;

            switch (cardValue)
            {
                case 1:
                    cardName = "A";
                    break;
                case 11:
                    cardName = "J";
                    break;
                case 12:
                    cardName = "Q";
                    break;
                case 13:
                    cardName = "K";
                    break;
                default:
                    cardName = cardValue.ToString();
                    break;

            }
            return cardName;
        }



        static void Main(string[] args)
        {
            int[] deck = new int[52];

            Initialize(deck);

            Shuffle(deck);

            Print(deck);
           
        }      
    }
}
