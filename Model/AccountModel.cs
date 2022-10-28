using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

namespace Model
{
   public class AccountModel
    {
        private FoodOnlineDbContext context = null;
        public AccountModel()
        {
            context = new FoodOnlineDbContext();
        }
        public bool Login(string UserName, string Password)
        {

        }
    }
}
