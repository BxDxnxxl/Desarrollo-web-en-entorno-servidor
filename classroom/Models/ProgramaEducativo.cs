
namespace Models;

class ProgramaEducativo
{
    private List<Estudiante> estudiantes;
    private List<Asignatura> asignaturas;

    public ProgramaEducativo()
    {
        estudiantes = new List<Estudiante>();
        asignaturas = new List<Asignatura>();
    }

    public void AñadirEstudiante(Estudiante estudiante)
    {
        if (estudiantes.Exists(e => e.Nombre == estudiante.Nombre))
            {
                Console.WriteLine($"El estudiante {estudiante.Nombre} ya existe en el programa.");
            }
            else
            {
                // Añadir la asignatura a la lista global
                estudiantes.Add(estudiante);
                Console.WriteLine($"El estudiante {estudiante.Nombre} con ha sido añadido.");
            }
    }

    public void MostrarEstudiantes()
    {
        Console.WriteLine("\n--- Lista de Estudiantes ---");
        foreach (var estudiante in estudiantes)
        {
            Console.WriteLine($"Estudiante: {estudiante.Nombre}");
        }
    }

    public Estudiante ObtenerEstudiante(string nombre)
    {
        foreach (var estudiante in estudiantes)
        {
            if (estudiante.Nombre == nombre)
            {
                return estudiante;
            }
        }
        return null;
    }

    public List<Estudiante> BuscarEstudiantesPorNombre(string parteDelNombre)
    {
        List<Estudiante> resultados = estudiantes.FindAll(e => e.Nombre.ToLower().Contains(parteDelNombre.ToLower()));
        return resultados;
    }

    public void EliminarEstudiante(string nombre)
    {
        Estudiante estudiante = ObtenerEstudiante(nombre);
        if (estudiante != null)
        {
            estudiantes.Remove(estudiante);
            Console.WriteLine($"El estudiante {nombre} ha sido eliminado.");
        }
        else
        {
            Console.WriteLine($"El estudiante {nombre} no fue encontrado.");
        }
    }

    public double CalcularPromedioGlobal()
    {
        double sumaPromedios = 0;
        int contadorEstudiantes = 0;

        foreach (var estudiante in estudiantes)
        {
            sumaPromedios += estudiante.CalcularPromedio();
            contadorEstudiantes++;
        }


        return contadorEstudiantes > 0 ? sumaPromedios / contadorEstudiantes : 0;
    }

    public void GenerarReporteEstudiante(Estudiante estudiante)
    {
        Console.WriteLine($"\n--- Reporte para {estudiante.Nombre} ---");
        estudiante.MostrarCalificaciones();
        double promedio = estudiante.CalcularPromedio();
        Console.WriteLine($"Promedio final: {promedio:F2}");
    }

    //funcion para añadir asignaturas a la lista global
    public void AñadirAsignatura(Asignatura asignatura){
            //comprobacion de si la asignatura que se va a añadir existe ya en la lista de asignaturas
        if (!asignaturas.Contains(asignatura)){
            asignaturas.Add(asignatura);
            Console.WriteLine($"La asignatura '{asignatura}' ha sido añadida a la lista global.");
        }
        else
        {
             Console.WriteLine($"La asignatura '{asignatura}' ya existe en la lista global.");
        }
    }

    //funcion para hacer ranking de estudiantes
    public void RankingEstudiantes()
    {
        // documentacion de como ordenar listas en c#: https://es.stackoverflow.com/questions/144984/ordenamiento-de-listas-c

        var ranking = ranking.OrderByDescending(alumno => alumno.CalcularPromedio()).ToList();

        Console.WriteLine("Ranking de Estudiantes: ");
        int posicion = 1;
        foreach (var estudiante in ranking)
        {
            //N2 se usa para indicar el numero de decimales que han de aparecer: https://es.stackoverflow.com/questions/91442/c%C3%B3mo-especificar-la-cantidad-de-decimales-de-un-double#:~:text=La%20forma%20mas%20f%C3%A1cil%3A,de%20n%C3%BAmero%20con%204%20decimales.
            Console.WriteLine($"{posicion}. {estudiante.Nombre} - Promedio: {estudiante.CalcularPromedio():N2}");
            posicion++;
        }
    }

    public void EstudiantesRiesgoAReprobar(){
        List<Estudiante> estudiantesEnRiesgo = new List<Estudiante>();

        //usamos foreach para comprobar si el estudiante tiene por debajo del 5 los promedios, si es asi lo añadimos a la lista de estudiantes en riesgo
        foreach (var estudiante in estudiantes)
        {
            if (estudiante.CalcularPromedio() < 5)
            {
                estudiantesEnRiesgo.Add(estudiante);
            }
        }

        // controlamos si hay estudiantes en riesgo
        if (estudiantesEnRiesgo.Count == 0)
        {
            Console.WriteLine("No hay estudiantes en riesgo de reprobar.");
        }
        else
        {
            Console.WriteLine("Estudiantes que estan en Riesgo de Reprobar: ");
            foreach (var estudiante in estudiantesEnRiesgo)
            {
                Console.WriteLine($"Estudiante: {estudiante.Nombre} - Promedio: {estudiante.CalcularPromedio():F2}");
            }
        }
    }

    public void MostrarAsignaturas(){

        Console.WriteLine("Estas son las asignaturas que hay en el programa educativo y los creditos que aportan: ");
        foreach (var asignatura in asignaturas)
        {
            Console.WriteLine($"Asignatura: {asignatura.Nombre} - Créditos: {asignatura.Creditos}");
        }
        
    }

}