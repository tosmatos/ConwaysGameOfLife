using ConwaysGameOfLife;

bool stop = false;
const int MAX_HEIGTH = 30, MAX_WIDTH = 60;
int[,] grid = new int[MAX_HEIGTH, MAX_WIDTH];
int generation = 0;

grid = new StarterArrays().starterArray1;
Console.CursorVisible = false;
Console.WindowWidth = MAX_WIDTH * 2;
Console.WindowHeight = MAX_HEIGTH * 2;
Thread.Sleep(1000);

while (!stop)
{
    DisplayGrid();
    Thread.Sleep(1000);
    grid = NextIteration();
    generation++;
}

void DisplayGrid()
{
    for (int i = 0; i < MAX_HEIGTH; i++)
    {
        for (int j = 0; j < MAX_WIDTH; j++)
        {
            Console.SetCursorPosition(j, i);
            Console.Write(grid[i, j] == 1 ? "O" : " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine($"Generation {generation}");
}

int[,] NextIteration()
{
    int[,] nextGrid = new int[MAX_HEIGTH, MAX_WIDTH];

    for (int y = 0; y < MAX_WIDTH; y++)
    {
        for (int x = 0; x < MAX_HEIGTH; x++)
        {
            int neighbors = GetNumberOfNeighbors(x, y);

            // if current cell is alive
            if (grid[x, y] == 1)
            {
                // Cell is alive
                nextGrid[x, y] = (neighbors == 2 || neighbors == 3) ? 1 : 0;
            }
            else
            {
                nextGrid[x, y] = (neighbors == 3) ? 1 : 0;
            }
        }
    }
    return nextGrid;
}

int GetNumberOfNeighbors(int x, int y)
{
    int neighbors = 0;

    // Loop over neighboring cells in a 3x3 grid around the cell at position (x,y)
    for (int dx = (x > 0 ? -1 : 0); dx <= (x == MAX_HEIGTH ? 0 : 1); dx++)
    {
        for (int dy = (y > 0 ? -1 : 0); dy <= (y == MAX_HEIGTH - 1 ? 0 : 1); dy++)
        {
            // if center cell
            if (dx == 0 && dy == 0)
                continue;

            int neighborX = (x + dx + MAX_HEIGTH) % MAX_HEIGTH;
            int neighborY = (y + dy + MAX_HEIGTH) % MAX_HEIGTH;

            // if cell not dead
            if (grid[neighborX, neighborY] == 1)
                neighbors++;

        }
    }
    return neighbors;
}