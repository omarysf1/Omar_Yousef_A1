using System.Collections.Generic;

namespace Omar_Yousef_A1.Models
{
    public enum CheckResult
    {
        CorrectPosition,
        WrongPosition,
        NotFound
    }

    public class Game
    {
        private List<Word> words = new List<Word>();
        private int playedCount = 0;
        private int winCount = 0;
        private int currentStreak = 0;
        private int maxStreak = 0;

        public Word CurrentWord { get; private set; } = null!;

        public Game()
        {
            PopulateWords();
            SelectNewWord();
        }

        private void PopulateWords()
        {
            words.Add(new Word { Text = "apple", Hint = "A fruit" });
            words.Add(new Word { Text = "table", Hint = "Furniture" });
            words.Add(new Word { Text = "cloud", Hint = "In the sky" });
            words.Add(new Word { Text = "grape", Hint = "Another fruit" });
            words.Add(new Word { Text = "chair", Hint = "You sit on it" });
            words.Add(new Word { Text = "light", Hint = "Opposite of dark" });
            words.Add(new Word { Text = "earth", Hint = "Our planet" });
            words.Add(new Word { Text = "water", Hint = "Essential for life" });
            words.Add(new Word { Text = "music", Hint = "Art form" });
            words.Add(new Word { Text = "happy", Hint = "Emotion" });
        }

        public bool CheckGuess(string guess)
        {
            playedCount++;
            if (guess == CurrentWord.Text)
            {
                winCount++;
                currentStreak++;
                if (currentStreak > maxStreak)
                    maxStreak = currentStreak;

                SelectNewWord();
                return true;
            }
            else
            {
                currentStreak = 0;
                return false;
            }
        }

        public CheckResult CheckLetter(char letter, int index)
        {
            if (index < 0 || index >= CurrentWord.Text.Length)
                return CheckResult.NotFound;

            if (CurrentWord.Text[index] == letter)
                return CheckResult.CorrectPosition;
            else if (CurrentWord.Text.Contains(letter))
                return CheckResult.WrongPosition;
            else
                return CheckResult.NotFound;
        }

        public void RevealWord()
        {
            playedCount++;
            currentStreak = 0;
            SelectNewWord();
        }

        public string GetCurrentHint()
        {
            return CurrentWord.Hint;
        }

        private void SelectNewWord()
        {
            Random random = new Random();
            CurrentWord = words[random.Next(words.Count)];
        }

        public int PlayedCount => playedCount;
        public double WinPercentage => playedCount == 0 ? 0 : (winCount / (double)playedCount) * 100;
        public int CurrentStreak => currentStreak;
        public int MaxStreak => maxStreak;
    }
}
