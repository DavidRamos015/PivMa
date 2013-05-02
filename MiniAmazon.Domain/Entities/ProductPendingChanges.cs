using System;

namespace MiniAmazon.Domain.Entities
{
    public class ProductPendingChanges : Product
    {
        public new virtual long Id { get; set; }
        public new virtual DateTime CreateDateTime { get; set; }
    }
}