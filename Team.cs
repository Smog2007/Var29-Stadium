namespace Stadion
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoachId { get; set; }
        public string City { get; set; }
        public decimal Budget { get; set; }
        /// Вычисляемое свойство: является ли команда богатой (бюджет > 100 млн).
        public bool IsRich => Budget > 100000000m;
        /// Возвращает информацию о команде(Name (City, Budget руб)).
        public string GetInfo()
        {
            return $"{Name} ({City}, {Budget} руб.)";
        }
    }
}