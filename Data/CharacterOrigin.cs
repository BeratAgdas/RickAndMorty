using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class CharacterOrigin
{
    public CharacterOrigin() { }
    public CharacterOrigin(string name = "", Uri url = null)
    {
        Name = name;
        Url = url;
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Uri Url { get; set; }
}
