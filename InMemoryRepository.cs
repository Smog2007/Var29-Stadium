using Stadion;
using System;
using System.Collections.Generic;
namespace Stadion
{
    public class InMemoryRepository
    {
        private List<Coach> _coaches;
        private List<Team> _teams;
        private List<Match> _matches;
        public InMemoryRepository()
        {
            _coaches = new List<Coach>
            {
                new Coach(1, "Иванов И.И."),
                new Coach(2, "Петров П.П."),
                new Coach(3, "Сидоров С.С."),
                new Coach(4, "Смирнов А.А."),
                new Coach(5, "Кузнецов Д.Д.")
            };
            _teams = new List<Team>
            {
                new Team(1, "Спартак", 1, "Москва", 500000000m),
                new Team(2, "Зенит", 2, "Санкт-Петербург", 450000000m),
                new Team(3, "ЦСКА", 3, "Москва", 300000000m),
                new Team(4, "Динамо", 4, "Москва", 150000000m),
                new Team(5, "Локомотив", 5, "Москва", 120000000m)
            };
            _matches = new List<Match>
            {
                new Match(1, 1, "Зенит", new DateTime(2025, 9, 1), "2:1", "Открытие Банк Арена"),
                new Match(2, 3, "Динамо", new DateTime(2025, 9, 8), "1:1", "ВЭБ Арена"),
                new Match(3, 2, "Спартак", new DateTime(2025, 9, 15), "0:2", "Газпром Арена"),
                new Match(4, 4, "ЦСКА", new DateTime(2025, 9, 22), "1:1", "ВТБ Арена"),
                new Match(5, 5, "Спартак", new DateTime(2025, 9, 29), "1:3", "РЖД Арена")
            };
        }
        public List<Coach> GetCoaches() => _coaches;
        public List<Team> GetTeams() => _teams;
        public List<Match> GetMatches() => _matches;
    }
}