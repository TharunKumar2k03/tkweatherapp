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

# Remove default nginx configuration first
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Create necessary directories and set permissions
RUN mkdir -p /var/cache/nginx /var/run \
    && chmod -R 755 /var/cache/nginx \
    && chown -R nginx:nginx /var/cache/nginx /var/run

EXPOSE 80 8080

# Use non-root user
USER nginx

CMD ["nginx", "-g", "daemon off;"]