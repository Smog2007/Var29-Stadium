using System;
namespace Stadion
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoachId { get; set; }
        public string City { get; set; }
        public decimal Budget { get; set; }
        public bool IsRich => Budget > 100000000m;
        public Team(int id, string name, int coachId, string city, decimal budget)
        {
            if (id <= 0)
                throw new ArgumentException("Id команды должен быть больше нуля.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название команды не может быть пустым.");
            if (coachId <= 0)
                throw new ArgumentException("Id тренера должен быть больше нуля.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("Город не может быть пустым.");
            if (budget < 0)
                throw new ArgumentException("Бюджет не может быть отрицательным.");
            Id = id;
            Name = name;
            CoachId = coachId;
            City = city;
            Budget = budget;
        }
        public string GetInfo()
        {
            return $"{Name} ({City}, {Budget} руб.)";
        }
    }
}