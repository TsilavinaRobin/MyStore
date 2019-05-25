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
using PayPal;

namespace MyStore.Controllers
{

    public class ACCUEILController : Controller
    {
        string connectionString = @" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\tsila\Documents\Visual Studio 2015\Projects\MyStore\MyStore\App_Data\MyStore.mdf';Initial Catalog=MyStore;Integrated Security = True";
        // GET: ACCUEIL
      static  int IDPANIER = 20;
        static int a = 0,b=3;
     

        public void tournerPage(int TabMax, int TabMin,int TabTotal)
        {
            int reste = TabTotal % 6;
            if (reste == 0)
            {
                PAGE.TabMin = TabMax;
                PAGE.TabMax = TabMax + 3;
            }
           

        }
        public ActionResult Index(int TabMin = 0, int TabMax = 6)
        {
           
            DataTable dt1 = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt6 = new DataTable();
           
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {

                SqlCon.Open();
                SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1,Date from GENRE ORDER BY IdGenre DESC ", SqlCon);
                SqlData.Fill(dt);

                if (dt.Rows.Count != 1)
                {

                    ViewBag.MonParam1 = dt.Rows[0][0].ToString();
                    ViewBag.MonParam2 = dt.Rows[0][1].ToString();
                    ViewBag.MonParam3 = dt.Rows[0][2].ToString();
                    ViewBag.MonParam4 = dt.Rows[0][3].ToString();
                    ViewBag.MonParam5 = dt.Rows[0][4].ToString();
                    List<DateTime> Semaine = new List<DateTime>();
                    DateTime x = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    // DateTime x = Convert.ToDateTime("13/12/ 2018");



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime InTable7 = Convert.ToDateTime(dt.Rows[i][7].ToString()).AddDays(7);
                        DateTime InTable = Convert.ToDateTime(dt.Rows[i][7].ToString());

                        if (x == Convert.ToDateTime(dt.Rows[i][7].ToString()).AddDays(7))
                        {

                            string query = "UPDATE GENRE SET Nouveau='false' where Date='" + dt.Rows[i][7].ToString() + "'";
                            SqlCommand SqlCom = new SqlCommand(query, SqlCon);

                            SqlCom.ExecuteNonQuery();


                        }

                    }
                    // ViewBag.Date = x;
                }


                SqlDataReader dr;
                string query1 = "SELECT Magasin FROM VENDEUR where Magasin <> 'NULL' ";
                SqlCommand SqlCom1 = new SqlCommand(query1, SqlCon);
                SqlCom1.ExecuteNonQuery();
                dr = SqlCom1.ExecuteReader();
                List<string> Boutique = new List<string>();
                while (dr.Read())
                {

                    Boutique.Add(dr[0].ToString());

                }
                ViewBag.Boutique = Boutique;

                // Essai 2





                return View(dt);
            }
            // return View();
        }
        public ActionResult Index1(int? page, int? IdCat,int? IdUser,string Pour="",string Continuer="false",string r="")
        {
            try
            {
                DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {

                SqlCon.Open();
                SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1,Date from GENRE ORDER BY IdGenre DESC ", SqlCon);
                SqlData.Fill(dt);

                if (dt.Rows.Count != 1)
                {
                    List<DateTime> Semaine = new List<DateTime>();
                    DateTime x = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime InTable7 = Convert.ToDateTime(dt.Rows[i][7].ToString()).AddDays(7);
                        DateTime InTable = Convert.ToDateTime(dt.Rows[i][7].ToString());

                        if (x == Convert.ToDateTime(dt.Rows[i][7].ToString()).AddDays(7))
                        {

                            string query = "UPDATE GENRE SET Nouveau='false' where Date='" + dt.Rows[i][7].ToString() + "'";
                            SqlCommand SqlCom = new SqlCommand(query, SqlCon);

                            SqlCom.ExecuteNonQuery();


                        }

                    }
                }
               
                MyStoreEntities4 db8 = new MyStoreEntities4();
                List<PANIER> PanLisL = new List<PANIER>();
                List<GENRE> Gq = new List<GENRE>();
                PanLisL = db8.PANIER.ToList();
                Gq = db8.GENRE.Where(x => x.IdPanier == ANDRANA.IDPANIER).ToList();
                ANDRANA.IDPANIER = PanLisL.Count + 1;
               
                if (Continuer == "false") { 
                    //foreach(var it in Gq) {
                    //    int x = Convert.ToInt32(it.NbrGenre) + Convert.ToInt32(it.NbrAch);
                    //    int y =0;
                    //    string query6 = "UPDATE GENRE SET IdPanier='" + null + "',NbrGenre='"+x+"',NbrAch='"+y+"' where IdPanier='" +it.IdPanier  + "'";
                    //SqlCommand SqlCom6 = new SqlCommand(query6, SqlCon);
                    //SqlCom6.ExecuteNonQuery();
                    //}
                }

                //}

            }
            //
            //ENTITY FRAMEWORK

            MyStoreEntities4 db = new MyStoreEntities4();
          
            List<GENRE> GenreList = new List<GENRE>();
            if (Pour == "" && IdCat==null&& IdUser==null && r == "")
            {
                GenreList = db.GENRE.ToList();
            }
            else if (Pour != "" && IdCat == null && IdUser == null && r == "")
            {
                GenreList = db.GENRE.Where(x => x.Pour == Pour).ToList();
            }
            else if (IdCat != null && Pour == "" && IdUser == null && r == "")
            {
                //MAKA CATEGORIE
                GenreList = db.GENRE.Where(x => x.IdCat == IdCat).ToList();
            }
            else if (IdCat == null && Pour == "" && IdUser != null && r == "")
            {
                //MAKA CATEGORIE
                GenreList = db.GENRE.Where(x => x.IdUser == IdUser).ToList();
            }
            else if (IdCat == null && Pour == "" && IdUser == null && r == "")
            {
                //MAKA CATEGORIE
                GenreList = db.GENRE.Where(x => x.IdUser == IdUser).ToList();
            }
            else if (IdCat == null && Pour == "" && IdUser == null && r != "")
            {
                //MAKA CATEGORIE
                GenreList = db.GENRE.Where(x => x.NomGenre == r).ToList();
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            GenreList.Reverse();
           // int id = 1;
            //GENRE gENRE = db.GENRE.Find(id);
            //gENRE.NomGenre = ViewBag.NomGenre;
            

            return View(GenreList.ToPagedList(pageNumber, pageSize));
            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }


        }

