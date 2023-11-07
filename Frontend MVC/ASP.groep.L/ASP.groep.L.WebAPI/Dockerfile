FROM microsoft/aspnetcore
RUN ["dotnet","publish -c Release -o publish"]
COPY ./publish /publish
WORKDIR /publish
EXPOSE 5001/tcp
ENTRYPOINT ["dotnet", "WebAPI.dll"]