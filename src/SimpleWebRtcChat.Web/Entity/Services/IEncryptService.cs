namespace SimpleWebRtcChat.Web.Entity.Services
{
	public interface IEncryptService
	{
		string Encrypt(string input);

		string Decrypt(string cipherText);
	}
}