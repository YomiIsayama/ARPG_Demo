/* Written by Kaz Crowe */
/* GameManager.cs */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SingleInstance;
using System.IO;

namespace SingleInstance
{
	public class GameManager : SingleMono<GameManager>
	{
		private int version;

		void Start()
		{
			version = 1;
            StartCoroutine(LoadAsset());

        }

        void Update()
		{
		}

		IEnumerator LoadAsset()
		{
			string assetpath = "file://" + Application.dataPath + "/AssetBundles/";
			string manifestName = "AssetBundles";
			string targetAssetName = "character/enemyybot.ab";
			//AssetBundle ab = AssetBundle.LoadFromFile(path);
			//AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
			WWW wwwManifest = WWW.LoadFromCacheOrDownload(assetpath+ manifestName, version);
			yield return wwwManifest;
			if(wwwManifest.error == null)
			{
				Debug.Log("download Complete");
				AssetBundle mainFestBundle = wwwManifest.assetBundle;
				AssetBundleManifest manifest = (AssetBundleManifest)mainFestBundle.LoadAsset("AssetBundlManifest");
				string[] dps = manifest.GetAllDependencies(targetAssetName);
				AssetBundle[] abs = new AssetBundle[dps.Length];
				for (int i = 0; i < dps.Length; i++)
				{
					string durl = assetpath + dps[i];
					WWW dwww = WWW.LoadFromCacheOrDownload(durl, version);
					yield return dwww;
					abs[i] = dwww.assetBundle;
				}

				WWW cubewww = WWW.LoadFromCacheOrDownload(assetpath + targetAssetName, version);
                yield return cubewww;
                if (cubewww.error == null)
                {
                    Debug.Log("cubewww Complete");
                    //得到Cube的资源列表
                    UnityEngine.AssetBundle cubeBundle = cubewww.assetBundle;
                    //通过资源包获取相对应的资源
                    GameObject cube = cubeBundle.LoadAsset(targetAssetName) as GameObject;
                    //创建对象
                    Instantiate(cube, Vector3.zero, Quaternion.identity);
                }
            }
		}
    }


}