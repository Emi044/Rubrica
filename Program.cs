using System.Text.Json;
using System.Text.Json.Serialization;

const string file = "rubrica.json";

Lista(ReadRubrica());

var str = Console.ReadLine();

if (args.Length > 0)
{
    if (args[0] == "lista")
        Lista(ReadRubrica());
    else if (args[0] == "cerca")
    {
        Lista(Cerca(args[1]));
    }
}
else
{
    Console.WriteLine("ciao");
}

List<Contatto> Cerca(string s)
{
    var l = new List<Contatto>();
    foreach (var c in ReadRubrica())
    {
        if (c.Contains(s)) l.Add(c);
    }
    return l;
}

Contatto Deserializza(string json)
{
    Contatto? contatto = JsonSerializer.Deserialize<Contatto>(json);
    if (contatto == null)
    {
        throw new Exception("JSON non valido.");
    }

    return contatto;
}
void Lista(List<Contatto> l)
{
    foreach (Contatto contatto in l)
    {
        Console.WriteLine(contatto.Nome + ' ' + contatto.Cognome + ": " + contatto.Numero);
    }
}

List<Contatto> ReadRubrica()
{
    return File.ReadLines(file)
        .Select(Deserializza).ToList();
}
class Contatto
{
    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";

    public Boolean Contains(string s)
    {
        return this.Nome.Contains(s) ||
                this.Cognome.Contains(s) ||
                this.Numero.Contains(s);
    }
}