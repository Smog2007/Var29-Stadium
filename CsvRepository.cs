using Stadion;
using System;
using System.Collections.Generic;
using System.IO;
namespace Stadion
{
    public class CsvRepository
    {
        private string _basePath;
        public CsvRepository(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentException("Путь к папке с данными не может быть пустым.");
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
                try
                {
                    Coach c = new Coach(int.Parse(parts[0]), parts[1]);
                    result.Add(c);
                }
                catch (Exception)
                {
                    continue;
                }
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
                try
                {
                    Team t = new Team(
                        int.Parse(parts[0]),
                        parts[1],
                        int.Parse(parts[2]),
                        parts[3],
                        decimal.Parse(parts[4])
                    );
                    result.Add(t);
                }
                catch (Exception)
                {
                    continue;
                }
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
                try
                {
                    Match m = new Match(
                        int.Parse(parts[0]),
                        int.Parse(parts[1]),
                        parts[2],
                        DateTime.ParseExact(parts[3], "dd.MM.yyyy", null),
                        parts[4],
                        parts[5]
                    );
                    result.Add(m);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return result;
        }
    }
}