using FlaxEngine;

namespace Game;

/// <summary>
/// BankPaths Script.
/// </summary>
public class BankPaths
{
#if FLAX_EDITOR
    public static string Path = Globals.ProjectFolder + "/Content/Fmod/Test/Build/Desktop/";
#else
    public static string Path = Globals.ProjectFolder + "/Output/Win64/Content/FmodBanks/";
#endif
}
