$name = $args[0]

dotnet ef migrations add $name --project ../LecturerLookup.Core --startup-project ../LecturerLookup.DiscordBot