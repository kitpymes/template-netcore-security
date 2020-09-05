# <img src="https://github.com/kitpymes/template-netcore-security/raw/master/docs/images/logo.png" height="30px"> Kitpymes.Core.Security

**Security, Json Web Token, Password Service, Encrypt, Decrypt, Seguridad, Token de sesión, Servicio de contraseña, encriptador, desencriptador**

[![Build Status](https://github.com/kitpymes/template-netcore-security/workflows/Kitpymes.Core.Security/badge.svg)](https://github.com/kitpymes/template-netcore-security/actions)
[![NuGet Status](https://img.shields.io/nuget/v/Kitpymes.Core.Security)](https://www.nuget.org/packages/Kitpymes.Core.Security/)
[![NuGet Download](https://img.shields.io/nuget/dt/Kitpymes.Core.Security)](https://www.nuget.org/stats/packages/Kitpymes.Core.Security?groupby=Version)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/kitpymes/template-netcore-security/blob/master/docs/LICENSE.txt)
[![Size Repo](https://img.shields.io/github/repo-size/kitpymes/template-netcore-security)](https://github.com/kitpymes/template-netcore-security/)
[![Last Commit](https://img.shields.io/github/last-commit/kitpymes/template-netcore-security)](https://github.com/kitpymes/template-netcore-security/)

## 📋 Requerimientos 

* Visual Studio >= 2019 (v16.3)

* NET TargetFramework >= netcoreapp3.0

* Net Core SDK >= 3.0.100

* C# >= 8.0

* Conocimientos sobre Inyección de Dependencias

## 🔧 Instalación 

_Se puede instalar usando el administrador de paquetes Nuget o CLI dotnet._

_Nuget_

```
Install-Package Kitpymes.Core.Security
```

_CLI dotnet_

```
dotnet add package Kitpymes.Core.Security
```

## ⌨️ Código

```cs
public class SecuritySettings
{
    public PasswordSettings PasswordSettings { get; set; } = new PasswordSettings();

    public JsonWebTokenSettings JsonWebTokenSettings { get; set; } = new JsonWebTokenSettings();

    public EncryptorSettings EncryptorSettings { get; set; } = new EncryptorSettings();
}
```

```cs
public static class SecurityServiceCollectionExtensions
{
    public static IServiceCollection LoadSecurity(this IServiceCollection services, IConfiguration configuration) {}

    public static IServiceCollection LoadSecurity(this IServiceCollection services, Action<SecurityOptions> options) {}

    public static IServiceCollection LoadSecurity(this IServiceCollection services, SecuritySettings settings) {}
}
```

### Encryptor

```cs
public interface IEncryptorService
{
    string Encrypt(string? value);

    string Decrypt(string? value);

    string Encrypt<T>(T value) 
        where T : class;

    T Decrypt<T>(string? value)
        where T : class, new();
}
```

```cs
public class EncryptorSettings
{
    public bool? Enabled { get; set; }
}
```

```cs
public static class EncryptorServiceCollectionExtensions
{
    public static IEncryptorService GetEncryptor(this IServiceCollection services) {}

    public static IServiceCollection LoadEncryptor(this IServiceCollection services, bool enabled = true) {}

    public static IServiceCollection LoadEncryptor(this IServiceCollection services, EncryptorSettings settings) {}
}
```

### JsonWebToken

```cs
public interface IJsonWebTokenService
{
    (string Token, string Expire) Encode(IList<Claim> claims, Dictionary<string, object>? headers = null);

    Dictionary<string, object> Decode(string? token);

    Task<(string Token, string Expire)> EncodeAsync(IList<Claim> claims, Dictionary<string, object>? headers = null);

    Task<Dictionary<string, object>> DecodeAsync(string? token);
}
```

```cs
public class ExpireSettings
{
    public int? Days { get; set; }

    public int? Hours { get; set; }

    public int? Minutes { get; set; }

    public int? Seconds { get; set; }
}
```

```cs
public class JsonWebTokenSettings
{
    [JsonIgnore]
    public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
    {
        ValidateIssuerSigningKey = !string.IsNullOrWhiteSpace(Key),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),

        ValidateIssuer = !string.IsNullOrWhiteSpace(ValidIssuer),
        ValidIssuer = ValidIssuer,

        ValidateAudience = !string.IsNullOrWhiteSpace(ValidAudience),
        ValidAudience = ValidAudience,

        ValidateLifetime = !(LifetimeValidator is null),
        LifetimeValidator = LifetimeValidator,

        // Tiempo de caducidad del búfer, el tiempo efectivo total es igual al tiempo más el tiempo de caducidad de Jwt. Si no está configurado, el valor predeterminado es 5 minutos.
        ClockSkew = TimeSpan.FromSeconds(30),

        RequireExpirationTime = _requireExpirationTime,
    };

    [JsonIgnore]
    public LifetimeValidator LifetimeValidator { get; set; } = (before, expires, token, param) => expires > DateTime.UtcNow;

    public ExpireSettings Expire { get; set; } = new ExpireSettings();

    public bool? Enabled { get; set; }

    public string? ValidAudience { get; set; }

    public string? ValidIssuer { get; set; }

    public string? Key { get; set; }

    public string? AuthenticateScheme { get; set; }

    public string? ChallengeScheme { get; set; }

    public bool? RequireExpirationTime { get; set; }
}
```

```cs
public static class JsonWebTokenServiceCollectionExtensions
{
    public static IJsonWebTokenService GetJsonWebToken(this IServiceCollection services) {}

    public static IServiceCollection LoadJsonWebToken(this IServiceCollection services, Action<JsonWebTokenOptions>? options = null, bool enabled = true) {}

    public static IServiceCollection LoadJsonWebToken(this IServiceCollection services, JsonWebTokenSettings settings) {}
}
```

### Password

```cs
public interface IPasswordService
{
    (bool hasErrors, string? hashPassword, List<PasswordResult>? errors) Create(string? plainPassword);

    bool Verify(string? plainPassword, string hashPassword);

    (bool hasErrors, List<PasswordResult>? errors) Validate(string? plainPassword);
}
```

```cs
public enum PasswordResult
{
    NullOrEmpty,

    RequireDigit,

    RequiredMinLength,

    RequiredUniqueChars,

    RequireEspecialChars,

    RequireLowercase,

    RequireUppercase,
}
```

```cs
public class PasswordSettings
{
    public bool? Enabled { get; set; }

    public bool? RequireDigit { get; set; }

    public bool? RequireLowercase { get; set; }

    public bool? RequireUppercase { get; set; }

    public bool? RequireEspecialChars { get; set; }

    public bool? RequiredUniqueChars { get; set; }

    public int? RequiredMinLength { get; set; }
}
```

```cs
public static class PasswordServiceCollectionExtensions
{
    public static IPasswordService GetPassword(this IServiceCollection services) {}

    public static IServiceCollection LoadPassword(this IServiceCollection services, Action<PasswordOptions>? options = null, bool enabled = true) {}

    public static IServiceCollection LoadPassword(this IServiceCollection services, PasswordSettings settings) {}
}
```

## ⚙️ Pruebas Unitarias

_Cada proyecto tiene su respectivo test, se ejecutan desde el "Explorador de pruebas"_

![Tests](images/screenshot/pruebas_unitarias.png)


## 🛠️ Construido con 

* [NET Core](https://dotnet.microsoft.com/download) - Framework de trabajo
* [C#](https://docs.microsoft.com/es-es/dotnet/csharp/) - Lenguaje de programación
* [Inserción de dependencias](https://docs.microsoft.com/es-es/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0) - Patrón de diseño de software
* [MSTest](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-mstest) - Pruebas unitarias
* [Nuget](https://www.nuget.org/) - Manejador de dependencias
* [Visual Studio](https://visualstudio.microsoft.com/) - Entorno de programacion


## ✒️ Autores 

* **Sebastian R Ferrari** - *Trabajo Inicial* - [kitpymes](https://kitpymes.com)


## 📄 Licencia 

* Este proyecto está bajo la Licencia [LICENSE](LICENSE.txt)


## 🎁 Gratitud 

* Este proyecto fue diseñado para compartir, creemos que es la mejor forma de ayudar 📢
* Cada persona que contribuya sera invitada a tomar una 🍺 
* Gracias a todos! 🤓

---
[Kitpymes](https://github.com/kitpymes) 😊