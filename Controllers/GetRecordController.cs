using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotosServer.Data;
using PhotosServer.Models;
using Microsoft.EntityFrameworkCore;

namespace PhtotServer.Controllers
{
    [Route("api/History")]
    public class GetRecordController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GetRecordController(ApplicationDbContext context)
        {
            //_hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpPost]
        public IActionResult GetHistory(String code, String name, String type, String start, String end)
        {

            var record = _context.Transact.Include(c => c.AgentInfo)
                                        .Include(c => c.TransactorInfo)
                                        .OrderByDescending(c => c.UpdateTime)
                                        .AsNoTracking();

            if (code != null)
            {
                record = record.Where(c => c.TransactorInfo.Code == code ||
                                                      c.AgentInfo.Code == code);
            }
            if (name != null)
            {
                record = record.Where(c => c.TransactorInfo.Name.Contains(name) ||
                                                      c.AgentInfo.Name.Contains(name));
            }
            if (type != null)
            {
                record = record.Where(c => c.ServiceType == Int32.Parse(type));
            }

            if (start != null)
            {
                DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)
                                    .AddMilliseconds(long.Parse(start) * 1000).AddHours(8);
                //startTime = startTime.AddMilliseconds(long.Parse(start));
                //var a=startTime.ToString();
                //DateTime b=record.FirstOrDefault().UpdateTime;
                record = record.Where(c => c.UpdateTime >= startTime);
            }
            if (end != null)
            {
                DateTime endTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)
                                    .AddMilliseconds(long.Parse(end) * 1000).AddHours(8);
                record = record.Where(c => c.UpdateTime <= endTime);
            }
            return Json(record);
        }

    }
}