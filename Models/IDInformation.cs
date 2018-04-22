using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotosServer.Models
{
    public class IDInformation
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Folk { get; set; }
        public string BirthDay { get; set; }
        public string Address { get; set; }
        public string Agency { get; set; }
        public string ExpireStart { get; set; }
        public string ExpireEnd { get; set; }
        public string ImageGuid { get; set; }
        public string CardImageGuid { get; set; }

        //public IEnumerable<Transact> Transacts {get;set;}
    }
}
