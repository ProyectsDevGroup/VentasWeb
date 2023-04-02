using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Producto()
        {
            return View();
        }

        // GET: Reporte
        public ActionResult Ventas()
        {
            return View();
        }

        public JsonResult ObtenerProducto(int idtienda, string codigoproducto)
        {
            List<ReporteProducto> lista = CD_Reportes.Instancia.ReporteProductoTienda(idtienda, codigoproducto);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerVenta(string fechainicio, string fechafin, int idtienda)
        {
            
            /*List<ReporteVenta> lista = CD_Reportes.Instancia.ReporteVenta(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idtienda);
            return Json(lista, JsonRequestBehavior.AllowGet);*/

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
						List<ReporteVenta> lista = CD_Reportes.Instancia.ReporteVenta( dateTimeInic, dateTimeFin, idtienda);
						if (lista == null)
							lista = new List<ReporteVenta>();

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


    }
}