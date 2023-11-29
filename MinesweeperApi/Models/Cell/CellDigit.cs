namespace MinesweeperApi.Models;

public class CellDigit : Cell
{
    public CellDigit(char charDigit) : base(charDigit)
    {
        ValidateDigitValue();
    }

    public CellDigit(int digit) : base((char)(digit + 48))
    {
        ValidateDigitValue();
    }

    public bool NoMinesAround => Value == CellValue.Zero;

    private void ValidateDigitValue()
    {
        if (Value < '0' || Value >= '9')
        {
            throw new ArgumentException("Invalid cell digit value");
        }
    }
}
