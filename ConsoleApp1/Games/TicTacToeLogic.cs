namespace GameAccount
{
    public class TicTacToeGame
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';

        public TicTacToeGame()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            char currentNumber = '1';

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = currentNumber;
                    currentNumber++;
                }
            }
        }

        public bool MakeMove(int moveNumber)
        {
            int row = -1;
            int column = -1;

            switch (moveNumber)
            {
                case 1:
                    row = 0;
                    column = 0;
                    break;
                case 2:
                    row = 0;
                    column = 1;
                    break;
                case 3:
                    row = 0;
                    column = 2;
                    break;
                case 4:
                    row = 1;
                    column = 0;
                    break;
                case 5:
                    row = 1;
                    column = 1;
                    break;
                case 6:
                    row = 1;
                    column = 2;
                    break;
                case 7:
                    row = 2;
                    column = 0;
                    break;
                case 8:
                    row = 2;
                    column = 1;
                    break;
                case 9:
                    row = 2;
                    column = 2;
                    break;
                default:
                    return false;
            }

            if (board[row, column] == 'X' || board[row, column] == 'O')
            {
                return false;
            }

            board[row, column] = currentPlayer;
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            return true;
        }

        public char CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return board[i, 0];
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return board[0, j];
                }
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return board[0, 0];
            }

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return board[0, 2];
            }

            return '.';
        }

        public bool IsBoardFull()
        {
            foreach (var cell in board)
            {
                if (cell != 'X' && cell != 'O')
                {
                    return false;
                }
            }

            return true;
        }

        public void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public char CurrentPlayer
        {
            get { return currentPlayer; }
        }
    }
}
