using wumpus;

Console.WriteLine("Hello, World!");
int? outerLayerSize = null;
string roomFile = null;

if (args.Count() > 0){
   string output = string.Empty;
   foreach (string s in args){
      output += $"{s},";
   }
   Console.WriteLine($"args: {output}");
   outerLayerSize = Convert.ToInt32(args[0]);
   roomFile = args[1];
}
// Now you can start the game with a path to a roomFile
// If the file is loaded from args[1] then it will be used as the 
// set of rooms in the game
Game g = new(outerLayerSize, roomFile);
bool exitGame = false;

while (!exitGame){
   
   var userChoice = GetUserChoice();
   switch (userChoice){
      case -1:{
         Environment.Exit(0);
         break;
      }
      case 0:{
         // user made invalid choice, try again
         break;
      }
      case int n when n > 0 && n < g.roomCount :{
         Console.WriteLine($"roomCount: {g.roomCount} - choice: {userChoice}");
         if (!g.MovePlayerToRoom(userChoice)){
            Console.WriteLine($"{userChoice} is not a valid room from here.");
            g.CheckPlayerLoc();
         }
         break;
      }
      default:{
         Console.WriteLine("It seems like you've entered an invalid room number.");
         g.CheckPlayerLoc();
         break;
      }
   }
}

int GetUserChoice(){
   Console.WriteLine("What would you like to do?");
   Console.WriteLine("Q <ENTER>: Quit");
   Console.WriteLine("Room Number: Move");
   try {
      var userInput = Console.ReadLine();
      if (userInput.ToUpper() == "Q"){ return -1; } //quit
      return Convert.ToInt32(userInput);
   }
   catch (Exception ex){
      Console.WriteLine("Invalid input! Try again. Either Q or a Room number.");
      return 0;
   }
}
