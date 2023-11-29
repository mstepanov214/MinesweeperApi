using MinesweeperApi.Models;

namespace MinesweeperApiTest;

[TestClass]
public class CellTest
{
    [TestMethod]
    public void Cell_Create()
    {
        Assert.AreEqual(new CellEmpty().Value, ' ');
        Assert.AreEqual(new CellX().Value, 'X');
        Assert.AreEqual(new CellM().Value, 'M');
        Assert.AreEqual(new CellDigit(5).Value, '5');
    }

    [TestMethod]
    public void Cell_Reveal()
    {
        var cell = new CellX();

        Assert.IsFalse(cell.Revealed);
        Assert.AreEqual(cell.DisplayedValue, ' ');

        cell.Reveal();

        Assert.IsTrue(cell.Revealed);
        Assert.AreEqual(cell.DisplayedValue, 'X');
    }

    [TestMethod]
    public void Cell_IsMine()
    {
        Assert.IsTrue(new CellX().IsMine);
        Assert.IsFalse(new CellDigit(0).IsMine);
    }

    [TestMethod]
    public void CellDigit_FromInt_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => new CellDigit(9));
        Assert.ThrowsException<ArgumentException>(() => new CellDigit(-1));
    }
}
