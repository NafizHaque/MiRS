namespace MiRs.Domain.Entities.RuneHunterData
{
    public class Category
    {
        public int Id {  get; set; }

        public string name { get; set; } = string.Empty;

        public ICollection<LevelTask> LevelTasks { get; set; }   
    }
}
