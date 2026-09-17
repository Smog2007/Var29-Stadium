namespace Stadion
{
    /// Репозиторий для чтения данных из CSV-файлов.
    public class CsvRepository
    {
        private string _basePath;

        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }
        public List<Coach> GetCoaches()
        {
            List<Coach> result = new List<Coach>();
            string path = Path.Combine(_basePath, "coaches.csv");
            if (!File.Exists(path)) return result;
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 2) continue;
                Coach c = new Coach
                {
                    Id = int.Parse(parts[0]),
                    FullName = parts[1]
                };
                result.Add(c);
            }
            return result;
        }
        public List<Team> GetTeams()
        {
            List<Team> result = new List<Team>();
            string path = Path.Combine(_basePath, "teams.csv");
            if (!File.Exists(path)) return result;
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;
                Team t = new Team
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    CoachId = int.Parse(parts[2]),
                    City = parts[3],
                    Budget = decimal.Parse(parts[4])
                };
                result.Add(t);
            }
            return result;
        }
        public List<Match> GetMatches()
        {
            List<Match> result = new List<Match>();
            string path = Path.Combine(_basePath, "matches.csv");
            if (!File.Exists(path)) return result;
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;
                Match m = new Match
                {
                    Id = int.Parse(parts[0]),
                    TeamId = int.Parse(parts[1]),
                    Opponent = parts[2],
                    Date = DateTime.ParseExact(parts[3], "dd.MM.yyyy", null),
                    Score = parts[4],
                    Stadium = parts[5]
                };
                result.Add(m);
            }
            return result;
        }
    }
}