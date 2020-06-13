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

        }

        void Update()
		{
		}

		//[System.Obsolete]
		//IEnumerator LoadAsset()
		//{
		//	Debug.Log("load asset");
		//	string assetpath = "file://" + Application.dataPath + "/AssetBundles/";
		//	string manifestName = "AssetBundles";
		//	string targetAssetName = "character/enemyybot.ab";
		//	//AssetBundle ab = AssetBundle.LoadFromFile(path);
		//	//AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
		//	WWW wwwManifest = WWW.LoadFromCacheOrDownload(assetpath+ manifestName, version);
		//	yield return wwwManifest;
		//	if(wwwManifest.error == null)
		//	{
		//		Debug.Log("download Complete");
		//		AssetBundle mainFestBundle = wwwManifest.assetBundle;
		//		AssetBundleManifest manifest = (AssetBundleManifest)mainFestBundle.LoadAsset("AssetBundlManifest");
		//		string[] dps = manifest.GetAllDependencies(targetAssetName);
		//		AssetBundle[] abs = new AssetBundle[dps.Length];
		//		for (int i = 0; i < dps.Length; i++)
		//		{
		//			string durl = assetpath + dps[i];
		//			WWW dwww = WWW.LoadFromCacheOrDownload(durl, version);
		//			yield return dwww;
		//			abs[i] = dwww.assetBundle;
		//		}

		//		WWW item = WWW.LoadFromCacheOrDownload(assetpath + targetAssetName, version);
  //              yield return item;
  //              if (item.error == null)
  //              {
  //                  Debug.Log("item Complete");
  //                  UnityEngine.AssetBundle cubeBundle = item.assetBundle;
  //                  GameObject cube = cubeBundle.LoadAsset(targetAssetName) as GameObject;
  //                  Instantiate(cube, Vector3.zero, Quaternion.identity);
  //              }
  //          }
		//}
    }


}