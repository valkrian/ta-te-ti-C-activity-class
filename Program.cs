using System;

class Program
{
    static char[,] board = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };

    static int currentPlayer = 1; // 1 = X, 2 = O
    static int moveCount = 0;

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            DisplayBoard();

            char playerSymbol = (currentPlayer == 1) ? 'X' : 'O';
            Console.Write($"\nPlayer {currentPlayer} ({playerSymbol}), choose a position (1-9): ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice) || !PlaceSymbol(choice, playerSymbol))
            {
                Console.WriteLine("Invalid move. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            moveCount++;

            if (CheckWin())
            {
                Console.Clear();
                DisplayBoard();
                Console.WriteLine($"\n🎉 Player {currentPlayer} ({playerSymbol}) wins!");
                break;
            }
            else if (moveCount == 9)
            {
                Console.Clear();
                DisplayBoard();
                Console.WriteLine("\n🤝 It's a draw!");
                break;
            }

            currentPlayer = (currentPlayer == 1) ? 2 : 1;
        }

        Console.WriteLine("\nGame over. Press Enter to exit.");
        Console.ReadLine();
    }

    static void DisplayBoard()
    {
        Console.WriteLine("=== Tic Tac Toe ===\n");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($" {board[i, 0]} | {board[i, 1]} | {board[i, 2]} ");
            if (i < 2) Console.WriteLine("---|---|---");
        }
    }

    static bool PlaceSymbol(int pos, char symbol)
    {
        int row = (pos - 1) / 3;
        int col = (pos - 1) % 3;

        if (pos < 1 || pos > 9 || board[row, col] == 'X' || board[row, col] == 'O')
            return false;

        board[row, col] = symbol;
        return true;
    }

    static bool CheckWin()
    {
        // Check rows and columns
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) ||
                (board[0, i] == board[1, i] && board[1, i] == board[2, i]))
                return true;
        }

        // Check diagonals
        return (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) ||
               (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]);
    }
}