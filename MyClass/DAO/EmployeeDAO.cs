using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class EmployeeDAO
    {
        private DbConnect db = new DbConnect();
        public List<Employee> getList(string status = "All")
        {
            List<Employee> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Employees.Where(m => m.Id != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Employees.Where(m => m.Id == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Employees.ToList();
                        break;
                    }

            }
            return list;
        }
        public Employee getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Employees.Find(id);
            }
        }
        public int Insert(Employee row)
        {
            db.Employees.Add(row);
            return db.SaveChanges();
        }
        public int Update(Employee row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Employee row)
        {
            db.Employees.Remove(row);
            return db.SaveChanges();
        }
    }
}

