using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class CustomerDAO
    {
        private DbConnect db = new DbConnect();
        public List<Customer> getList(string status = "All")
        {
            List<Customer> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Customers.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Customers.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Customers.ToList();
                        break;
                    }

            }
            return list;
        }
        public Customer getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Customers.Find(id);
            }
        }
        public int Insert(Customer row)
        {
            db.Customers.Add(row);
            return db.SaveChanges();
        }
        public int Update(Customer row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Customer row)
        {
            db.Customers.Remove(row);
            return db.SaveChanges();
        }
    }
}

