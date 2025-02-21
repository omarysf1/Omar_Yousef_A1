using Microsoft.Maui.Controls;
using Omar_Yousef_A1.Models;

namespace Omar_Yousef_A1
{
    public partial class MainPage : ContentPage
    {
        private Game game = new Game();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string guess = GuessEntry.Text.Trim().ToLower();
            if (guess.Length != 5)
            {
                DisplayAlert("Error", "Guessed word must be 5 letters long.", "OK");
                return;
            }

            bool isCorrect = game.CheckGuess(guess);
            if (isCorrect)
            {
                SetAllLabelsColor(Colors.Green);
                DisplayAlert("Congratulations", "Well done! You guessed the right word. Start new game.", "OK");
                ClearUI();
            }
            else
            {
                for (int i = 0; i < guess.Length; i++)
                {
                    char c = guess[i];
                    CheckResult result = game.CheckLetter(c, i);
                    Color color = result switch
                    {
                        CheckResult.CorrectPosition => Colors.Green,
                        CheckResult.WrongPosition => Colors.Yellow,
                        _ => Colors.Gray,
                    };
                    UpdateLabel(i, c.ToString().ToUpper(), color);
                }
            }
            GuessEntry.Text = string.Empty;
        }

        private void UpdateLabel(int index, string text, Color color)
        {
            Label label = (Label)FindByName($"LetterLabel{index + 1}");
            label.Text = text;
            label.BackgroundColor = color;
        }

        private void SetAllLabelsColor(Color color)
        {
            for (int i = 0; i < 5; i++)
                UpdateLabel(i, "", color);
        }

        private void ClearUI()
        {
            for (int i = 0; i < 5; i++)
                UpdateLabel(i, "", Colors.LightGray);
        }

        private void OnShowHintClicked(object sender, EventArgs e)
        {
            DisplayAlert("Hint", game.GetCurrentHint(), "OK");
        }

        private void OnShowAnswerClicked(object sender, EventArgs e)
        {
            DisplayAlert("Answer", game.CurrentWord.Text, "OK");
            game.RevealWord();
            ClearUI();
        }

        private void OnShowStatsClicked(object sender, EventArgs e)
        {
            string stats = $"Played: {game.PlayedCount}\nWin %: {game.WinPercentage:F1}%\nCurrent Streak: {game.CurrentStreak}\nMax Streak: {game.MaxStreak}";
            DisplayAlert("Statistics", stats, "OK");
        }

        private void OnGuessTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 5)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 5);
        }
    }
}
