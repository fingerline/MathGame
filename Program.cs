List<string> gameTypes = new List<string> {"a","s","m","d","h"};
List<string> gameHistory = new List<string> {};

Console.WriteLine("Welcome to the Math Game! Here you will be presented a series of 5 questions of a mathematical operation of your choosing.");
DateTime currentDateTime = DateTime.Now;
Console.WriteLine($"It is currently {currentDateTime.ToString("t")} on {currentDateTime.ToString("d")}.");

do{
    string selectedGameMode = GameModeSelection();
    PlayGame(selectedGameMode);
} while (true);

string GameModeSelection(){
    string userMenuSelection = "";
    do {
        Console.WriteLine("\nPlease choose a gamemode to get started.\n"
                    + "A: Addition\n" 
                    + "S: Subtraction\n"
                    + "M: Multiplication\n"
                    + "D: Division\n"
                    + "H: Visualize Game History");
        userMenuSelection = Console.ReadLine().ToLower();
        if(userMenuSelection == "h"){
            VisualizeGameHistory();
            continue;
        } else if(!gameTypes.Contains(userMenuSelection)){
            Console.WriteLine("Please input a valid gametype selection.");
            continue;
        } else {
            return userMenuSelection;
        }        
    } while (true);
}

void VisualizeGameHistory(){
    if(gameHistory.Count == 0){
        Console.WriteLine("No history to display. Go play a game!");
        return;
    } else {
        Console.WriteLine("Games Played This Session:");
        foreach(string game in gameHistory){
            Console.WriteLine(game);
        }
    }
    Console.WriteLine("\nPress any key to return to the menu...");
    Console.ReadKey();
}

void PlayGame(string selectedGameMode){
    int gameLimit = 5;
    int digitDifficulty = 11;

    Random numberGenerator = new Random();
    DateTime startTime = DateTime.Now;
    int answersCorrect = 0;

    Console.WriteLine("Starting Game...");
    for(int i = 0; i < gameLimit; i++){
        int firstNumber = numberGenerator.Next(digitDifficulty);
        int secondNumber = numberGenerator.Next(digitDifficulty);
        int answer = -1;
        string operand = "";

        switch (selectedGameMode) {
            case "a":
                operand = "+";
                answer = firstNumber + secondNumber;
                break;
            case "s":
                operand = "-";
                answer = firstNumber - secondNumber;
                break;
            case "m":
                operand = "*";
                answer = firstNumber * secondNumber;
                break;
            case "d":
                operand = "/";
                answer = firstNumber;
                firstNumber *= secondNumber;
                break;
        }

        bool validAnswer = false;
        int userAnswer = 0;
        do {
            Console.WriteLine($"{i+1})\tWhat is {firstNumber} {operand} {secondNumber}?");
            string userResponse = Console.ReadLine();

            validAnswer = int.TryParse(userResponse, out userAnswer);
            if(!validAnswer){
                Console.WriteLine("Answer not an integer response. Try again.");
            }
        } while (!validAnswer);

        if (userAnswer == answer){
            Console.WriteLine("Correct!\n");
            answersCorrect++;
        } else {
            Console.WriteLine($"Incorrect. {firstNumber} {operand} {secondNumber} = {answer}.\n");
        }
    }
    DateTime endTime = DateTime.Now;
    int timeTaken = (int) endTime.Subtract(startTime).TotalSeconds;
    Console.WriteLine($"Game is done! You got {answersCorrect} of {gameLimit} correct, and took {timeTaken} seconds.");
    gameHistory.Add(startTime.ToString("h:mmtt") + " " + selectedGameMode.ToUpper());
}
