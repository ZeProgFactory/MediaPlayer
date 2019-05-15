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
|Xamarin.iOS| | [![Build status](https://build.appcenter.ms/v0.1/apps/4427f9f1-a7ee-4b86-b690-be87c518f62b/branches/master/badge)](https://appcenter.ms) | 1 % audio |
|Xamarin.Android| | [![Build status](https://build.appcenter.ms/v0.1/apps/faa57107-5590-491c-af93-2aa56bf1c7be/branches/master/badge)](https://appcenter.ms) | 50 % audio |
|Xamarin.Mac| | | |
|Xamarin.WPF| | | |
|WPF| | | |

## Next steps
* Android
* finish UWP implementation
* code review
* Mac, WPF, ...
* sync native playlist with intenal playlist (UWP, ...)
* video
* enhance/(re)design sample application
