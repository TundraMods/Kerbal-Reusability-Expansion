Deployable Aero Surface Plugin for Kerbal Space Program 1.2.2 by DerpyFirework

This plugin contain a single PartModule to prevent aerodynamic surfaces from working based on an animation state or any other module implementing IScalarModule.

Use:

MODULE
{
	name = ModuleDeployableAero
	DeployModuleIndex = 0 //Index of module implementing IScalarModule
	DeployModulePos = 1 //Position (0 or 1) of animation considered deployed. Part spawns at 0.
}

License:
This plugin is released into the Public Domain.