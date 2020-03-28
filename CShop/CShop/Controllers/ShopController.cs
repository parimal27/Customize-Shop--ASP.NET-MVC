using CShop.Models;
using Microsoft.Owin.Security.Twitter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CShop.Controllers
{
    public class ShopController : Controller
    {
       public DMODEL1 d = new DMODEL1();
        public ActionResult Index()
        {
            if(Convert.ToInt32(Session["login"])==1)
            {
                Session["login"] = 0 ;
                ViewBag.loginset = 1;
            }
            return View();
        }
        public ActionResult Product(int? pcate)
        {
            if(pcate==null)
            {
                pcate = 1;
            }   
            
            var t = from tt in d.Products select tt;
            var temp = t.Where(ty=>ty.pcate==pcate);
            return View(temp);
        }

       public JsonResult AjaxSingle(int spid11,int spid22)
        {
            var spartid12 = from im in d.ProductPartMaps where im.partid1 == spid11 && im.partid2 == spid22 select im;

            var image = spartid12.Select(x => x.image).FirstOrDefault();
            //var p1 = d.Parts.Where(x => x.partID == spid11);
            //var p2 = d.Parts.Where(x => x.partID == spid22);
            var p = from p11 in d.Parts  select p11;
            var p1 = p.Where(x=>x.partID==spid11);
            var p2 = p.Where(x=>x.partID==spid22);
            Dictionary<string, string> json = new Dictionary<string, string>();
            json.Add("image",image);
            json.Add("spid1", spartid12.Select(x => x.partid1).FirstOrDefault().ToString());
            json.Add("spid2", spartid12.Select(x => x.partid2).FirstOrDefault().ToString());
            int price = ViewBag.price = p1.Select(x => x.price).FirstOrDefault() + p2.Select(x => x.price).FirstOrDefault();
            json.Add("price",price.ToString());
            string name,desc;
            if (p1.Select(x => x.name).Single().Equals(p2.Select(x => x.name).Single()))
                name = p1.Select(x => x.name).Single();
            else
                name = p1.Select(x => x.name).Single() + " & " + p2.Select(x => x.name).Single();
            if (p1.Select(x => x.desc).Single().Equals(p2.Select(x => x.desc).Single()))
                desc = p1.Select(x => x.desc).Single();
            else
                desc = p1.Select(x => x.desc).Single() + " & " + p2.Select(x => x.desc).Single();
            json.Add("name",name);
            json.Add("desc", desc);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(json));
        }
        public JsonResult Search(string search)
        {
            Dictionary<string, string> json = new Dictionary<string, string>();

            var t = from df in d.Products where df.pname.Contains(search) select df;
            string dfg=null;
            foreach (var y in t)
            {
                dfg = dfg + @"<a href='/Shop/Single/?pid=" + y.PId + "' >" + y.pname+"</a><br>";
            }
            json.Add("name", dfg);
            json.Add("d","hello");
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(json));
           
        }
        public ActionResult Single(int? pid,int? spid1,int? spid2)
        {
            var gdata = from ddd in d.Parts select ddd;
            var pnamearray = gdata.Select(x => x.cate).Distinct();
            ViewBag.partnames = gdata.Select(x => x.cate).Distinct();
           
            if (pid!=null)
            {
                var set = from s in d.Products where s.PId == pid select s;
                var sett = set.Select(x=>x.pimage).Single();
        
                var spartid12 = from ss in d.ProductPartMaps select ss;
                var spartid121 = spartid12.Where(x => x.image==sett).Select(x=>x.image).SingleOrDefault();
                var partid1= spartid12.Where(x => x.image == sett).Select(x => x.partid1).SingleOrDefault();
                var partid2= spartid12.Where(x => x.image == sett).Select(x => x.partid2).SingleOrDefault();
                var p = from a in d.Parts select a;
             var p1 = p.Where(x => x.partID == partid1);
                var p2 = p.Where(x => x.partID == partid2);
                ViewBag.spid1 = p1.Select(t=>t.partID).SingleOrDefault();
                ViewBag.spid2 = p2.Select(t => t.partID).SingleOrDefault();
                ViewBag.price = p1.Select(x=>x.price).FirstOrDefault()+p2.Select(x=>x.price).FirstOrDefault();
                int p1price = Convert.ToInt32(p1.Select(x => x.price).Single());
                int p2price = Convert.ToInt32(p2.Select(x => x.price).Single());
                ViewBag.price = p1price + p2price;
               
                if (p1.Select(x => x.desc).Single().Equals(p2.Select(x => x.desc).Single()))
                    ViewBag.desc = p1.Select(x => x.desc).Single();
                else
                    ViewBag.desc = p1.Select(x => x.desc).Single() + " " + p2.Select(x => x.desc).Single();
                if (p1.Select(x => x.name).Single().Equals(p2.Select(x => x.name).Single()))
                    ViewBag.name = p1.Select(x => x.name).Single();
                else
                ViewBag.name = p1.Select(x => x.name).Single() +" "+ p2.Select(x => x.name).Single(); 
               ViewBag.image = spartid121.ToString();
                 
            } 
            else 
            {
                if(spid1!=null || spid2!=null)
                {
                    ViewBag.spid1 = spid1;
                    ViewBag.spid2 = spid2;
                    var spartid12 = from im in d.ProductPartMaps where im.partid1 == spid1 && im.partid2 == spid2 select im;
                    ViewBag.image = spartid12.Select(x => x.image).FirstOrDefault();
                 //   gdata=from ddd in d.Parts select ddd;
                }
            }
            if(pid<10)
            {
                ViewBag.pname1 = "Dials";
                ViewBag.pname2 = "Belts";
                var gdata1 = from ddd in d.Parts where ddd.cate=="belt"  || ddd.cate=="dial" select ddd;
                return View(gdata1.ToList());
               
            }
            else 
            {
                ViewBag.pname1 = "Lens";
                ViewBag.pname2 = "Sticks";
                var gdata1 = from ddd in d.Parts where ddd.cate == "lens" || ddd.cate == "stick" select ddd;
                return View(gdata1.ToList());

            }
          
        }
        public ActionResult Register(string name,string mno,string email,string password)
        {
            Models.Login l = new Models.Login
            {
                name = name,
                email = email,
                password = password,
                mno = Convert.ToInt64(mno),
                cate="user"
                
            };
            try
            {
                d.Logins.Add(l);
                d.SaveChanges();
                Dictionary<string, string> ttt = new Dictionary<string, string>();
                ttt.Add("valid", "Shop/Index");
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                return Json(serializer.Serialize(ttt));
            }
            catch
            {
                Dictionary<string, string> ttt = new Dictionary<string, string>();
                ttt.Add("valid", "User already Exist..");
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                return Json(serializer.Serialize(ttt));
            }
           
        }
        [HttpPost]
        public JsonResult CheckQty(int qty,int spid1,int spid2)
        {
            var qtyv = from q in d.Parts select q;
            var qq1 = qtyv.Where(x => x.noparts >= qty && x.partID == spid1).Select(x=>x.noparts).SingleOrDefault();
            var qq2 = qtyv.Where(x => x.noparts >= qty && x.partID == spid2).Select(x => x.noparts).SingleOrDefault();
            string qtyvalid="Sorry,Please try after some days Quantity is not available that much..";
           
            Dictionary<string, string> ty = new Dictionary<string, string>();
            int i = 0;
            if (qq1 >= qty && qq2 >= qty)
            {
                i = 1;
                qtyvalid = "Available...Please Hurry up!";
            }
            ty.Add("qty", qtyvalid);
            ty.Add("flag", i.ToString());
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(ty));
        }
        [HttpPost]
        public JsonResult DeleteCart(int id)
        {
            var t = from cc in d.Carts select cc;
            Cart c=t.Where(x => x.CartID == id).SingleOrDefault();
            if (c != null)
            {
                d.Carts.Remove(c);
                d.SaveChanges();
            }
            Dictionary<string, string> ttt = new Dictionary<string, string>();
            ttt.Add("q","deleted");
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            return Json(serializer.Serialize(ttt));
        }
       
        public ActionResult Cart(int? qty,int? spid1,int? spid2,string image,string pname,int? price)
        {
            if (Session["email"] == null)
            {
                Session["login"] = 1;
                return RedirectToAction("Index");

            }
            string s = (string)Session["email"];
            if (qty != null)
            {
                Cart c = new Models.Cart
                {
                    spid1 = spid1,
                    spid2 = spid2,
                    pname = pname,
                    uid = s,
                    qty = qty,
                    image = image,
                    price = price
                };
                d.Carts.Add(c);
                d.SaveChanges();
            }
            var t = from cc in d.Carts  select cc;
            

            return View(t.ToList());
        }
        [HttpPost]
        public ActionResult Login(string email,string password,string url)
        {
            try
            {
                var l = from ll in d.Logins where ll.email == email && ll.password==password select ll;
                if (l.Count() == 1)
                {
                    Session["email"] = email;
                    Session["cate"] = l.Select(x => x.cate).SingleOrDefault().ToString();
                   // FormsAuthentication.SetAuthCookie(email,false);
                    Dictionary<string, string> ttt = new Dictionary<string, string>();
                    if(Session["cate"].ToString().Equals("user"))
                    ttt.Add("valid", "Shop/Index");
                    else
                    ttt.Add("valid", "Products/Index");
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    return Json(serializer.Serialize(ttt));

                }
                else
                {
                    Dictionary<string, string> ttt = new Dictionary<string, string>();
                    ttt.Add("valid", "Please enter Correct Credentials..");
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    return Json(serializer.Serialize(ttt));

                }
            }
            catch
            {
                Dictionary<string, string> ttt = new Dictionary<string, string>();
                ttt.Add("valid", "Please enter Correct Credentials.");
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                return Json(serializer.Serialize(ttt));

            }
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
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
    }
}