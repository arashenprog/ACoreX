nuget pack ..\AssemblyLoader\ACoreX.AssemblyLoader\ACoreX.AssemblyLoader.csproj
nuget pack ..\Authentication\ACoreX.Authentication.Abstractions\ACoreX.Authentication.Abstractions.csproj
nuget pack ..\Authentication\ACoreX.Authentication.Core\ACoreX.Authentication.Core.csproj
nuget pack ..\Authentication\ACoreX.Authentication.JWT\ACoreX.Authentication.JWT.csproj

nuget pack ..\Configurations\ACoreX.Configurations.Abstractions\ACoreX.Configurations.Abstractions.csproj
nuget pack ..\Configurations\ACoreX.Configurations.NetCore\ACoreX.Configurations.NetCore.csproj

nuget pack ..\Data\ACoreX.Data.Abstractions\ACoreX.Data.Abstractions.csproj
nuget pack ..\Data\ACoreX.Data.Dapper\ACoreX.Data.Dapper.csproj
nuget pack ..\Data\ACoreX.Data.EFCore\ACoreX.Data.EFCore.csproj
nuget pack ..\Extensions\ACoreX.Base.BaseExtensions\ACoreX.Extensions.Base.Abstractions.csproj
nuget pack ..\Extensions\ACoreX.Extensions.Types\ACoreX.Extensions.Types.csproj
nuget pack ..\Extensions\ACoreX.Plugin\ACoreX.Plugin.csproj
nuget pack ..\Injector\ACoreX.Injector.Abstraction\ACoreX.Injector.Abstractions.csproj
nuget pack ..\Injector\ACoreX.Injector.Core\ACoreX.Injector.Core.csproj
nuget pack ..\Injector\ACoreX.Injector.NetCore\ACoreX.Injector.NetCore.csproj

nuget pack ..\WebAPI\ACoreX.WebAPI\ACoreX.WebAPI.csproj
nuget pack ..\WebAPI\ACoreX.WebAPI.Abstractions\ACoreX.WebAPI.Abstractions.csproj
xcopy /y *.nupkg  \\192.168.105.55\LocalNuget
efg generate