using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wet_api.Data
{
    public class Device
    {
        public Device(string title, string? description = null)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Description = description;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Base { get; set; }

        public string? Href { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, Property>? Properties { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, Action>? Actions { get; set; }
    }
}