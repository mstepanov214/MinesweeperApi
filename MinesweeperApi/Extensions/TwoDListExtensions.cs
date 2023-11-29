namespace MinesweeperApi.Extensions;

public static class TwoDListExtensions
{
    public static (int x, int y)[] GetNeighbourIndexes<T>(this List<List<T>> field, int row, int col)
    {
        var possibleNeighbours = new (int x, int y)[] {
            (row - 1, col),
            (row + 1, col),
            (row, col + 1),
            (row, col - 1),
            (row - 1, col + 1),
            (row - 1, col - 1),
            (row + 1, col + 1),
            (row + 1, col - 1)
        };

        return possibleNeighbours
            .Where(n => field.IsValidNeighbour(n.x, n.y))
            .ToArray();
    }

    private static bool IsValidNeighbour<T>(this List<List<T>> field, int row, int col)
    {
        return row >= 0 &&
               row < field.Count &&
               col >= 0 &&
               col < field[row].Count;
    }

    public static int TotalCount<T>(this List<List<T>> field, Func<T, bool> predicate)
    {
        return field
            .SelectMany(row => row)
            .Count(predicate);
    }

    public static int TotalCount<T>(this List<List<T>> field)
    {
        return field
            .SelectMany(row => row)
            .Count();
    }
}
