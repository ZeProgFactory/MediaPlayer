# MediaPlayer
Cross platform media player lib</BR>
based on ideas and code of [martijn00](https://github.com/martijn00) ( https://github.com/martijn00/XamarinMediaManager )

MediaPlayer is basically a clone of martijn00s XamarinMediaManager. It started all when I tried to contribute to this project. </BR>
Any media player is a quite complex project and martijn00 is a quite sophisticated developer. Whereas my first steps went well, I was quickly struggling with the project structure and implementation. When I saw that I took more time analyzing the project than developing features I started thinking about this reimplementation… </BR>
</BR>
Doing so I finally I understand many of the tricks hidden in martijn00s project.</BR>
</BR>
So, I didn’t reinvent the wheel, I disassembled and reassembled it 😉

## Platform Support

|Platform|Version|Build|State|
| ------------------- | :------------------: |  :------------------: | :------------------: |
|Windows 10 UWP| | [![Build status](https://build.appcenter.ms/v0.1/apps/c04f9cb9-8f2a-4d33-9011-0f1fe8235713/branches/master/badge)](https://appcenter.ms) |  90 % audio |
|Xamarin.iOS| | [![Build status](https://build.appcenter.ms/v0.1/apps/4427f9f1-a7ee-4b86-b690-be87c518f62b/branches/master/badge)](https://appcenter.ms) | 30 % audio |
|Xamarin.Android| | [![Build status](https://build.appcenter.ms/v0.1/apps/faa57107-5590-491c-af93-2aa56bf1c7be/branches/master/badge)](https://appcenter.ms) | 90 % audio |
|Xamarin.Mac| | | |
|Xamarin.WPF| | | |
|WPF| | | |

## How to use
MediaPlayer.Current.Init();  
MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");  

### Native player
Windows.Media.Playback.MediaPlayer Player = (Windows.Media.Playback.MediaPlayer)MediaPlayer.Current.Player;  


## API
### Methods
MediaPlayer.Current.Pause();  
MediaPlayer.Current.Play("http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4");  
MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");  
MediaPlayer.Current.Play(mi);  
MediaPlayer.Current.Play();  

MediaPlayer.Current.Playlist.Current   
MediaPlayer.Current.Playlist.PlayNext();  
MediaPlayer.Current.Playlist.PlayPrevious();  
MediaPlayer.Current.Playlist.Add( MediaItem.GetNew("http://www.zpf.fr/podcast/02.mp3", MediaType.Audio, MediaLocation.Remote) );   

MediaPlayer.Current.StepBackward(); 
MediaPlayer.Current.StepForward();  
MediaPlayer.Current.Stop();  

MediaPlayer.Current.MediaExtractor.CreateMediaItem(mediaItem);  


### Properies
MediaPlayer.Current.Duration  
MediaPlayer.Current.Position  
MediaPlayer.Current.Playlist  
MediaPlayer.Current.Playlist.RepeatMode = RepeatMode.Off;  
MediaPlayer.Current.Playlist.ShuffleMode = ShuffleMode.Off;  
MediaPlayer.Current.Play(NextItem);  
MediaPlayer.Current.Play(PreviousItem);  
MediaPlayer.Current.State  
   
### Events
| event                | UWP | iOS |Android| Mac | WPF |
| -------------------- |:---:|:---:|:-----:|:---:|:---:|
| BufferingChanged     |     |     |       |     |     |   
| MediaItemChanged     |  X  |     |   X   |     |     |  
| MediaItemFailed      |  X  |     |       |     |     |  
| MediaItemFinished    |  X  |     |   X   |     |     |  
| PositionChanged      |  X  |  X  |   X   |  X  |  X  |
| StateChanged         |  X  |     |       |     |     |


## Next steps
* code review
* iOS, Mac, WPF, ...
* sync native playlist with intenal playlist (UWP, ...)
* check ExoPlayer on Android
* video
* enhance/(re)design sample application

## How to build
!!! It seam that since update to VS2019 16.1.0/16.1.1 the build doesn't function anymore. It's still fine with VS2017. !!!  
On Windows you can build the solution with Visual Studio 2019 with the latest Xamarin, .NET Core and UWP installed.   
For the moment the solution doesn't build with VS2019 on MacOS: https://developercommunity.visualstudio.com/content/problem/536913/vsfm-2019-doesnt-work-with-project-file-sdks-like.html .
