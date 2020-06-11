using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("AssetBundles/Build AssetBundles")]
    static void BuildAssetBundle()
    {
        string dir = "AssetBundles";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }

}