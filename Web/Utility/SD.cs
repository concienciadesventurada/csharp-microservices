namespace Web.Utility {
    public class SD {
        public enum ApiType {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        // HACK: Temporary fix becasue we are not setting the role anywhere else
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "Authorization";
    }
}