
namespace wumpus;

public class Game{
   // Loc are simply the room numbers (1 to roomCount)
   public int roomCount{get;}
   // The outer and inner layer sizes will always be the same
   private readonly int OUTER_LAYER_SIZE;
   private readonly int MIDDLE_LAYER_SIZE;
   public int WumpusLoc{get;set;}
   public int PlayerLoc{get;set;}
   private RoomLayer currentLayer;
   public List<int> Bats{get;set;} = new();
   public const int TOTAL_TRAPS = 2;
   public List<int> Traps {get;set;} = new();
   public List<int> Rooms  = new();
   private List<int> Outer = new();
   private List<int> Inner = new();
   private List<int> Middle = new();
   
   private enum RoomLayer{
      outer,
      middle,
      inner
   }

   public Game(int? outerLayerSize = 5, string? roomFile=null){
      //initialize the game
      //Set the total number of rooms based on layer sizes
      OUTER_LAYER_SIZE = outerLayerSize ?? 5;
      MIDDLE_LAYER_SIZE = OUTER_LAYER_SIZE * 2;
      roomCount = 2 * MIDDLE_LAYER_SIZE;
      Console.WriteLine("Starting game...");
      RandomizeRooms(roomFile);
      Console.WriteLine("Setting locations of game objects & player");
      SetRandomLocations();
      DisplayLocations();
      CheckPlayerLoc();
      CheckForHazards();
   }

   public void CheckPlayerLoc(){
      Console.WriteLine("####### Location ########");
      Console.WriteLine($"### You are in room {PlayerLoc}. ###");
      Console.WriteLine($"Tunnels lead to {string.Join(",",GetConnectedRooms(PlayerLoc))}");
   }

   public bool MovePlayerToRoom(int targetRoom){
      if (GetConnectedRooms(PlayerLoc).Contains(targetRoom)){
         PlayerLoc = targetRoom;
         CheckPlayerLoc();
         return true;
      }
      return false;
   }

   private void CheckForHazards(){
   }

   private List<int> GetConnectedRooms(int location){
      var currentLayer = GetRoomLayer(location);
      Console.WriteLine($"PlayerLoc currentLayer: {currentLayer}");
      List<int> connectedRooms = new();
      var roomIndex = -1;
      switch (currentLayer)
      {
         // Inner and Outer work the 
         case RoomLayer.inner:
         case RoomLayer.outer:{
            roomIndex = GetRoomIndex(location);
            var left = roomIndex -1;
            if (left < 0){ left = OUTER_LAYER_SIZE-1;}
            var right = roomIndex +1;
            if (right >= OUTER_LAYER_SIZE){ right = 0;}
            Console.WriteLine($"roomIndex: {roomIndex} left: {left} right: {right}");            
            if (currentLayer == RoomLayer.inner){
               connectedRooms.Add(Inner.ToArray()[left]);
               connectedRooms.Add(Inner.ToArray()[right]);
               var targetIdx = (roomIndex * 2)+1 >= MIDDLE_LAYER_SIZE ? MIDDLE_LAYER_SIZE -1 : (roomIndex * 2)+1; 
               connectedRooms.Add(Middle.ToArray()[targetIdx] ); 
               return connectedRooms;
            }
            connectedRooms.Add(Outer.ToArray()[left]);
            connectedRooms.Add(Outer.ToArray()[right]);
            connectedRooms.Add(Middle.ToArray()[roomIndex * 2]); 
            return connectedRooms;
            
            break;
         }
        case RoomLayer.middle:{
            roomIndex = GetRoomIndex(location);
            var left = roomIndex -1;
            if (left < 0){ left = MIDDLE_LAYER_SIZE-1;}
            var right = roomIndex +1;
            if (right >= MIDDLE_LAYER_SIZE){ right = 0;}

            connectedRooms.Add(Middle.ToArray()[left]);
            connectedRooms.Add(Middle.ToArray()[right]);
            if (roomIndex % 2 == 0){
               var targetIdx = (roomIndex / 2);
               connectedRooms.Add(Outer.ToArray()[targetIdx]);
            }
            else{
               // 1 3 5 7 9
               var targetIdx = ((roomIndex +1)/2) -1;
               Console.WriteLine($"targetIdx: {targetIdx}");
               connectedRooms.Add(Inner.ToArray()[targetIdx]);
            }
           return connectedRooms;   
            break;
         }
      }
      return connectedRooms;
   }

