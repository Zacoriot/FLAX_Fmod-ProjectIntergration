using FlaxEngine;

namespace Game;

/// <summary>
/// BankPaths Script.
/// </summary>
public class BankPaths
{
#if FLAX_EDITOR
    //Path to .bank files in project
    public static string Path = Globals.ProjectFolder + "/Content/Fmod/Test/Build/Desktop/";
#else
    //Path to where you will place your .bank files when the project is built
    public static string Path = Globals.ProjectFolder + "/Output/Win64/Content/FmodBanks/";
#endif
}
