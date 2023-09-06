namespace Web.Models.DTO {
    public class RequestDTO {
        // FIX: Cannot import SD for some reason
        public Web.Utility.SD.ApiType ApiType { get; set; } = Web.Utility.SD.ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}