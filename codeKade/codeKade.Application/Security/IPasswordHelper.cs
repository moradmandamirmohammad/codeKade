namespace codeKade.Application.Security
{
    public interface IPasswordHelper
    {
        string EncodePasswordMd5(string password);
    }
}
