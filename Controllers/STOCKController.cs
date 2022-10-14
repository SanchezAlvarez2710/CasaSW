using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaSW.Models.ViewModel;
using CasaSW.Models;

namespace CasaSW.Controllers
{
    public class STOCKController : Controller
    {
        CasaSW.Models.CASASWEntities db = new Models.CASASWEntities();
        // GET: STOCK
        public ActionResult Index()
        {
            var OrderProduct = from o in db.ORDER
                               join p in db.PRODUCT on o.id_product equals p.id_product
                               select new OrderProduct
                               {

                                   id_persona_ = (int)p.id_persona,                                   
                                   orderName_ = o.orderName,
                                   state_ = o.state,
                                   subtotal_ = o.subtotal,
                                   total_ = o.total,
                                   id_product_ = p.id_product,
                                   product_name = p.name,
                                   product_type = p.type,
                                   product_version = p.version,
                                   product_description = p.description
                               };
                               
            if (OrderProduct == null)
            {
                return HttpNotFound();
            }
            return View(OrderProduct);
        }
    }
}
