using Microsoft.AspNetCore.Http;
using System;


namespace PhotosServer.Models
{
    public class InformationViewModel
    {
        public String Name { get; set; }
        public String IDNumber { get; set; }
        public String AgentName { get; set; }
        public String AgentIDNumber { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile AgentPhoto { get; set; }
        public IFormFile HandlePhoto { get; set; }
        public IFormFile IDCardImage { get; set; }
        public IFormFile AgentIDCardImage { get; set; }
        public int ServiceType { get; set; }

    }
}
