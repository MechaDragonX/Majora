# Majora

## What is Majora?
Majora is an audio player project. A terminal and desktop version are ostensibly compelte for .NET and a temrinal version is in the works in C++. The .NET terminal version was made for testing and a desktop GUI version using [Avalonia](https://github.com/AvaloniaUI/Avalonia), a cross-platform UI framework.

## How do I run it?
You can get the latest .NET binaries from the [releases page](https://github.com/MechaDragonX/Bheithir/releases). **There are 64-bit Windows, macOS, and Linux builds available for the terminal and desktop GUI program.**
### C++
No binaries are available yet. The [SFML](https://www.sfml-dev.org/) library is necesarry for compilation.
#### Linux
- Debian-based Distributions
    - Install the `libsfml-dev` package
- Arch Linux-based Distributions
    - install the `sfml` package
#### macOS
- Method 1: Command Line
    - Install the [Homebrew](https://brew.sh/) package manager with the following command:
```sh
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```
    - Install the `sfml` package.
        - Supported on x86 Macs from Mojave onward, and all M1 Macs running Big Sur, and presumably all future versions.
- Method 2: Via Xcode
    - [See SFML's documentation](https://www.sfml-dev.org/tutorials/2.5/start-osx.php)
#### Windows
    - Method 1: Manually
        - Download the version that corresponds to the C++ compiler you are using and copy all the folders to the corresponding locations where the compiler is located on your system.
            - Detailed instructions for MinGW can be found on this website that explains how to install the [SDL](https://www.libsdl.org/) library.
                - This should apply to other compilers. Since I primarily compile for Linux, I prefer using nearly the same thing across all systems.
    - Method 2: Via Visual Studio (Uses Microsoft Visual C++)
        - [See SFML's documentation](https://www.sfml-dev.org/tutorials/2.5/start-vc.php)
    - Method 3: Via Code::Blocks (Uses MinGW)
        - [See SFML's documentation](https://www.sfml-dev.org/tutorials/2.5/start-cb.php)

## What file types are supported right now?
### GUI Desktop Application (.NET)
The GUI desktop application uses [LibVLC](https://wiki.videolan.org/LibVLC/) so it should support all audio formats supports VLC supports including:
- [Waveform](https://en.wikipedia.org/wiki/WAV)
    - Standard (`*.wav`, `*.wave`) and 64-bit (`*.w64`)
- [Free Lossless Audio Codec](https://en.wikipedia.org/wiki/FLAC)
    - `*.flac`
- [Ogg Vorbis](https://en.wikipedia.org/wiki/Vorbis)
    - `*.ogg`
- [MPEG-1/MPEG-2 Audio Layer 3](https://en.wikipedia.org/wiki/MP3)
    - MP3
    - `*.mp3`
- [Advanced Audio Coding](https://en.wikipedia.org/wiki/Advanced_Audio_Coding)
    - Includes MPEG-4 and Apple containers
    - `*.aac` and `*.m4a`
### Terminal Program (.NET)
The terminal program still uses different libraries and I don't end to change it. It supports these formats:
- [Waveform](https://en.wikipedia.org/wiki/WAV)
    - Standard (`*.wav`, `*.wave`) and 64-bit (`*.w64`)
- [Free Lossless Audio Codec](https://en.wikipedia.org/wiki/FLAC)
    - `*.flac`
- [Ogg Vorbis](https://en.wikipedia.org/wiki/Vorbis)
    - `*.ogg`
- [MPEG-1/MPEG-2 Audio Layer 3](https://en.wikipedia.org/wiki/MP3)
    - MP3
    - `*.mp3`
- [Advanced Audio Coding](https://en.wikipedia.org/wiki/Advanced_Audio_Coding)
    - Includes MPEG-4 and Apple containers
    - `*.aac` and `*.m4a`
- [Audio Interchange File Format](https://en.wikipedia.org/wiki/Audio_Interchange_File_Format)
    - `*.aiff`
- [AU/SND](https://en.wikipedia.org/wiki/Au_file_format)
    - Usually encoded with the [μ-law algorithm](https://en.wikipedia.org/wiki/%CE%9C-law_algorithm)

## Is it functional?
Both the the .NET terminal and desltop program and are completely functional. At the moment, no further development will done. The C++ terminal version is in progress and no terminals are available at the moment. Please see the [releases page](https://github.com/MechaDragonX/Majora/releases) for further details on completed features and how to use the programs. Check the [issues](https://github.com/MechaDragonX/Majora/issues) tab for any issues.

## What the hell is this name?
The name is a reference to the video game, [*The Legend of Zelda: Majora's Mask*](https://en.wikipedia.org/wiki/The_Legend_of_Zelda:_Majora%27s_Mask). In the game, you help various people in the main quests, each time getting a special song or instrument associated with that race (Goron, Zora, etc.). As such, the progression is tied to the music. The meaning and impact of the game is tied to this musical progression. There's no special association with this progression however. I just like the name and the reference.
