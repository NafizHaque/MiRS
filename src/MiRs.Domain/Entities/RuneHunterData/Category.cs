namespace MiRs.Domain.Entities.RuneHunterData
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public ICollection<Level>? Level { get; set; }
    }
}
