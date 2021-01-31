using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {

        private const int VOUCHER_CODE_LENGTH = 8;
        private readonly VoucherContext _context;

        public VoucherController(VoucherContext context)
        {
            _context = context;
            if (_context.Vouchers.Count() == 0)
            {
                _context.Vouchers.Add(new Voucher("test"));
                _context.SaveChanges();
            }
        }


        // GET: api/Voucher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voucher>>> GetVouchers()
        {
            return await _context.Vouchers.ToListAsync();
        }

        // POST: api/Voucher
        [HttpPost]
        public async Task<ActionResult<Voucher>> PostVoucher(string name)
        {

            var voucher = new Voucher(name);

            await _context.Vouchers.AddAsync(voucher);

            var voucherId = voucher.Id;

            string voucherCode = ConvertIdToVoucherCode(voucherId);

            voucher.VoucherCode = voucherCode;

            await _context.SaveChangesAsync();

            return voucher;

        }

        // Get: api/Voucher/5
        [HttpGet("{voucherCode}")]
        public async Task<ActionResult<Voucher>> GetVoucher(string voucherCode)
        {

            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherCode == voucherCode);

            if (voucher == null)
            {
                return NotFound();
            }

            return voucher;
        }

        private static string ConvertIdToVoucherCode(Guid voucherId)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(voucherId.ToString());
            var voucherCode = Convert.ToBase64String(plainTextBytes).Substring(0, VOUCHER_CODE_LENGTH);
            return voucherCode;
        }
    }
}
