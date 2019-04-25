
using System.IO;

using UnityEditor;

namespace Hotfix
{
    [InitializeOnLoad]
    public class Startup
    {
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
        private const string CodeDir = "Assets/Res/Code/";
        private const string HotfixDll = "Hotfix.dll";
        private const string HotfixPdb = "Hotfix.pdb";

        static Startup()
        {

            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "hotfix.dll.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "hotfix.pdb.bytes"), true);
            AssetDatabase.Refresh();
        }
    }
}