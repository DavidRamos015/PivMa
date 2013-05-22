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
            var item1 = Builder<Product>.CreateNew().Build();
            item1.Active = true;
            item1.PendingChange = false;
            item1.Name = "Nexus 5";
            item1.Description = "New N5 By Google 24mpx 128gb Android 6.0 6500mAh";
            item1.Picture1 = @"http://fs01.androidpit.info/userfiles/1017988/image/nexus5_v2.png";
            item1.CategoryId = 1;
            item1.Inventory = 10;
            item1.AccountId = 1;
            item1.Price = 350;


            var item2 = Builder<Product>.CreateNew().Build();
            item2.Active = true;
            item2.PendingChange = false;
            item2.Name = "Samsung Galaxy S5";
            item2.Description = "New  By samsung 20mpx 100gb Android 5.0 3500mAh";
            item2.Picture1 = @"http://i-cdn.phonearena.com/images/articles/80482-image/Awesome-Galaxy-S-VI-concept-skips-a-generation-hints-at-where-Samsung-should-head-after-the-S-IV.jpg";
            item2.CategoryId = 1;
            item2.Inventory = 10;
            item2.AccountId = 1;
            item2.Price = 800;

            var item3 = Builder<Product>.CreateNew().Build();
            item3.Active = true;
            item3.PendingChange = false;
            item3.Name = "Iphone 6";
            item3.Description = "New iphone By apple 12mpx  64gb Ios 6.0 3200mAh";
            item3.Picture1 = @"http://www.tecnonauta.com/img/fotos/849-iphone5-jpg.jpg";
            item3.CategoryId = 1;
            item3.Inventory = 10;
            item3.AccountId = 1;
            item3.Price = 4500;


            //var items = Builder<Product>.CreateListOfSize(10).Build();
            Session.Save(item1);
            Session.Save(item2);
            Session.Save(item3);
        }
    }
}