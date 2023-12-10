using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module14.Homework;

namespace Module14
{
    public class Homework
    {
        public static void Start()
        {
            Game game = new Game();
            game.StartGame();
        }
        public enum CardSuit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }
        public enum CardRang
        {
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
        public class Card
        {
            public CardSuit Suit;
            public CardRang Rang;
            public Card(CardSuit suit, CardRang rang)
            {
                this.Suit = suit;
                this.Rang = rang;
            }
            public override string ToString()
            {
                return $"{Suit} {Rang}";
            }
        }
        public class Player
        {
            public Queue<Card> Cards;
            public Player()
            {
                Cards = new Queue<Card>();
            }
            public void ShowCards()
            {
                Console.WriteLine($"Player's cards: {string.Join(", ", Cards)}");
            }
        }
        public class Game
        {
            private List<Player> players;
            private Queue<Card> deck;

            public Game()
            {
                players = new List<Player>();
                deck = new Queue<Card>();
                InitializeDeck();
                ShuffleDeck();
                DealCards();
            }

            private void InitializeDeck()
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardRang rank in Enum.GetValues(typeof(CardRang)))
                    {
                        deck.Enqueue(new Card(suit, rank));
                    }
                }
            }

            private void ShuffleDeck()
            {
                Random rand = new Random();
                deck = new Queue<Card>(deck.OrderBy(card => rand.Next()));
            }

            private void DealCards()
            {
                int playerCount = 2; // Установите нужное количество игроков (от 2 до 6)
                int cardsPerPlayer = deck.Count / playerCount;

                for (int i = 0; i < playerCount; i++)
                {
                    Player player = new Player();
                    for (int j = 0; j < cardsPerPlayer; j++)
                    {
                        player.Cards.Enqueue(deck.Dequeue());
                    }
                    players.Add(player);
                }
            }

            public void StartGame()
            {
                Console.WriteLine("Initial player hands:");
                foreach (var player in players)
                {
                    player.ShowCards();
                }

                while (!IsGameOver())
                {
                    PlayRound();
                }

                Console.WriteLine($"Player {GetWinner().ToString()} wins!");
            }

            private bool IsGameOver()
            {
                return players.Count(p => p.Cards.Count > 0) == 1;
            }

            private void PlayRound()
            {
                List<Card> roundCards = new List<Card>();

                foreach (var player in players)
                {
                    if (player.Cards.Count > 0)
                    {
                        Card drawnCard = player.Cards.Dequeue();
                        Console.WriteLine($"Player {players.IndexOf(player) + 1} draws: {drawnCard}");
                        roundCards.Add(drawnCard);
                    }
                }

                int maxRank = roundCards.Max(card => (int)card.Rang);
                Player winningPlayer = players.Find(p => p.Cards.Any(card => (int)card.Rang == maxRank));

                Console.WriteLine($"Player {players.IndexOf(winningPlayer) + 1} wins the round!");

                foreach (var card in roundCards)
                {
                    winningPlayer.Cards.Enqueue(card);
                }

                foreach (var player in players)
                {
                    player.ShowCards();
                }
            }

            private Player GetWinner()
            {
                return players.First(p => p.Cards.Count > 0);
            }
        }
    }
}
