using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class Episode
{

    private Episode() { }
    public Episode(int id = 0, string name = "", DateTime? airDate = null,
          string episodeCode = "", List<Uri> characters = null,
          Uri url = null, DateTime? created = null)
    {
        Id = id;
        Name = name;
        AirDate = airDate;
        EpisodeCode = episodeCode;
        Characters = characters;
        Url = url;
        Created = created;
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? AirDate { get; set; }
    public string EpisodeCode { get; set; }
    public List<Uri> Characters { get; set; }
    public Uri Url { get; set; }
    public DateTime? Created { get; set; }
}
