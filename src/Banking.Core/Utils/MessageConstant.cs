namespace Banking.Core.Utils
{
    public static class MessageConstant
    {
        public static string ItemNotFound(Type itemType) {return $"{itemType.Name} Not Found";}
        public static string ItemNotCreated(Type itemType) {return $"Unable to create {itemType.Name}";}
        public static string ItemCreated(Type itemType, int? id) {return $"{itemType.Name} Id {id} created sucessfully";}
        public static string ItemDeleted(Type itemType, int? id) {return $"{itemType} Id {id} deleted successfully";}
        public static string ItemActivated(Type itemType, int? id) {return $"{itemType} Id {id} activated successfully";}
        public static string ItemNotDeleted(Type itemType, int? id) {return $"Unable to delete {itemType} Id  {id}";}
        public static string ItemUpdated(Type itemType, int? id) {return $"{itemType} Id {id} updated successfully";}
        public static string ItemNotUpdated(Type itemType, int? id) {return $"Unable to update {itemType} Id {id}";}
        public static string ItemIdError(Type itemType, int? id, int? itemId) { return $"{itemType} Id {itemId} doesn't match provided Id {id} ";}
    }

    public static class BankAccountMessageConstant
    {
        public const string AccountWithFoundsError = "Account can't be deleted, check Balance";
        public const string BadRouteCreate = "Route unavailable, Customer Id is missing";
        public const string InvalidMonth = "Invalid month value";
    }

    public static class CustomerMessageConstant
    {
        public static string CustomerWithAccounts(int id) { return $"Unable to inactivate Customer Id {id}, open bank account found.";}
        public const string InvalidCustomerData = "Invalid Customer Data";
    }

    public static class LoginMessageConstant
    {
        public const string LoginError = "Login Error. Check Login and Password";
        public const string UserAlreadyExists = "Selected Login already in use";
        public const string InvalidUserData = "Invalid Login or Password";
    }

    public static class TransactionMessageConstant
    {
        public const string DepositError = "Unable to process Deposit, check account and value";
        public const string WithdrawError = "Unable to process Withdraw, check account, balance and value";
        public const string TransferError = "Unable to process Transfer, check origin and desitny account, balance and value";
    }
}