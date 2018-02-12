# Eman Adventures

[![Build Status](https://travis-ci.org/nightblade9/eman-adventures.svg?branch=master)](https://travis-ci.org/nightblade9/eman-adventures)

Procedurally-generated RPG. Made with MonoGame/C#.

# Modding (Future Plans)

This game will eventually include mods. To create a mod:

- Open up Visual Studio or MonoDevelop and create a new class library project
- Reference the API project only. Don't reference the UI (you won't need to install MonoGame)
- Create a single class that subclasses `ModConfiguration`

To test your mod:
- Build your project
- Place your DLL in the "Mods" directory of the game
- Check the "Mods" menu to make sure your mod appears

You don't need MonoGame installed for some simple mods (eg. a new class), but you will need it for more involved mods. If you want to modify visual or audio aspects, you can find the game assets and use those as templates or starting-points.

# Roadmap

This is a large project that will span several milestones. Our goal is to include a minimal, fun core game, along with three plugins (easy, medium, and hard), as well as documentation on how we created those plugins.

Milestones (soon to be converted to GitHub milestones):

- Very, very basic (MVP) generated RPG game
- Proof-of-concept mod (Pattern Warrior battle system)
- More complete RPG
- Pattern Warrior TC (Total Conversion)
- Fishing area mod
- Ogre class mod
