using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using MyStore.Models;
using System.Web.Helpers;
using PagedList;

namespace MyStore.Controllers
{
   
    public class VENDEURController : Controller
    {
        // GET: VENDEUR
        string connectionString = @" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\tsila\Documents\Visual Studio 2015\Projects\MyStore\MyStore\App_Data\MyStore.mdf';Initial Catalog=MyStore;Integrated Security = True";
                                    
        public ActionResult Index(int? page,string ms="")
        {
            try
            {
                MyStoreEntities4 db = new MyStoreEntities4();

            List<VENDEUR> VendeurList = new List<VENDEUR>();

            VendeurList = db.VENDEUR.ToList();
          
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.Mon = ms;

            return View(VendeurList.ToPagedList(pageNumber, pageSize));
             }
              catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }

        }
        public ActionResult Article(int? page,int? id,string ms="")
        {
            try
            {
                MyStoreEntities4 db = new MyStoreEntities4();

            List<PANIER> PanierList = new List<PANIER>();

            PanierList = db.PANIER.Where(x => x.IdUser!=null).ToList();
            PanierList.Where(x => x.IdUser == id).ToList();
            int pageSize = 7;
            int pageNumber = (page ?? 1);

            ViewBag.Mon = ms;

            return View(PanierList.ToPagedList(pageNumber, pageSize));
            }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
     
        public ActionResult Autoriser(int id)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "UPDATE VENDEUR  SET Autorisation='true' where IdUser='" + id + "' ";
                SqlCommand SqlCom = new SqlCommand(query, SqlCon);
              
                ViewBag.MonParam1 = "MODIFICATION AVEC SUCCES";
                SqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Index",new { ms= "L'utilisateur est autorisé succès!!" });
             }
       catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Livrer(int id)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "UPDATE GENRE  SET Etat='true' where IdPanier='" + id + "' ";
                SqlCommand SqlCom = new SqlCommand(query, SqlCon);

                ViewBag.Mon = "La livraison de votre article est effectuée avec succèes!!";
                SqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Article",new {ms= "La livraison de votre article est effectuée avec succèes!!" });
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Bloquer(int id)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "UPDATE VENDEUR  SET Autorisation='false' where IdUser='" + id + "' ";
                SqlCommand SqlCom = new SqlCommand(query, SqlCon);

