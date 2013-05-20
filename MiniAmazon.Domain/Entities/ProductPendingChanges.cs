using System;

namespace MiniAmazon.Domain.Entities
{
    public class ProductPendingChanges : IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreateDateTimePendingChange { get; set; }
        public virtual string Comments { get; set; }
        public virtual bool Approved { get; set; }
        public virtual string CommentsWhyNotApproved { get; set; }


        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime CreateDateTime { get; set; }

        public virtual string Picture1 { get; set; }
        public virtual string Picture2 { get; set; }
        public virtual string Picture3 { get; set; }
        public virtual string Picture4 { get; set; }

        public virtual string YoutubeLink { get; set; }

        public virtual int Inventory { get; set; }

        public virtual bool PostOnFacebook { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool PendingChange { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual long AccountId { get; set; }

        public virtual long ProductId { get; set; }

        public static ProductPendingChanges CopyData(Product item)
        {
            var itemWithChanges = new ProductPendingChanges
            {
                CreateDateTime = item.CreateDateTime,
                CreateDateTimePendingChange = DateTime.Now,
                Comments = "",
                CommentsWhyNotApproved = "",
                CategoryId = item.CategoryId,
                Description = item.Description,
                Inventory = item.Inventory,
                Name = item.Name,
                PendingChange = true,
                Picture1 = item.Picture1,
                Picture2 = item.Picture2,
                Picture3 = item.Picture3,
                Picture4 = item.Picture4,
                PostOnFacebook = item.PostOnFacebook,
                Price = item.Price,
                YoutubeLink = item.YoutubeLink,
                ProductId = item.Id,
                Active = item.Active,
                Approved = false,
                AccountId = item.AccountId

            };

            return itemWithChanges;
        }

    }


}