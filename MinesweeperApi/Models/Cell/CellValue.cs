namespace MinesweeperApi.Models;

internal static class CellValue
{
    /// <summary>
    /// Empty cell
    /// </summary>
    public const char Empty = ' ';

    /// <summary>
    /// Cell with no mines around
    /// </summary>
    public const char Zero = '0';

    /// <summary>
    /// Mine cell
    /// </summary>
    public const char X = 'X';

    /// <summary>
    /// Marked mine cell (after victory)
    /// </summary>
    public const char M = 'M';
}
