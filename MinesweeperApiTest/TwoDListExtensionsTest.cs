using MinesweeperApi.Extensions;

namespace MinesweeperApiTest;

[TestClass]
public class TwoDListExtensionsTest
{
    private static readonly List<List<int>> _twoDList = [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9]
    ];

    [TestMethod]
    public void TwoDList_GetNeighbourIndexes()
    {
        var neighbours = _twoDList.GetNeighbourIndexes(0, 0);

        Assert.AreEqual(neighbours.Length, 3);
        CollectionAssert.Contains(neighbours, (0, 1));
        CollectionAssert.Contains(neighbours, (1, 1));
        CollectionAssert.Contains(neighbours, (1, 0));
    }

    [TestMethod]
    public void TwoDList_TotalCount()
    {
        Assert.AreEqual(_twoDList.TotalCount(), 9);
        Assert.AreEqual(_twoDList.TotalCount(d => d > 4), 5);
    }
}
