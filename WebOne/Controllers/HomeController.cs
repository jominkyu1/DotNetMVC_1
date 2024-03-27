using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast;
using WebOne.Filter;
using WebOne.Service;

using PagedList;

namespace WebOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService = new HomeService();
        private readonly HangService _hangService = new HangService();

        //[PipelineFilter]
        public ActionResult Index(string transNum, int? deliveryCase, string hangDetail, int? page)
        {
            var pageNumber = page ?? 1;
            if (!string.IsNullOrEmpty(transNum) && deliveryCase > 0)
            {
                switch (deliveryCase)
                {
                    case 1:
                        ViewBag.TransInfo = _homeService.GetTransInfo(transNum);
                        ViewBag.TransList = _homeService.GetTransList(transNum);
                        break;
                    case 2:
                        ViewBag.HangList = _hangService.FindByPchNo(transNum);
                        ViewBag.HangDetailList = _hangService.FindDetailByPchNo(hangDetail).ToPagedList(pageNumber, 15);
                        break;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {
            return new HttpUnauthorizedResult();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}