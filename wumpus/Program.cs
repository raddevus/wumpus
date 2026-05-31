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
