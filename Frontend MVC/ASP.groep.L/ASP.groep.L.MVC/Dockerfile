FROM mcr.microsoft.com/dotnet/aspnet
COPY ./publish /publish
WORKDIR /publish
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "AdminMVC.dll"]