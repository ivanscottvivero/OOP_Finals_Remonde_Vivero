namespace TechGuildManager
{
    public class Alumni : GuildMember
    {
        public Alumni(int id, string name)
            : base(id, name)
        {
        }

        public override int CalculateEventPoints(int basePoints)
        {
            return basePoints;
        }
    }
}