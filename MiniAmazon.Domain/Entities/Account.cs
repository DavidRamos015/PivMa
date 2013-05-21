using System.Collections.Generic;

namespace MiniAmazon.Domain.Entities
{
    public class Account : IEntity
    {
        private readonly IList<Sale> _sales = new List<Sale>();

        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual int Age { get; set; }

        public virtual int CountryId { get; set; }

        public virtual string Genre { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool Locked { get; set; }

        public virtual bool PendingConfirmation { get; set; }
        
        public virtual string Picture { get; set; }
        public virtual string WenSite1 { get; set; }
        public virtual string WenSite2 { get; set; }
        public virtual string WenSite3 { get; set; }
        public virtual string WenSite4 { get; set; }
        public virtual string PublicEmail { get; set; }
        public virtual string About { get; set; }

        public virtual IEnumerable<Sale> Sales
        {
            get { return _sales; }
        }

        //public virtual IEnumerable<Role> Roles
        //{
        //    get { return _roles; }
        //}


        
        

        
        //public virtual void AddRole(Role role)
        //{
        //    if (!_roles.Contains(role))
        //    {
        //        _roles.Add(role);
        //    }
        //}

        public virtual void AddSale(Sale sale)
        {
            if (!_sales.Contains(sale))
            {
                _sales.Add(sale);
            }
        }
    }
}

