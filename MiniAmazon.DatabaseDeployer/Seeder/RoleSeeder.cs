using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using MiniAmazon.Data;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class RoleSeeder : Seeder
    {
        public RoleSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var item1 = Builder<Role>.CreateNew().Build();
            item1.Name = Utility.AdminRole;

            var item2 = Builder<Role>.CreateNew().Build();
            item2.Name = Utility.UserRole;
            

            Session.Save(item1);
            Session.Save(item2);
        }
    }
}
