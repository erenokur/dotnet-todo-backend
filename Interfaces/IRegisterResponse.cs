public interface IRegisterResponse
{
    string id { get; set; }
    string username { get; set; }
    string email { get; set; }
    string accessToken { get; set; }
}