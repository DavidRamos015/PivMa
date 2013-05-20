using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class Account_RoleSeeder : Seeder
    {
        public Account_RoleSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var account_Role = new Account_Role
                {
                 Role_Id =1,
                 Account_Id=1
                };

            Session.Save(account_Role);
        }
    }
}
