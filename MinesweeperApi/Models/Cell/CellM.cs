namespace MinesweeperApi.Models;

public class CellM : Cell
{
    public CellM() : base(CellValue.M)
    {
        Revealed = true;
    }
}