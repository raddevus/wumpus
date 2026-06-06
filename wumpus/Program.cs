using wumpus;

Console.WriteLine("Hello, World!");
int? outerLayerSize = null;
if (args.Count() > 0){
   string output = string.Empty;
   foreach (string s in args){
      output += $"{s},";
   }
   Console.WriteLine($"args: {output}");
   outerLayerSize = Convert.ToInt32(args[0]);
}
Game g = new(outerLayerSize);
bool exitGame = false;

while (!exitGame){
   
   var userChoice = GetUserChoice();
   if (userChoice == -1){
      Environment.Exit(0);
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
