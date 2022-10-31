workspace "VoteBot"
    architecture "x86_64"
    startproject "VoteBot"

    configurations {
        "Debug",
        "Release",
        "Dist"
    }

    flags {
        "MultiprocessorCompile"
    }

project "VoteBot"
    kind "ConsoleApp"
    language "C#"
    dotnetframework "4.7.2"

    nuget {
        "Discord.Net:3.8.1",
        "Discord.Net.Core:3.8.1",
        "Discord.Net.Rest:3.8.1",
        "Discord.Net.WebSocket:3.8.1",
        "Discord.Net.Commands:3.8.1",
        "Discord.Net.Webhook:3.8.1",
        "Discord.Net.Interactions:3.8.1",
        "System.Data.SqlClient:4.8.4",
        "SpotifyAPI.Web:6.2.2",
        "SpotifyAPI.Web.Auth:6.2.2",
    }

    targetdir ("./Bin/")
    objdir ("./Bin-Int/")

    files {
        "src/**.cs",
    }

    filter "configurations:Debug"
        optimize "Off"
        symbols "Default"

    filter "configurations:Release"
        optimize "On"
        symbols "Default"

    filter "configurations:Dist"
        optimize "Full"
        symbols "Off"