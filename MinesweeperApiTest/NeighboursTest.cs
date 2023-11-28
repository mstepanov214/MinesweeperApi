using MinesweeperApi.Extensions;

namespace MinesweeperApiTest;

[TestClass]
public class NeighboursTest
{
    [TestMethod]
    public void TwoDList_GetNeighbourIndexes()
    {
        List<List<int>> field = [
            [1, 2, 3],
            [4, 5, 6],
            [7, 8, 9]
        ];

        var neighbours = field.GetNeighbourIndexes(0, 0);

        Assert.AreEqual(neighbours.Length, 3);
        CollectionAssert.Contains(neighbours, (0, 1));
        CollectionAssert.Contains(neighbours, (1, 1));
        CollectionAssert.Contains(neighbours, (1, 0));
    }
}
