dotnet pack ..\AssemblyLoader\ACoreX.AssemblyLoader\ACoreX.AssemblyLoader.csproj -o ./
dotnet pack ..\Authentication\ACoreX.Authentication.Abstractions\ACoreX.Authentication.Abstractions.csproj -o ./
dotnet pack ..\Authentication\ACoreX.Authentication.Core\ACoreX.Authentication.Core.csproj -o ./
dotnet pack ..\Authentication\ACoreX.Authentication.JWT\ACoreX.Authentication.JWT.csproj -o ./

dotnet pack ..\Configurations\ACoreX.Configurations.Abstractions\ACoreX.Configurations.Abstractions.csproj -o ./
dotnet pack ..\Configurations\ACoreX.Configurations.NetCore\ACoreX.Configurations.NetCore.csproj -o ./

dotnet pack ..\Data\ACoreX.Data.Abstractions\ACoreX.Data.Abstractions.csproj -o ./
dotnet pack ..\Data\ACoreX.Data.Dapper\ACoreX.Data.Dapper.csproj -o ./
dotnet pack ..\Data\ACoreX.Data.EFCore\ACoreX.Data.EFCore.csproj -o ./
dotnet pack ..\Extensions\ACoreX.Base.BaseExtensions\ACoreX.Extensions.Base.Abstractions.csproj -o ./
dotnet pack ..\Extensions\ACoreX.Extensions.Types\ACoreX.Extensions.Types.csproj -o ./
dotnet pack ..\Extensions\ACoreX.Plugin\ACoreX.Plugin.csproj -o ./
dotnet pack ..\Injector\ACoreX.Injector.Abstraction\ACoreX.Injector.Abstractions.csproj -o ./
dotnet pack ..\Injector\ACoreX.Injector.Core\ACoreX.Injector.Core.csproj -o ./
dotnet pack ..\Injector\ACoreX.Injector.NetCore\ACoreX.Injector.NetCore.csproj -o ./

dotnet pack ..\WebAPI\ACoreX.WebAPI\ACoreX.WebAPI.csproj -o ./
dotnet pack ..\WebAPI\ACoreX.WebAPI.Abstractions\ACoreX.WebAPI.Abstractions.csproj -o ./
xcopy /y *.nupkg  \\192.168.105.55\LocalNuget
efg generate