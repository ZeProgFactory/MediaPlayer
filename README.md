# MediaPlayer - !!! Preview !!!
Cross platform media player lib</BR>
based on ideas and code of [martijn00](https://github.com/martijn00) ( https://github.com/martijn00/XamarinMediaManager )

[You’ll find the story behind this MediaPlayer lib here …](https://zeprogfactory.github.io/MediaPlayer/)
  

## Current platform support (evolves on nearly daily basis)

|Platform|Version|Build|State|
| ------------------- | :------------------: |  :------------------: | :------------------: |
|Windows 10 UWP| | [![Build status](https://build.appcenter.ms/v0.1/apps/c04f9cb9-8f2a-4d33-9011-0f1fe8235713/branches/master/badge)](https://appcenter.ms) |  90 % audio |
|Xamarin.iOS| | [![Build status](https://build.appcenter.ms/v0.1/apps/4427f9f1-a7ee-4b86-b690-be87c518f62b/branches/master/badge)](https://appcenter.ms) | 30 % audio |
|Xamarin.Android| | [![Build status](https://build.appcenter.ms/v0.1/apps/faa57107-5590-491c-af93-2aa56bf1c7be/branches/master/badge)](https://appcenter.ms) | 90 % audio |
|Xamarin.Mac| | | |
|Xamarin.WPF| | | |
|WPF| | | |

## Setup
* Available on NuGet: https://www.nuget.org/packages/ZPFMediaPlayer [![NuGet](https://img.shields.io/nuget/v/ZPFMediaPlayer.svg)](https://www.nuget.org/packages/ZPFMediaPlayer/)
* For Xamarin.Forms install into your Main and Client projects.


## How to use

A Xamarin.Forms sample how to use MediaPlayer is in this Git, but here are the  basics ...

### Initialization 
Before using the MediaPlayer you had to initialize it in the platform dependent project. 

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


### Play 
```csharp
MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");  
```

### Native player
*Android*
```csharp
Android.Media.MediaPlayer Player = (Android.Media.MediaPlayer)MediaPlayer.Current.Player;  
```
Remark: The Android Player type will probably change in the future to the ExoPlayer. So check the doc on each release. 
  

*UWP*
```csharp
Windows.Media.Playback.MediaPlayer Player = (Windows.Media.Playback.MediaPlayer)MediaPlayer.Current.Player;  
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
```csharp
MediaPlayer.Current.Duration  
MediaPlayer.Current.Position  
MediaPlayer.Current.Playlist  
MediaPlayer.Current.Playlist.RepeatMode = RepeatMode.Off;  
MediaPlayer.Current.Playlist.ShuffleMode = ShuffleMode.Off;  
MediaPlayer.Current.Play(NextItem);  
MediaPlayer.Current.Play(PreviousItem);  
MediaPlayer.Current.State  
```
   
### Events
| event                | UWP | iOS |Android| Mac | WPF |
| -------------------- |:---:|:---:|:-----:|:---:|:---:|
| BufferingChanged     |     |     |       |     |     |   
| MediaItemChanged     |  X  |     |   X   |     |     |  
| MediaItemFailed      |  X  |     |       |     |     |  
| MediaItemFinished    |  X  |     |   X   |     |     |  
| PositionChanged      |  X  |  X  |   X   |  X  |  X  |
| StateChanged         |  X  |     |       |     |     |

X = implemented, blanc = net yet implemented  
  

## Next steps
* code review
* iOS, Mac, WPF, ...
* sync native playlist with internal playlist (UWP, ...)
* check ExoPlayer on Android
* video
* enhance/(re)design sample application

## How to build
!!! It seam that since update to VS2019 16.1.0/16.1.1 the build doesn't function anymore. It's still fine with VS2017. !!!  
On Windows you can build the solution with Visual Studio 2019 with the latest Xamarin, .NET Core and UWP installed.   
For the moment the solution doesn't build with VS2019 on MacOS: https://developercommunity.visualstudio.com/content/problem/536913/vsfm-2019-doesnt-work-with-project-file-sdks-like.html .
