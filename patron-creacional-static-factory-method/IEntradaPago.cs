

namespace WCFWebPay.Servicios
{
    public interface IEntradaPago
    {  
        /// <summary>
        /// Adapta la InsLogTrace a un DTO adecuado para el proveedor de pagos online
        /// </summary>
        /// <returns></returns>
        EntradaPagoRespuesta AdaptarTrace();
    }
}