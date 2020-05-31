using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace SimpleWebRtcChat.Web.Entity.Services
{
	public class EncryptService : IEncryptService
	{
		private readonly IDataProtectionProvider _dataProtectionProvider;
		private readonly IConfiguration _config;
		private string Key => _config.GetValue<string>("ChatConfiguration:EncryptKey");

		public EncryptService(IDataProtectionProvider dataProtectionProvider, IConfiguration config)
		{
			_dataProtectionProvider = dataProtectionProvider;
			_config = config;
		}

		public string Encrypt(string input)
		{
			var protector = _dataProtectionProvider.CreateProtector(Key);
			return protector.Protect(input);
		}

		public string Decrypt(string cipherText)
		{
			var protector = _dataProtectionProvider.CreateProtector(Key);
			return protector.Unprotect(cipherText);
		}
	}
}