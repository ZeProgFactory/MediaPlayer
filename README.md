# MediaPlayer - !!! Preview !!!
Cross platform media player lib</BR>
based on ideas and code of [martijn00](https://github.com/martijn00) ( https://github.com/martijn00/XamarinMediaManager )

[You’ll find the story behind this MediaPlayer lib here …](https://zeprogfactory.github.io/MediaPlayer/)
   
[Xmarin.Forms samples for the MediaPlayer …](https://github.com/ZeProgFactory/MediaPlayer_SamplesXF)   
[Simple Xamarin, UWP & WPF samples for the MediaPlayer …](https://github.com/ZeProgFactory/MediaPlayer_Buddies)     
and for those interested some links about the tricks of this library and examples in the [wiki](https://github.com/ZeProgFactory/MediaPlayer/wiki) section... 
  
## Current platform support (evolves on nearly daily basis)

|platform|sample|build|state|  
| ------------------- | :------------------: | :------------------: |  :------------------: |  
|UWP| XF.UWP | [![Build status](https://build.appcenter.ms/v0.1/apps/c04f9cb9-8f2a-4d33-9011-0f1fe8235713/branches/master/badge)](https://appcenter.ms) |  90 % audio |
|iOS| XF.iOS| [![Build status](https://build.appcenter.ms/v0.1/apps/4427f9f1-a7ee-4b86-b690-be87c518f62b/branches/master/badge)](https://appcenter.ms) | 60 % audio |
|Android| XF.Android| [![Build status](https://build.appcenter.ms/v0.1/apps/faa57107-5590-491c-af93-2aa56bf1c7be/branches/add-code-of-conduct-1/badge)](https://appcenter.ms) | 90 % audio |
|MacOS| XF.Mac | | 60 % audio |
| WPF | XF.WPF | | 70 % audio |
   
   
## Setup
* Available on NuGet: https://www.nuget.org/packages/ZPFMediaPlayer [![NuGet](https://img.shields.io/nuget/v/ZPFMediaPlayer.svg)](https://www.nuget.org/packages/ZPFMediaPlayer/)
* For Xamarin.Forms install into your Main and Client projects.


## How to use
A Xamarin.Forms sample showing how to use MediaPlayer is in this Git, but here are the  basics ...

### authorizations
#### iOS and  MacOS - Info.plist
If you wish to access to streamning sites you should add folowing lines to your ```Info.plist```.  
```XML
	<key>NSAppTransportSecurity</key>
	<dict>
		<key>NSAllowsArbitraryLoads</key>
		<true/>
		<key> NSAllowsArbitraryLoadsInWebContent</key>
		<true/>
	</dict>
```  

### Initialization 
Before using the MediaPlayer you should initialize it in the platform dependent project. 

Start by adding the using at the header of each concerned file ...
```csharp
using ZPF.Media;
```  
  
*for an Android project: MainActivity.cs: OnCreate:* 
```csharp
...
MediaPlayer.Current.Init();  
LoadApplication(new App());
```   
  
*for a UWP project: App.xaml.cs: OnLaunched:* 
```csharp
...
MediaPlayer.Current.Init();  
Xamarin.Forms.Forms.Init(e);
...
```
  
*for a WPF project: MainWindow.xaml.cs: MainWindow():* 
```csharp
...
ZPF.Media.MediaPlayer.Current.Init(this);
Forms.Init();
...
```

| MediaPlayer.Current.Init() | UWP | iOS |Android| Mac | WPF |
| -------------------------- |:---:|:---:|:-----:|:---:|:---:|
| Mandatory                  |  no |  no |   no  |  no | yes |   


### Play 
```csharp
MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");  
```

On WPF you should `try .. catch` the `Play` method for the moment:  
[[WPF] Fatal exception when playing special song](https://github.com/ZeProgFactory/MediaPlayer/issues/3)


### Native player
| platform | nativ player |
| ------------------- | :-----------: |
| Android | Android.Media.MediaPlayer |
| iOS     | AVKit.AVPlayerViewController |
| MacOS   | AVKit.AVPlayerView |
| UWP     | Windows.Media.Playback.MediaPlayer |
| WPF     | System.Windows.Controls.MediaElement |

*Android*
```csharp
Android.Media.MediaPlayer Player = (Android.Media.MediaPlayer)MediaPlayer.Current.Player;  
```
Remark: The Android Player type will probably change in the future to the ExoPlayer. So check the doc on each release. 
  

*UWP*
```csharp
Windows.Media.Playback.MediaPlayer Player = (Windows.Media.Playback.MediaPlayer)MediaPlayer.Current.Player;  
```
  

*WPF*
```csharp
System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player; 
```     
   
   
### Code Sample
[Main page source code from sample program ...](https://raw.githubusercontent.com/ZeProgFactory/MediaPlayer/master/Samples/MediaPlayerSample/Pages/MainPage.xaml.cs)

## API
### Methods
```csharp
MediaPlayer.Current.Pause();  
MediaPlayer.Current.Play("http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4");  
MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");  
MediaPlayer.Current.Play(mi);  
MediaPlayer.Current.Play();  
```

```csharp
MediaPlayer.Current.Playlist.Current   
MediaPlayer.Current.Playlist.PlayNext();  
MediaPlayer.Current.Playlist.PlayPrevious();  
MediaPlayer.Current.Playlist.Add( MediaItem.GetNew("http://www.zpf.fr/podcast/02.mp3", MediaType.Audio, MediaLocation.Remote) );   
```

```csharp
MediaPlayer.Current.StepBackward(); 
MediaPlayer.Current.StepForward();  
MediaPlayer.Current.Stop();  
```

```csharp
MediaPlayer.Current.MediaExtractor.CreateMediaItem(mediaItem);  
```


### Properties
| Property | Description |
| --- | --- |
| `Duration` | `TimeSpan`
| `Position` | `TimeSpan`
| `State`    | Playing, Paused, Stopped, Loading, Buffering, Failed

```csharp
MediaPlayer.Current.Playlist  
MediaPlayer.Current.Playlist.RepeatMode = RepeatMode.Off;  
MediaPlayer.Current.Playlist.ShuffleMode = ShuffleMode.Off;  
MediaPlayer.Current.Play(NextItem);  
MediaPlayer.Current.Play(PreviousItem);  
```

#### Volume properties
| Property | Description |
| --- | --- |
| `Balance` | From -1 (Left) to +1 (right). 0 = center. |  
| `Volume` | From 0 to 1. |  
| `Muted` | `True` or `False` |    

### Events
| event                | UWP | iOS |Android| Mac | WPF |
| -------------------- |:---:|:---:|:-----:|:---:|:---:|
| MediaItemChanged     |  X  |     |   X   |     |     |  
| MediaItemFailed      |  X  |  X  |       |  X  |  X  |  
| MediaItemFinished    |  X  |  X  |   X   |  X  |  X  |  
| PositionChanged      |  X  |  X  |   X   |  X  |  X  |
| StateChanged         |  X  |     |       |     |  X  |

X = implemented, blank = not yet implemented  
  

## Next steps
* code review
* iOS, Mac, WPF, ...
* MediaExtractor (WPF)**
* sync native playlist with internal playlist (UWP, ...)
* check ExoPlayer on Android
* video
* enhance/(re)design sample application

## How to build
### EDI
**!!! It seam that since update to VS2019 16.1.0/16.1.1 the build doesn't function anymore. It's still fine with VS2017. !!!**  
  
On Windows you can build the solution with Visual Studio 2019 with the latest Xamarin, .NET Core and UWP installed.  
  
For the moment the solution doesn't build with VS2019 on MacOS: https://developercommunity.visualstudio.com/content/problem/536913/vsfm-2019-doesnt-work-with-project-file-sdks-like.html .  
  
Nor on Microsoft Visual Studio Professional 2019 Version 16.1.1
https://github.com/onovotny/MSBuildSdkExtras/issues/168 https://developercommunity.visualstudio.com/content/problem/536913/vsfm-2019-doesnt-work-with-project-file-sdks-like.html

**Microsoft Visual Studio Professional 2017 Version 15.9.12
OK - Nuget - Android, iOS, MacOS, UWP, WPF**

it doesn't build on
Microsoft Visual Studio Enterprise 2019 (Preview) for Mac Version 8.1 Preview (8.1 build 2460)


### Visual Studio 2017 Developer Command Prompt
It builds on 'Visual Studio 2017 Developer Command Prompt'   
   
Build WPF debug
```
msbuild zpfmediaplayer.csproj /p:TargetsToBuild=Wpf /t:Rebuild  
```
Build all platformes debug
```
msbuild zpfmediaplayer.csproj /p:TargetsToBuild=All /t:Rebuild  
```   
   
Build WPF release
```
msbuild zpfmediaplayer.csproj /p:TargetsToBuild=Wpf;Configuration=Release /t:Rebuild  
```    
Build all platformes release (--> Nuget)
```
msbuild zpfmediaplayer.csproj /p:TargetsToBuild=All;Configuration=Release /t:Rebuild  
```
