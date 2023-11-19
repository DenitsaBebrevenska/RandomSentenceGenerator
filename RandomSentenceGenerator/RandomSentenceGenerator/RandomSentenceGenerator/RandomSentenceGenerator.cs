namespace RandomSentenceGenerator
{
	internal class RandomSentenceGenerator
	{
		static string[] _fileNames = { "Subjects.txt", "Verbs + prepositions.txt", "Nouns.txt",
			"Adverbs.txt", "Places.txt", "Other.txt","Random exclamations.txt"};
		static List<string> _subjects = GetListContent(_fileNames[0]);
		static List<string> _verbs = GetListContent(_fileNames[1]);
		static List<string> _nouns = GetListContent(_fileNames[2]);
		static List<string> _adverbs = GetListContent(_fileNames[3]);
		static List<string> _places = GetListContent(_fileNames[4]);
		static List<string> _others = GetListContent(_fileNames[5]);
		static List<string> _exclamations = GetListContent(_fileNames[6]);
		static void Main()
		{
			Introduction();
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Press ENTER to create a sentence!");
				Console.WriteLine("Or press any other key to quit!");
				var keyPressed = Console.ReadKey();
				if (keyPressed.KeyChar != 13)
				{
					return;
				}

				string[] sentence = AppendWords();
				Console.ForegroundColor = ConsoleColor.Red;
				PrintSentence(sentence);
				string exclamation = GenerateExclamation();
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(exclamation);
				Console.WriteLine();
			}

		}
		static List<string> GetListContent(string file)
		{
			List<string> list = File.
				ReadAllText($@"E:\txt\{file}").
				Split('|').
				ToList();
			return list;
		}

		static string[] AppendWords()
		{
			bool isSecondSubject = false;
			string subject1 = GenerateSubject1();
			string other = GenerateOther(subject1, isSecondSubject);
			isSecondSubject = true;
			string subject2 = GenerateSubject2(subject1);
			string other2 = GenerateOther(subject2, isSecondSubject);
			string adverb = GenerateAdverb();
			string verb = GenerateVerb();
			string noun = GenerateNoun();
			string place = GeneratePlace();
			string[] sentence = { other, subject1, "and", other2, subject2, adverb, verb, noun, "at", place };
			return sentence;
		}

		
		static string GenerateVerb()
		{
			Random verbRandom = new Random();
			int index = verbRandom.Next(_verbs.Count);
			string word = _verbs[index];
			return word;
		}
		static string GenerateSubject1()
		{
			Random subjectRandom = new Random();
			int index = subjectRandom.Next(_subjects.Count);
			string word = _subjects[index];
			return word;
		}
		private static string GenerateSubject2(string subject1)
		{
			string word;
			Random subjectRandom = new Random();
			while (true) //we can`t have two Julia Roberts in the same sentence!
			{
				int index = subjectRandom.Next(_subjects.Count);
				word = _subjects[index];
				if (word != subject1)
				{
					break;
				}
			}

			return word;
		}

		static string GenerateOther(string subject, bool isSecondSubject) //others are defining words for plural subjects like "some"
		{
			Random otherRandom = new Random();
			if (IsFirstLetterCapital(subject)) //if it is a name it doesn`t need additives
			{
				return " ";
			}

			int index; 
			//first subject additive should start with a capital letter => beginning of sentence
			index = !isSecondSubject ? otherRandom.Next(0, 15) : otherRandom.Next(15, 30);
			string word = _others[index];
			return word;
		}
		static string GenerateNoun()
		{
			Random nounRandom = new Random();
			int index = nounRandom.Next(_nouns.Count);
			string word = _nouns[index];
			return word;
		}
		static string GenerateAdverb()
		{
			Random adverbRandom = new Random();
			int index = adverbRandom.Next(_adverbs.Count);
			string word = _adverbs[index];
			return word;
		}
		static string GeneratePlace()
		{
			Random placeRandom = new Random();
			int index = placeRandom.Next(_places.Count);
			string word = _places[index];
			return word;
		}
		static string GenerateExclamation()
		{
			Random exclamationRandom = new Random();
			int index = exclamationRandom.Next(_exclamations.Count);
			string word = _exclamations[index];
			return word;
		}

		static bool IsFirstLetterCapital(string subject)
		{
			char firstLetter = subject[0];
			return firstLetter >= 65 && firstLetter <= 90;
		}

		static void PrintSentence(string[] sentence)
		{
			string output = string.Empty;
			for (int i = 0; i < sentence.Length; i++)
			{
				if (sentence[i] != " ")
				{
					output += sentence[i] + " ";
				}
			}

			output = output.TrimEnd();
			output += ".";
			Console.WriteLine(output);
		}

		static void Introduction()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Welcome to the Random Sentence Generator!");
			Console.WriteLine("Let`s have a laugh or wonder what is that even supposed to mean...");
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}
	}
}