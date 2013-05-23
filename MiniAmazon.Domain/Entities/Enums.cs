namespace MiniAmazon.Domain.Entities
{
    public enum MailOperationType
    {
        RegisterAccount = 1,
        ProductDataChange = 2,
        BuyConfirmacion = 3,
        PasswordChange = 4,
        PasswordChangedWhenUpdatedProfile = 6,
        UserDisableorLocked = 5,
        ContactToSeller = 6
    }

    public enum RateType
    {
        Product = 1,
        Account = 2
    }

    public enum ExternalProvider : int
    {
        Google = 1,
        Linked = 2,
        Yahoo = 3,
        Facebook = 4,
        Microsoft = 5

    }

    public enum ContactType : int
    {
        ContactToSeller = 1,
        ContactToUS = 2
    }
}