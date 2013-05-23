using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class SearchInputModel
    {
        public virtual long Id { get; set; }

        public virtual string ProductName { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string VendorName { get; set; }

        public virtual string Description { get; set; }

        public virtual string Price { get; set; }

        public virtual string Picture1 { get; set; }
        public virtual string Picture2 { get; set; }
        public virtual string Picture3 { get; set; }
        public virtual string Picture4 { get; set; }
        public virtual string YoutubeLink { get; set; }
        public virtual string Inventory { get; set; }

        public virtual string AccountID { get; set; }

        public SearchInputModel()
        { }

        public SearchInputModel(long _id, string _ProductName,
                                string _CategoryName,
                                string _VendorName,
                                string _Description,
                                string _Price,
                                string _Picture1,
                                string _Picture2,
                                string _Picture3,
                                string _Picture4,
                                string _YoutubeLink,
                                string _Inventory,
                                string _AccountID
                                )
        {
            Id = _id;
            ProductName = _ProductName;
            CategoryName = _CategoryName;
            VendorName = _VendorName;

            Description = _Description;

            Price = _Price;

            Picture1 = _Picture1;
            Picture2 = _Picture1;
            Picture3 = _Picture1;
            Picture4 = _Picture1;
            YoutubeLink = _YoutubeLink;
            Inventory = _Inventory;
            AccountID = _AccountID;
        }

    }
}