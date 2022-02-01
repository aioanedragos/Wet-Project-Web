namespace wet_ui.Data
{ 
    public class Property
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string? Unit { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
    }
}