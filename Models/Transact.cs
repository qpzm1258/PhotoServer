using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotosServer.Models
{
    public class Transact
    {
        [Key]
        public int ID { get; set; }
        public int TransactorId { get; set; }
        [ForeignKey("TransactorId")]
        public IDInformation TransactorInfo { get; set; }
        public int? AgentId { get; set; }
        [ForeignKey("AgentId")]
        public IDInformation AgentInfo { get; set; }
        public String HandlePhotoGuid { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ServiceType { get; set; }
    }
}