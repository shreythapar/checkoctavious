using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Incedo_Octavius_Demo_2.Data;
using Incedo_Octavius_Demo_2.Models;
using MySql.Data.MySqlClient;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class kol_tableController : Controller
    {
        private Incedo_Octavius_Demo_2_kol_table_Context db = new Incedo_Octavius_Demo_2_kol_table_Context();

        // GET: kol_table
        public ActionResult Index()
        {
            List<kol_table> kols = new List<kol_table>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius_db.kol_table";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();

                            kols.Add(new kol_table
                            {
                                KolID = Convert.ToInt32(sdr["KolID"]),
                                MDMID = Convert.ToInt32(sdr["MDMID"]),
                                First_Name = sdr["First_Name"].ToString(),
                                Last_Name = sdr["Last_Name"].ToString(),
                                Mail = sdr["Mail"].ToString(),
                                Address = sdr["Address"].ToString(),
                                City = sdr["City"].ToString(),
                                State = sdr["State"].ToString(),
                                Zip = Convert.ToInt32(sdr["Zip"]),
                                Profile_Status = Convert.ToInt32(sdr["Profile_Status"])

                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(kols);
            //return View(db.KOL_DATA.ToList());
        }

        // GET: kol_table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_table kol_table = db.kol_table.Find(id);
            if (kol_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_table);
        }

        // GET: kol_table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kol_table/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KolID,First_Name,Last_Name,Mail,Address,City,State,Zip,Profile_Status,MDMID")] kol_table kol_table)
        {
            if (ModelState.IsValid)
            {
                db.kol_table.Add(kol_table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kol_table);
        }

        // GET: kol_table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_table kol_table = db.kol_table.Find(id);
            if (kol_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_table);
        }

        // POST: kol_table/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KolID,First_Name,Last_Name,Mail,Address,City,State,Zip,Profile_Status,MDMID")] kol_table kol_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kol_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kol_table);
        }

        // GET: kol_table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_table kol_table = db.kol_table.Find(id);
            if (kol_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_table);
        }

        // POST: kol_table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kol_table kol_table = db.kol_table.Find(id);
            db.kol_table.Remove(kol_table);
            db.SaveChanges();
            return RedirectToAction("Index");
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