        //public ActionResult Pagination(int TabMin, int TabMax, string NN)
        //{
          
        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1 from GENRE  ORDER BY IdGenre DESC", SqlCon);
        //        SqlData.Fill(dt);
        //        if (dt.Rows.Count != 1)
        //        {

        //            ViewBag.MonParam1 = dt.Rows[0][0].ToString();
        //            ViewBag.MonParam2 = dt.Rows[0][1].ToString();
        //            ViewBag.MonParam3 = dt.Rows[0][2].ToString();
        //            ViewBag.MonParam4 = dt.Rows[0][3].ToString();
        //            ViewBag.MonParam5 = dt.Rows[0][4].ToString();



        //        }
               

        //          if (NN == "suiv") {
        //        int fin = TabMin + 1;
        //        if (dt.Rows.Count-1 > TabMax)
        //        {
        //                // int div = dt.Rows.Count / 3;
        //                ViewBag.Vue2 = "prec";
        //                ViewBag.Vue1 = NN;
        //                ViewBag.TabMin = TabMax;
        //            ViewBag.TabMax = TabMax + 3;
        //        }
               
        //        else 
        //        {
        //                ViewBag.Vue2 = "prec";
        //                ViewBag.Vue1 = "";
        //                int reste = dt.Rows.Count % 3;
        //            ViewBag.TabMin = TabMax;
        //            ViewBag.TabMax = TabMax + reste;
                   
        //        }
        //       }
        //        else if (NN == "prec")
        //        {
        //            //  if (dt.Rows.Count - 1 > TabMax)
        //            // {
        //            // int div = dt.Rows.Count / 3;   7 et 6
        //            ViewBag.Vue1 = "suiv";

        //            ViewBag.Vue2 = NN;
        //            ViewBag.TabMax = TabMin;
        //                ViewBag.TabMin = TabMin - 3;
        //         //   }

        //             if (TabMin ==0)
        //            {
        //                ViewBag.Vue1 = "suiv";
        //                ViewBag.Vue2 = "";
        //                int reste = dt.Rows.Count % 3;
        //                ViewBag.TabMin = TabMin;
        //                ViewBag.TabMax = TabMin - reste;

