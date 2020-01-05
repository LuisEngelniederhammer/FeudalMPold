# Project FeudalMP

## Goal
Recreate a game which should continue gameplay like the Mount&Blade: Warband "Persistent World" mod.  
At it's core there is not really much to the gameplay itself, it's just a sandbox-like map which is populated by players that do what they please. Gameplay is open and not restrictive.  
It's medieval themed, armor and weapons, resource gathering and crafting revolves around this concept, of course combat as well.  
The "World" is not some fancy open world mmorpg stuff but meerely just a simple "map". Each server can have a map that is currently played on. The map consists of difference castles and villages that can be occupied by factions made of players. There is full deplomacy between these factions, like war to capture the village of another faction or maybe join together to fight the biggest faction on the map.  
This is only multiplayer, no npcs. Everyone can host servers, the software will also be available open source. The principle is a drop-in/drop-out multiplayer. You join the current map whenever you please and just start playing. No lobbying or coop-wating.

## Background
I'm a software developer as a full-time job and I've already worked on "Persistent Kingdoms" which was a mod build upon "Persistent World". The M&B Modding is just too exhausting and restrictive why I've decided to try and build the mod into a gameengine. This obviously has to be done almost from scratch. While my strength surely is programming, 3D Modelling, Animations, Sound and 2D Art is far off for me. I'm looking into them but its all new to me as my background is not in game development. These aspects will be done some time later in the development process, hopefully with the help of others.

## What's currently the plan
Check out the loose projectplan for some general informations. In short:  
* Networking  
The first and most challenging task will be networking. This is what I currently do. As soon as this is done, most of the other stuff will build upon it as this game will be multiplayer-only. Any interaction that has to be broadcasted across the network for other players to see will be using the now build networking, so it better be good.
  
* Combat  
As soon as networking is done probably the worst tasks of all will follow: fun combat over network. I want to do this in the style of M&B, meaning 4 directions of attack and 4 directions of blocking those attacks. This includes a good hit-detection and reporting to feel right, animations that really show a hit and some really good syncing across the network.

* Gameplay
When the top two tasks are done, I can start to do some first gameplay mechanics that should fill the world with activities beside or build upon combat.

* etc

## Loose projectplan
* Infrastructure and architecture
  * setting up git
  * setup server-side build pipline
* Base functionalities
  * Menu Navigation
  * Client Identification (through steam open-id)
  * Networking (currently work in progress) (high prio)
  * Character Logic
    * Inventory
    * Armor
    * Weapons + Equipments
    * Health system 
  * World Interaction
    * Triggers
    * Interactions with objects/physics
* Gameplay core mechanics
  * Player interactions
    * Chat
    * Trading
    * Inventory Storage
    * Groups/Guilds etc.
  * Combat (high prio)
  * Resources: farming/harvesting + usage for crafting
  * Feudal system for players to participate in and interact with other guilds/groups
* Service and other (super low prio)
  * Anti-Cheat
  * create a client update service (low prio, done before first beta release)
  * map repository
  * easy server administration

## Contributing
In general I am open to contributions as long as they actually contribute something.
* If you can help with 3D Models, Animations, 2D Art, Scripting etc. please do so
* Donations or other monetary means are surely welcome but will only be accepted when and if there is a playable version of the game which must contain the core gameplay features. 
