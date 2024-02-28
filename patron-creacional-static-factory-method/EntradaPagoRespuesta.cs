

namespace WCFWebPay.Servicios
{
    public class EntradaPagoRespuesta
    {
        public ProveedorPagoOnline Proveedor { get; set; }

        public EstadoPago Estado { get; set; }

        public string RUTTitular { get; set; } = string.Empty;

        public string DigVerRUTTitular { get; set; } = string.Empty;

        public int IdInstitucion { get; set; }

        public string NombreInstitucion { get; set; } = string.Empty;

        public DateTime FechaMensaje { get; set; }

        public DateTime FechaTransaccion { get; set; } 

        public decimal MontoPago { get; set; }

        public string IdPagoExterno { get; set; } = string.Empty;

        public string FormaPago { get; set; } = string.Empty;

        public Guid[] GuidsVencimiento { get; set; }

        public List<Etiqueta> Etiquetas { get; set; } = Enumerable.Empty<Etiqueta>().ToList();
    }
}