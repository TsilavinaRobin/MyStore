using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using MyStore.Models;
using System.IO;
using PagedList;

namespace MyStore.Controllers
{
    public class LOGINController : Controller
    {
        // GET: LOGIN
        string connectionString = @" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\tsila\Documents\Visual Studio 2015\Projects\MyStore\MyStore\App_Data\MyStore.mdf';Initial Catalog=MyStore;Integrated Security = True";
        static int IDUSER = 20;
        static int IDCAT = 20;
        static int IDPROD = 20;
        //static int IDUSER = ANDRANA.IDUSER;
        //static int IDCAT = ANDRANA.IDCAT;
        //static int IDPROD = ANDRANA.IDPROD;
        //  SqlCommand cmd;
        //  SqlDataReader dr;
        public ActionResult Index()
        {
            return View();
        }

        // GET: LOGIN/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LOGIN/Create
        public ActionResult Create(string v="")
        {
           
            try
            {
                ViewBag.log = v;
                return View(new LOGIN());
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        // POST: LOGIN/Create
        [HttpPost]
        public ActionResult Create(LOGIN LOGINS)
        {
            try
            {
                // TODO: Add insert logic here
                DataTable dt = new DataTable();
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    string query = "SELECT * FROM VENDEUR where Email=@Email AND  Mdp=@Mdp AND Type=@Type ";
                    SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                    SqlDa.SelectCommand.Parameters.AddWithValue("@Email", LOGINS.Email);
                    SqlDa.SelectCommand.Parameters.AddWithValue("@Mdp", LOGINS.Mdp);
                    SqlDa.SelectCommand.Parameters.AddWithValue("@Type", LOGINS.Type);
                    SqlDa.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        
                        IDUSER = Convert.ToInt32(dt.Rows[0][0].ToString());
                        ViewBag.MonParam1 =IDUSER ;
                        ViewBag.MonParam2 = dt.Rows[0][4].ToString();
                        ViewBag.MonParam3 = dt.Rows[0][5].ToString();
                        ANDRANA.Nom = true;
                        ANDRANA.Type = LOGINS.Type;
                        ANDRANA.Name= dt.Rows[0][1].ToString();
                        ANDRANA.Autorisation = dt.Rows[0][9].ToString();
                        ANDRANA.IDUSER = Convert.ToInt32(dt.Rows[0][0].ToString());
                        if (ANDRANA.Type == "Vendeur") {

                        return RedirectToAction("Categorie1", new { III = IDUSER });
                        }
                        else
                        {
                            return RedirectToAction("Index","VENDEUR" );
                        }


                    }
                    else
                    {
                        string Erreur = "mot de passe ou e-mail incorrecte";

                        ViewBag.MonParam3 = Erreur;
                       // ViewBag.MonParam4 = IDUSER;
                        return View();
                    }

                  //  return View();
                }

                //2èME ESSAI
                //DataTable dt = new DataTable();
                //using (SqlConnection SqlCon = new SqlConnection(connectionString))
                //{
                //    SqlCon.Open();
                //    string query = "SELECT * FROM VENDEUR where Email=@Email AND  Mdp=@Mdp ";
                //    SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                //    SqlDa.SelectCommand.Parameters.AddWithValue("@Email", LOGINS.Email);
                //    SqlDa.SelectCommand.Parameters.AddWithValue("@Mdp", LOGINS.Mdp);
                //    SqlDa.Fill(dt);
                    

                //}
              
              

                ////int id;

                //int lig = dt.Rows.Count;
                ////if (lig == 1)
                ////{
                ////    string em= dt.Rows[0][4].ToString(); 
                ////    string md = dt.Rows[0][5].ToString();
                ////    LOGINS.isValid(em, md);
                ////}

                //  IDUSER = 2; 
                ////LOGINS.isConnected(lig);
                //if (LOGINS.isConnected(lig) == true)
                //{
                //    //  IDUSER= Convert.ToInt32(dt.Rows[0][0].ToString());
                //    // ViewBag.MonParam4 = "ts mana";
                //    //    LOGINS.Mdp = dt.Rows[0][5].ToString();

                //    //return RedirectToAction("Create");

                //    return RedirectToAction("Categorie1");
                //}
                //else
                //{
                //    string Erreur = "mot de passe incorrecte";

                //    ViewBag.MonParam3 = Erreur;
                //    ViewBag.MonParam4 = IDUSER;
                //    return View();
                //}



            }
            catch
            {
                
                    return RedirectToAction("HttpNotFoun", "ACCUEIL");
               
            }
        }
      //  int ka;
       public ActionResult Deconnexion()
        {
            try
            {
                ANDRANA.IDUSER = 0;
                ANDRANA.Name = "";
                ANDRANA.IDPROD = 0;
                ANDRANA.IDPANIER = 0;
                ANDRANA.IDUSER = 0;
                ANDRANA.IDCAT = 0;
                return RedirectToAction("Create");
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
           
        }
        public ActionResult Categorie1(int? III)
        {

     
            try
            {
                // SqlDataReader dr;

                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {

                    SqlCon.Open();
                    SqlDataReader dr;
                    string query = "SELECT NomCat FROM CATEGORIE ";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    SqlCom.ExecuteNonQuery();
                    dr = SqlCom.ExecuteReader();
                    List<string> Catt = new List<string>();
                    while (dr.Read())
                    {
                        Catt.Add(dr[0].ToString());
                    }
                    ViewBag.Cat = Catt;

                }

                return View();
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        [HttpPost]
        public ActionResult Categorie1(CATEGORIE1 CATEGORIES)
        {
            

           // ViewBag.MonParam5 = iz;
            try {
                if(CATEGORIES.NomCat=="autre")
                {
                   // ViewBag.Vue = true;
                    return View("Produit");
                }
                int nbrLigne = 0;
                DataTable dt = new DataTable();
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    string query = "SELECT * FROM CATEGORIE";
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
                        string query = "INSERT INTO CATEGORIE (IdCat,NomCat,IdUser) VALUES (@IdCat,@NomCat,@IdUser)";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.Parameters.AddWithValue("@IdCat", val);
                        SqlCom.Parameters.AddWithValue("@NomCat", CATEGORIES.NomCat);
                        SqlCom.Parameters.AddWithValue("@IdUser", IDUSER );
                        IDCAT = val;
                        ANDRANA.IDCAT = IDCAT;
                        SqlCom.ExecuteNonQuery();
                        //string query1 = "INSERT INTO VENDEUR (IdUser,IdCat) VALUES (@IdUser,@IdCat)";
                        //SqlCommand SqlCom1 = new SqlCommand(query1, SqlCon);
                        //SqlCom1.ExecuteNonQuery();
                        return RedirectToAction("Produit1", new { III = IDCAT });
                    }
                }
                else
                {
                    //VERIFICATION SI CATEGORIE EXISTE (CONDITION)
                    DataTable dt1 = new DataTable();
                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {
                        SqlCon.Open();
                        string query = "SELECT * FROM CATEGORIE WHERE NomCat='"+ CATEGORIES.NomCat + "'";
                        SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                        SqlDa.Fill(dt1);
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        using (SqlConnection SqlCon = new SqlConnection(connectionString))
                        {
                            SqlCon.Open();
                            MyStoreEntities4 s = new MyStoreEntities4();
                            CATEGORIE c = new CATEGORIE();
                            c = s.CATEGORIE.ToList().Last();
                            val = c.IdCat + 1;
                            string query = "INSERT INTO CATEGORIE (IdCat,NomCat,IdUser) VALUES (@IdCat,@NomCat,@IdUser)";
                            SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                            SqlCom.Parameters.AddWithValue("@IdCat", val);
                            SqlCom.Parameters.AddWithValue("@NomCat", CATEGORIES.NomCat);
                            SqlCom.Parameters.AddWithValue("@IdUser", IDUSER);
                            SqlCom.ExecuteNonQuery();
                            IDCAT = val;
                            ANDRANA.IDCAT = IDCAT;
                            return RedirectToAction("Produit1", new { III = IDCAT });

                        }
                    }
                    else
                    {
                      
                        IDCAT = Convert.ToInt32(dt1.Rows[0][0].ToString());
                        return RedirectToAction("Produit1", new { III = IDCAT });
                    }
                        //FIN
                      
                }
   }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        //PRODUIT debut
        public ActionResult Produit1(int? III)
        {
            //2eme essai
            // ViewBag.MonParam5 = III;

            // ka = III;
            try
            { 
            return View();
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
            //  return RedirectToAction("Categorie1", new { III });
        }
        [HttpPost]
        public ActionResult Produit1(PRODUIT1 PRODUITS)
        {
            // ViewBag.MonParam5 = iz;
            try
            {
                int nbrLigne = 0;
                DataTable dt = new DataTable();
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    string query = "SELECT * FROM PRODUIT";
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


                        string query = "INSERT INTO PRODUIT (IdProd,NomPro,NbrPro,IdCat) VALUES (@IdProd,@NomPro,@NbrPro,@IdCat)";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.Parameters.AddWithValue("@IdProd", val);
                        SqlCom.Parameters.AddWithValue("@NomPro", PRODUITS.NomPro);
                    SqlCom.Parameters.AddWithValue("@NbrPro", 1);
                    SqlCom.Parameters.AddWithValue("@IdCat", IDCAT);
                        IDPROD = val;
                        ANDRANA.IDPROD = IDPROD;
                        SqlCom.ExecuteNonQuery();
                        // return RedirectToAction("index");
                        ViewBag.MonParam0 = "Produit est ajouter avec succès";
                        return RedirectToAction("Genre2",new { III = IDPROD });

                    }
                }
                else
                {
                    using (SqlConnection SqlCon = new SqlConnection(connectionString))
                    {
                        SqlCon.Open();
                        MyStoreEntities4 d = new MyStoreEntities4();
                        PRODUIT p1 = new PRODUIT();
                        p1 = d.PRODUIT.ToList().Last();
                        val = p1.IdProd + 1;
                        string query = "INSERT INTO PRODUIT (IdProd,NomPro,NbrPro,IdCat) VALUES (@IdProd,@NomPro,@NbrPro,@IdCat)";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                        SqlCom.Parameters.AddWithValue("@IdProd", val);
                        SqlCom.Parameters.AddWithValue("@NomPro", PRODUITS.NomPro);
                        SqlCom.Parameters.AddWithValue("@NbrPro", 1);
                        SqlCom.Parameters.AddWithValue("@IdCat", IDCAT);
                        SqlCom.ExecuteNonQuery();
                        IDPROD = val;
                        ANDRANA.IDPROD = IDPROD;
                        //  return RedirectToAction("Index");
                        ViewBag.MonParam0 = "Produit est ajouter avec succès";
                        return RedirectToAction("Genre2", new { III = IDPROD });


                    }
                }
                //  return RedirectToAction("Index");



                // TODO: Add insert logic here


            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }

        //PRODUIT FIN
        //GENRE DEBUT
        public ActionResult Genre2(int? III)
        {
            try
            {
            
            ViewBag.Essai = IDPROD;
            return View();
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }

        }
        [HttpPost]
        public ActionResult Genre2(GENRE1 GENRES)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(GENRES.ImageFile.FileName);
            string extension = Path.GetExtension(GENRES.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            GENRES.Image = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            GENRES.ImageFile.SaveAs(fileName);

            //ViewBag.M1= GENRES.Image;
            // ViewBag.M2 = GENRES.NomGenre;
            //ViewBag.M3 = ;
            //ViewBag.M4 = GENRES.Prix;
            //ViewBag.M5 = GENRES.Taille;
            //ViewBag.M6 = GENRES.Description;
            //ViewBag.M7 = GENRES.Couleur;
            int nbrLigne = 0;
            DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT * FROM GENRE";
                SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                SqlDa.Fill(dt);
            }
            nbrLigne = Convert.ToInt32(dt.Rows.Count.ToString());
            int val = 1;
            //Base de donnée 
            if (dt.Rows.Count == 0)
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();

                    

                    string query = "INSERT INTO GENRE (IdGenre, NomGenre,Image, Taille,Prix,Ref,Description,Couleur,NbrGenre,Etat,IdProd,Nouveau,Reduction,Prix1,Pour,Date,IdUser,IdCat,NbrAch) VALUES ('" + val + "', '" + GENRES.NomGenre + "','" + GENRES.Image + "','" + GENRES.Taille + "','" + GENRES.Prix + "','" + val + "/100','" + GENRES.Description + "','" + GENRES.Couleur + "','" + GENRES.NbrGenre + "','false','" + IDPROD + "','true','"+GENRES.Reduction+"','"+GENRES.CalculPrix1()+"','"+GENRES.Pour+"','"+ DateTime.Now.ToShortDateString() + "','"+IDUSER+"','"+IDCAT+"','"+1+"');  ";
                    
                      
                    
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    SqlCom.ExecuteNonQuery();
                    // return RedirectToAction("index");
                    ViewBag.Mon = "La publication de votre article est effectuée avec succèes!!";
                    // return RedirectToAction("Genre1", new { III = IDPROD });

                    return View();

                }
            }
            else
            {
                MyStoreEntities4 d = new MyStoreEntities4();
                GENRE g = new GENRE();
                g = d.GENRE.ToList().Last();
                val = g.IdGenre + 1;

                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();


                    string query = "INSERT INTO GENRE (IdGenre, NomGenre,Image, Taille,Prix,Ref,Description,Couleur,NbrGenre,Etat,IdProd,Nouveau,Reduction,Prix1,Pour,Date,IdUser,IdCat,NbrAch) VALUES ('" + val + "', '" + GENRES.NomGenre + "','" + GENRES.Image + "','" + GENRES.Taille + "','" + GENRES.Prix + "','" + val + "/100','" + GENRES.Description + "','" + GENRES.Couleur + "','" + GENRES.NbrGenre + "','false','" + IDPROD + "','true','" + GENRES.Reduction + "','" + GENRES.CalculPrix1() + "','" + GENRES.Pour + "','" + DateTime.Now.ToShortDateString() + "','" + IDUSER + "','" + IDCAT + "','" + 1 + "');  ";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    SqlCom.ExecuteNonQuery();

                    ViewBag.Mon ="La publication de votre article est effectuée avec succèes!!";
               
                    return View();

                }

            }
            //  return View();
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }

        }

        //public ActionResult Genre1(int III)
        //{
        //    ViewBag.Essai = IDPROD;
        //    return View();

        //}
        //[HttpPost]
        //public ActionResult Genre1(GENRE GENRES)
        //{
        //    string fileName = Path.GetFileNameWithoutExtension(GENRES.ImageFile.FileName);
        //    string extension = Path.GetExtension(GENRES.ImageFile.FileName);
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //    GENRES.Image = "~/Image/" + fileName;
        //    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
        //    GENRES.ImageFile.SaveAs(fileName);

        //    //   ViewBag.Ess = GENRES.Image;
        //    //Mampiditra ao anat base
        //    //  try {
        //    //using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    //{
        //    //    SqlCon.Open();


        //    //    string query = "INSERT INTO GENRE VALUES (@IdGenre,@NomGenre,@Image,@Taille,@Prix,@Ref,@Description,@Couleur,@NbrGenre,@Etat,@IdProd)";
        //    //    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
        //    //    SqlCom.Parameters.AddWithValue("@IdGenre", 1);
        //    //    SqlCom.Parameters.AddWithValue("@NomGenre", GENRES.NomGenre);
        //    //    SqlCom.Parameters.AddWithValue("@Image", GENRES.Image);
        //    //    SqlCom.Parameters.AddWithValue("@Taille", GENRES.Taille);
        //    //    SqlCom.Parameters.AddWithValue("@Prix", GENRES.Prix);
        //    //    SqlCom.Parameters.AddWithValue("@Ref", 1);
        //    //    SqlCom.Parameters.AddWithValue("@Description", GENRES.Description);
        //    //    SqlCom.Parameters.AddWithValue("@Couleur", GENRES.Couleur);
        //    //    SqlCom.Parameters.AddWithValue("@NbrGenre", GENRES.NbrGenre);
        //    //    SqlCom.Parameters.AddWithValue("@Etat", false);
        //    //    SqlCom.Parameters.AddWithValue("@IdProd", IDPROD);
        //    //  //  IDPROD = val;
        //    //    SqlCom.ExecuteNonQuery();
        //    //    // return RedirectToAction("index");
        //    //    ViewBag.Mon = " succès";
        //    //        // return RedirectToAction("Genre1", new { III = IDPROD });
        //    //        return View();

        //    //    }

        //    // }
        //    //catch(Exception ex)
        //    //{
        //    //    ViewBag.Mon = "Echec"+ex.Message;
        //    //    return View();
        //    //}
        //   // ViewBag.Mon = " succès";
        //    return View();

        //}

        //GENRE FIN
        // GET: LOGIN/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LOGIN/Edit/5
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

        // GET: LOGIN/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LOGIN/Delete/5
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
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Notification(int? III, int? page,string m="")
        {
            try
            {
                ViewBag.l = m;
            MyStoreEntities4 Db = new MyStoreEntities4();
            List<GENRE> g = new List<GENRE>();
            g = Db.GENRE.Where(x => x.IdUser == III).ToList();
       
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            g.Reverse();
            return View(g.ToPagedList(pageNumber, pageSize));
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
                string query = "UPDATE GENRE  SET Etat='true' where IdGenre='" + id + "' ";
                SqlCommand SqlCom = new SqlCommand(query, SqlCon);

                ViewBag.MonParam1 = "MODIFICATION AVEC SUCCES";
                SqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Notification",new {III=ANDRANA.IDUSER,m="vb" });
             }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
            //return Redirect("Index");
        }
    }
}
