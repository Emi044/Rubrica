using System.Text.Json;
using System.Text.Json.Serialization;

const string file = "rubrica.json";

List<Contatto> contattos = await ReadRubrica();

List<string> azione;
azione = args.ToList();

while (!CheckArgs(azione))
{
    Console.WriteLine("Non è stata specificata un'azione, cosa desideri fare? [lista, ricerca, nuovo] ");
    Console.Write("Azione -> ");
    azione = Console.ReadLine().Split(' ').ToList();
}
if (azione[0] == "lista")
    Lista(contattos);
else if (azione[0] == "ricerca")
{
    if (azione.Count == 2) Lista(Cerca(azione[1]));
    else
    {
        Console.WriteLine("Non è stato specificato nessuno parametro di ricerca");
        Lista(Cerca(Console.ReadLine()));
    }
}
else if (azione[0] == "nuovo")
{
    if (azione.Count == 4) AddContatto(azione[1], azione[2], azione[3]);
    else if (azione.Count == 3)
    {
        Console.Write("Inserire il numero di telefono ->");
        string nu = Console.ReadLine();
        AddContatto(azione[1], azione[2], nu);
    }
    else if (azione.Count == 2)
    {
        Console.Write("Inserire il cognome ->");
        string co = Console.ReadLine();
        Console.Write("Inserire il numero di telefono ->");
        string nu = Console.ReadLine();
        AddContatto(azione[1], co, nu);
    }
    else
    {
        Console.Write("Inserire il nome ->");
        string no = Console.ReadLine();
        Console.Write("Inserire il cognome ->");
        string co = Console.ReadLine();
        Console.Write("Inserire il numero di telefono ->");
        string nu = Console.ReadLine();
        AddContatto(no, co, nu);
    }
}

void AddContatto(string nome, string cognome, string numero)
{
    Contatto c = new Contatto();
    c.Nome = nome;
    c.Cognome = cognome;
    c.Numero = numero;
    contattos.Add(c);
    File.AppendAllText("rubrica.json", $"\n{JsonSerializer.Serialize(c)}");
}

List<Contatto> Cerca(string s)
{
    var l = new List<Contatto>();
    foreach (Contatto c in contattos)
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

async Task<List<Contatto>> ReadRubrica() 
{
    return (await File.ReadAllLinesAsync(file)).Select(Deserializza).ToList();
}

Boolean CheckArgs(List<String> azioni)
{
    if (azioni.Count == 0) return false;
    if (azioni[0] == "lista" && azioni.Count == 1) return true;
    else if (azioni[0] == "ricerca" && (azioni.Count == 2 || azioni.Count == 1)) return true;
    else if (azioni[0] == "nuovo" && (azioni.Count >= 1 || azioni.Count <= 4)) return true;
    else return false;
}
class Contatto
{
    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
    public string Numero { get; set; } = "";

    public Boolean Contains(string s)
    {
        return this.Nome.ToLower().Contains(s.ToLower()) ||
                this.Cognome.ToLower().Contains(s.ToLower()) ||
                this.Numero.ToLower().Contains(s.ToLower());
    }
}