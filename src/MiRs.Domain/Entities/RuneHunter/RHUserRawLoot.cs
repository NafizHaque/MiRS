namespace MiRs.Domain.Entities.RuneHunter
{
    public class RHUserRawLoot
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public ulong UserId { get; set; }

        public string Loot { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string Mobname { get; set; } = string.Empty;

        public int MobLevel { get; set; }

        public DateTimeOffset DateLogged { get; set; }

        public bool Processed { get; set; }
    }
}
