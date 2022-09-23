# ElderlyCare.Main

Layer description:
https://www.youtube.com/watch?v=fhM0V2N1GpY&ab_channel=AmichaiMantinband


User secrets:
https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows

 
 dotnet user-secrets init --project .\ElderlyCare.API\
 dotnet user-secrets set --project .\ElderlyCare.API\ "JwtSettings:Secret" "super-secret-key-from-user-secrets"
 dotnet user-secrets list --project .\ElderlyCare.API\