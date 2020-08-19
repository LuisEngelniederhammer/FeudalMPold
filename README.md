# Project FeudalMP

## Goal
Recreate a game which should continue gameplay like the Mount&Blade: Warband "Persistent World"/"Persistent Kingdoms" mod.  
At it's core there is not really much to the gameplay itself, it's just a sandbox-like map which is populated by players that do what they please. Gameplay is open and not restrictive.  
It's medieval themed, armor and weapons, resource gathering and crafting revolves around this concept, of course combat as well. World interactions is "skill/action"-based, which to my understanding is "you do an action and the world directly response to that". If you mine rock, you have to actively hit the rock to get something out of it. No enraging "waiting" or "countdowns" until a resource is mined ready.  
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




# FeudalMP Versuch 3

*Aim & Concept*
* Aim is Persistent World/Persistent Kingdoms as standalone game
* Combat over network needs to work correctly before anything else will be done, the game is shit otherwise
* low-poly style(assets) to reduce production overhead/complexity
* open-source for contribution
* Godot as engine -> Scene/Map/Asset Editing/Modding directly with the engine as tool -> easy and everyone can do it
* 

# Shaders
### open
* Rust/Degredation on iron objects
* Terrain
* Light/Godrays
* Wind and Foliage interaction
* Player and Foliage interaction
### done
* Water shader currently done, ugly but one might think it could be water

# Networking
### open
* Dedicated Server Node
* Client Synchronisation
  * Position - udp
  * Play Animation - udp
  * Attack/Hitdetection ???
  * Distance calculation and network traffic optimizations - udp
  * item and object placement and access - udp
  * containers like chests - tcp
  * time of day - udp
* Master Server List Service
* Scene and Asset download if locally not present
* Auto Updater / Scrapp if Steam is used
* Steam open-id authentication
* Ban-list based on steam base64 id, mac and ip (put in AGB)
### done
* help :+)
# Gameplay and World
* Implement animations for player character
* the whole PW Array of stuff + other things later on
* Combat first, this needs to work
# UI/UX
### open
* Main Menu
* Settings
  * Keysettings
  * Graphic
* Server Browser
* Inventory
* minimalistic HUD
* Interaction with objects or players
# Dummy/Test Animations
### open
* walking - legs
---
* idle - upper body - unarmed
* walking - upper body - unarmed
* running - upper body - unarmed
* attack - upper body - unarmed - left
* attack - upper body - unarmed - right
* attack - upper body - unarmed - up
* block - upper body - unarmed - left
* block - upper body - unarmed - right
* block - upper body - unarmed - up
* unsheathe - upper body
* sheathe - upper body
---
* idle - upper body - one handed
* walking - upper body - one handed
* running - upper body - one handed
* attack - upper body - one handed - left
* attack - upper body - one handed - right
* attack - upper body - one handed - overhead
* attack - upper body - one handed - pirce
* block - upper body - one handed - left
* block - upper body - one handed - right
* block - upper body - one handed - overhead
* block - upper body - one handed - pirce
* unsheathe - upper body
* sheathe - upper body
---
* idle - upper body - one handed + shield
* walking - upper body - one handed + shield
* running - upper body - one handed + shield
* attack - upper body - one handed + shield - left
* attack - upper body - one handed + shield - right
* attack - upper body - one handed + shield - overhead
* attack - upper body - one handed + shield - pirce
* block - upper body - one handed + shield - left
* block - upper body - one handed + shield - right
* block - upper body - one handed + shield - overhead
* block - upper body - one handed + shield - pirce
* unsheathe - upper body
* sheathe - upper body
---
* idle - upper body - two handed
* walking - upper body - two handed
* running - upper body - two handed
* attack - upper body - two handed - left
* attack - upper body - two handed - right
* attack - upper body - two handed - overhead
* attack - upper body - two handed - pirce
* block - upper body - two handed - left
* block - upper body - two handed - right
* block - upper body - two handed - overhead
* block - upper body - two handed - pirce
* unsheathe - upper body
* sheathe - upper body
---
* idle - upper body - spear-like
* walking - upper body - spear-like
* running - upper body - spear-like
* attack - upper body - spear-like - left
* attack - upper body - spear-like - right
* attack - upper body - spear-like - overhead
* attack - upper body - spear-like - pirce
* block - upper body -  spear-like - left
* block - upper body -  spear-like - right
* block - upper body -  spear-like - overhead
* block - upper body -  spear-like - pirce
* unsheathe - upper body
* sheathe - upper body
---
* access inventory - whole amature
* winding a gate or a bucket from a well - whole amature
* kick
* emotes etc.: sitting, waving, pointing etc.
* carry heavy objects/items - upper body

* *death - although should probably be ragdoll, easier than animations, maybe mixture of animation and ragdoll*
### done
* running - legs
* idle - upper body - two handed

### definitions
* **upper body** = animation is only using bones from the upper body amature so movement can used another animation, independent of the upper body -> movement only has to be animated once and can be used for all equiped items/weapons
* **legs** = animations that only do movement using the leg bones
* **unarmed** = either fists or the weapon is currenty put into the scabbard
* **one handed** = every weapon that should be used with one hand, like short swords, daggers, maces etc.
* **two handed** = longswords, axes etc.
* **spear-like** = spears, pikes, longsword in mordhau position


###### Support me
if you whish to support me, please go ahead.
[![](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=QPDPYYPQWW6NU&source=url)
