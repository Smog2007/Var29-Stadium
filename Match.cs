using System;
namespace Stadion
{
    public class Match
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Opponent { get; set; }
        public DateTime Date { get; set; }
        public string Score { get; set; }
        public string Stadium { get; set; }
        public Match(int id, int teamId, string opponent, DateTime date, string score, string stadium)
        {
            if (id <= 0)
                throw new ArgumentException("Id матча должен быть больше нуля.");
            if (teamId <= 0)
                throw new ArgumentException("Id команды должен быть больше нуля.");
            if (string.IsNullOrWhiteSpace(opponent))
                throw new ArgumentException("Соперник не может быть пустым.");
            if (string.IsNullOrWhiteSpace(score))
                throw new ArgumentException("Счет не может быть пустым.");
            if (string.IsNullOrWhiteSpace(stadium))
                throw new ArgumentException("Стадион не может быть пустым.");
            Id = id;
            TeamId = teamId;
            Opponent = opponent;
            Date = date;
            Score = score;
            Stadium = stadium;
        }
        public int GetGoals()
        {
            if (string.IsNullOrEmpty(Score)) return 0;
            string[] parts = Score.Split(':');
            int totalGoals = 0;
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int goals))
                {
                    totalGoals += goals;
                }
            }
            return totalGoals;
        }
        public string GetInfo(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) return "—";
            return $"{teamName} — {Opponent} ({Score}, {Date:dd.MM.yyyy})";
        }
    }
}