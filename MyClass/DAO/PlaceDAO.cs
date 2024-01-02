using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class PlaceDAO
    {
        private DbConnect db = new DbConnect();
        public List<Place> getList(string status = "All")
        {
            List<Place> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Places.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Places.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Places.ToList();
                        break;
                    }

            }
            return list;
        }
        public Place getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Places.Find(id);
            }
        }
        public int Insert(Place row)
        {
            db.Places.Add(row);
            return db.SaveChanges();
        }
        public int Update(Place row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Place row)
        {
            db.Places.Remove(row);
            return db.SaveChanges();
        }
    }
}
