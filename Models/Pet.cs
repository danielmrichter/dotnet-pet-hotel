using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    //predetermined options for breed
    public enum PetBreedType {}
    //predetermined options for color
    public enum PetColorType {}

    //This is the object structure for a pet
    public class Pet 
    {
        public int id {get; set;}
        
        //[Required] means that it is a required property to add a new pet to the pets table
        [Required]
        public string name {get; set;}
        [Required]
        public string breed {get; set;}

        [Required]
        public string color {get; set;}

        //? allows the property to be nullable
        public DateTime? checkedInAt {get; set;}

        //petOwnerid is a foreignKey linked to the id in the petOwners table
        [ForeignKey("petOwner")]
        [Required]
        public int petOwnerid {get; set;}
        public PetOwner petOwner {get; set;}
    }
}
