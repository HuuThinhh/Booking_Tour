using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class BookingDAO
    {
        private DbConnect db = new DbConnect();
        public List<Booking> getList(string status = "All")
        {
            List<Booking> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Bookings.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Bookings.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Bookings.ToList();
                        break;
                    }

            }
            return list;
        }
        public Booking getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Bookings.Find(id);
            }
        }
        public int Insert(Booking row)
        {
            db.Bookings.Add(row);
            return db.SaveChanges();
        }
        public int Update(Booking row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Booking row)
        {
            db.Bookings.Remove(row);
            return db.SaveChanges();
        }
    }
}

