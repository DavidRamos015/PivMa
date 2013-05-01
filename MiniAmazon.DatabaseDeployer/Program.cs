using System;
using System.Collections.Generic;
using System.Threading;
using AcklenAvenue.Data.NHibernate;
using DomainDrivenDatabaseDeployer;
using FluentNHibernate.Cfg.Db;
using MiniAmazon.Data;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer
{
    class Program
    {
        static void Main()
        {

            MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
                ConnectionString(x => x.FromConnectionStringWithKey("MiniAmazon"));

            DomainDrivenDatabaseDeployer.DatabaseDeployer dd = null;
            ISessionFactory sessionFactory = new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration)
               .Build(cfg => { dd = new DomainDrivenDatabaseDeployer.DatabaseDeployer(cfg); });

            dd.Drop();
            Console.WriteLine("Database dropped.");
            Thread.Sleep(1000);

            dd.Create();
            Console.WriteLine("Database created.");

            ISession session = sessionFactory.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                dd.Seed(new List<IDataSeeder>
                            {
                                new AccountSeeder(session),
                                new SaleSeeder(session)
                                //new Categories(session)
                            });
                tx.Commit();
            }
            session.Close();
            sessionFactory.Close();
            Console.WriteLine("Seed data added.");
            Thread.Sleep(2000);
        }
    }
}
