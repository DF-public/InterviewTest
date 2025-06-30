namespace AccessControl
{
    public class Door
    {
        public string Id;
        public int Number;
        public string Name;
        public string Description;
        public DateTime CreatedAt;
        public List<string> DoorsIdsWithAccess;
    }
}