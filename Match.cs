namespace Stadion
{
    /// Представляет футбольный матч.
    public class Match
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Opponent { get; set; }
        public DateTime Date { get; set; }
        public string Score { get; set; }
        public string Stadium { get; set; }
        /// Вычисляет сумму голов из строки Score (формат "X:Y").
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
        /// Возвращает информацию о матче.
        /// name="teamName" Название команды-хозяина поля
        public string GetInfo(string teamName)
        {
            return $"{teamName} — {Opponent} ({Score}, {Date:dd.MM.yyyy})";
        }
    }
}