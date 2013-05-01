using System.Collections.Generic;
using AutoMapper;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;
using Ninject.Modules;

namespace MiniAmazon.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<AccountInputModel, Account>();
            Mapper.CreateMap<CategoriesInputModel, Categories>();
            Mapper.CreateMap<Categories, CategoriesInputModel>();


            Mapper.CreateMap<Product, ProductInputModel>();
            Mapper.CreateMap<ProductInputModel, Product>();

            
            Mapper.CreateMap<Account, AccountLockedInputModel>();
            Mapper.CreateMap<AccountLockedInputModel, Account>();

            

        }
    }
}