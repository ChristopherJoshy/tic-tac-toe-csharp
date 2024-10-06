using System;

namespace TicTacToe
{
    class Program
    {
        static char[,] board = {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };

        static char currentMarker;
        static int currentPlayer;

        static void Main(string[] args)
        {
            StartGame();
        }

        static void StartGame()
        {
            Console.WriteLine("Player 1, choose your marker (X or O): ");
            currentMarker = Console.ReadLine().ToUpper()[0];

            while (currentMarker != 'X' && currentMarker != 'O')
            {
                Console.WriteLine("Invalid marker! Please choose X or O: ");
                currentMarker = Console.ReadLine().ToUpper()[0];
            }

            currentPlayer = 1;
            DrawBoard();
            PlayGame();
        }

        static void PlayGame()
        {
            int winner = 0;

            while (winner == 0 && !IsBoardFull())
            {
                Console.WriteLine($"Player {currentPlayer}'s turn. Enter a slot (1-9): ");
                bool validMove = int.TryParse(Console.ReadLine(), out int slot);

                if (!validMove || !MarkSlot(slot))
                {
                    Console.WriteLine("Invalid slot! Try again.");
                    continue;
                }

                DrawBoard();
                winner = CheckWinner();

                if (winner == 0)
                {
                    TogglePlayer();
                }
            }

            if (winner != 0)
            {
                Console.WriteLine($"Player {currentPlayer} wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }

        static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  {0} | {1} | {2} ", board[0, 0], board[0, 1], board[0, 2]);
            Console.WriteLine(" ---|---|---");
            Console.WriteLine("  {0} | {1} | {2} ", board[1, 0], board[1, 1], board[1, 2]);
            Console.WriteLine(" ---|---|---");
            Console.WriteLine("  {0} | {1} | {2} ", board[2, 0], board[2, 1], board[2, 2]);
        }

        static bool MarkSlot(int slot)
        {
            if (slot < 1 || slot > 9) return false;

            int row = (slot - 1) / 3;
            int col = (slot - 1) % 3;

            if (board[row, col] != 'X' && board[row, col] != 'O')
            {
                board[row, col] = currentMarker;
                return true;
            }
            return false;
        }

        static void TogglePlayer()
        {
            currentMarker = (currentMarker == 'X') ? 'O' : 'X';
            currentPlayer = (currentPlayer == 1) ? 2 : 1;
        }

        static int CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return currentPlayer;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return currentPlayer;
                }
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return currentPlayer;
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return currentPlayer;

            return 0;
        }

        static bool IsBoardFull()
        {
            foreach (char c in board)
            {
                if (c != 'X' && c != 'O') return false;
            }
            return true;
        }
    }
}
