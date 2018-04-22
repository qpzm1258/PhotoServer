using System;

namespace PhotosServer.Models
{
    public class Information
    {
        public int InformationId { get; set; }
        public String Name { get; set; }
        public String IDNumber { get; set; }
        public String AgentName { get; set; }
        public String AgentIDNumber { get; set; }
        public String Photo { get; set; }
        public String AgentPhoto { get; set; }
        public String HandlePhoto { get; set; }
        public DateTime UpdateTime { get; set; }
        public String IDCardImage { get; set; }
        public String AgentIDCardImage { get; set; }
        public int ServiceType { get; set; }
    }
}
