using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhtotServer.Controllers
{
    [Route("api/ServerStatus")]
    public class CheckStatusController : Controller
    {
		[HttpGet]
		public IActionResult GetStatus()
		{
			return CreatedAtAction("GetStatus", new { result = true });
		}
		
	}
}