        //            }
        //        }

        //        return View(dt);
        //    }
        //   // return View();
        //}
        //public ActionResult PaginationNav(int TabMin, int TabMax, string NN,string Pour)
        //{
        //    ViewBag.Pour = Pour;
        //    ViewBag.NN = NN;

        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1 from GENRE  where Pour='" + Pour + "'  ORDER BY IdGenre DESC", SqlCon);
        //       SqlData.Fill(dt);
        //        if (dt.Rows.Count != 1)
        //        {

        //            //ViewBag.MonParam1 = dt.Rows[0][0].ToString();
        //            //ViewBag.MonParam2 = dt.Rows[0][1].ToString();
        //            //ViewBag.MonParam3 = dt.Rows[0][2].ToString();
        //            //ViewBag.MonParam4 = dt.Rows[0][3].ToString();
        //            //ViewBag.MonParam5 = dt.Rows[0][4].ToString();
                    


        //        }
        //        int NbrLigne = dt.Rows.Count;
        //        int v = NbrLigne % 3;
        //        int v1 = v - 1;
        //        int IlayNiova = 0;
              
        //        if (NbrLigne <= 2)
        //        {

        //            IlayNiova = NbrLigne;
        //        }
        //        else
        //        {
        //            IlayNiova = 3;
        //        }

        //        if (NN == "suiv")
        //        {
        //            int fin = TabMin + 1;
        //            if (dt.Rows.Count - 1 > TabMax)
        //            {
        //                // int div = dt.Rows.Count / 3;
        //                ViewBag.Vue2 = "prec";
        //                ViewBag.Vue1 = NN;
        //                ViewBag.TabMin = TabMax;
        //                ViewBag.TabMax = TabMax + v1;
        //            }

        //            else
        //            {
        //                ViewBag.Vue2 = "prec";
        //                ViewBag.Vue1 = "";
        //                int reste = dt.Rows.Count % v1;
        //                ViewBag.TabMin = TabMax;
        //                ViewBag.TabMax = TabMax + reste;

        //            }
        //        }
        //        else if (NN == "prec")
        //        {
        //            //  if (dt.Rows.Count - 1 > TabMax)
        //            // {
        //            // int div = dt.Rows.Count / 3;   7 et 6
        //            ViewBag.Vue1 = "suiv";

        //            ViewBag.Vue2 = NN;
        //            ViewBag.TabMax = TabMin;
        //            ViewBag.TabMin = TabMin - (v+1);
        //            //   }

        //            if (TabMin == 0)
        //            {
        //                ViewBag.Vue1 = "suiv";
        //                ViewBag.Vue2 = "";
        //                int reste = dt.Rows.Count % (v + 1);
        //                ViewBag.TabMin = TabMin;
        //                ViewBag.TabMax = TabMin - reste;

        //            }
        //        }

        //        return View(dt);
        //    }
        //    // return View();
        //}

        //public ActionResult Nav(string Pour)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1 from GENRE where Pour='"+Pour+ "' ORDER BY IdGenre DESC", SqlCon);
        //        SqlData.Fill(dt);
        //        if (dt.Rows.Count != 0)
        //        {

        //            ViewBag.MonParam1 = dt.Rows[0][0].ToString();
        //            ViewBag.MonParam2 = dt.Rows[0][1].ToString();
        //            ViewBag.MonParam3 = dt.Rows[0][2].ToString();
        //            ViewBag.MonParam4 = dt.Rows[0][3].ToString();
        //            ViewBag.MonParam5 = dt.Rows[0][4].ToString();
        //            ViewBag.Pour = Pour;
        //        }

        //        return View(dt);
        //    }
        //    // return View();
        //}
        //public ActionResult Nav1(string Pour)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData = new SqlDataAdapter("Select IdCat, NomCat from CATEGORIE where NomCat='" + Pour + "'", SqlCon);
        //        SqlData.Fill(dt);
        //        if (dt.Rows.Count == 1)
        //        {

        //            ViewBag.IdCat = dt.Rows[0][0].ToString();
        //            ViewBag.NomCat = dt.Rows[0][1].ToString();
               

        //        }

