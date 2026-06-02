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
    
    [Fact]
    public void TestPlayerLocs()
    {

      var rootPath = AppContext.BaseDirectory;
      var targetFile = "5x10x5.dat";
      Game g = new (5, Path.Combine(rootPath,targetFile));
      g.PlayerLoc = 1;
      g.CheckPlayerLoc();
      g.PlayerLoc = 5;
      g.CheckPlayerLoc();
      g.PlayerLoc = 6;
      g.CheckPlayerLoc();
      g.PlayerLoc = 15;
      g.CheckPlayerLoc();
      g.PlayerLoc = 16;
      g.CheckPlayerLoc();
      g.PlayerLoc = 20;
      g.CheckPlayerLoc();
    }

}
