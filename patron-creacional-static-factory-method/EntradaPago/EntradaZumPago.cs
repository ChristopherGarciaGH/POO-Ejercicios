
namespace WCFWebPay.Servicios
{
    public class EntradaZumPago : IEntradaPago
    {
        #region Constantes

        private const string OK = "Resultado OK";

        #endregion

        #region Campos

        private CachingContext _cacheador = new CachingContext();
        private InsLogTrace _trace = null;

        #endregion

        #region Propiedades

        [JsonProperty("rut")]
        public string Rut { get; set; }

        [JsonProperty("DigVerRut")]
        public string DigitoVerificadorRut { get; set; }

        [JsonProperty("canal")]
        public int Canal { get; set; }

        [JsonProperty("idInstitucion")]
        public int IdInstitucion { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("IdPagoExterno")]
        public string IdPagoExterno { get; set; }

        [JsonProperty("MontoPago")]
        public decimal MontoPago { get; set; }

        [JsonProperty("MedioPago")]
        public string MedioPago { get; set; }

        [JsonProperty("FechaMensaje")]
        public DateTime FechaMensaje { get; set; }

        [JsonProperty("FechaTransaccion")]
        public DateTime FechaTransaccion { get; set; }

        [JsonProperty("GuidsVencimientos")]
        public Guid[] GuidsVencimiento { get; set; }

        #endregion

        #region Constructores

        public EntradaZumPago(InsLogTrace trace)
        {
            _trace = trace;
        }

        // Constructor personalizado para la deserialización
        [JsonConstructor]
        public EntradaZumPago(
            [JsonProperty("MontoPago")] string montoPago,
            [JsonProperty("FechaMensaje")] string fechaMensaje,
            [JsonProperty("FechaTransaccion")] string fechaTransaccion)
        {
            MontoPago = ConversorTipos.ToDecimal(montoPago);
            FechaMensaje = string.IsNullOrEmpty(fechaMensaje) ? DateTime.MinValue : ConversorTipos.ToDateTime(fechaMensaje);
            FechaTransaccion = string.IsNullOrEmpty(fechaTransaccion) ? DateTime.MinValue : ConversorTipos.ToDateTime(fechaTransaccion);
        }

        #endregion

        public EntradaPagoRespuesta AdaptarTrace()
        {
            // Si no hay trace de pago online, no es posible instanciar la entrada pago
            if (_trace == null)
                return null;

            try
            {
                // Deserializamos la entrada UPago
                var entradaZumPago = JsonConvert.DeserializeObject<EntradaZumPago>(_trace.Datos);

                // Establecemos Institucion por cacheador
                InsInstitucion institucion = _cacheador.Instalacion.Institucion.Get(entradaZumPago.IdInstitucion);

                return new EntradaPagoRespuesta
                {
                    Proveedor = ProveedorPagoOnline.UPAGO,
                    Estado = _trace.Mensaje == OK ? EstadoPago.Inyectado : EstadoPago.NoInyectado,
                    IdInstitucion = entradaZumPago.IdInstitucion,
                    NombreInstitucion = institucion?.Nombre,
                    RUTTitular = entradaZumPago.Rut,
                    DigVerRUTTitular = entradaZumPago.DigitoVerificadorRut,
                    FechaMensaje = entradaZumPago.FechaMensaje,
                    FechaTransaccion = entradaZumPago.FechaTransaccion,
                    MontoPago = entradaZumPago.MontoPago,
                    IdPagoExterno = entradaZumPago.IdPagoExterno,
                    FormaPago = entradaZumPago.MedioPago,
                    GuidsVencimiento = entradaZumPago.GuidsVencimiento,
                    Etiquetas = EtiquetaFactory.CreateEtiquetas(_trace, ProveedorPagoOnline.ZUMPAGO, GuidsVencimiento)
                };
            }
            catch (JsonException ex)
            {
                return null;
            }
        }
    }
}