   private RoomLayer GetRoomLayer(int location){
      // Calculates the layer that the room is in
      // This is part of determining how the bats & player can move
      if (Outer.Contains(location)){return RoomLayer.outer;}
      if (Inner.Contains(location)){return RoomLayer.inner;}
      if (Middle.Contains(location)){return RoomLayer.middle;}
      // This last return will NOT ever be hit
      return RoomLayer.inner;
   }

   private int GetRoomIndex(int location){
      // Calculates the layer that the room is in
      // This is part of determining how the bats & player can move
      if (Outer.Contains(location)){
         for (int i =0;i<Outer.Count;i++){
            if (Outer.ToArray()[i] == location){ return i;}
         }
      }
      if (Inner.Contains(location)){
         for (int i =0;i<Inner.Count;i++){
            if (Inner.ToArray()[i] == location){ return i;}
         }
      }
      if (Middle.Contains(location)){
         for (int i =0;i<Middle.Count;i++){
            if (Middle.ToArray()[i] == location){ return i;}
         }
      }
      // this last return will never be hit
      return -1;
   }

   private void SetRandomLocations(){
      Random rnd = new();
      // Set Bat's initial location
      Bats.Add(rnd.Next(1,roomCount+1));
      // This adds the 2nd bat and insures it's not in same room as 1st one
      while (Bats.Count() < 2){
         var batLoc = rnd.Next(1,roomCount+1);
         if (!Bats.Contains(batLoc)){ Bats.Add(batLoc);}
      }
   
      // Setup Rule: The Player may be initially located at the same
      // room as the bat, but neither bat nor player may be 
      // located initially in a trap room
      PlayerLoc = rnd.Next(1, roomCount+1);

      // Set Trap locations - 2 of them - can't be the same as the bat or other trap
      for (int traps =0; traps < TOTAL_TRAPS; traps++){
         while (Traps.Count() <= traps){
            var loc = rnd.Next(1, roomCount+1);
            if (Bats.Contains(loc) || PlayerLoc == loc){Console.WriteLine("Oops! same location!"); continue;} // it was same as bat location so try again
            if (!Traps.Contains(loc)){
               Traps.Add(loc);
            }
         }
      }
   }

   private void DisplayLocations(){
      Console.WriteLine($"Bat location: {string.Join(",",Bats)}");
      Console.WriteLine($"Player location: {PlayerLoc}");
      Console.WriteLine($"Trap locations: {string.Join(",",Traps)}");
   }

   async private void RandomizeRooms(string? roomFile=null){
      // Load Rooms (layers) from file
      if (roomFile != null){
        try{
           string [] allFileRooms = File.ReadAllLines(roomFile);
           var lineCount = 0;
           foreach (string line in allFileRooms){
              string [] rooms = line.Split(",",StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
              foreach (string room in rooms){
                 switch (lineCount){
                    case 0:{
                       Outer.Add(Convert.ToInt32(room));
                       break;
                    }
                   case 1:{
                      Middle.Add(Convert.ToInt32(room));
                      break;
                   }
                   case 2:{
                      Inner.Add(Convert.ToInt32(room));
                      break;
                   }
                 }
              }
              lineCount++;
           }
           // Rooms are loaded from file, return & do no more work in this method
           return;
        }
        catch(Exception ex){
           Console.WriteLine($"Error loading file! {ex.Message}");
        }

      }
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
               case int n when n >= 0 && n <= OUTER_LAYER_SIZE -1:{
                  Outer.Add(randVal);
                  break;
               } 
               case int n when n >= OUTER_LAYER_SIZE && n <= (OUTER_LAYER_SIZE*2)-1:{
                  Inner.Add(randVal);
                  break;
               } 
               case int n when n >= (OUTER_LAYER_SIZE*2) && n <= (roomCount -1 ):{
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
