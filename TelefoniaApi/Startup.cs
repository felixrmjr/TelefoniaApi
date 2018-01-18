using System;
using System.Net.Http.Formatting;
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelefoniaApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configuração da Web API self-host. 
            var config = new HttpConfiguration();

            // Configurando rotas
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Habilitando o cors
            config.EnableCors();

            // Configurações de formato de saida
            ConfiguracaoJson(config);

            // Configuração de acesso
            ConfiguracaoAcessoToken(appBuilder);

            // Ativando as configurações
            appBuilder.UseWebApi(config);
        }

        private void ConfiguracaoJson(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            config.Formatters.JsonFormatter.SerializerSettings = jsonSerializerSettings;
        }

        private void ConfiguracaoAcessoToken(IAppBuilder app)
        {
            var optionsConfigurationToken = new OAuthAuthorizationServerOptions
            {
                // Permitindo acesso ao endereço de fornecimento do token precisar de HTTPS. Em produção o valor deve ser FALSE
                AllowInsecureHttp = true,

                // Endereço do fornecimento do token de acesso
                TokenEndpointPath = new PathString("/token"),

                // Tempo do token de acesso
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),

                Provider = new ProviderTokenAccess(), 
            };

            // Ativar o token de acesso WebApi
            app.UseOAuthAuthorizationServer(optionsConfigurationToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
