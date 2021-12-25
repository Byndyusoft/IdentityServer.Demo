using System.IO;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace IdentityServer
{
    public static class RsaParameterHelper
    {
        private const int BitSize = 2048;
        private const string FileName = "key.rsa";

        public static RsaSecurityKey GetOrCreateRsaSecurityKey(string rootPath)
        {
            var fileName = Path.Combine(rootPath, FileName);
            if (File.Exists(fileName) == false)
                GenerateNewParameters(fileName);

            var serialized = File.ReadAllText(fileName);
            var rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(serialized);
            var rsaSecurityKey = new RsaSecurityKey(rsaParameters);

            return rsaSecurityKey;
        }

        private static void GenerateNewParameters(string fileName)
        {
            var rsa = RSA.Create(BitSize);
            var exportParameters = rsa.ExportParameters(true);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(exportParameters));
        }
    }
}