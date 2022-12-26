using System;
namespace TicTacToe
{
    internal static class Program
    {
        enum CellState
        {
            EMPTY = 0,
            X = 1,
            O = 2
        }
        enum GameState
        {
            xMove = 1,
            oMove = 2,
            xWin = 3,
            oWin = 4,
            draw = 5
        }

        private static GameState now;

        private static readonly CellState[,] gameBoard = new CellState[3,3];

        private static void CreatingEmptyBoard()
        {
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    gameBoard[i,j] = CellState.EMPTY;
                }
            }
        }

        private static void PrintBoard()
        {
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    switch(gameBoard[i,j])
                    {
                        case CellState.X:
                            Console.Write("|x");
                            break;
                        case CellState.O:
                            Console.Write("|o");
                            break;
                        case CellState.EMPTY:
                            Console.Write("| ");
                            break;
                    }
                }
                Console.WriteLine("|");
            }
        }

        private static void CheckVerticals()
        {
            for(var i = 0; i < 3; i++)
            {
                if(gameBoard[i,0] == gameBoard[i,1] && gameBoard[i,1] == gameBoard[i,2] && gameBoard[i,2] != CellState.EMPTY)
                {
                    switch(now)
                    {
                        case GameState.xMove:
                            now = GameState.xWin;
                            break;
                        case GameState.oMove:
                            now = GameState.oWin;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void CheckHorizontals()
        {
            for(var i = 0; i < 3; i++)
            {
                if(gameBoard[0,i] == gameBoard[1,i] && gameBoard[1,i] == gameBoard[2,i] && gameBoard[2,i] != CellState.EMPTY)
                {
                    switch(now)
                    {
                        case GameState.xMove:
                            now = GameState.xWin;
                            break;
                        case GameState.oMove:
                            now = GameState.oWin;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void CheckDiagonals()
        {
            if((gameBoard[0,0] == gameBoard[1,1] && gameBoard[1,1] == gameBoard[2,2] && gameBoard[2,2] != CellState.EMPTY) || (gameBoard[0,2] == gameBoard[1,1] && gameBoard[1,1] == gameBoard[2,0] && gameBoard[2,0] != CellState.EMPTY))
            {
                switch(now)
                    {
                        case GameState.xMove:
                            now = GameState.xWin;
                            break;
                        case GameState.oMove:
                            now = GameState.oWin;
                            break;
                        default:
                            break;
                    }
            }
        }

        private static void CheckDraw()
        {
             for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    if(gameBoard[i, j] == CellState.EMPTY || now == GameState.xWin || now == GameState.oWin)
                        return;
                }
            }
            now = GameState.draw;
        }

        private static void UserMove()
        {
            while(true)
            {
                Console.WriteLine("Write coordinates");
                Console.Write(">>> ");
                var userInput = Console.ReadLine();
                var splited = userInput!.Split();
                if(splited.Length != 2)
                {
                    Console.WriteLine("Incorrect Input");
                    continue;
                }
                var tryVertical = int.TryParse(splited[0], out var vertical);
                var tryHorizontal = int.TryParse(splited[1], out var horizontal);
                if(!tryVertical && !tryHorizontal)
                {
                    Console.WriteLine("Incorrect Input");
                    continue;
                }
                else if(horizontal > 3 || horizontal < 1 || vertical > 3 || vertical < 1)
                {
                    Console.WriteLine("Incorrect Input");
                    continue;
                }
                else if(gameBoard[vertical-1,horizontal-1] != CellState.EMPTY)
                {
                    Console.WriteLine("Sorry, you use this cell");
                    continue;
                }
                switch(now)
                {
                    case GameState.xMove:
                        gameBoard[vertical-1, horizontal-1] = CellState.X;
                        break;
                    case GameState.oMove:
                        gameBoard[vertical-1, horizontal-1] = CellState.O;
                        break;
                }
                return;
            }
        }

        private static void GameLoop()
        {
            CreatingEmptyBoard();
            
            now = GameState.xMove;

            while(true)
            {
                PrintBoard();

                UserMove();

                CheckVerticals();

                CheckHorizontals();
                
                CheckDiagonals();

                CheckDraw();

                switch(now)
                {
                    case GameState.xMove:
                        now = GameState.oMove;
                        break;
                    case GameState.oMove:
                        now = GameState.xMove;
                        break;
                    case GameState.xWin:
                        PrintBoard();
                        Console.WriteLine("X win");
                        return;
                    case GameState.oWin:
                        PrintBoard();
                        Console.WriteLine("O win");
                        return;
                    case GameState.draw:
                        PrintBoard();
                        Console.WriteLine("Ooops, draw :(");
                        return;
                }
            }
        }

        public static void Main()
        {
            Console.WriteLine("Hello, it's TicTacToe");
            Console.WriteLine("I think you know the rules");

            while(true)
            {
                Console.WriteLine("Do you want start?(yes or no)");
                Console.Write(">>> ");
                var userInput = Console.ReadLine();
                if(userInput!.ToLower() == "yes")
                    GameLoop();
                else if(userInput.ToLower() == "no")
                    break;
                else
                    Console.WriteLine("Incorrect input");
            }
            Console.WriteLine("Goodbye <3");
        }
    }
}