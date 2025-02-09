### =================================== ###
# Localization Manager Package User Guide #
### =================================== ###

# How to use the package in your Unity project
> Open your Unity project
> Acces your Unity Package Manager window ("Window -> Package Manager")
> On the top left corner, click on the "+" icon
> Select "Install package from git URL"
> Enter the git URL (https://github.com/BiscuitPrime/Localization_Manager.git)
> Click "Install"
> Wait for a few seconds
> Voilà !

# How to use the localization system in Unity

The goal of this package is to offer the user a standardized and (somewhat) scalable way to implement rapidly a localization system in their Unity project. 
A game supporting various languages, selectable by the player, is always a nice plus.

The package is articulated around a unique script : **LocalizationManager.cs**.
This script should be placed as a component to an empty game object, preferrably present in the first scene of the project.

The script is not destroyed on load, but that is optional (mind you however that not doing so will require the project to load the script everytime a new scene is loaded).

To use the localization system, when displaying a text in the project, use the return value of the **LocalizationManager.Instance.LocalText(string tag)** function.
The text returned will be the one defined in the JSON language files according to the current language selected (indicated by the field "_option" in LocalizationManager).

This latter field can be either setup in the game object itself (SerializeField) or through setter and getter functions.

# How to setup the languages
The system works on tags defined in the **JSONLocalizationTextClass**. These tags will be used by the system as a non-localized way to indentify localized text data, defined in JSON files.

These JSON language files must be defined in the Resources folder, name at your discretion, and contain fields associating a tag to a text data in the language defined by that JSON file (consult Samples for more information).

In the LocalizationManager instance, the user simply needs to create the dictionary linking the JSON language files' to the associated **LOCALIZATION_OPTIONS** enum.

# How to add a new language
Simply create a new JSON file. Each field must be a tag defined in **JSONLocalizationTextClass**.

# How to add a new tag
Add a new field to the **JSONLocalizationTextClass**.
