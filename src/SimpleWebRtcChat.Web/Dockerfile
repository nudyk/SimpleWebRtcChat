#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["SimpleWebRtcChat.Web/SimpleWebRtcChat.Web.csproj", "SimpleWebRtcChat.Web/"]
#RUN dotnet restore "SimpleWebRtcChat.Web/SimpleWebRtcChat.Web.csproj"
#COPY . .
#WORKDIR "/src/SimpleWebRtcChat.Web"
#RUN dotnet build "/src/SimpleWebRtcChat.Web" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "SimpleWebRtcChat.Web.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SimpleWebRtcChat.Web.dll"]
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SimpleWebRtcChat.Web.csproj", "SimpleWebRtcChat.Web/"]
RUN dotnet restore "SimpleWebRtcChat.Web/SimpleWebRtcChat.Web.csproj"
WORKDIR "/src/SimpleWebRtcChat.Web"
COPY . .
RUN dotnet build "/src/SimpleWebRtcChat.Web" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "/src/SimpleWebRtcChat.Web" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SimpleWebRtcChat.Web.dll