                ViewBag.MonParam1 = "MODIFICATION AVEC SUCCES";
                SqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Index", new { ms = "L'utilisateur est Bloqué succès!!" });
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        //return View();

      
        // GET: VENDEUR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VENDEUR/Create
        public ActionResult Create()
        {
            try
            {
                return View(new VENDEUR1());
            }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        // POST: VENDEUR/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VENDEUR1 VENDEURS)
        {

            // return View(model);
            //try
            //{
               // if (VENDEURS.isValid())
               // {
                    int nbrLigne = 0;
                    DataTable dt = new DataTable();
                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {
                        SqlCon.Open();
                        string query = "SELECT * FROM VENDEUR";
                        SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                        SqlDa.Fill(dt);
                    }
                    nbrLigne = Convert.ToInt32(dt.Rows.Count.ToString());
                    int val = 1;
                    if (dt.Rows.Count == 0)
                    {
                        using (SqlConnection SqlCon = new SqlConnection(connectionString))
                        {
                            SqlCon.Open();


                            string query = "INSERT INTO VENDEUR VALUES (@IdUser,@Nom,@Prenom,@DateNaiss,@Email,@Mdp,@Magasin,@Contact,@Type,@Autorisation,@Lieu)";
                            SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                            SqlCom.Parameters.AddWithValue("@IdUser", val);
                            SqlCom.Parameters.AddWithValue("@Nom", VENDEURS.Nom);
                            SqlCom.Parameters.AddWithValue("@Prenom", VENDEURS.Prenom);
                            SqlCom.Parameters.AddWithValue("@DateNaiss", VENDEURS.DateNaiss);
                            SqlCom.Parameters.AddWithValue("@Email", VENDEURS.Email);
                            SqlCom.Parameters.AddWithValue("@Mdp", VENDEURS.Mdp);
                            SqlCom.Parameters.AddWithValue("@Magasin", VENDEURS.Magasin);
                            SqlCom.Parameters.AddWithValue("@Contact", VENDEURS.Contact);
                            SqlCom.Parameters.AddWithValue("@Type", "Vendeur");
                            SqlCom.Parameters.AddWithValue("@Autorisation", "false");
                            SqlCom.Parameters.AddWithValue("@Lieu", VENDEURS.Lieu);
                            SqlCom.ExecuteNonQuery();
                            return RedirectToAction("Create", "LOGIN");


                        }
                    }
                    else
                    {
                        using (SqlConnection SqlCon = new SqlConnection(connectionString))
                        {
                            SqlCon.Open();
                            MyStoreEntities4 GJ = new MyStoreEntities4();
                            VENDEUR vvn = new VENDEUR();
                            vvn = GJ.VENDEUR.ToList().Last();

                            val = vvn.IdUser + 1;
                    string query;
                    if (VENDEURS.Magasin != null)
                    {
                         query = "INSERT INTO VENDEUR (IdUser,Nom,Prenom,DateNaiss,Email,Mdp,Magasin,Contact,Type,Autorisation,Lieu)  VALUES (@IdUser,@Nom,@Prenom,@DateNaiss,@Email,@Mdp,@Magasin,@Contact,@Type,@Autorisation,@Lieu)";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.Parameters.AddWithValue("@IdUser", val);
                        SqlCom.Parameters.AddWithValue("@Nom", VENDEURS.Nom);
                        SqlCom.Parameters.AddWithValue("@Prenom", VENDEURS.Prenom);
                        SqlCom.Parameters.AddWithValue("@DateNaiss", VENDEURS.DateNaiss);
                        SqlCom.Parameters.AddWithValue("@Email", VENDEURS.Email);
                        SqlCom.Parameters.AddWithValue("@Mdp", VENDEURS.Mdp);

                        SqlCom.Parameters.AddWithValue("@Magasin", VENDEURS.Magasin);


                        SqlCom.Parameters.AddWithValue("@Contact", VENDEURS.Contact);
                        SqlCom.Parameters.AddWithValue("@Type", "Vendeur");
                        SqlCom.Parameters.AddWithValue("@Autorisation", "false");
                        SqlCom.Parameters.AddWithValue("@Lieu", VENDEURS.Lieu);
                        SqlCom.ExecuteNonQuery();
                    }
                    else {
                    query = "INSERT INTO VENDEUR (IdUser,Nom,Prenom,DateNaiss,Email,Mdp,Contact,Type,Autorisation,Lieu)  VALUES (@IdUser,@Nom,@Prenom,@DateNaiss,@Email,@Mdp,@Contact,@Type,@Autorisation,@Lieu)";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.Parameters.AddWithValue("@IdUser", val);
                        SqlCom.Parameters.AddWithValue("@Nom", VENDEURS.Nom);
                        SqlCom.Parameters.AddWithValue("@Prenom", VENDEURS.Prenom);
                        SqlCom.Parameters.AddWithValue("@DateNaiss", VENDEURS.DateNaiss);
                        SqlCom.Parameters.AddWithValue("@Email", VENDEURS.Email);
                        SqlCom.Parameters.AddWithValue("@Mdp", VENDEURS.Mdp);
                        SqlCom.Parameters.AddWithValue("@Contact", VENDEURS.Contact);
                        SqlCom.Parameters.AddWithValue("@Type", "Vendeur");
                        SqlCom.Parameters.AddWithValue("@Autorisation", "false");
                        SqlCom.Parameters.AddWithValue("@Lieu", VENDEURS.Lieu);
                        SqlCom.ExecuteNonQuery();
                    }
                   
                            SqlCon.Close();
                            return RedirectToAction("Create", "LOGIN", new {v="vb" });


                        }

                    }
                    //  return RedirectToAction("Index");
                //}
                //else
                //{
                //    ViewBag.Erreur = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.";
                //    return View();
                //}

                // TODO: Add insert logic here


           // }
            //catch
            //{
            //    return RedirectToAction("HttpNotFoun", "ACCUEIL");
            //}
        }

        // GET: VENDEUR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VENDEUR/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VENDEUR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VENDEUR/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
