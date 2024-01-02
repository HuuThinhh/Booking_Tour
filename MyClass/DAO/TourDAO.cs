using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class TourDAO
    {
        private DbConnect db = new DbConnect();
        

        public List<Tour> getList(string status = "All")
        {
            List<Tour> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Tours.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Tours.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Tours.ToList();
                        break;
                    }

            }
            return list;
        }
        public Tour getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Tours.Find(id);
            }
        }
        public int Insert(Tour row)
        {
            db.Tours.Add(row);
            return db.SaveChanges();
        }
        public int Update(Tour row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Tour row)
        {
            db.Tours.Remove(row);
            return db.SaveChanges();
        }
    }
}
