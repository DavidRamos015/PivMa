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
                    Name = "Admin",
                    Email = "admin@pivma.com",
                    Password = "0123456789",
                    Age=24,
                    CountryId=1,
                    Genre ="M",
                };
            account.PendingConfirmation = false;
            account.Active = true;
            account.Locked = false;

            //var role = new Role();
            //role.Name = Data.Utility.AdminRole;

            //account.AddRole(role);
            Session.Save(account);
        }
    }
}