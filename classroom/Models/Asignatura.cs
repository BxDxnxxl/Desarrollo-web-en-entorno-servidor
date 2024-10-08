namespace Models;

class Asignatura
{
    public string Nombre { get; set; }
    public int Creditos { get; set; }

    public Asignatura(string nombre, int creditos)
    {
        Nombre = nombre;
        Creditos = creditos;
    }

    public void MostrarDetalles()
    {
        Console.WriteLine($"Asignatura: {Nombre}, Créditos: {Creditos}");
      //  Console.WriteLine("Asignatura: " + Nombre + " Créditos: " + Creditos);
    }
}
