namespace Stadion
{
    /// Представляет тренера команды.
    public class Coach
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        /// Возвращает информацию о тренере (ФИО).
        public string GetInfo()
        {
            return FullName;
        }
    }
}