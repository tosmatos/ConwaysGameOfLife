using ConwaysGameOfLife;

bool stop = false;
const int MAX_ROWS = 30, MAX_COLUMNS = 60;
int[,] grid = new int[MAX_ROWS, MAX_COLUMNS];
int generation = 0;

Array.Copy(new StarterArrays().starterArray1, grid, MAX_COLUMNS * MAX_ROWS);
Console.CursorVisible = false;
Console.WindowWidth = MAX_COLUMNS * 2;
Console.WindowHeight = MAX_ROWS * 2;
Thread.Sleep(1000);

while (!stop)
{
	DisplayGrid();
	Thread.Sleep(1000);
	int[,] newGrid = NextIteration();
	grid = new int[MAX_ROWS, MAX_COLUMNS];  
	Array.Copy(newGrid, grid, MAX_ROWS * MAX_COLUMNS);
	generation++;
}

void DisplayGrid()
{
	Console.WriteLine($"Generation {generation}");
	for (int row = 0; row < MAX_ROWS; row++)
	{
		for (int column = 0; column < MAX_COLUMNS; column++)
		{
			Console.SetCursorPosition(column, row);
			Console.Write(grid[row, column] == 1 ? "O" : " ");
		}
		Console.WriteLine();
	}
}

int[,] NextIteration()
{
	int[,] nextGrid = new int[MAX_ROWS, MAX_COLUMNS];

	for (int row = 0; row < MAX_ROWS; row++)
	{
		for (int column = 0; column < MAX_COLUMNS; column++)
		{
			int neighbors = GetNumberOfNeighbors(row, column);

			//Console.WriteLine($"Grid value at ({row}, {column}) before update: {grid[row, column]}");
			//Console.WriteLine($"Number of neighbors at ({row}, {column}): {neighbors}");

			// if current cell is alive
			if (grid[row, column] == 1)
			{
				// Cell is alive
				nextGrid[row, column] = (neighbors == 2 || neighbors == 3) ? 1 : 0;
			}
			else
			{
				nextGrid[row, column] = (neighbors == 3) ? 1 : 0;
			}
		}
	}
	return nextGrid;
}

int GetNumberOfNeighbors(int row, int column)
{
	int neighbors = 0;

	// Loop over neighboring cells in a 3x3 grid around the cell at position (x,y)
	for (int dx = (row > 0 ? -1 : 0); dx <= (row == MAX_ROWS - 1 ? 0 : 1); dx++)
	{
		for (int dy = (column > 0 ? -1 : 0); dy <= (column == MAX_COLUMNS - 1 ? 0 : 1); dy++)
		{
			// if center cell
			if (dx == 0 && dy == 0)
				continue;

			int neighborRow = row + dx;
			int neighborColumn = column + dy;

			// if cell not dead
			if (grid[neighborRow, neighborColumn] == 1)
				neighbors++;

		}
	}
	return neighbors;
}