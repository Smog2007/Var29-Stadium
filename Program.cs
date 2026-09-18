using Stadion;
using System;
using System.Collections.Generic;
namespace Stadion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Coach> coaches = new List<Coach>();
            List<Team> teams = new List<Team>();
            List<Match> matches = new List<Match>();
            try
            {
                Console.WriteLine("Выберите источник данных:");
                Console.WriteLine("1 - InMemoryRepository");
                Console.WriteLine("2 - CsvRepository");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Выбор не может быть пустым.");
                    return;
                }
                switch (choice)
                {
                    case "1":
                        InMemoryRepository memoryRepo = new InMemoryRepository();
                        coaches = memoryRepo.GetCoaches();
                        teams = memoryRepo.GetTeams();
                        matches = memoryRepo.GetMatches();
                        Console.WriteLine("\nДанные успешно загружены из памяти.\n");
                        break;
                    case "2":
                        CsvRepository csvRepo = new CsvRepository("data");
                        coaches = csvRepo.GetCoaches();
                        teams = csvRepo.GetTeams();
                        matches = csvRepo.GetMatches();
                        Console.WriteLine("\nДанные успешно загружены из CSV.\n");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                return;
            }
            Console.WriteLine("=== 1. Поиск тренера команды ===");
            Coach coach = FindCoach("Спартак", teams, coaches);
            Console.WriteLine($"FindCoach(\"Спартак\"): {(coach != null ? coach.GetInfo() : "null")}");
            Console.WriteLine("\n=== 2. Поиск команды матча ===");
            Match sampleMatch = matches.Count > 0 ? matches[0] : null;
            Team team = FindTeam(sampleMatch, teams);
            Console.WriteLine($"FindTeam(match \"{sampleMatch?.GetInfo("Спартак")}\"): {(team != null ? team.GetInfo() : "null")}");

            Console.WriteLine("\n=== 3. Общее количество голов ===");
            int totalGoals = GetTotalGoals(matches);
            Console.WriteLine($"GetTotalGoals: {totalGoals}");

            Console.WriteLine("\n=== 4. Очки команд ===");
            Dictionary<string, int> stats = GetTeamStats(matches, teams);
            foreach (var pair in stats)
            {
                Console.WriteLine($"{pair.Key} — {pair.Value}");
            }

            Console.WriteLine("\n=== 5. Вывод всех матчей ===");
            PrintAllMatches(matches, teams, coaches);

            Console.ReadLine();
        }

        static Coach FindCoach(string teamName, List<Team> teams, List<Coach> coaches)
        {
            if (string.IsNullOrWhiteSpace(teamName) || teams == null || coaches == null)
                return null;

            Team targetTeam = null;
            foreach (Team team in teams)
            {
                if (team.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                {
                    targetTeam = team;
                    break;
                }
            }

            if (targetTeam == null) return null;

            foreach (Coach coach in coaches)
            {
                if (coach.Id == targetTeam.CoachId)
                {
                    return coach;
                }
            }
            return null;
        }

        static Team FindTeam(Match match, List<Team> teams)
        {
            if (match == null || teams == null) return null;

            foreach (Team team in teams)
            {
                if (team.Id == match.TeamId)
                {
                    return team;
                }
            }
            return null;
        }

        static int GetTotalGoals(List<Match> matches)
        {
            if (matches == null || matches.Count == 0) return 0;

            int total = 0;
            foreach (Match match in matches)
            {
                if (match != null)
                {
                    total += match.GetGoals();
                }
            }
            return total;
        }

        static Dictionary<string, int> GetTeamStats(List<Match> matches, List<Team> teams)
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            if (matches == null || teams == null) return stats;

            foreach (Team team in teams)
            {
                if (team != null && !stats.ContainsKey(team.Name))
                {
                    stats[team.Name] = 0;
                }
            }

            foreach (Match match in matches)
            {
                if (match == null) continue;

                string homeTeamName = "";
                foreach (Team t in teams)
                {
                    if (t != null && t.Id == match.TeamId)
                    {
                        homeTeamName = t.Name;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(homeTeamName)) continue;

                string[] scoreParts = match.Score.Split(':');
                if (scoreParts.Length != 2) continue;

                if (!int.TryParse(scoreParts[0], out int homeGoals)) continue;
                if (!int.TryParse(scoreParts[1], out int awayGoals)) continue;

                if (stats.ContainsKey(homeTeamName))
                {
                    if (homeGoals > awayGoals) stats[homeTeamName] += 3;
                    else if (homeGoals == awayGoals) stats[homeTeamName] += 1;
                }

                if (stats.ContainsKey(match.Opponent))
                {
                    if (awayGoals > homeGoals) stats[match.Opponent] += 3;
                    else if (awayGoals == homeGoals) stats[match.Opponent] += 1;
                }
            }

            return stats;
        }

        static void PrintAllMatches(List<Match> matches, List<Team> teams, List<Coach> coaches)
        {
            if (matches == null || teams == null || coaches == null)
            {
                Console.WriteLine("—");
                return;
            }

            foreach (Match match in matches)
            {
                if (match == null) continue;

                Team team = FindTeam(match, teams);
                if (team == null)
                {
                    Console.WriteLine("—");
                    continue;
                }

                Coach coach = null;
                foreach (Coach c in coaches)
                {
                    if (c != null && c.Id == team.CoachId)
                    {
                        coach = c;
                        break;
                    }
                }

                string coachName = coach != null ? coach.FullName : "—";
                Console.WriteLine($"\"{match.GetInfo(team.Name)}\" — команда \"{team.Name}\", тренер {coachName}");
            }
        }
    }
}