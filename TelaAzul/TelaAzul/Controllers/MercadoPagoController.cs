using Microsoft.AspNetCore.Mvc;

namespace TelaAzul.Controllers
{
    public class MercadoPagoController : Controller
    {
        [HttpGet]
        public IActionResult retornoMercadoPago(string collection_id, string collection_status, string payment_id,
               string status, string external_reference, string payment_type, string merchant_order_id,
               string preference_id, string site_id, string processing_mode, string merchant_account_id)
        {
            //obter o pagamento pelo idPreferencia(preference_id);
            if (status == "approved")
            {
                //baixar como pago
                return RedirectToAction("Finalizacao", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
