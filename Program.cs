namespace Stadion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - InMemoryRepository");
            Console.WriteLine("2 - CsvRepository");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();
            List<Coach> coaches;
            List<Team> teams;
            List<Match> matches;
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
            /// 1. Поиск тренера команды
            Console.WriteLine("=== 1. Поиск тренера команды ===");
            Coach coach = FindCoach("Спартак", teams, coaches);
            Console.WriteLine($"FindCoach(\"Спартак\"): {(coach != null ? coach.GetInfo() : "null")}");
            /// 2. Поиск команды матча
            Console.WriteLine("\n=== 2. Поиск команды матча ===");
            Match sampleMatch = matches.Count > 0 ? matches[0] : null;
            Team team = FindTeam(sampleMatch, teams);
            Console.WriteLine($"FindTeam(match \"{sampleMatch?.GetInfo("Спартак")}\"): {(team != null ? team.GetInfo() : "null")}");
            /// 3. Общее количество голов
            Console.WriteLine("\n=== 3. Общее количество голов ===");
            int totalGoals = GetTotalGoals(matches);
            Console.WriteLine($"GetTotalGoals: {totalGoals}");
            /// 4. Очки команд
            Console.WriteLine("\n=== 4. Очки команд ===");
            Dictionary<string, int> stats = GetTeamStats(matches, teams);
            foreach (var pair in stats)
            {
                Console.WriteLine($"{pair.Key} — {pair.Value}");
            }
            /// 5. Вывод всех матчей
            Console.WriteLine("\n=== 5. Вывод всех матчей ===");
            PrintAllMatches(matches, teams, coaches);
            Console.ReadLine();
        }
        /// Ищет тренера по названию команды
        static Coach FindCoach(string teamName, List<Team> teams, List<Coach> coaches)
        {
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
        /// Ищет команду, которой принадлежит матч.
        static Team FindTeam(Match match, List<Team> teams)
        {
            if (match == null) return null;
            foreach (Team team in teams)
            {
                if (team.Id == match.TeamId)
                {
                    return team;
                }
            }
            return null;
        }
        /// Подсчитывает общее количество голов во всех матчах.
        static int GetTotalGoals(List<Match> matches)
        {
            if (matches == null || matches.Count == 0) return 0;
            int total = 0;
            foreach (Match match in matches)
            {
                total += match.GetGoals();
            }
            return total;
        }
        /// Подсчитывает очки команд на основе результатов матчей.
        static Dictionary<string, int> GetTeamStats(List<Match> matches, List<Team> teams)
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            /// Инициализация словаря для всех команд
            foreach (Team team in teams)
            {
                stats[team.Name] = 0;
            }
            foreach (Match match in matches)
            {
                string homeTeamName = "";
                foreach (Team t in teams)
                {
                    if (t.Id == match.TeamId)
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
                /// Начисление очков домашней команде
                if (stats.ContainsKey(homeTeamName))
                {
                    if (homeGoals > awayGoals) stats[homeTeamName] += 3;
                    else if (homeGoals == awayGoals) stats[homeTeamName] += 1;
                }
                /// Начисление очков гостевой команде (сопернику)
                if (stats.ContainsKey(match.Opponent))
                {
                    if (awayGoals > homeGoals) stats[match.Opponent] += 3;
                    else if (awayGoals == homeGoals) stats[match.Opponent] += 1;
                }
            }
            return stats;
        }
        /// Выводит все матчи с информацией о команде и тренере.
        static void PrintAllMatches(List<Match> matches, List<Team> teams, List<Coach> coaches)
        {
            foreach (Match match in matches)
            {
                Team team = FindTeam(match, teams);
                if (team == null)
                {
                    Console.WriteLine("—");
                    continue;
                }
                Coach coach = null;
                foreach (Coach c in coaches)
                {
                    if (c.Id == team.CoachId)
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