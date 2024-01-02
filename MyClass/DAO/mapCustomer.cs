using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClass.Models;

namespace WebDuLich.Controllers

{
    public class mapCustomer
    {
        DbConnect db = new DbConnect();

        public Customer TimKiem(string email)
        {
            try
            {
                
                var user = db.Customers.SingleOrDefault(m => m.Email.ToLower() == email.ToLower());
                return user;
            }
            catch
            {
                return null;
            }
        }
        public List<Customer> DanhSach()
        {
            var users = db.Customers.ToList();
            return users;
        }

        public void ThemMoi(string firstName, string lastName, string email, string phone, string password)
        {
            Customer tk = new Customer();
            tk.FirstName = firstName;
            tk.LastName = lastName;
            tk.Email = email;
            tk.Phone = phone;
            tk.Password = password;
            db.Customers.Add(tk);
            db.SaveChanges();
        }

        public bool ThemMoi(Customer tk)
        {
            try
            {
                db.Customers.Add(tk);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ChinhSua(Customer tk, string firstname, string lastname, string address)
        {
            Customer cus = db.Customers.Find(tk.Id);
            tk.FirstName = firstname;
            tk.LastName = lastname;
            tk.Address = address;
            cus.FirstName= tk.FirstName;
            cus.LastName= tk.LastName;
            cus.Address= tk.Address;
            db.SaveChanges();
        }
        public void DoiMK(Customer tk, string newpass)
        {
            Customer cus = db.Customers.Find(tk.Id);
            tk.Password= newpass;
            cus.Password= newpass;
            db.SaveChanges();
        }
    }
}