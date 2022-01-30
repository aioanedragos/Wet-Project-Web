namespace wet_api.Data
{
    public class Action
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }

    public class Input
    {
        public string type { get; set; }
    }
}