using DomainDrivenDatabaseDeployer;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class AccountSeeder : Seeder
    {
        public AccountSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var account = new Account
                {
                    Name = "David Ramos",
                    Email = "david.ramos@grupoleitz.com",
                    Password = "0123456789"
                };
            account.PendingConfirmation = false;
            account.Active = true;
            account.Locked = false;
            Session.Save(account);
        }
    }
}