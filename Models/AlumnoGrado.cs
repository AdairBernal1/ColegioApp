using System.Security.AccessControl;

namespace ColegioApp.Models{
    public class AlumnoGrado{
        public int Id {get; set;}
        public int AlumnoID{get; set;}
        public int GradoID {get; set;}
        public string Seccion {get; set;}

        //Navigation properties
        public Alumno Alumno  {get; set;}
        public Grado Grado {get; set;}
    }
}

