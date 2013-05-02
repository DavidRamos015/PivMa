using DomainDrivenDatabaseDeployer;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class Seeder : IDataSeeder
    {
        protected readonly ISession Session;

        public Seeder(ISession session)
        {
            Session = session;
        }

        public virtual void Seed()
        {

        }
    }
}