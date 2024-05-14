using System;

struct Maze
{
    static char[,] maze;
    static int playerx;
    static int playery;
    static int exitx;
    static int exity;
    static int moveCounter;

    static void Main(string[] args)
    {
        bool restart = true;

        while (restart)
        {
            InitializeLevel();
            ConsoleKeyInfo key;

            while (moveCounter > 0)
            {
                Console.Clear();
                ShowMaze();

                key = Console.ReadKey();
                PlayerMove(key);

                if (playerx == exitx && playery == exity)
                {
                    Console.Clear();
                    Console.Beep();
                    Console.WriteLine("CONGRATS -- YOU WON !!!");

                    if (moveCounter <= 0)
                    {
                        Console.Beep();
                        Console.WriteLine("You reached the maximum difficulty. You win!");
                        break;
                    }

                    Console.WriteLine($"You won with: {moveCounter} moves left !");
                    Console.WriteLine("Press any key to continue to the next level...");
                    Console.ReadKey();
                    break;
                }
            }

            if (moveCounter <= 0)
            {
                Console.Beep(500, 500);
                Console.WriteLine("You ran out of moves. Do you want to retry? (y/n)");

                char retryKey = char.ToLower(Console.ReadKey().KeyChar);

                if (retryKey == 'y')
                {
                    restart = true;
                }
                else
                {
                    restart = false;
                    Console.Beep(1000, 500);
                    Console.WriteLine("Exiting game. Goodbye!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("DESIGNED AND CREATED BY; \n             RAWLINGS");
                    Console.ResetColor();
                }
            }
        }
    }

    static void InitializeLevel()
    {
        Random random = new Random();

        // Initialize maze size
        int rows = 15;
        int columns = 20;

        maze = new char[rows, columns];

        // Reset player's position
        playerx = random.Next(1, columns - 1);
        playery = random.Next(1, rows - 1);

        // Reset exit position
        exitx = random.Next(1, columns - 1);
        exity = random.Next(1, rows - 1);

        // Reset move counter
        moveCounter = 30 - 1;

        // Generate maze
        GenerateRandomMaze();
    }

    static void GenerateRandomMaze()
    {
        Random random = new Random();

        // Generate outer walls
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            maze[i, 0] = '█';         // Left wall
            maze[i, maze.GetLength(1) - 1] = '█'; // Right wall
        }

        for (int j = 0; j < maze.GetLength(1); j++)
        {
            maze[0, j] = '█';            // Top wall
            maze[maze.GetLength(0) - 1, j] = '█'; // Bottom wall
        }

        // Generate inner walls
        for (int i = 1; i < maze.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < maze.GetLength(1) - 1; j++)
            {
                if (random.Next(100) < 30)
                {
                    maze[i, j] = '█'; // Wall
                }
                else
                {
                    maze[i, j] = ' ';
                }
            }
        }

        // Place exit in a walkable position
        PlaceExitInWalkablePosition(random);
    }

    static void PlaceExitInWalkablePosition(Random random)
    {
        int maxAttempts = 100;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            exitx = random.Next(1, maze.GetLength(1) - 1);
            exity = random.Next(1, maze.GetLength(0) - 1);

            if (maze[exity, exitx] == ' ')
            {
                maze[exity, exitx] = '▒'; // Set exit after creating the maze
                return;
            }

            attempts++;
        }

    }

    static void ShowMaze()
    {
        Console.WriteLine($"Moves left: {moveCounter}");
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (i == playery && j == playerx)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('P');
                }
                else if (i == exity && j == exitx)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('▒');
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(maze[i, j]);
                }
            }
            Console.WriteLine();
        }
    }

    static void PlayerMove(ConsoleKeyInfo key)
    {
        int newplayerx = playerx;
        int newplayery = playery;

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                newplayery--;
                break;
            case ConsoleKey.LeftArrow:
                newplayerx--;
                break;
            case ConsoleKey.DownArrow:
                newplayery++;
                break;
            case ConsoleKey.RightArrow:
                newplayerx++;
                break;
        }

        if (newplayerx >= 0 && newplayery >= 0 && newplayerx < maze.GetLength(1) && newplayery < maze.GetLength(0) && maze[newplayery, newplayerx] != '█')
        {
            playerx = newplayerx;
            playery = newplayery;
             // Decrease move counter on successful move
            //Console.Beep(100, 100);
        }
    }
}
