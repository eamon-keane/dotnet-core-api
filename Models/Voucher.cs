using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Voucher
    {
        public Voucher(string name)
        {
            IsValid = true;
            Name = name;
            CreationDateTimeUTC = DateTime.UtcNow;
        }
        [Key]
        public Guid Id { get; set; }

        public string VoucherCode { get; set; }

        public bool IsValid { get; set; } = true;

        public string UsedById { get; set; }

        public string Name {get;set;}

        public DateTime CreationDateTimeUTC {get;set;}
    }
}
