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
    }
}