namespace Web.Response;

public class Info
{
    public int Count { get; set; }
    public int Pages { get; set; }
    public string Next { get; set; }
    public object Prev { get; set; }
}

public class EpisodeResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime AirDate { get; set; }
    public string Episode { get; set; }
    public List<string> Characters { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
}

public class EpisodesResponse
{
    public Info Info { get; set; }
    public List<EpisodeResponse> Results { get; set; }
}