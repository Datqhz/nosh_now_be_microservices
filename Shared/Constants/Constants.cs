namespace Shared.Constants;

public class Constants
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Restaurant = "Restaurant";
        public const string ServiceStaff = "ServiceStaff";
        public const string Chef = "Chef";
    }

    public static class CustomClaimTypes
    {
        public const string AccountId = "account_id";
        public const string Role = "role";
        public const string UserName = "user_name";
        public const string ClientId = "user_client_id";
    }

    public static class ImageDefault
    {
        public const string AvatarDefault =
            "https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg";
    }

    public static class CustomerBenefit
    {
        public const double ReceivePointRatio = 2;
    }
}