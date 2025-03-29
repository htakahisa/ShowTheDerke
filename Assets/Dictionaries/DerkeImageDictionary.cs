using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;




public class DerkeImageDictionary : MonoBehaviour
{
    public string assetAddress = "Omoko"; // Addressableに設定したアセットのアドレス

    // resources辞書を定義
    private Dictionary<string, Sprite> resources = new Dictionary<string, Sprite>();

    public static DerkeImageDictionary derkeImageDic;



    private void Awake()
    {
        LoadAllAddressables();
        derkeImageDic = this;
    }

    void LoadAllAddressables()
    {
        // Addressablesの全アセットのアドレスを取得
        Addressables.LoadResourceLocationsAsync("sprite").Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var locations = handle.Result;
                foreach (var location in locations)
                {
                    string address = location.PrimaryKey;
                    LoadAssetAsync(address);
                }
            }
            else
            {
                Debug.LogError("Failed to load addressable locations.");
            }
        };
    }

    void LoadAssetAsync(string address)
    {
        // Addressごとに非同期でロード
        Addressables.LoadAssetAsync<Sprite>(address).Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // resources に追加（重複チェック付き）
                if (!resources.ContainsKey(address))
                {
                    resources[address] = op.Result;
                    Debug.Log($"Asset '{address}' loaded and added to resources.");
                }
                else
                {
                    Debug.Log($"Asset '{address}' already exists in resources.");
                }
            }
            else
            {
                Debug.LogError($"Failed to load asset: {address}");
            }
        };
    }

    public Sprite GetImageDerke(string name)
    {
        if (resources.ContainsKey(name))
        {
            return resources[name];
        }
        else
        {
            Debug.LogError($"Image '{name}' not found in resources.");
            return null;
        }
    }

}
