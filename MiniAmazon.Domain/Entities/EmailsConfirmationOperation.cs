using System;

namespace MiniAmazon.Domain.Entities
{
    public class EmailsConfirmationOperation : IEntity
    {
        public EmailsConfirmationOperation()
        {
            Active = true;
            CreateDateTime = DateTime.Now;
        }

        public virtual long Id { get; set; }

        public virtual DateTime CreateDateTime { get; set; }

        public virtual int MailOperationTypeId { get; set; }

        public virtual string ControllerToRedirect { get; set; }
        
        public virtual string ViewToRedirect { get; set; }

        public virtual long ObjectID { get; set; }

        public virtual string CodeToConfirm { get; set; }

        public virtual bool Active { get; set; }

    }
}