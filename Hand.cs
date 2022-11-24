namespace BlackJack;

/// <summary>
/// A class representing a player's hand and the related functions required for
/// gameplay.
/// </summary>
public class Hand
{
    /// <summary>
    /// The list of cards currently held in the hand. The Card.IsInUse boolean
    /// must be set to true upon being added to the hand and false upon being
    /// removed/cleared as to prevent duplicate cards being drawn from the deck
    /// or cards being locked from use respectively. 
    /// </summary>
    private readonly List<Card> _heldCards = new();
    
    /// <summary>
    /// The maximum value the hand may equate to before being considered "bust".
    /// </summary>
    private const int MaxHandValue = 21;
    
    /// <summary>
    /// Returns the held card at the specified index.
    /// </summary>
    /// <param name="index">Index of the card to return.</param>
    public Card this[int index] => _heldCards[index];

    public int Size => _heldCards.Count;

    /// <summary>
    /// The sum value of all Cards held within the hand, dynamically calculating
    /// the value of Aces in an attempt to achieve the maximum value without
    /// causing the hand to bust.
    /// </summary>
    public int Value
    {
        get
        {
            int sum = 0, aceCount = 0;

            foreach (int curValue in _heldCards.Select(card => card.Value))
            {
                if (curValue == Card.AceValue) {
                    aceCount++;
                    continue;
                }

                sum += curValue;
            }

            // Handle aces after the "hard value" cards have been calculated.
            // This ensures no accidental busts where ace's values could've been lowered.
            while (aceCount-- > 0) {
                int remainingPoints = MaxHandValue - sum;

                // If we can fit in all aces to come with enough room for an 11, raise
                // this ace's value.
                if (((remainingPoints - aceCount)) >= 11) {
                    sum += 11;
                } else {
                    sum++;
                }
            }

            return sum;
        }
    }

    /// <summary>
    /// Flag to check for whether the hand's value has overstepped the maximum
    /// permitted value in game (resulting in a bust).
    /// </summary>
    public bool IsHandBust => (Value > MaxHandValue);

    /// <summary>
    /// Adds a card to the hand, marking it as "in use" to prevent duplicates.
    /// </summary>
    /// <param name="card">The card to be added to the hand from the deck.</param>
    public void AddCard(Card card)
    {
        card.IsInUse = true;
        _heldCards.Add(card);
    }

    /// <summary>
    /// Marks the cards in the hand as available to be drawn again
    /// before clearing the internal list of cards held.
    /// </summary>
    public void Clear()
    {
        foreach (var card in _heldCards)
        {
            card.IsInUse = false;
        }
        _heldCards.Clear();
    }
}