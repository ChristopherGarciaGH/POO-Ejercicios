


namespace WCFWebPay.Servicios
{
    public static class EntradaPagoFactory
    {
        private const string REGISTRANDO_ENTRADA = "Registrando entrada";

        #region Métodos privados

        /// <summary>
        /// Adaptar la traza al proveedor
        /// </summary>
        public static IEntradaPago Create(InsLogTrace trace)
        {
            // Si la entrada es simplemente un registro de entrada, no nos interesa
            if (trace.Mensaje.Contains(REGISTRANDO_ENTRADA))
                return null;

            switch (GetProveedor(trace.Metodo))
            {
                case ProveedorPagoOnline.UPAGO: return new EntradaUPago(trace);
                case ProveedorPagoOnline.ZUMPAGO: return new EntradaZumPago(trace);
                default: return null;
            }
        }

        public static ProveedorPagoOnline GetProveedor(string metodo)
        {
            switch (metodo)
            {
               case "SetPagoTitular": return ProveedorPagoOnline.UPAGO;
               case "SetPagoAlumno": return ProveedorPagoOnline.ZUMPAGO;
               default: return ProveedorPagoOnline.NONE;
            }
        }

        #endregion
    }
}