using System;
namespace Stadion
{
    public class Coach
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Coach(int id, string fullName)
        {
            if (id <= 0)
                throw new ArgumentException("Id тренера должен быть больше нуля.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО тренера не может быть пустым.");
            Id = id;
            FullName = fullName;
        }
        public string GetInfo()
        {
            return FullName;
        }
    }
} 