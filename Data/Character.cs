using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Character
{

    public Character() { }

    public Character(int id = 0, string name = "", CharacterStatus status = 0,
           string species = "", string type = "", CharacterGender gender = 0,
           CharacterLocation location = null, CharacterOrigin origin = null, Uri image = null,
           IEnumerable<Uri> episode = null, Uri url = null, DateTime? created = null)
    {
        Id = id;
        Name = name;
        Status = status;
        Species = species;
        Type = type;
        Gender = gender;
        Location = location;
        Origin = origin;
        Image = image;
        Episode = episode;
        Url = url;
        Created = created;
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public CharacterStatus Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public CharacterGender Gender { get; set; }
    public CharacterLocation Location { get; set; }
    public CharacterOrigin Origin { get; set; }
    public Uri Image { get; set; }
    public IEnumerable<Uri> Episode { get; set; }
    public Uri Url { get; set; }
    public DateTime? Created { get; set; }
}


public enum CharacterGender
{
    male = 0,
    female = 1
}


public enum CharacterStatus
{
    Unknown = 0
}