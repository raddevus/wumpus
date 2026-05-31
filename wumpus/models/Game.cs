
namespace wumpus;

class Game{
   // Loc are simply the room numbers (1 to roomCount)
   private int roomCount = 20;
   public int WumpusLoc{get;set;}
   public int PlayerLoc{get;set;}
   public int BatLoc{get;set;}
   public int[] Traps {get;set;} = new int[2];
   public List<int> Rooms  = new();
   private List<int> Outer = new();
   private List<int> Inner = new();
   private List<int> Middle = new();

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

      // mix them up
      Random rnd = new();
      int counter = 0;
      while (allRooms.Count > 0){
         var randVal = rnd.Next(1,roomCount+1);
         var result = allRooms.Remove(randVal);
         if (result){
            Rooms.Add(randVal);
            switch (counter++){
               case int n when n >= 0 && n <=4:{
                  Outer.Add(randVal);
                  break;
               } 
               case int n when n >= 5 && n <= 9:{
                  Inner.Add(randVal);
                  break;
               } 
               case int n when n >= 10 && n <= 19:{
                  Middle.Add(randVal);
                  break;
               } 
            }
         }

      }
      DisplayRooms(Rooms);
   }
   
   private void DisplayRooms(List<int> rooms){

      string roomList = string.Empty;
/*      foreach (int x in rooms){
         roomList += $"{x},";
      }

      Console.WriteLine($"{roomList}");
*/
      roomList = string.Empty;
      foreach (int x in Outer){
         roomList += $"{x},";
      }

      Console.WriteLine($"{roomList}");

      roomList = string.Empty;
      foreach (int x in Middle){
         roomList += $"{x},";
      }

      Console.WriteLine($"{roomList}");
   
      roomList = string.Empty;
      foreach (int x in Inner){
         roomList += $"{x},";
      }

      Console.WriteLine($"{roomList}");
   }
}
