namespace BlackJack;

/// <summary>
/// Class container for initializing an array of Cards ready to be used in
/// playing the game.
/// </summary>
public class Deck
{
    /// <summary>
    /// The amount of unique cards in the deck.
    /// </summary>
    private const int DeckSize = 52;
    /// <summary>
    /// Internal array containing each unique card instance.
    /// </summary>
    private Card[] Cards { get; } = new Card[DeckSize];
    /// <summary>
    /// Private Instance of random generator to use in drawing cards.
    /// </summary>
    private readonly Random _randomGen = new Random();
    
    /// <summary>
    /// Operator to allow access to the cards contained within the deck. The
    /// cards are organised in the following pattern:
    ///     { ACE, [2..10], JACK, QUEEN, KING }
    /// This pattern is repeated for each suit in the order of Clubs, Diamonds,
    /// Hearts, Spades (as defined in the Suits enum). Equivalent cards between
    /// suits are stored at offsets of Suits.Size (13). A generic equation
    /// to determine the index of any card exists as such:
    ///     (Card.Value - 1) + (Card.Suit * Suits.Size)
    /// where the value of Aces, Jacks, Queens and Kings are 1, 11, 12 and 13
    /// respectively and the suits follow the previous CDHS order in a zero-based
    /// indexing system (i.e. Hearts would be 2). 
    /// </summary>
    /// <param name="index">Index of the card to return.</param>
    public Card this[int index] => Cards[index];

    public Deck()
    {
        // Initialize cards
        for (Suits suit = 0; suit < Suits.Count; suit++)
        {
            // Initialize cards 2-10
            for (int i = 2; i < 11; i++)
            {
                Cards[(i - 1) + ((int)suit * (int)Suits.Size)] = new Card
                {
                    IsInUse = false,
                    Name = i.ToString(),
                    Value = i,
                    Suit = suit
                };
            }
            
            // Initialize face cards
            Cards[(int)suit * (int)Suits.Size] = new Card
            {
                IsInUse = false,
                Name = "Ace",
                Value = Card.AceValue,
                Suit = suit
            };
            Cards[((int)suit * (int)Suits.Size) + 10] = new Card
            {
                IsInUse = false,
                Name = "Jack",
                Value = 10,
                Suit = suit
            };
            Cards[((int)suit * (int)Suits.Size) + 11] = new Card
            {
                IsInUse = false,
                Name = "Queen",
                Value = 10,
                Suit = suit
            };
            Cards[((int)suit * (int)Suits.Size) + 12] = new Card
            {
                IsInUse = false,
                Name = "King",
                Value = 10,
                Suit = suit
            };
        }
    }

    public Card DrawAvailableCard()
    {
        int timeout = 1000;
        do
        {
            var returnCard = Cards[_randomGen.Next(0, DeckSize)];
            if (!returnCard.IsInUse)
            {
                return returnCard;
            }
        } while (timeout-- > 0);

        throw new IndexOutOfRangeException(
            "Timed out while trying to draw a card. (Are they all in use?)"
        );
    }
}