namespace BlackJack;

public enum Suits
{
    Clubs,
    Diamonds,
    Hearts,
    Spades,
    
    /// <summary>
    /// Number of suits (4).
    /// </summary>
    Count,
    
    /// <summary>
    /// Number of cards in each suit (13).
    /// </summary>
    Size = 13
}

/// <summary>
/// Normally, I would make this a struct - however, since the IsInUse bool is
/// constantly externally set I felt it was safer to design the Card as a class. 
/// </summary>
public class Card
{
    public const int AceValue = -1;
    
    /// <summary>
    /// Is this card currently in someone's deck? If so, don't deal it.
    /// </summary>
    public bool IsInUse;
    
    /// <summary>
    /// String representation of the card's value (suit not included).
    /// </summary>
    public string Name = string.Empty;
    
    /// <summary>
    /// Integer value of the card. Ace's value is dynamically interpreted
    /// depending on the current value of the hand (11 or 1 depending on if the
    /// former would cause the holder to bust).
    /// </summary>
    public int Value;
    
    /// <summary>
    /// Enum value identifying the suit the card belongs to.
    /// </summary>
    public Suits Suit;

    public string FormatName() => $"{Name} of {Suit}";
}
