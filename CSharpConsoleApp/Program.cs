using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

class HangmanGame
{
	public static string[] GetData()
	{
		return new string[] { "Appseconnect", "Integration", "IPAAS", "Business Process", "Marketplace", "OnPremise", "Credential", "Configuration", "Repositories", "Deploy", "Design", "Custom App", "Private App", "Touchpoint", "Adapter", "Database", "Approval List", "Connections", "Lookups", "Automation", "Platform", "Connectivity", "Cloud", "Synchronization", "Ecommerce", "Enterprise", "Solutions", "Middleware", "Integration", "Workflow", "IntegrationHub", "SaaS", "Workflow", "Automation", "Data Migration", "Data Transformation", "ProcessFlow", "B2B Integration", "Mapper", "Data Mapping", "DataExchange", "Customization", "Scalability", "Data Validation", "CRM Integration", "ERP Integration", "Cloud Services", "Hybrid Integration", "Event-Driven Architecture", "Business Logic", "Data Warehousing", "API Management", "RESTful API", "SOAP API", "Application Integration", "Digital Transformation", "Data Analytics", "Cloud Migration", "Security", "Azure DevOps", "Continuous Integration", "Continuous Deployment", "API Security", "Monitoring", "Performance Optimization", "API Gateway", "API Documentation", "Data Access", "Data Storage", "Web Services", "Collaboration", "Data Flow", "Customer Experience", "Business Insights", "Portal.APPSeCONNECT.com", "User Authentication", "Data Backup", "devportal.insync.pro", "stageportal.insync.one", "Real-time Data Sync", "OAuth 2.0", "Client Credential", "Authorization Code", "APIKEY", "NO AUTH", "DataSync", "Manual Execution", "Auto Sync", "Object Key Result", "Initiative", "Hackathon", "DOT NET Framework", "EntityFramework", "Visual Studio", "Data Security", "API Testing", "Regression Testing" };
	}
	static void Main()
	{
		string[] words = GetData();
		Random random = new Random();

		Console.WriteLine("Welcome to Hangman!");
		Console.WriteLine("Each player has 6 attempts to guess the word.");
		Console.WriteLine();

		Console.Write("Enter the number of players: ");
		int numPlayers = int.Parse(Console.ReadLine());

		Dictionary<int, string> playerNames = new Dictionary<int, string>();
		Dictionary<int, int> playerScores = new Dictionary<int, int>();
		var alreadyUsedWord = new List<string>();

		for (int i = 1; i <= numPlayers; i++)
		{
			Console.Write($"Enter the name for Player {i}: ");
			string playerName = Console.ReadLine();
			playerNames[i] = playerName;
			playerScores[i] = 0;
		}

		int currentPlayer = 1; // Start with the first player

		while (true)
		{

			if (!(words.Length > 0))
			{
				Console.WriteLine("No more words to guess, Thank You !!!.");
				break;
			}

			Console.Clear();
			string selectedWord = words[random.Next(words.Length)].ToLower();

			//while (true)
			//{
			//	if (alreadyUsedWord.Contains(selectedWord))
			//	{
			//		selectedWord = words[random.Next(words.Length)].ToLower();
			//	}
			//	else
			//	{
			//		alreadyUsedWord.Add(selectedWord);
			//		break;
			//	}
			//}


			// Remove spaces and special characters from the selected word
			selectedWord = Regex.Replace(selectedWord, "[^a-zA-Z]+", "");

			char[] guessedWord = new char[selectedWord.Length];
			HashSet<char> usedLetters = new HashSet<char>();
			int attempts = 6;

			// Show underscores initially
			for (int i = 0; i < selectedWord.Length; i++)
			{
				guessedWord[i] = '*';
			}

			int visibleLetters = selectedWord.Length < 5 ? 0 : 3;

			// Place 3 random letters in random positions
			for (int i = 0; i < visibleLetters; i++)
			{
				int randomIndex;
				do
				{
					randomIndex = random.Next(selectedWord.Length);
				}
				while (guessedWord[randomIndex] != '*');

				guessedWord[randomIndex] = selectedWord[randomIndex];
			}


			while (attempts > 0)
			{
				Console.WriteLine($"Word: {new string(guessedWord)}");
				Console.WriteLine($"Hint: The word has {selectedWord.Length} letters.");
				Console.WriteLine($"Attempts left for {playerNames[currentPlayer]}: {attempts}");
				Console.Write("Enter a letter: ");
				char guess = char.ToLower(Console.ReadLine()[0]);

				if (usedLetters.Contains(guess))
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"You've already guessed the letter '{guess}'.");
					Console.ResetColor();
				}
				else if (selectedWord.Contains(guess))
				{
					for (int i = 0; i < selectedWord.Length; i++)
					{
						if (selectedWord[i] == guess)
						{
							guessedWord[i] = guess;
						}
					}
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Correct guess!");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Incorrect guess.");
					Console.ResetColor();
					attempts--;
				}

				usedLetters.Add(guess);

				if (new string(guessedWord) == selectedWord)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"Congratulations, {playerNames[currentPlayer]}! You've guessed the word: {selectedWord}");
					Console.ResetColor();
					playerScores[currentPlayer]++;
					break;
				}

				if (attempts == 0)
				{
					Console.WriteLine($"No more attempts left for {playerNames[currentPlayer]}. The word was: {selectedWord}");
					break;
				}
			}

			Console.WriteLine("Current Scores:");
			foreach (var player in playerScores)
			{
				Console.WriteLine($"{playerNames[player.Key]}: {player.Value} points");
			}

			currentPlayer++;
			if (currentPlayer > numPlayers)
			{
				currentPlayer = 1;
			}

			var newWord = words.ToList();
			newWord.RemoveAll(word => word.ToLower() == selectedWord.ToLower());
			words = newWord.ToArray();

			Console.Write("Do you want to play another round? (yes/no): ");
			string playAgain = Console.ReadLine().ToLower();
			if (!playAgain.ToLower().Contains("y"))
			{
				Console.Clear();
				Console.WriteLine("Thanks for playing!");
				var maxScore = playerScores.Max(x => x.Value);
				var winners = playerScores.Where(x => x.Value == maxScore).Select(x => playerNames[x.Key]);
				if (winners.Count() > 1)
				{
					Console.WriteLine("It's a tie! Winners: " + string.Join(", ", winners));
				}
				else
				{
					Console.WriteLine("The winner is: " + string.Join("", winners));
				}
				return;
			}
		}
	}
}
