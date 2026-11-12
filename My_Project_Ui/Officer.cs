namespace TechGuildManager
{
    public class Officer : GuildMember, IOrganizer
    {
        public Officer(int id, string name)
            : base(id, name)
        {
        }

        public override int CalculateEventPoints(int basePoints)
        {
            return basePoints * 2;
        }

        public void OrganizeEvent(string eventName)
        {
            Console.WriteLine($"{Name} is organizing {eventName}.");
        }
    }
}