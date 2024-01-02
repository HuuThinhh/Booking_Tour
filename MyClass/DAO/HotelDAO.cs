using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class HotelDAO
    {
        private DbConnect db = new DbConnect();
        public List<Hotel> getList(string status = "All")
        {
            List<Hotel> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Hotels.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Hotels.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Hotels.ToList();
                        break;
                    }

            }
            return list;
        }
        public Hotel getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Hotels.Find(id);
            }
        }
        public int Insert(Hotel row)
        {
            db.Hotels.Add(row);
            return db.SaveChanges();
        }
        public int Update(Hotel row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Hotel row)
        {
            db.Hotels.Remove(row);
            return db.SaveChanges();
        }
    }
}

