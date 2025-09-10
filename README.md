# Great Engine <span style="color:green">v1.1.0</span>

### Changelog
**Version 1.1.0**

> Changelog only contains things that are important to note
- Game Changes
    - Changed menu layout to be aligned left
    - Score collision now dynamically resembles pipe gap size (± 0.5f)
    - The loading bar now displays the proper percentage
    - Score collision width can be altered in CONFIG
    
- Settings
    - Added anti-aliasing toggle to settings 
    - Changed settings layout to be less compact
    - Each setting calls LoadSettings() everytime it's altered to ensure settings were saved 
    
- Optimisations
    - "CONSTANTS_Set.cs" has been improved so much that the Update function now contains 4 lines of code, a try-catch stack trace for null variables has been implemented for required objects
    - "PipePart.cs" has been changed so that many things like pipe rotation are handled at Start
    - "GameUI.cs" now only updates the Death UI when it's enabled and the game score text only changes when pipes are passed through using AnimateScore() 

- Fixes
    - Pipes now get cleared without possible memory loss using a reference to the instanciated prefab and not the prefab itself
    - Options now update live (Like they were supposed to originally)

- Code
    - Code comments have appeared in some areas of code that would look absurd to beginners without explaination

<br/>

### About Great Engine
Great Engine is an official framework by Tureen built with Unity to create Great Flarp Birb games without needing to set up the frivolous mechanics beforehand.

It was mainly created because Tureen was tired of recreating the same framework for each Great Flarp Birb project and preferred to have a starting point that could be updated seperately from the main games.

Updates to Great Engine may cause development times to simmer if games made with Great Engine decide to port to later versions of the framework.

<br/>

### Submitting Features
To submit features to Great Engine, create new GitHub issues with the Feature tag and use them to explain what Great Engine should include.

<img src="Assets/Sprites/UI/Logo/GE_Icon%20GithubVersion.png" width="200"/> <img src="Assets/Sprites/UI/Logo/Great%20Engine.png" width="250"/>

The GitHub is a shared place for the code to exist and be hosted, as well as debated in GitHub issues which makes them the perfect place to submit features for Great Engine.

<br/>

### Usage
Great Engine's usage policy follows Apache 2.0, but when a game uses Great Engine [including parts of Great Engine] it must specify **clearly** that it does in an unskippable location of the game such as the intro or on the published page.

Tureen must be credited - alternatively as Tureen Creative Media - on the published page if Great Engine/parts of Great Engine were used in your project or game.

 > "Published page" refers to the page the project or game was published to, such as itch.io, steampowered.com and other game sites.