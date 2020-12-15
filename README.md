# Libraries.Auth
Minecraft authentication library contains yggdrasil and the *new* Microsoft Authentication method

## Current state of development
### Yggdrasil method
- 100% done

### Microsoft Authentication method
- :ballot_box_with_check: Authenticate with Microsoft
- :ballot_box_with_check: Authenticate with Xbox Live
- :ballot_box_with_check: Authenticate with Minecraft
- :arrows_counterclockwise: Checking Game Ownership
- :asterisk: Get the profile

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