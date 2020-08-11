# Get the .NET Core 3.1.3 runtime image from Microsoft made for Ubuntu
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.7-bionic

# Set the currect work directory in the image
WORKDIR /app

# Copy the /release folder into the current work directory
COPY /release ./

# Execute the program dll
ENTRYPOINT ["dotnet", "MarketplaceService.dll"]