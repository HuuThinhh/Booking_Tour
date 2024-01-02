using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ReviewDAO
    {
        private DbConnect db = new DbConnect();
        public List<Review> getList(string status = "All")
        {
            List<Review> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Reviews.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Reviews.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Reviews.ToList();
                        break;
                    }

            }
            return list;
        }
        public Review getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Reviews.Find(id);
            }
        }
        public int Insert(Review row)
        {
            db.Reviews.Add(row);
            return db.SaveChanges();
        }
        public int Update(Review row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Review row)
        {
            db.Reviews.Remove(row);
            return db.SaveChanges();
        }
    }
}
