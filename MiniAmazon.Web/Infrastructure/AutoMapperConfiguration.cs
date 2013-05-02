using AutoMapper;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

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

            Mapper.CreateMap<ProductPendingChanges, ProductUpdateInputModel>();
            Mapper.CreateMap<ProductUpdateInputModel, ProductPendingChanges>();


            Mapper.CreateMap<ProductPendingChanges, ProductUpdateApprovalInputModel>();
            Mapper.CreateMap<ProductUpdateApprovalInputModel, ProductPendingChanges>();
            

            Mapper.CreateMap<Account, AccountLockedInputModel>();
            Mapper.CreateMap<AccountLockedInputModel, Account>();

        }
    }

    //public class ValidatingDataController : BootstrapBaseController
    //{

    //    public static ActionResult IsValid(IRepository repository, Expression<Func<IEntity, bool>> expression)
    //    {
    //        var item = repository.First(expression);

    //        if (item == null)
    //        {
    //            return Json(true, JsonRequestBehavior.AllowGet);
    //        }
    //        else
    //        {
    //            return Json(false, JsonRequestBehavior.AllowGet);
    //        }
    //    }


    //}
}