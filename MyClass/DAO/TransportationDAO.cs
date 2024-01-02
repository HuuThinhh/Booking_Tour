using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class TransportationDAO
    {
        private DbConnect db = new DbConnect();
        public List<Transportation> getList(string status = "All")
        {
            List<Transportation> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Transportations.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Transportations.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Transportations.ToList();
                        break;
                    }

            }
            return list;
        }
        public Transportation getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Transportations.Find(id);
            }
        }
        public int Insert(Transportation row)
        {
            db.Transportations.Add(row);
            return db.SaveChanges();
        }
        public int Update(Transportation row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Transportation row)
        {
            db.Transportations.Remove(row);
            return db.SaveChanges();
        }
    }
}

