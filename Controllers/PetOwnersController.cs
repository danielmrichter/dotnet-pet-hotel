using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        //Gets all data from the petOwners table in the pet_hotel database
        [HttpGet]
        //IEnumberable<PetOwner> means the function will return a list of 
        // PetOwner objects
        public IEnumerable<PetOwner> GetPets() {
            //_context is referring to the database
            //petOwners is the name of a table in the database
            return _context.petOwners;
        }

        //Gets a specific pet owner, using the id, from the petOwners table in the
        //pet_hotel database
         [HttpGet("{id}")]
        //ActionResult allows flexability to return multiple types of data
        public ActionResult<PetOwner> GetById(int id) {
            PetOwner petOwner =  _context.petOwners
                //.SingleorDefault expects one or default value if there is none
                //finds the id match from the petOwners table
                .SingleOrDefault(petOwner => petOwner.id == id);
            //null is the defualt value in the petOwners table if no id is found
            if(petOwner is null) {
                //returns 404 status
                return NotFound();
            }
            //returns the PetOwner object of the matching id
            return petOwner; // :) Hope y'all are having fun. :) DON'T FORGET TO RECOVER!
        }   // this stuff is fun :)🙌
        
        //Adds a pet owner to the petOwners table in the pet_hotel database
        [HttpPost]
        public PetOwner Post(PetOwner petOwner)
        {
            //adds the new PetOwner object to the petOwners table
            _context.Add(petOwner);
            //commits changes to the database
            _context.SaveChanges();
            //sends a 201 success back to client
            Response.StatusCode = 201;
            //returns the PetOwner object that was added to the petOwners table
            return petOwner;
        }

        //Updates a pet owner in the petOwners table in the pet_hotel database
        [HttpPut("{id}")]
        public PetOwner Put(int id,PetOwner petOwner)
        {
            //assigning the PetOwner object key id to the parameter id
            petOwner.id = id;
            //update that specific row with the new PetOwner object
            _context.Update(petOwner);
            //save changes
            _context.SaveChanges();
            //return the updated PetOwner object
            return petOwner;
        }

        //Deletes a pet owner from the petOwners table in the pet_hotel database
         [HttpDelete("{id}")]
        public void Delete(int id) 
        {
            //set the petOwner variable equal to the pet owner in the petOwners 
            //table with the id that was sent as a parameter
            PetOwner petOwner = _context.petOwners.Find(id);

            //remove that pet owner from the petOwners table
            _context.petOwners.Remove(petOwner);

            //save changes
            _context.SaveChanges();
            //send status 204 - no content to return
            Response.StatusCode = 204;
        }
    }
}
