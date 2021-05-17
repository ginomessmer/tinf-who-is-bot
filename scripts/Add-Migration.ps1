$name = $args[0]

dotnet ef migrations add $name --project ../src/TinfWhoIs.Core --startup-project ../src/TinfWhoIs.DiscordBot