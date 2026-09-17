namespace Stadion
{
    /// Репозиторий для работы с тестовыми данными в памяти.
    public class InMemoryRepository
    {
        private List<Coach> _coaches;
        private List<Team> _teams;
        private List<Match> _matches;
        public InMemoryRepository()
        {
            _coaches = new List<Coach>
            {
                new Coach { Id = 1, FullName = "Иванов И.И." },
                new Coach { Id = 2, FullName = "Петров П.П." },
                new Coach { Id = 3, FullName = "Сидоров С.С." },
                new Coach { Id = 4, FullName = "Смирнов А.А." },
                new Coach { Id = 5, FullName = "Кузнецов Д.Д." }
            };
            _teams = new List<Team>
            {
                new Team { Id = 1, Name = "Спартак", CoachId = 1, City = "Москва", Budget = 500000000m },
                new Team { Id = 2, Name = "Зенит", CoachId = 2, City = "Санкт-Петербург", Budget = 450000000m },
                new Team { Id = 3, Name = "ЦСКА", CoachId = 3, City = "Москва", Budget = 300000000m },
                new Team { Id = 4, Name = "Динамо", CoachId = 4, City = "Москва", Budget = 150000000m },
                new Team { Id = 5, Name = "Локомотив", CoachId = 5, City = "Москва", Budget = 120000000m }
            };
            _matches = new List<Match>
            {
                new Match { Id = 1, TeamId = 1, Opponent = "Зенит", Date = new DateTime(2025, 9, 1), Score = "2:1", Stadium = "Открытие Банк Арена" },
                new Match { Id = 2, TeamId = 3, Opponent = "Динамо", Date = new DateTime(2025, 9, 8), Score = "1:1", Stadium = "ВЭБ Арена" },
                new Match { Id = 3, TeamId = 2, Opponent = "Спартак", Date = new DateTime(2025, 9, 15), Score = "0:2", Stadium = "Газпром Арена" },
                new Match { Id = 4, TeamId = 4, Opponent = "ЦСКА", Date = new DateTime(2025, 9, 22), Score = "1:1", Stadium = "ВТБ Арена" },
                new Match { Id = 5, TeamId = 5, Opponent = "Спартак", Date = new DateTime(2025, 9, 29), Score = "1:3", Stadium = "РЖД Арена" }
            };
        }
        public List<Coach> GetCoaches() => _coaches;
        public List<Team> GetTeams() => _teams;
        public List<Match> GetMatches() => _matches;
    }
}