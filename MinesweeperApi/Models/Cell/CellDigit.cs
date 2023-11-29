namespace MinesweeperApi.Models;

public class CellDigit : Cell
{
    public CellDigit(int digit) : base(ToChar(digit)) { }

    private static char ToChar(int digit)
    {
        if (digit < 0 || digit > 8)
        {
            throw new ArgumentException("Invalid cell digit value", nameof(digit));
        }
        return (char)(digit + 48);
    }

    public bool NoMinesAround => Value == CellValue.Zero;
}
