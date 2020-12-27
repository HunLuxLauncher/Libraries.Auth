# Libraries.Auth
Minecraft authentication library contains yggdrasil and the *new* Microsoft Authentication method

> **IMPORTANT!**
> All of HunLuxLauncher's projects **require** CzompiSoftware's NuGet server (because most of the packages are from there), so you need to add a NuGet repository to your VS or download [this NuGet.config](https://raw.githubusercontent.com/CzompiSoftware/SampleProject/master/nuget.config) file and place it next to your .sln file.
> If you'd like to manually add CzompiSoftware's NuGet server, then add the following url to your `Visual Studio` or `NuGet.config` file:
> ```
> https://nuget.czompisoftware.hu/v3/index.json
> ```

## Current state of development
### Yggdrasil method
- 100% done

### Microsoft Authentication method
- :ballot_box_with_check: Authenticate with Microsoft
- :ballot_box_with_check: Authenticate with Xbox Live
- :ballot_box_with_check: Authenticate with Minecraft
- :ballot_box_with_check: Checking Game Ownership
- :ballot_box_with_check: Get the profile

## How does it work?
### Microsoft Authentication method
> ### Authentication procedure
> > #### Authenticate with Microsoft #1
> > - In this part, you will need user interaction to provide their Microsoft account details to OAuth client and then we will handle the rest of the process.
> 
> > #### Authenticate with Microsoft #2
> > - Got our *first* token. :yay:
> 
> ### Used sources:
> > - [wiki.vg](https://wiki.vg/Microsoft_Authentication_Scheme)
> > - [Calling Xbox Live Services from Your Title Service](http://strauss.hu/download/16)

> A product of [Czompi Software](https://czompisoftware.hu/en/).
