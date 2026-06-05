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

    [Fact]
    public void TestAllPlayerLocs()
    {
      // ###########################
      // Tests every room in the file
      // ###########################
      var rootPath = AppContext.BaseDirectory;
      var targetFile = "5x10x5.dat";
      Game g = new (5, Path.Combine(rootPath,targetFile));
      g.PlayerLoc = 1;
      g.CheckPlayerLoc();
      g.PlayerLoc = 2;
      g.CheckPlayerLoc();
      g.PlayerLoc = 3;
      g.CheckPlayerLoc();
      g.PlayerLoc = 4;
      g.CheckPlayerLoc();
      g.PlayerLoc = 5;
      g.CheckPlayerLoc();
      g.PlayerLoc = 6;
      g.CheckPlayerLoc();
      g.PlayerLoc = 7;
      g.CheckPlayerLoc();
      g.PlayerLoc = 8;
      g.CheckPlayerLoc();
      g.PlayerLoc = 9;
      g.CheckPlayerLoc();
      g.PlayerLoc = 10;
      g.CheckPlayerLoc();
      g.PlayerLoc = 11;
      g.CheckPlayerLoc();
      g.PlayerLoc = 12;
      g.CheckPlayerLoc();
      g.PlayerLoc = 13;
      g.CheckPlayerLoc();
      g.PlayerLoc = 14;
      g.CheckPlayerLoc();
      g.PlayerLoc = 15;
      g.CheckPlayerLoc();
      g.PlayerLoc = 16;
      g.CheckPlayerLoc();
      g.PlayerLoc = 17;
      g.CheckPlayerLoc();
      g.PlayerLoc = 18;
      g.CheckPlayerLoc();
      g.PlayerLoc = 19;
      g.CheckPlayerLoc();
      g.PlayerLoc = 20;
      g.CheckPlayerLoc();
    }

    [Fact]
    public void TestAllPlayerLocs3x6()
    {
      // ###########################
      // Tests every room in the file
      // ###########################
      var rootPath = AppContext.BaseDirectory;
      var targetFile = "3x6.dat";
      Game g = new (3, Path.Combine(rootPath,targetFile));
      g.PlayerLoc = 1;
      g.CheckPlayerLoc();
      g.PlayerLoc = 2;
      g.CheckPlayerLoc();
      g.PlayerLoc = 3;
      g.CheckPlayerLoc();
      g.PlayerLoc = 4;
      g.CheckPlayerLoc();
      g.PlayerLoc = 5;
      g.CheckPlayerLoc();
      g.PlayerLoc = 6;
      g.CheckPlayerLoc();
      g.PlayerLoc = 7;
      g.CheckPlayerLoc();
      g.PlayerLoc = 8;
      g.CheckPlayerLoc();
      g.PlayerLoc = 9;
      g.CheckPlayerLoc();
      g.PlayerLoc = 10;
      g.CheckPlayerLoc();
      g.PlayerLoc = 11;
      g.CheckPlayerLoc();
      g.PlayerLoc = 12;
      g.CheckPlayerLoc();

    }
}
