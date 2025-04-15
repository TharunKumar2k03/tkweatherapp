# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire solution first
COPY . .

# Restore dependencies
RUN dotnet restore

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the published files from build stage
COPY --from=build /app/publish/wwwroot .

# Copy nginx configuration
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]