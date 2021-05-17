$name = $args[0]

dotnet ef migrations add $name --project ../src/LecturerLookup.Core --startup-project ../src/LecturerLookup.DiscordBot