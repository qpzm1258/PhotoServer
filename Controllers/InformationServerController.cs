using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotosServer.Data;
using PhotosServer.Models;

namespace PhotosServer.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    [Route("api/InformationServer")]
    public class InformationServerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public InformationServerController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: api/InformationServer
        [HttpGet]
        public IEnumerable<Information> GetInformation()
        {
            return _context.Information;
        }

        // GET: api/InformationServer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInformation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var information = await _context.Information.SingleOrDefaultAsync(m => m.InformationId == id);

            if (information == null)
            {
                return NotFound();
            }

            return Ok(information);
        }

        // PUT: api/InformationServer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation([FromRoute] int id, [FromBody] Information information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != information.InformationId)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InformationServer
        [HttpPost]
        public async Task<IActionResult> PostInformation(InformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Information information = new Information();
            var rooPath = _hostingEnvironment.WebRootPath;
            var uploadsPath = rooPath + "/Uploads/HandlePhoto/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
            var cardInfoPath = rooPath + "/Uploads/CardInfo";
            var photoPath = cardInfoPath + "/Photo/";
            var cardImagePath = cardInfoPath + "/CardImage/";
            if (model.Name == null)
            {
                return CreatedAtAction("GetInformation", new { result = false, errmesg = "办理者姓名不能为空" });
            }

            if (model.ServiceType > 1)
            {
                return CreatedAtAction("GetInformation", new { result = false, errmesg = "办理类型错误" });
            }

            Directory.CreateDirectory(uploadsPath);
            Directory.CreateDirectory(photoPath);
            Directory.CreateDirectory(cardImagePath);
            IDInformation transactor = await _context.IDInformation.Where(c => c.Name == model.Name
                                                                && c.Code == model.IDNumber
                                                                && c.Address == model.Address
                                                                && c.Agency == model.Agency
                                                                && c.BirthDay == model.BirthDay
                                                                && c.ExpireEnd == model.ExpireEnd
                                                                && c.ExpireStart == model.ExpireStart
                                                                && c.Folk == model.Folk
                                                                && c.Gender == model.Gender).AsTracking().FirstOrDefaultAsync();
            IDInformation agent = await _context.IDInformation.Where(c => c.Name == model.AgentName
                                                                && c.Code == model.AgentIDNumber
                                                                && c.Address == model.AgentAddress
                                                                && c.Agency == model.AgentAgency
                                                                && c.BirthDay == model.AgentBirthDay
                                                                && c.ExpireEnd == model.AgentExpireEnd
                                                                && c.ExpireStart == model.AgentExpireStart
                                                                && c.Folk == model.AgentFolk
                                                                && c.Gender == model.AgentGender).AsTracking().FirstOrDefaultAsync();

            if (transactor == new IDInformation())
            {
                String _photo = null, _idcardimage = null;
                if (model.Photo != null)
                {
                    _photo = Path.Combine(photoPath, System.Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.Photo.FileName).ToLower());
                    using (var memoryStream = new FileStream(_photo, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(memoryStream);
                    }
                }

                if (model.IDCardImage != null)
                {
                    _idcardimage = Path.Combine(cardImagePath, System.Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.IDCardImage.FileName).ToLower());
                    using (var memoryStream = new FileStream(_idcardimage, FileMode.Create))
                    {
                        await model.IDCardImage.CopyToAsync(memoryStream);
                    }
                }
                transactor = new IDInformation()
                {
                    Name = model.Name
                                                ,
                    Code = model.IDNumber
                                                ,
                    Address = model.Address
                                                ,
                    Agency = model.Agency
                                                ,
                    BirthDay = model.BirthDay
                                                ,
                    ExpireEnd = model.ExpireEnd
                                                ,
                    ExpireStart = model.ExpireStart
                                                ,
                    Folk = model.Folk
                                                ,
                    Gender = model.Gender
                                                ,
                    ImageGuid = Path.GetFileNameWithoutExtension(_photo)
                                                ,
                    CardImageGuid = Path.GetFileNameWithoutExtension(_idcardimage)
                };

            }

            if (agent == new IDInformation() && model.AgentIDNumber != null)
            {
                String _agentPhoto = null, _agentidcardimage = null;
                if (model.AgentPhoto != null)
                {
                    _agentPhoto = Path.Combine(photoPath, System.Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.AgentPhoto.FileName).ToLower());
                    using (var memoryStream = new FileStream(_agentPhoto, FileMode.Create))
                    {
                        await model.AgentPhoto.CopyToAsync(memoryStream);
                    }
                }

                if (model.AgentIDCardImage != null)
                {
                    _agentidcardimage = Path.Combine(cardImagePath, System.Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.AgentIDCardImage.FileName).ToLower());
                    using (var memoryStream = new FileStream(_agentidcardimage, FileMode.Create))
                    {
                        await model.AgentIDCardImage.CopyToAsync(memoryStream);
                    }
                }

                agent = new IDInformation()
                {
                    Name = model.AgentName
                                                ,
                    Code = model.AgentIDNumber
                                                ,
                    Address = model.AgentAddress
                                                ,
                    Agency = model.AgentAgency
                                                ,
                    BirthDay = model.AgentBirthDay
                                                ,
                    ExpireEnd = model.AgentExpireEnd
                                                ,
                    ExpireStart = model.AgentExpireStart
                                                ,
                    Folk = model.AgentFolk
                                                ,
                    Gender = model.AgentGender
                                                ,
                    ImageGuid = Path.GetFileNameWithoutExtension(_agentPhoto)
                                                ,
                    CardImageGuid = Path.GetFileNameWithoutExtension(_agentidcardimage)
                };
            }

            String _handelPhoto = null;
            if (model.HandlePhoto != null)
            {
                _handelPhoto = Path.Combine(uploadsPath, System.Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.HandlePhoto.FileName).ToLower());
                using (var memoryStream = new FileStream(_handelPhoto, FileMode.Create))
                {
                    await model.HandlePhoto.CopyToAsync(memoryStream);
                }
            }
            else
            {
                return CreatedAtAction("GetInformation", new { result = false, errmesg = "无办理照片" });
            }

            Transact transact = new Transact()
            {
                UpdateTime = DateTime.Now,
                ServiceType = model.ServiceType,
                TransactorInfo = transactor,
                HandlePhotoGuid = Path.GetFileNameWithoutExtension(_handelPhoto)
            };

            if (agent != new IDInformation())
            {
                transact.AgentInfo = agent;
            }
            //information.Name = model.Name;
            //information.IDNumber = model.IDNumber;
            //information.AgentName = model.AgentName;
            //information.AgentIDNumber = model.AgentIDNumber;
            //information.Photo = Path.GetFileName(_photo);
            //information.HandlePhoto = Path.GetFileName(_handelPhoto);
            //information.AgentPhoto = Path.GetFileName(_agentPhoto);
            //information.AgentIDCardImage = Path.GetFileName(_agentidcardimage);
            //information.IDCardImage = Path.GetFileName(_idcardimage);
            //information.UpdateTime = DateTime.Now;
            //information.ServiceType = model.ServiceType;
            _context.Transact.Add(transact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { result = true });
        }

        // DELETE: api/InformationServer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var information = await _context.Information.SingleOrDefaultAsync(m => m.InformationId == id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return Ok(information);
        }

        private bool InformationExists(int id)
        {
            return _context.Information.Any(e => e.InformationId == id);
        }
    }
}