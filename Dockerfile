# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Shopping-Cart.Presentation/*.csproj Shopping-Cart.Presentation/
COPY Shopping-Cart.Application/*.csproj Shopping-Cart.Application/
COPY Shopping-Cart.Core/*.csproj Shopping-Cart.Core/
COPY Shopping-Cart.Infra/*.csproj Shopping-Cart.Infra/

# Restore ENTRY project
RUN dotnet restore Shopping-Cart.Presentation/Shopping-Cart.Presentation.csproj

COPY . .

RUN dotnet publish Shopping-Cart.Presentation/Shopping-Cart.Presentation.csproj \
    -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Shopping-Cart.Presentation.dll"]
