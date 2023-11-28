using MinesweeperApi.GameMechanics;

namespace MinesweeperApiTest;

[TestClass]
public class MinefieldBuilderTest
{
    [TestMethod]
    public void MinefieldBuilder_CheckMinesCount()
    {
        int width = 5;
        int height = 5;
        int minesCount = 7;

        var field = new MinefieldBuilder()
            .GenerateField(width, height)
            .SetMines(minesCount)
            .Build();

        var cells = field.SelectMany(row => row);

        Assert.AreEqual(cells.Count(c => c.IsMine), minesCount);
    }
}