
namespace wumpus;

class Game{
   // Loc are simply the room numbers (1 to roomCount)
   private int roomCount = 20;
   public int WumpusLoc{get;set;}
   public int PlayerLoc{get;set;}
   public int BatLoc{get;set;}
   public int[] Traps {get;set;} = new int[2];
   public List<int> Rooms  = new();

   public Game(){
      //initialize the game
      Console.WriteLine("Starting game...");
      RandomizeRooms();
   }

   private void RandomizeRooms(){
      List<int> allRooms = new();
      // add all rooms
      for (int x = 1; x <= roomCount;x++){
         allRooms.Add(x);
      }
      DisplayRooms(allRooms);
      // mix them up
      Random rnd = new();
      while (allRooms.Count > 0){
         var randVal = rnd.Next(1,roomCount+1);
         var result = allRooms.Remove(randVal);
         if (result){
            Rooms.Add(randVal);
         }
      }
      DisplayRooms(Rooms);
   }
   
   private void DisplayRooms(List<int> rooms){

      string roomList = string.Empty;
      foreach (int x in rooms){
         roomList += $"{x},";
      }

      Console.WriteLine($"{roomList}");
   }
}
