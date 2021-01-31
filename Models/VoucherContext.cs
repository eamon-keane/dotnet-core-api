using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class VoucherContext:DbContext
    {
        public VoucherContext(DbContextOptions<VoucherContext> options):base(options)
        {

        }

        public DbSet<Voucher> Vouchers {get;set;}
    }
}
