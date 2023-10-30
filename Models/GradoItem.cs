namespace ColegioApp.Models{
    public class Grado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProfesorID { get; set; }
        // Navigation property to Profesor
        public Profesor Profesor { get; set; }

    }
}
