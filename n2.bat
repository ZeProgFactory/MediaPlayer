UpdateVersionInfo -v=auto -n=MediaPlayer\ZPFMediaPlayer.csproj

rem ..\..\_Units_\_Tools_\Nuget pack MediaPlayer\ZPFMediaPlayer.csproj -build -Properties Configuration=Release

rem dotnet build MediaPlayer\ZPFMediaPlayer.csproj -c=Release
rem move MediaPlayer\bin\Release\*.nupkg .