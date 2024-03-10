# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy everything else and build app
COPY . .
RUN dotnet restore
WORKDIR /source
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 5041
ENV ASPNETCORE_URLS=http://+:5041
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "me-faz-um-pix.dll"]

CMD ["dotnet", "watch"]