

namespace WcfWebPay.Servicios
{
    public class Etiqueta
    {
        public string Nombre { get; set; }

        public System.Drawing.Color Color { get; set; }

        #region Constructores privados

        // Factory Method: constructor semántico
        public static Etiqueta CreateOK()
        {
            // Instanciamos la Etiqueta
            return new Etiqueta
            {
                Nombre = "OK",
                Color = System.Drawing.Color.LightGreen
            };
        }

        // Factory Method: constructor semántico
        public static Etiqueta CreateKO(string nombre, System.Drawing.Color color)
        {
            // Instanciamos la Etiqueta
            return new Etiqueta
            {
                Nombre = nombre,
                Color = color
            };
        }

        #endregion
    }

    public class EtiquetaFactory
    {
        #region Constantes
        private const string OK = "Resultado OK";
        #endregion

        // Factory Method
        public static List<Etiqueta> CreateEtiquetas(InsLogTrace trace, ProveedorPagoOnline proveedor, Guid[] guidsVencimiento)
        {
            // Validar argumento
            if (trace == null)
                return Enumerable.Empty<Etiqueta>().ToList();

            // Si el pago está inyectado, la etiqueta es OK
            if (trace.Mensaje == OK)
                return new List<Etiqueta> { Etiqueta.CreateOK() };

            try
            {
                // Intentamos leer el error del trace
                Error error = JsonConvert.DeserializeObject<Error>(trace.Mensaje);

                // Sin error, no podemos continuar
                if (error == null)
                    return Enumerable.Empty<Etiqueta>().ToList();

                // Si el error es de negocio sobre el estado del vencimiento, investigamos a fondo la situación actual
                if (error.ErrorCode == (int)ResponseError.VencimientosNoCobrables)
                    return CreateEtiquetasEstadoVencimiento(proveedor, guidsVencimiento);

                // En cualquier otro caso, devolvemos la descripción del propio error detectado en la traza
                return new List<Etiqueta> { Etiqueta.CreateKO(error.Descripcion, Color.DarkOrange) };
            }
            catch (JsonException ex)
            {
                return Enumerable.Empty<Etiqueta>().ToList();
            }
        }

        private static List<Etiqueta> CreateEtiquetasEstadoVencimiento(ProveedorPagoOnline proveedor, Guid[] guidsVencimiento)
        {
            // Leemos los vencimientos del pago online
            RecVencimientoCollection vencimientos = ManagerRecVencimiento.GetColeccionByGuids(guidsVencimiento);
            if (vencimientos == null || vencimientos.Count <= 0)
                return Enumerable.Empty<Etiqueta>().ToList();

            List<Etiqueta> etiquetas = new List<Etiqueta>();

            // Comprobamos el estado actual del cargo y obtenemos su etiqueta
            foreach (var vencimiento in vencimientos)
            {
                switch (vencimiento.Estado)
                {
                    case VencimientoEstado.CobradoPapel:
                        etiquetas.Add(Etiqueta.CreateKO($"Cargo {vencimiento.ToString()} ya cobrado. Posible reintento de {proveedor}.", Color.LightYellow));
                        break;

                    case VencimientoEstado.Anulado:
                        etiquetas.Add(Etiqueta.CreateKO($"Cargo {vencimiento.ToString()} anulado. Posible modificación hecha por el colegio.", Color.Orange));
                        break;

                    case VencimientoEstado.EmitidoPapel:
                    case VencimientoEstado.Facturado:
                        etiquetas.Add(Etiqueta.CreateKO($"Cargo {vencimiento.ToString()} no cobrado. Realizar reintento desde {proveedor}.", Color.OrangeRed));
                        break;

                    case VencimientoEstado.Pendiente:
                        etiquetas.Add(Etiqueta.CreateKO($"Cargo {vencimiento.ToString()} no cobrado íntegramente. Verificar importe pago en {proveedor} y Alexia.", Color.DarkRed));
                        break;
                }
            }
            return etiquetas;
        }
    }
}
