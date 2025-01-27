# Build aþamasý için .NET SDK'sýný kullanýyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Çalýþma dizini oluþturuyoruz
WORKDIR /app
COPY /bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "Hospital.dll"]
