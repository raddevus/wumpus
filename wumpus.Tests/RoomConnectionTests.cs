using wumpus;
namespace wumpus.Tests;

public class RoomConnectionTests 
{
    [Fact]
    public void TestSize5_10_5()
    {

      var rootPath = AppContext.BaseDirectory;
      var targetFile = "5x10x5.dat";
      Game g = new (5, Path.Combine(rootPath,targetFile));
    }
}
