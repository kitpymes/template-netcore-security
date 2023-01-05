using Kitpymes.Core.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Tests.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

            /*** JsonWebToken ***/
            //services.LoadSecurity(options => options.WithAuthentication(auth => auth.WithJsonWebToken(Configuration)));
            services.LoadSecurity(security =>
            {
                security.WithAuthentication(auth => auth.WithJsonWebToken(options => options
                    .WithEnabled()
                    .WithPublicKey("MIIBCgKCAQEA3XnVp+0h9h0xVGo4TKKUd9qYEBfkrj+t3W0Ex58jWXM6UELi+c2VnmrYR3YFARNYsjqaNXEvhsFsoRYQi8aYJ61KPaOb+/hT6dCaGIR0PEqs5r6+0wK0IXIvjiQH647OHkci+nVNj0dxSwSTJ4rnrwa7ez9cw85fe+xpGtm0KszxLnzR65DkiuFhzkgXXD25+v5uBKXTTOiggcU52MEENsIdvdnPQsHdghgfIJCOUcIUt3CjVcMy2mJOOlC1FaBqb8Suj8ydL3cUVSad95coonzlwLm0X9bv12RKPal6QPaUn2zSQO64Syt6mpOF1ogZIyWX1IJbJ+uZltvoqVXSuQIDAQAB")
                    .WithPrivateKey("MIIEpAIBAAKCAQEA3XnVp+0h9h0xVGo4TKKUd9qYEBfkrj+t3W0Ex58jWXM6UELi+c2VnmrYR3YFARNYsjqaNXEvhsFsoRYQi8aYJ61KPaOb+/hT6dCaGIR0PEqs5r6+0wK0IXIvjiQH647OHkci+nVNj0dxSwSTJ4rnrwa7ez9cw85fe+xpGtm0KszxLnzR65DkiuFhzkgXXD25+v5uBKXTTOiggcU52MEENsIdvdnPQsHdghgfIJCOUcIUt3CjVcMy2mJOOlC1FaBqb8Suj8ydL3cUVSad95coonzlwLm0X9bv12RKPal6QPaUn2zSQO64Syt6mpOF1ogZIyWX1IJbJ+uZltvoqVXSuQIDAQABAoIBAA7bGUXGVjzYAHMVHOmnDiZr9z89Gw3FH7h2k5eASTK60/KGSgtPivWxXQiOFg/YaF6sJ6PmD7YOS2cSv9FgZNxkd1JjIxdntNk+MNfsKo/QwoBH0yz8RXDo49+48v8N+S12wBXkwGsX87WAfQ5t9tR6syC1Q1evBCCf3vz6FWe5b4v6liFlObP2DkuyGBOszworebLNIektVE3IgrST5Ug3SqjGvQfYzaQc9feuypM1CDNbNa+YUWgRfbE3I1Zm/cGIUx6UZWpmzeCbVZvkNhe2p+HWytCmQyeXkCDiJSL9SWtjE69NTjyZNaeNiAVSZZfiRQJYHMPIPmsGz1LP6gECgYEA976YjzQAfD0eZRbRoMU+1I9VhmpGAoHY0PbXK6Bn4SsR7nUEYfLFOyVYEgY7+j9m+dkQ2BnIx64p9BthywVMsVz03DIFwX2RmpXFSUvhi513J5gwpStKQ4fRWOKeo5eed23gFgORgQUcZyrhHYngAIbGmEXq3HndETq688LNG8kCgYEA5Nsm/cSlbKkWexm9hdxcJYnLnssgaipgyYm3wJaJettvueEYLN7nlUOsmJiKBdzNgzPlrFKwhMYGKujX0jbb+6neOdrkrLYTlM2E12UF3J3Jb2Vo4C8hERDBUF5nbj0ejeWVLNf6XDM5G0/U6D/v54+RQtc0FO6Fq1fdRHihl3ECgYEA8Q6rMuvObRSrNoF1h5FlCT5OYXOWm2nspacIxpJVgWp/EB8cHJI0BtDXWPJn+Z8ne4QSN7MfAewdNkBOb1TAwvNH7dlecF7WBiwkwqTq2QZmokgbTRtKL1s//9TBtzwYHOz0VXczZO6sLFY0pKE7LEf3hMLDO7P3voP+1KB/IsECgYEAkc8bIZXXe8qjWx0ERjQmtXf8tphed6W2ctF+tIJ050TGA1zpxTTjwZMTdUnCroeGnLBFBazNcFqmdVJSp0lykP4iayQOjAeZpDXpDD7+JBiXzZSD5EnCvJe7QtRS+8aPIAMFL0QP7Axqtv7+/FY4KIWtHDP2p9tsaMWbYVOmSNECgYA5xIBHwemTcUEswnclX+J2I7ZTMyXeeLaLtCakrz9aPmjLhEonHfVwCFDIStRf9gsD98tiM3pxFfkCJkJpUx9+RKT7WU82DSH0ukeTYMQTpALt0GAAJTX6DbzI2Pr0PpAJvI2XWwPUCLGUemgsOZlZcM/x53VXc1rEbvHOl5AFvw==")));
            });


            /*** Cookies ***/
            //services.LoadSecurity(options => options.WithAuthentication(auth => auth.WithJsonWebToken(Configuration)));
            //services.LoadSecurity(security =>
            //{
            //    security.WithAuthentication(auth => auth.WithCookies(options => options
            //        .WithEnabled().WithCookieName("NOMBRE_COOKIE")
            //        .WithLoginPath("/Cookies/login")
            //        .WithLogoutPath("/Cookies/logout")
            //        .WithExpire(0,0,1,0)
            //        .WithSlidingExpiration(false)
            //        .WithScheme()));;
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.LoadAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}