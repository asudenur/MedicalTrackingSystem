# Build a�amas� i�in .NET SDK's�n� kullan�yoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0

# �al��ma dizini olu�turuyoruz
WORKDIR /app
COPY /bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "Hospital.dll"]