        //       // return View();
        //    }
        //    //LISTE 
        //    DataTable dt1 = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData1 = new SqlDataAdapter("SELECT  GENRE.IdGenre, GENRE.NomGenre, GENRE.Image, GENRE.Prix,GENRE.IdProd,GENRE.Nouveau,GENRE.Prix1 FROM GENRE,PRODUIT,CATEGORIE WHERE (CATEGORIE.IdCat=PRODUIT.IdCat) AND (CATEGORIE.IdCat='" + dt.Rows[0][0].ToString() + "') AND (PRODUIT.IdProd=GENRE.IdProd)  ORDER BY IdGenre DESC", SqlCon);
        //        SqlData1.Fill(dt1);
        //        if (dt1.Rows.Count != 0)
        //        {
        //            ViewBag.IdProd = dt1.Rows[0][0].ToString();
        //            ViewBag.NomProd = dt1.Rows[0][1].ToString();
        //            return View(dt1);

        //        }


        //    }
        //    return View(dt1);
        //}
        //public ActionResult Boutique(string Boutique="", int TabMax = 0, int TabMin = 0)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData = new SqlDataAdapter("Select IdUser, Nom from VENDEUR where Magasin='" + Boutique + "'", SqlCon);
        //        SqlData.Fill(dt);
               
        //        if (dt.Rows.Count == 1)
        //        {

        //            ViewBag.IdUser1 = dt.Rows[0][0].ToString();
        //            ViewBag.Nom1 = dt.Rows[0][1].ToString();


        //        }
             
        //        // return View();
        //    }
        //    //LISTE 
        //    DataTable dt1 = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        SqlDataAdapter SqlData1 = new SqlDataAdapter("SELECT  GENRE.IdGenre, GENRE.NomGenre, GENRE.Image, GENRE.Prix,GENRE.IdProd,GENRE.Nouveau,GENRE.Prix1   FROM VENDEUR,CATEGORIE,PRODUIT,GENRE WHERE (CATEGORIE.IdUser=VENDEUR.IdUser) AND (VENDEUR.IdUser='"+ dt.Rows[0][0].ToString() + "') AND (PRODUIT.IdCat=CATEGORIE.IdCat) AND (GENRE.IdProd=PRODUIT.IdProd) ORDER BY IdGenre DESC", SqlCon);
        //        SqlData1.Fill(dt1);
        //        if (dt1.Rows.Count != 0)
        //        {
        //            ViewBag.IdProd = dt1.Rows[0][0].ToString();
        //            ViewBag.NomProd = dt1.Rows[0][1].ToString();
        //            return View(dt1);

        //        }


        //    }
        //    return View(dt1);
        //}

