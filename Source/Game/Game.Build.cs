using Flax.Build;
using Flax.Build.NativeCpp;
using System.IO;

public class Game : GameModule
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // C# and C++ scripting
        BuildNativeCode = true;
    }

    /// <inheritdoc />
    public override void Setup(BuildOptions options)
    {
        base.Setup(options);

        options.ScriptingAPI.IgnoreMissingDocumentationWarnings = true;


        // FMOD CORE //
        var FmodCoreX64 = Path.Combine(FolderPath, "..", "..", "Content", "Fmod", "Core", "x64");
        var FmodCoreInclude = Path.Combine(FolderPath, "..", "..", "Source", "Fmod", "Core");
        //libs
        options.LinkEnv.InputLibraries.Add(Path.Combine(FmodCoreX64, "fmod_vc.lib"));
        options.LinkEnv.InputLibraries.Add(Path.Combine(FmodCoreX64, "fmodL_vc.lib"));
        //dll
        options.DependencyFiles.Add(Path.Combine(FmodCoreX64, "fmod.dll"));
        options.DependencyFiles.Add(Path.Combine(FmodCoreX64, "fmodL.dll"));
        // include
        options.CompileEnv.IncludePaths.Add(FmodCoreInclude);

        // FMOD STUDIO //
        var FmodStudioX64 = Path.Combine(FolderPath, "..", "..", "Content", "Fmod", "Studio", "x64");
        var FmodStudioInclude = Path.Combine(FolderPath, "..", "..", "Source", "Fmod", "Studio");
        //libs
        options.LinkEnv.InputLibraries.Add(Path.Combine(FmodStudioX64, "fmodstudio_vc.lib"));
        options.LinkEnv.InputLibraries.Add(Path.Combine(FmodStudioX64, "fmodstudioL_vc.lib"));
        //dll
        options.DependencyFiles.Add(Path.Combine(FmodStudioX64, "fmodstudio.dll"));
        options.DependencyFiles.Add(Path.Combine(FmodStudioX64, "fmodstudioL.dll"));
        // include
        options.CompileEnv.IncludePaths.Add(FmodStudioInclude);

        // Here you can modify the build options for your game module
        // To reference another module use: options.PublicDependencies.Add("Audio");
        // To add C++ define use: options.PublicDefinitions.Add("COMPILE_WITH_FLAX");
        // To learn more see scripting documentation.
    }
}
