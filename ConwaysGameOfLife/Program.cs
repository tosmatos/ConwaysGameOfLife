using ConwaysGameOfLife;

bool stop = false;
int maxHeight = 30, maxWidth = 60;
int[,] grid = new int[maxHeight, maxWidth];

grid = new StarterArrays().starterArray1;
Console.CursorVisible = false;
Console.WindowWidth = maxWidth * 2;
Console.WindowHeight = maxHeight * 2;

while (!stop)
{
	Console.Clear();
	DisplayGrid();
	Thread.Sleep(1000);
	grid = NextIteration();
}

void DisplayGrid()
{
	for (int i = 0; i < maxHeight; i++)
	{
		for (int j = 0; j < maxWidth; j++)
		{
			Console.Write(grid[i, j] == 1 ? "O" : "X");
			//Console.SetCursorPosition(j, i);
			//Console.Write(grid[i, j] == 1 ? "O" : "X");
		}
		Console.WriteLine();
	}
}

int[,] NextIteration()
{
	int[,] nextField = new int[maxHeight, maxWidth];

	for (int y = 0; y < maxWidth; y++)
	{
		for (int x = 0; x < maxHeight; x++)
		{
			int neighbors = GetNumberOfNeighbors(x, y);

			// if current cell is alive
			if (grid[x, y] == 1)
			{
				if (neighbors < 2 || neighbors > 3)
				{
					nextField[x, y] = 0;
					continue;
				}

				if (neighbors == 2 || neighbors == 3)
				{
					nextField[x, y] = 1;
					continue;
				}
			}
			else
			{
				if (neighbors == 3)
				{
					nextField[x, y] = 1;
				}
			}
		}
	}

	return nextField;
}

int GetNumberOfNeighbors(int x, int y)
{
	int neighbors = 0;
	int n = maxWidth;

	// Loop over neighboring cells in a 3x3 grid around the cell at position (x,y)
	for (int dx = (x > 0 ? -1 : 0); dx <= (x < n - 1 ? 1 : 0); dx++)
	{
		for (int dy = (y > 0 ? -1 : 0); dy <= (y < n - 1 ? 1 : 0); dy++)
		{
			// if not center cell
			if (dx != 0 || dy != 0)
			{
				// if cell not dead
				if (grid[x + dx , y + dy] != 0)
					neighbors++;
			}
		}
	}
	return neighbors;
}