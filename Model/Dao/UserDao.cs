using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public  class UserDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }

        public long InsertFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.Username == entity.Username);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }


        }



        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.Username == userName);
        }
        public int Login(string username, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }

                }
            }

        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString)); //giam dan
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            return !user.Status;
        }

        public bool ChekUserName(string userName)
        {
            return db.Users.Count(x => x.Username == userName) > 0;
        }
        public bool ChekEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
