using FizzWare.NBuilder;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class CategoriesSeeder : DatabaseDeployer.Seeder.Seeder
    {
        public CategoriesSeeder(ISession session)
            : base(session)
        {
        }

        public override void Seed()
        {
            var items = Builder<Categories>.CreateNew().Build();
            items.Name = "Electronica";
            items.IdParent =0;
            items.Active = true;
            Session.Save(items);
        }
    }
}