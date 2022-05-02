using lib_apriori_net.Data;
using lib_apriori_net.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_mvc_apriori_net.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            try
            {
                var data = loadData.LoadExcelFromStream(file.InputStream);
                loadData.LearnProcess(data);

                ViewBag.Message = "Fichero procesado con exito";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Carga fallida";
                return View("Index");
            }
        }

        [HttpGet]
        public JsonResult loadDataIA()
        {
            SingletonPattern singletonPattern = new SingletonPattern();
            var datos = singletonPattern._singletonSelfService.FinalResult.ToList() ;

            return Json(datos, JsonRequestBehavior.AllowGet);
        }
    }
}