using System;
using System.ComponentModel.DataAnnotations;

namespace ColegioApp.Models{
    public class Alumno{
        public int Id {get; set;}
        public string Nombre {get; set;}
        public string Apellidos {get; set;}
        public string Genero {get; set;}
        public DateTime FechaNacimiento{get; set;}

    }
}

