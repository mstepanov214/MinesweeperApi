using MinesweeperApi.Extensions;
using MinesweeperApi.GameMechanics;

namespace MinesweeperApiTest;

[TestClass]
public class MinefieldBuilderTest
{
    [TestMethod]
    [DataRow(10, 10, 5)]
    [DataRow(5, 7, 5)]
    [DataRow(4, 3, 4)]
    [DataRow(7, 5, 5)]
    [DataRow(5, 7, 5)]
    [DataRow(10, 9, 7)]
    [DataRow(6, 3, 1)]
    [DataRow(30, 20, 30)]
    public void MinefieldBuilder_CheckMinesCount(int width, int height, int minesCount)
    {
        var field = new MinefieldBuilder()
            .GenerateField(width, height)
            .SetMines(minesCount)
            .Build();

        Assert.AreEqual(field.TotalCount(c => c.IsMine), minesCount);
    }
}