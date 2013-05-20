using System.Linq;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using MiniAmazon.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class SaleSeeder : Seeder
    {
        public SaleSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var account = Session.Query<Account>().First(x => x.Email == "admin@pivma.com");
            var sale = Builder<Sale>.CreateNew().Build();
            Session.Save(sale);
            account.AddSale(sale);
            Session.Update(account);
        }
    }
}