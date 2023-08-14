namespace preguntados_frontend.Models
{
    /// <summary>
    /// Clase que modela la vista de error (clase default de proyecto ASP.NET Core)
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}