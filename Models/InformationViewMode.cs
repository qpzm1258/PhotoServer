using Microsoft.AspNetCore.Http;
using System;


namespace PhotosServer.Models
{
    public class InformationViewModel
    {
        public String Name { get; set; }
        public String IDNumber { get; set; }
        public string Gender { get; set; }
        public string Folk { get; set; }
        public string BirthDay { get; set; }
        public string Address { get; set; }
        public string Agency { get; set; }
        public string ExpireStart { get; set; }
        public string ExpireEnd { get; set; }
        public String AgentName { get; set; }
        public String AgentIDNumber { get; set; }
        public string AgentGender { get; set; }
        public string AgentFolk { get; set; }
        public string AgentBirthDay { get; set; }
        public string AgentAddress { get; set; }
        public string AgentAgency { get; set; }
        public string AgentExpireStart { get; set; }
        public string AgentExpireEnd { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile AgentPhoto { get; set; }
        public IFormFile HandlePhoto { get; set; }
        public IFormFile IDCardImage { get; set; }
        public IFormFile AgentIDCardImage { get; set; }
        public int ServiceType { get; set; }

    }
}
