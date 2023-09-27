using System.Text.Json;
using System.Text.Json.Serialization;

const string file = "rubrica.json";

Lista();

string str = Console.ReadLine();

if (args.Length > 0)
{
    if (args[0] == "lista")
        Lista();
    else if (args[0] == "cerca")
    {
        ReadRubrica().ForEach(contact =>
        {
            if (contact.Contains(args[1]))
                Console.WriteLine(contact.Nome);
        });
    }
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
void Lista()
{
    foreach (Contatto contatto in ReadRubrica())
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