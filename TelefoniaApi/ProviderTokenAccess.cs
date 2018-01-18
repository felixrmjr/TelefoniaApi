using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TelefoniaApi.Models;

namespace TelefoniaApi
{
    public class ProviderTokenAccess : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication (OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var config = ConfigurationManager.AppSettings;

            var body = $"username={context.UserName}&password={context.Password}&client_id={config["client_id"]}&grant_type=password";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(config["api_autenticacao"], new StringContent(body, Encoding.UTF8));

                if (!string.IsNullOrWhiteSpace(response.Content.ToString()))
                {
                    var user = JsonConvert.DeserializeObject<Login>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings()
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });


                    // cancelando a emissão do token se o usuário 
                    //não for encontrado
                    if (string.IsNullOrEmpty(user.AccessToken))
                    {
                        context.SetError("invalid_grant", "Usuário não encontrado ou a senha está incorreta.");
                        return;
                    }

                    // emitindo o token com informacoes extras se o usuário existe
                    var identyUser = new ClaimsIdentity(context.Options.AuthenticationType);
                    identyUser.AddClaim(new Claim(ClaimTypes.Role, "user"));

                    context.Validated(identyUser);

                }
            }

            
        }

        public static IEnumerable<Autenticacao> Users()
        {
            return new List<Autenticacao>
            {
                new Autenticacao { Username = "Marcelo", Password = "admin" },
                new Autenticacao { Username = "Joao", Password = "12345" },

            };
        }

    }
}