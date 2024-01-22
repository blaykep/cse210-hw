using System.Reflection.Metadata;

public class Job {      /*public so anyone can access it*/
    
    public string _company; /*private string company;*/

    public string _jobtitle;

    public int _startyear;

    public int _endyear;

    public Job(string companyname, string title, int start, int end) {
        _company = companyname;
        _jobtitle = title;
        _startyear = start;
        _endyear = end;
    }

    internal static Job job(string v1, string v2, int v3, int v4)
    {
        throw new NotImplementedException();
    }

    public void Display() {
        Console.WriteLine($"{_jobtitle} ({_company}) {_startyear}-{_endyear}");
    }
}