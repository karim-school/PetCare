using PetCareAPI.Enums;

namespace PetCareAPI.Forms;

public class NewPetForm
{
    public int CustomerId { get; set; }
    
    public string Name { get; set; }
    
    public char SpeciesId { get; set; }
    
    public string Breed { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public Sex Sex { get; set; }

    public class Response
    {
        public int Id { get; set; }
    }
}