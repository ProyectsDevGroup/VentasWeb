using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class CompraController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Compra
        public ActionResult Crear()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }
        // GET: Compra
        public ActionResult Consultar()
        {
            return View();
        }

        public ActionResult Documento(int idcompra = 0) {
            
            Compra oCompra = CD_Compra.Instancia.ObtenerDetalleCompra(idcompra);

            if (oCompra == null) {
                oCompra = new Compra();
            }


            return View(oCompra);
        }


        public JsonResult Obtener(string fechainicio, string fechafin, int idproveedor, int idtienda)
        {
            /*List<Compra> lista = CD_Compra.Instancia.ObtenerListaCompra(DateTime.Parse(fechainicio), DateTime.Parse(fechafin), idproveedor, idtienda);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);*/

			try
			{
				DateTime dateTimeInic;
				DateTime dateTimeFin;
				string[] validformats = new[] { "dd/MM/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
										"MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

				CultureInfo provider = CultureInfo.InvariantCulture;

				if (DateTime.TryParseExact(fechainicio, validformats, provider,
											DateTimeStyles.None, out dateTimeInic))
				{
					if (DateTime.TryParseExact(fechafin, validformats, provider,
											DateTimeStyles.None, out dateTimeFin))
					{
						List<Compra> lista = CD_Compra.Instancia.ObtenerListaCompra(dateTimeInic, dateTimeFin, idproveedor, idtienda);
						if (lista == null)
							lista = new List<Compra>();

						return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
					}
					else
					{
						Console.WriteLine("Unable to parse the specified date");
					}
				}
				else
				{
					Console.WriteLine("Unable to parse the specified date");
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}


        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            xml = xml.Replace("!idusuario¡", SesionUsuario.IdUsuario.ToString());

            bool respuesta  = CD_Compra.Instancia.RegistrarCompra(xml);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }



    }
}