        // GET: ACCUEIL/Details/5
        public ActionResult Details(int IdGenre,int IdProd)
        {
            try
            {
                // ViewBag.Genre = IdProd;
                DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image,Taille, Prix,Description,NbrGenre,Couleur,Ref,Prix1 from GENRE where IdGenre='"+IdGenre+"'", SqlCon);
                SqlData.Fill(dt);
                if (dt.Rows.Count == 1)
                {

                    ViewBag.MonParam1 = dt.Rows[0][0].ToString();
                    ViewBag.MonParam2 = dt.Rows[0][1].ToString();
                    ViewBag.MonParam3 = dt.Rows[0][2].ToString();
                    ViewBag.MonParam4 = dt.Rows[0][3].ToString();
                    ViewBag.MonParam5 = dt.Rows[0][4].ToString();
                    ViewBag.MonParam6 = dt.Rows[0][5].ToString();
                    ViewBag.MonParam7 = dt.Rows[0][6].ToString();
                    ViewBag.MonParam8 = dt.Rows[0][7].ToString();
                    ViewBag.MonParam9 = dt.Rows[0][8].ToString();
                   ViewBag.MonParam10 = dt.Rows[0][9].ToString();

                }

                //return View(dt);
            }
            //LISTE 
            DataTable dt1 = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre,IdProd, Image from GENRE where IdProd='" + IdProd + "'", SqlCon);
                SqlData.Fill(dt1);
                if (dt.Rows.Count != 0)
                {

                    return View(dt1);

                }

               
            }
           return View(dt1);
             }
               catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        //public ActionResult Panier()
        //{
           
        //    DataTable dt = new DataTable();
        //    using (SqlConnection SqlCon = new SqlConnection(connectionString))
        //    {
        //        SqlCon.Open();
        //        string query = "SELECT * FROM PANIER";
        //        SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
        //        SqlDa.Fill(dt);
        //    }

        //    return RedirectToAction("Terminer", new { III = IDPANIER });

        //}
       
        public ActionResult Panier(int IdGenre,int? NbrGenre)
        {
            try
            {
                int nbrLigne = 0;
            DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT * FROM PANIER";
                SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                SqlDa.Fill(dt);
            }
            nbrLigne = Convert.ToInt32(dt.Rows.Count.ToString());
            int val = 1;
            if (dt.Rows.Count == 0)
            {
           
                // ViewBag.Gen = IdGenre;
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                    MyStoreEntities4 b = new MyStoreEntities4();
                    GENRE g = new GENRE();
                    //g = b.GENRE.Where(x => x.IdGenre == IdGenre).First();
                    //int NbrGenre1 = (Convert.ToInt32(g.NbrGenre) - Convert.ToInt32(NbrGenre));
                    //string query = "UPDATE GENRE SET IdPanier='" + val + "',NbrGenre='" + NbrGenre1 + "',NbrAch='" + NbrGenre + "' where IdGenre='" + IdGenre + "' ";
                        g = b.GENRE.Where(x => x.IdGenre == IdGenre).First();
                        int NbrGenre1 = (Convert.ToInt32(g.NbrGenre) - Convert.ToInt32(NbrGenre));
                        string query = "UPDATE GENRE SET IdPanier='" + val + "' where IdGenre='" + IdGenre + "' ";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    IDPANIER = val;
                    ViewBag.MonParam1 = IDPANIER;
                    ANDRANA.IDPANIER = IDPANIER;
                    SqlCom.ExecuteNonQuery();
            }
                
                return RedirectToAction("Terminer", new { III = IDPANIER, N = NbrGenre });
            }
            else
            {
                MyStoreEntities4 b = new MyStoreEntities4();
                PANIER p1 = new PANIER();
                p1 = b.PANIER.ToList().Last();
                val = p1.IdPanier + 1;
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                   
                    GENRE g = new GENRE();
                    g = b.GENRE.Where(x => x.IdGenre == IdGenre).First();
                        //int NbrGenre1=(Convert.ToInt32(g.NbrGenre) - Convert.ToInt32(NbrGenre));
                        //  string query = "UPDATE GENRE SET IdPanier='"+val+"',NbrGenre='"+ NbrGenre1 + "',NbrAch='"+NbrGenre+"' where IdGenre='" + IdGenre + "' ";
                        int NbrGenre1 = (Convert.ToInt32(g.NbrGenre) - Convert.ToInt32(NbrGenre));
                        string query = "UPDATE GENRE SET IdPanier='" + val + "' where IdGenre='" + IdGenre + "' ";
                        SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    IDPANIER = val;
                    ViewBag.MonParam1 = IDPANIER;
                    ANDRANA.IDPANIER = IDPANIER;
                    SqlCom.ExecuteNonQuery();
                }
                ViewBag.K = IdGenre;
                ViewBag.S = NbrGenre;
                //return View();
                return RedirectToAction("Terminer", new { III = IDPANIER, N=NbrGenre });
            }

            }
            catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Valider(int NbrProPanier,int PrixTotal)
        {
            // ViewBag.M1 = NbrProPanier;
            // ViewBag.M2 = PrixTotal;
            try
            {
                int nbrLigne = 0;
            DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT * FROM PANIER";
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


                    string query = "INSERT INTO PANIER (IdPanier, NbrProPanier,DateAchat, PrixTotal) VALUES ('"+val+"', '"+ NbrProPanier + "','"+ DateTime.Now.ToShortDateString() + "','" + PrixTotal + "')";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    //SqlCom.Parameters.AddWithValue("@IdPanier", val);
                    //SqlCom.Parameters.AddWithValue("@NbrProPanier", NbrProPanier);
                    //SqlCom.Parameters.AddWithValue("@DateAchat",DateTime.Now.ToShortDateString() );
                    //SqlCom.Parameters.AddWithValue("@PrixTotal", PrixTotal);
                    SqlCom.ExecuteNonQuery();
                    ViewBag.Message = "Avec succès1";


                    return RedirectToAction("Acheteur");
                }
                //UPDATE PANIER SET IdUser='1' where IdPanier='1'
              
              
            }
            else
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    MyStoreEntities4 D = new MyStoreEntities4();
                    PANIER px = new PANIER();
                    px = D.PANIER.ToList().Last();
                    val = px.IdPanier + 1;
                    string query = "INSERT INTO PANIER (IdPanier, NbrProPanier,DateAchat, PrixTotal) VALUES ('" + val + "', '" + NbrProPanier + "','" + DateTime.Now.ToShortDateString() + "','" + PrixTotal + "')";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    //SqlCom.Parameters.AddWithValue("@IdPanier", val);
                    //SqlCom.Parameters.AddWithValue("@NbrProPanier", NbrProPanier);
                    //SqlCom.Parameters.AddWithValue("@DateAchat", DateTime.Now.ToShortDateString());
                    //SqlCom.Parameters.AddWithValue("@PrixTotal", PrixTotal);
                    SqlCom.ExecuteNonQuery();
                    SqlCon.Close();
                    ViewBag.Message = "Avec succès 2";
                    return RedirectToAction("Acheteur");


                }

            }
             }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
            // return View();
        }
        public ActionResult Acheteur(string m = "")
        {
            try
            {
                ViewBag.l = m;
          
            return View();
             }
              catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        [HttpPost]
        public ActionResult Acheteur(ACHETEUR1 ACHETEURS )
        {
            //ViewBag.S1 = ACHETEURS.Nom;
            //ViewBag.S2 = ACHETEURS.Prenom;
            //ViewBag.S3 = ACHETEURS.DateNaiss;
            try
            {
                int nbrLigne = 0;
            DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT * FROM ACHETEUR";
                SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                SqlDa.Fill(dt);
            }
            nbrLigne = Convert.ToInt32(dt.Rows.Count.ToString());
            int val = 1;
            if (dt.Rows.Count == 0)
            {
                ViewBag.S1 = nbrLigne;
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    // INSERT INTO ACHETEUR(IdUser, Nom, Prenom, DateNaiss) VALUES('1', 'exempl', 'exemple', '2018')

                    string query = "INSERT INTO ACHETEUR (IdUser, Nom,Prenom,DateNaiss,Adresse,NumTel,Email) VALUES ('" + val + "', '" + ACHETEURS.Nom + "','" + ACHETEURS.Prenom + "','" + ACHETEURS.DateNaiss + "','"+ACHETEURS.Adresse+"','"+ACHETEURS.NumTel+"','"+ACHETEURS.Email+"')";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                  
                    SqlCom.ExecuteNonQuery();
                    double liv;
                    if (ACHETEURS.Livraison == "livrer" && ACHETEURS.Adresse == "Antananarivo")
                    {
                        liv = 2000;
                    }
                    else if (ACHETEURS.Livraison == "livrer")
                    {
                        liv = 5000;
                    }
                    else
                    {
                        liv = 0;
                    }
                    MyStoreEntities4 db8 = new MyStoreEntities4();
                 
                    PANIER PanLisL = new PANIER();
                    PanLisL = db8.PANIER.Where(x => x.IdPanier == IDPANIER).First();
                    double privLiv = Convert.ToDouble(PanLisL.PrixTotal) + liv; 
                    string query1 = "UPDATE PANIER SET IdUser='" + val + "',PrixTotal='"+privLiv+"' ,Livraison='" + liv + "' where IdPanier='"+IDPANIER+"'";    
                    SqlCommand SqlCom1 = new SqlCommand(query1, SqlCon);

                    SqlCom1.ExecuteNonQuery();
                    string query2 = "UPDATE GENRE SET IdUser1='" + val + "' where IdPanier='" + IDPANIER + "'";
                    SqlCommand SqlCom2 = new SqlCommand(query2, SqlCon);

                    SqlCom2.ExecuteNonQuery();

                    //return RedirectToAction("Acheteur", new { m = "vb" });
                        return RedirectToAction("Index","Paypal");


                    }
               
            }
            else
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
                {
                    SqlCon.Open();
                    MyStoreEntities4 xx = new MyStoreEntities4();
                    ACHETEUR cc = new ACHETEUR();
                    cc = xx.ACHETEUR.ToList().Last();

                    val = cc.IdUser + 1;
                    string query = "INSERT INTO ACHETEUR (IdUser, Nom,Prenom,DateNaiss,Adresse,NumTel,Email) VALUES ('" + val + "', '" + ACHETEURS.Nom + "','" + ACHETEURS.Prenom + "','" + ACHETEURS.DateNaiss + "','" + ACHETEURS.Adresse + "','" + ACHETEURS.NumTel + "','" + ACHETEURS.Email + "')";
                    SqlCommand SqlCom = new SqlCommand(query, SqlCon);
                    SqlCom.ExecuteNonQuery();
                    int liv;
                    if (ACHETEURS.Livraison == "livrer"&& ACHETEURS.Adresse=="Antananarivo") {
                        liv = 2000;
                    }
                    else if(ACHETEURS.Livraison == "livrer")
                    {
                        liv = 5000;
                    }
                    else
                    {
                        liv = 0;
                    }

                    MyStoreEntities4 db8 = new MyStoreEntities4();

                    PANIER PanLisL = new PANIER();
                    PanLisL = db8.PANIER.Where(x => x.IdPanier == IDPANIER).First();
                    double privLiv = Convert.ToDouble(PanLisL.PrixTotal) + liv;
                    string query1 = "UPDATE PANIER SET IdUser='" + val + "',PrixTotal='" + privLiv + "' ,Livraison='" + liv + "' where IdPanier='"+IDPANIER+"'";
                    SqlCommand SqlCom1 = new SqlCommand(query1, SqlCon);

                    SqlCom1.ExecuteNonQuery();
                    string query2 = "UPDATE GENRE SET IdUser1='"+val+"' where IdPanier='"+IDPANIER+"'";
                    SqlCommand SqlCom2 = new SqlCommand(query2, SqlCon);

                    SqlCom2.ExecuteNonQuery();

                    SqlCon.Close();
                    ViewBag.Message = ACHETEURS.Livraison;
                  //  return RedirectToAction("Acheteur",new  {m="vb" });
                        return RedirectToAction("Index", "Paypal");


                    }
            }
            }
    catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Terminer(int III,int? N)
        {
            try
            {
                DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                ViewBag.ParmD = IDPANIER;
                SqlCon.Open();
                string query = "SELECT * FROM GENRE where IdPanier='"+ IDPANIER + "'";
                SqlDataAdapter SqlDa = new SqlDataAdapter(query, SqlCon);
                SqlDa.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                     ViewBag.N= N;
                    return View(dt);
                }
            }
          
            return View(dt);
            }
         catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }


        }
        // GET: ACCUEIL/Create
        public ActionResult Recherche(string article)
        {
            try
            {
                DataTable dt = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                SqlDataAdapter SqlData = new SqlDataAdapter("Select IdGenre, NomGenre, Image, Prix,IdProd,Nouveau,Prix1,Date from GENRE where NomGenre='"+article+"' ORDER BY IdGenre DESC ", SqlCon);
                SqlData.Fill(dt);
                if (dt.Rows.Count != 0) {
                    return View(dt);
                }
            }
            ViewBag.article = article;
           // return PartialView();
            return View(dt);
             }
             catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCUEIL/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ACCUEIL/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ACCUEIL/Edit/5
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

        // GET: ACCUEIL/Delete/5
        public ActionResult Delete(int IdGenre,int NbrGenre,int NbrAch)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();


                int h = NbrGenre + NbrAch;
                    //string query1 = "UPDATE GENRE SET IdPanier='"+null+"',NbrGenre='"+ h  + "',NbrAch='"+null+"' where IdGenre='" + IdGenre + "'";
                    string query1 = "UPDATE GENRE SET IdPanier='"+null+"' where IdGenre='" + IdGenre + "'";
                    SqlCommand SqlCom1 = new SqlCommand(query1, SqlCon);

                SqlCom1.ExecuteNonQuery();
                SqlCon.Close();

                return RedirectToAction("Terminer", new { III = IDPANIER });

            }
             }
           catch
            {
                return RedirectToAction("HttpNotFoun", "ACCUEIL");
            }
        }
       
        public PAGE page(PAGE page)
        {
           
           
            return page;
        }
        public ActionResult Apropos()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult HttpNotFoun()
        {
            return View();
        }

        // POST: ACCUEIL/Delete/5
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
