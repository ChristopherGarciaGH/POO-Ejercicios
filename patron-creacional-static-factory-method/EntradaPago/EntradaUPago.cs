

namespace WCFWebPay.Servicios
{
    public class EntradaUPago : IEntradaPago
    {
        #region Constantes

        private const string OK = "Resultado OK";
        private const string KO_VALIDAR_VENCIMIENTOS = "Error al validar vencimientos";

        #endregion

        #region Campos

        private CachingContext _cacheador = new CachingContext();
        private InsLogTrace _trace = null;

        // Excepciones a la norma: DS Temuco y Cumbres utilizan ZumPago pero pagan por SetPagoTitular
        // Cumbres, DS Temuco, Everest, Luis Campino
        private static List<int> institucionesExcepcion = new List<int> { 1352, 1342, 1395, 1400 };

        #endregion

        #region Propiedades

        [JsonProperty("rut")]
        public string Rut { get; set; }

        [JsonProperty("DigVerRut")]
        public string DigVerRut { get; set; }

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
        public MedioPago MedioPago { get; set; }

        [JsonProperty("FechaMensaje")]
        public DateTime FechaMensaje { get; set; } 

        [JsonProperty("FechaTransaccion")]
        public DateTime FechaTransaccion { get; set; } 

        [JsonProperty("GuidsVencimientos")]
        public Guid[] GuidsVencimiento { get; set; }

        #endregion

        #region Constructores
        public EntradaUPago(InsLogTrace trace) {
            _trace = trace;
        }

        // Constructor personalizado para la deserialización
        [JsonConstructor]
        public EntradaUPago(
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
                var entradaUPago = JsonConvert.DeserializeObject<EntradaUPago>(_trace.Datos);

                // Establecemos Institucion por cacheador
                InsInstitucion institucion = _cacheador.Instalacion.Institucion.Get(entradaUPago.IdInstitucion);

                // Tratamos excepciones de institucion
                ProveedorPagoOnline proveedor = ProveedorPagoOnline.UPAGO;
                if (institucionesExcepcion.Contains(entradaUPago.IdInstitucion))
                    proveedor = ProveedorPagoOnline.ZUMPAGO;

                return new EntradaPagoRespuesta
                {
                    Proveedor = proveedor,
                    Estado = _trace.Mensaje == OK ? EstadoPago.Inyectado : EstadoPago.NoInyectado,
                    IdInstitucion = entradaUPago.IdInstitucion,
                    NombreInstitucion = institucion?.Nombre,
                    RUTTitular = entradaUPago.Rut,
                    DigVerRUTTitular = entradaUPago.DigVerRut,
                    FechaMensaje = entradaUPago.FechaMensaje,
                    FechaTransaccion = entradaUPago.FechaTransaccion,
                    MontoPago = entradaUPago.MontoPago,
                    IdPagoExterno = entradaUPago.IdPagoExterno,
                    FormaPago = entradaUPago.MedioPago.GetFormaPago(),
                    GuidsVencimiento = entradaUPago.GuidsVencimiento,
                    Etiquetas = EtiquetaFactory.CreateEtiquetas(_trace, ProveedorPagoOnline.UPAGO, GuidsVencimiento)
                };
            }
            catch (JsonException ex)
            {
                return null;
            }
        }
    }

    public class MedioPago
    {
        [JsonProperty("IdTransaccion")]
        public string IdTransaccion { get; set; }

        [JsonProperty("CodigoAutorizacion")]
        public string CodigoAutorizacion { get; set; }

        [JsonProperty("TipoPago")]
        public string TipoPago { get; set; }

        [JsonProperty("SubTipoPago")]
        public string SubTipoPago { get; set; }

        [JsonProperty("NroCuotas")]
        public string NroCuotas { get; set; }

        [JsonProperty("UltimosDigitosTarjeta")]
        public string UltimosDigitosTarjeta { get; set; }

        [JsonProperty("PasarelaPago")]
        public string PasarelaPago { get; set; }

        [JsonProperty("TipoPagoAutomatico")]
        public string TipoPagoAutomatico { get; set; }

        [JsonProperty("CodigoRespuesta")]
        public string CodigoRespuesta { get; set; }

        [JsonProperty("DescripcionRespuesta")]
        public string DescripcionRespuesta { get; set; }

        public string GetFormaPago()
        {
            var propiedadesNoVacias = new[]
            {
                this.TipoPago,
                this.SubTipoPago,
                this.PasarelaPago,
                this.TipoPagoAutomatico
            };

            var formaPago = string.Join(" - ", propiedadesNoVacias.Where(p => !string.IsNullOrEmpty(p)));

            return formaPago;
        }
    }
}