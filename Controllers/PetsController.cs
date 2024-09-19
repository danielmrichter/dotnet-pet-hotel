using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> pets()
        {
            return _context.pets.Include(pet => pet.petOwner);
        }
        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            Pet pet = _context.pets
                .SingleOrDefault(pet => pet.id == id);

            if (pet is null)
            {
                return NotFound();
            }

            return pet; // :) Hope y'all are having fun. :) DON'T FORGET TO RECOVER!
        }
        [HttpPost]
        public Pet Post(Pet pet)
        {
            _context.Add(pet);
            _context.SaveChanges();
            Response.StatusCode = 201;
            return pet;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Pet pet = _context.pets.Find(id);

            _context.pets.Remove(pet);

            _context.SaveChanges();
            Response.StatusCode = 204;
        }
        [HttpPut("{id}")]
        public Pet Put(int id,Pet pet)
        {
            pet.id = id;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }

         [HttpPut("{id}/checkin")]
        public Pet CheckIn(int id)
        {
            Pet pet = _context.pets.SingleOrDefault(pet => pet.id == id);
            pet.checkedInAt = DateTime.UtcNow;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
        [HttpPut("{id}/checkout")]
        public Pet CheckOut(int id)
        {
            Pet pet = _context.pets.SingleOrDefault(pet => pet.id == id);
            pet.checkedInAt = null;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
    }
}
