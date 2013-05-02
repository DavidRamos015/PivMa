using FizzWare.NBuilder;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer.Seeder
{
    public class ProductsSeeder : Seeder
    {
        public ProductsSeeder(ISession session)
            : base(session)
        {
        }
        public override void Seed()
        {
            var items = Builder<Product>.CreateNew().Build();
            items.Active = true;
            
            //var items = Builder<Product>.CreateListOfSize(10).Build();
            Session.Save(items);
        }
    }
}