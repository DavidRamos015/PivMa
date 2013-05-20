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
    }
}