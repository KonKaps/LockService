using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockServiceController : ControllerBase
    {
        private LockServiceDatabase _context;
        public LockServiceController(LockServiceDatabase context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locks>>> GetItems()
        {
            var bd = _context.HistoryItems.Last().BuildingId;

            return await (from locks in _context.LocksData
                          where locks.BuildingId.Contains(bd)
                          select locks).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<HistoryData>> PostItem(HistoryData input)
        {
            if (input.Name.Contains("="))
            {
                int weight = 0;
                string buldingName;
                string[] NameWeight = input.Name.Split("=");
                buldingName = NameWeight[0].TrimEnd();

                Int32.TryParse(NameWeight[1], out weight);

                Buildings bouldingsresult = new Buildings();
                bouldingsresult = (from b in _context.BuildingsData
                                             where b.Name.ToLower().Contains(buldingName.ToLower())
                                             select b).FirstOrDefault();
                if (bouldingsresult == null)
                    return null;

                bouldingsresult.Weight = weight;


                List<Locks> locksResult = (from l in _context.LocksData
                                           where l.BuildingId.ToLower().Contains(bouldingsresult.Id.ToLower())
                                           select l).ToList();

                foreach (Locks lk in locksResult)
                {
                    lk.Weight = weight - 1;
                }

                //keep history of Building locks modified
                HistoryData historyData = new HistoryData();
                historyData.Name = buldingName;
                historyData.BuildingId = bouldingsresult.Id;
                _context.HistoryItems.Add(historyData); 

            }
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItems),  input);
        }
    }
}
