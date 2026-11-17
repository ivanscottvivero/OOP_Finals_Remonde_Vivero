namespace TechGuildManager
{
    public interface IOrganizer // interface its use to tell as additional condition in the contract of the polymorphism 
    {
        void OrganizeEvent(string eventName);
    }
}