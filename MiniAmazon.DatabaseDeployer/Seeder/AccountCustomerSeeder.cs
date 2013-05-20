using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class AccountCustomerSeeder:Seeder
    {
        public AccountCustomerSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var item = new AccountCustomer
                {
                 AccountId_C=1,
                 AccountId_V=1
                };

            Session.Save(item);
        }
    }
}
