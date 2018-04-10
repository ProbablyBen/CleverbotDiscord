# CleverbotDiscord

- This project is an integration of the popular chatbot, cleverbot into discord.
- If you would like this bot in your discord, you may invite it with this [url](https://discordapp.com/api/oauth2/authorize?client_id=432052078899101716&permissions=0&scope=bot).

## Screenshots

![Cleverbot in action](https://github.com/ProbablyBen/CleverbotDiscord/raw/master/screenshots/cleverbot_in_action.png)

## Setup

Clone this repository onto your computer.

Go into the `CleverbotDiscord` directory and open `App.config` with your favorite editor.

Fill in the token for your discord bot token like this:

```xml
<add key="DiscordToken" value="INSERT_TOKEN_HERE" />
```

Fill in your cleverbot api key. If you don't have one, you can purchase one [here](https://www.cleverbot.com/api/).

```xml
<add key="CleverbotKey" value="INSERT_API_KEY_HERE" />
```

Open the .sln file with Visual Studio 2017 and build the project.

## Contributing
If you wish to contribute, you may open a pull request.