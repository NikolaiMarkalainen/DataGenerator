
using backend.Models;

public class GeneratedData
{
    public GeneratedNumbers Numbers { get; set; } = new GeneratedNumbers();
    public GeneratedOpenStrings OpenStrings { get; set; } = new GeneratedOpenStrings();
    public GeneratedFixedStrings FixedStrings { get; set; } = new GeneratedFixedStrings();
    public GeneratedFirstNames FirstNames { get; set; } = new GeneratedFirstNames();
    public GeneratedLastNames LastNames { get; set; } = new GeneratedLastNames();
    public GeneratedCountries Countries { get; set; } = new GeneratedCountries();
    public GeneratedIds Ids { get; set; } = new GeneratedIds();
    public List<GeneratedObjects> CustomObjects { get; set; } = new List<GeneratedObjects>();

}


public class GeneratedNumbers 
{
    public string Name { get; set; }
    public List<double> Numbers { get; set; }

    public GeneratedNumbers()
    {
        Numbers = new List<double>();
    }
}

public class GeneratedOpenStrings
{
    public string Name { get; set; }

    public List<string> OpenStrings { get; set; }

    public GeneratedOpenStrings()
    {
        OpenStrings = new List<string>();
    }
}

public class GeneratedFixedStrings
{
    public string Name { get; set; }
    public List<string> FixedStrings { get; set;}

    public GeneratedFixedStrings()
    {
        FixedStrings = new List<string>();
    }
}

public class GeneratedFirstNames
{
    public string Name { get; set; }
    public List<string> Firstnames { get; set; }

    public GeneratedFirstNames()
    {
        Firstnames = new List<string>();
    }
}
public class GeneratedLastNames
{
    public string Name { get; set; }
    public List<string> Lastnames { get; set; }

    public GeneratedLastNames()
    {
        Lastnames = new List<string>();
    }
}

public class GeneratedCountries
{
    public string Name { get; set; }
    public List<string> Countries { get; set; }

    public GeneratedCountries()
    {
        Countries = new List<string>();
    }
}

public class GeneratedIds
{
    public string Name { get; set; }
    public List<string> Ids { get; set; }

    public GeneratedIds()
    {
        Ids = new List<string>();
    }
}
public class GeneratedObjects
{
    public string Name { get; set; }
    public Dictionary<string, object> Data { get; set; } 
    public GeneratedObjects()
    {
        Data = new Dictionary<string, object>();
    }
}