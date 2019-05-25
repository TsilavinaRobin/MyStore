using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyStore.Models;
using PagedList;
using System.Data.SqlClient;

namespace MyStore.Controllers
{
    public class CRUDController : Controller

    {
        private MyStoreEntities4 db = new MyStoreEntities4();
        string connectionString = @" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\tsila\Documents\Visual Studio 2015\Projects\MyStore\MyStore\App_Data\MyStore.mdf';Initial Catalog=MyStore;Integrated Security = True";

        // GET: CRUD
        public ActionResult Index(int? page,string lm="")
        {
            try
            {
                int pageSize = 3;
            int pageNumber = (page ?? 1);
          
            List<GENRE> g = new List<GENRE>();
            g = db.GENRE.Where(x => x.IdUser == ANDRANA.IDUSER).ToList();
            if (lm == "vvvv") {
            ViewBag.Message = "Les réductions des prix des articles sont effectuées avec succèes";
            }
            else if(lm == "vb")
            {
                ViewBag.Message = "La suppression est effectuée avec succèes";
            }
            return View(g.ToPagedList(pageNumber, pageSize));
             }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }

        }

        // GET: CRUD/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRE gENRE = db.GENRE.Find(id);
            if (gENRE == null)
            {
                return HttpNotFound();
            }
            return View(gENRE);
             }
               catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CRUD/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGenre,NomGenre,Image,Taille,Prix,Ref,Description,Couleur,NbrGenre,Etat,IdProd,IdPanier,Nouveau,Reduction,Prix1,Pour,Date,IdUser,IdCat,IdUser1")] GENRE gENRE)
        {
            if (ModelState.IsValid)
            {
                db.GENRE.Add(gENRE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gENRE);
        }

        // GET: CRUD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRE gENRE = db.GENRE.Find(id);
            if (gENRE == null)
            {
               
                    return RedirectToAction("HttpNotFoun", "ACCUEIL");
                
            }
            return View(gENRE);
        }
        public ActionResult Remise(int? id,double? Reduction,string Tout="")
        {
            try
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<GENRE> ListGenre = new List<GENRE>();
            ListGenre = db.GENRE.Where(x => x.IdUser == ANDRANA.IDUSER).ToList();
            GENRE gENRE = db.GENRE.Find(id);
       gENRE.Prix1= (gENRE.Prix-(gENRE.Prix * Reduction) / 100);
            ViewBag.E = Convert.ToInt32(gENRE.Prix1);

            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                if (Tout == "Un")
                {
                string query = "UPDATE GENRE  SET Prix1='" + Convert.ToInt32(gENRE.Prix1) + "',Reduction='" + Reduction + "' where IdGenre='" + id + "' "; 
                SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                SqlCom.ExecuteNonQuery();
                    ViewBag.Message = "Lavoaloany";
                }
                else if(Tout=="Tout")
                {
                    foreach(var item in ListGenre)
                    {
                        item.Prix1 = (item.Prix - (item.Prix * Reduction) / 100);
                       
                        string query = "UPDATE GENRE  SET Prix1='" + Convert.ToInt32(item.Prix1) + "',Reduction='" + Reduction + "' where IdGenre='" + item.IdGenre + "' ";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.ExecuteNonQuery();
                        ViewBag.Message = "La réduction de prix des articles sont effectuées avec succès";
                    }
                }
              

            }

            if (gENRE == null)
            {
                
                    return RedirectToAction("HttpNotFoun", "ACCUEIL");
                
            }
            return RedirectToAction("Index",new { @lm="vvvv"});
            }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }



        // POST: CRUD/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGenre,NomGenre,Image,Taille,Prix,Ref,Description,Couleur,NbrGenre,Etat,IdProd,IdPanier,Nouveau,Reduction,Prix1,Pour,Date,IdUser,IdCat,IdUser1")] GENRE gENRE)
        {
            try
            {
                if (ModelState.IsValid)
            {
                db.Entry(gENRE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gENRE);
             }
              catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }

        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENRE gENRE = db.GENRE.Find(id);
            db.GENRE.Remove(gENRE);
            db.SaveChanges();
          
            if (gENRE == null)
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
            return RedirectToAction("Index", new { @lm = "vb" });
             }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        // POST: CRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            GENRE gENRE = db.GENRE.Find(id);
            db.GENRE.Remove(gENRE);
            db.SaveChanges();
            return RedirectToAction("Index", new { @lm = "vb" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
