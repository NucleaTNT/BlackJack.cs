using BlackJack;

void DealerChoiceLoop(Deck deck, Hand dealerHand) {
    while (dealerHand.Value < 17 && !dealerHand.IsHandBust) {
        dealerHand.AddCard(deck.DrawAvailableCard());
    }
}

void PlayerChoiceLoop(Deck deck, Hand playerHand) {
    while (!playerHand.IsHandBust) {
        Console.Write($"Your hand's value is {playerHand.Value}. [");
        for (int i = 0; i < playerHand.Size; i++) {
            Console.Write(
                $"{playerHand[i].FormatName()}" + 
                (i + 1 == playerHand.Size ? "]\n" : ", "));
        }

        char playerInput;
        do {
            Console.Write("Would you like to [h]it or [s]tick? ");
            playerInput = Console.ReadLine()!.ToLower()[0];
            Console.WriteLine();
        } while (playerInput is not ('h' or 's'));

        if (playerInput == 'h') {
            playerHand.AddCard(deck.DrawAvailableCard());
        } else {
            break;
        }
    }
}

Deck deck = new();
Hand playerHand = new(), dealerHand = new();

for (int i = 0; i < 2; i++)
{
    playerHand.AddCard(deck.DrawAvailableCard());
    dealerHand.AddCard(deck.DrawAvailableCard());
}

PlayerChoiceLoop(deck, playerHand);
DealerChoiceLoop(deck, dealerHand);

Console.Write("Player Hand: ");
for (int i = 0; i < playerHand.Size; i++)
{
    Console.Write(playerHand[i].FormatName() + (i + 1 == playerHand.Size ? " | " : ", "));
}
Console.WriteLine($"[{playerHand.Value}/" + (playerHand.IsHandBust ? "Bust]" : "Not Bust]"));

Console.Write("Dealer Hand: ");
for (int i = 0; i < dealerHand.Size; i++)
{
    Console.Write(dealerHand[i].FormatName() + (i + 1 == dealerHand.Size ? " | " : ", "));
}
Console.WriteLine($"[{dealerHand.Value}/" + (dealerHand.IsHandBust ? "Bust]" : "Not Bust]"));

GameOutcomes gameOutcome;
if (dealerHand.IsHandBust)
{
    gameOutcome = playerHand.IsHandBust ? GameOutcomes.Tie : GameOutcomes.PlayerWin;
} else if (playerHand.IsHandBust) {
    gameOutcome = GameOutcomes.DealerWin;
} else {
    if (playerHand.Value > dealerHand.Value) {
        gameOutcome = GameOutcomes.PlayerWin;
    } else if (playerHand.Value < dealerHand.Value) {
        gameOutcome = GameOutcomes.DealerWin;
    } else {
        gameOutcome = GameOutcomes.Tie;
    }
}

switch (gameOutcome) {
    case GameOutcomes.DealerWin: {
        Console.WriteLine("\nThe Dealer has won!");
        break;
    }

    case GameOutcomes.PlayerWin: {
        Console.WriteLine("\nCongratulations you have won!");
        break;
    }

    case GameOutcomes.Tie: {
        Console.WriteLine("\nTie! Nobody wins.");
        break; 
    }
    
    default:
        throw new ArgumentOutOfRangeException("gameOutcome",
            "Failed to determine this rounds winner!");
}