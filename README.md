# Libraries.Auth
Minecraft authentication library contains yggdrasil and the *new* Microsoft Authentication method

> **IMPORTANT!**
> All of HunLuxLauncher's projects **require** CzompiSoftware's NuGet server (because most of the packages are from there), so you need to add a NuGet repository to your VS or download [this NuGet.config](https://raw.githubusercontent.com/CzompiSoftware/SampleProject/master/nuget.config) file and place it next to your .sln file.
> If you'd like to manually add CzompiSoftware's NuGet server, then add the following url to your `Visual Studio` or `NuGet.config` file:
> ```
> https://nuget.czompisoftware.hu/v3/index.json
> ```

## Current state of development
### Yggdrasil Authentication method
- :ballot_box_with_check: Authenticate
- :ballot_box_with_check: Invalidate
- :ballot_box_with_check: Refresh
- :ballot_box_with_check: Signout
- :ballot_box_with_check: Validate


### Microsoft Authentication method
- :ballot_box_with_check: Authenticate with Microsoft
- :ballot_box_with_check: Authenticate with Xbox Live
- :ballot_box_with_check: Authenticate with Minecraft
- :ballot_box_with_check: Checking Game Ownership
- :ballot_box_with_check: Get the profile

## How does it work?
### Yggdrasil Authentication method
> Find it out yourself, but keep in mind, that it will be deprecated after they switch fully to **Microsoft Authentication Scheme**.
> > To help a bit, here is how to start using the *Yggdrasil Authentication method*:
> > ```cs
> > using hu.hunluxlauncher.libraries.auth.yggdrasil;
> > ...
> > Authenticator authenticator = new Authenticator(user_agent, client_token);
> > ```

### Microsoft Authentication method
> Check out the [sample WPF project](https://github.com/HunLuxLauncher/Libraries.Auth.Tests) to understand how does it work.

### Used sources:
- [wiki.vg](https://wiki.vg/Microsoft_Authentication_Scheme) 
- [Calling Xbox Live Services from Your Title Service](http://strauss.hu/download/16)

> A product of [Czompi Software](https://czompisoftware.hu/en/).
