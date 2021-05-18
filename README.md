# TINF Whois Bot
Der TINF [Whois](https://de.wikipedia.org/wiki/Whois) Bot ist ein Discord Bot, der das Suchen, Bewerten und Empfehlen von Dozierenden erlaubt.

## Commands
Parameter mit `[]` umrandet sind optionale Parameter.
- `.whois <name>`: Sucht einen Lehrenden anhand des Suchbegriffes.
- `.rate <id>`: Gibt dir die Möglichkeit einen Lehrenden zu bewerten. Funktioniert nur im privaten Chat mit dem Bot.
- `.add name: "<name>" email: <email@example.com> telephone: "[telNr]" courses: "[id1, id2, ...]" avatarUrl: [http://example.com/avatar.png] office: "[untertitel]" location: "[raumNr]"`: Fügt einen neuen Lehrenden anhand der Beschreibung hinzu.*
- `.courses <suchbegriff>`: Sucht einen Kurs und gibt die IDs zurück.
- `.commend <id> <text>`: Schreibt eine Empfehlung an den Dozierenden, die dann für andere sichtbar ist.

*: Beim Parameter `courses` müssen die Kurse als ihre IDs angegeben und durch Kommas getrennt werden. Die IDs erhält ihr mit dem `.courses` Command.

## Deployment
> TODO

## Contribute
> TODO
