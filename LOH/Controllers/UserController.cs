using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOH;
using LOH.Data;

namespace LOH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LOHContext _context;
        private readonly Random _rnd = new Random();
        public UserController(LOHContext context)
        {
            _context = context;
        }
        #region Work
        // GET: api/User/Work
        [Route("~/api/user/work/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<WorkResult>> UserWork(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            var workresult = new WorkResult();
            var num = _rnd.Next(0, 10);
            var reward = _rnd.Next(0, 50);
            if (DateTime.Now < user.LastWorkTime.AddMinutes(10))
            {
                workresult.Result = -1;
                workresult.Message = $"Wait {(user.LastWorkTime.AddMinutes(10) - DateTime.Now).Minutes} minutes before working again";
            }
            else if (num < 5)
            {
                workresult.Message = $"Work Success. \nEarned {reward}";

                user.Gold += reward;
                user.LastWorkTime = DateTime.Now;

                _context.Update(user);
                _context.SaveChanges();
            }
            else if (num < 9)
            {
                workresult.Result = 1;
                workresult.Message = $"You did a bad job and didn't earn any gold.";

                user.LastWorkTime = DateTime.Now;
                _context.Update(user);
                _context.SaveChanges();
            }
            else
            {
                workresult.Result = 22;
                workresult.Message = $"You broke some goods while working and now need to pay for them. \nLost {-reward}";

                user.Gold -= reward;
                user.LastWorkTime = DateTime.Now;

                _context.Update(user);
                _context.SaveChanges();
            }
            return workresult;
        }
        #endregion
        #region Battle
        [Route("~/api/user/battle/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Battle>> UserBattle(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            var tempmonster = new Monster
            {
                Damage = 2,
                Defense = 0,
                Name = "Slime",
                Experience = 20,
                Gold = 1,
                Health = 20,
                Level = 1,
                Mana = 5
            };
            var battle = new Battle { PlayerID = id, Monster = tempmonster };
            user.Battle = battle;

            _context.Update(user);
            _context.SaveChanges();

            return battle;
        }
        [Route("~/api/user/battle/{id:int}/hit")]
        [HttpGet]
        public async Task<ActionResult<Battle>> UserBattleHit(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null || user.Battle == null)
            {
                return null;
            }
            var battle = user.Battle;
            var monster = battle.Monster;

            monster.Health =- user.Damage;
            user.Health =- monster.Damage;

            if(monster.Health <= 0 || user.Health <= 0)
            {
                user.Battle = null;
                _context.Update(user);
                _context.SaveChanges();
            }
            return battle;
        }
        #endregion
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if(id == -1)
            {
                return new User();
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.PlayerID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.PlayerID }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.PlayerID == id);
        }
    }
}
