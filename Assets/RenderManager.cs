
using System.Linq;
using UnityEngine;
using TMPro;
class RenderManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public TMP_Text label;
    public GameObject wallPrefab, trapPrefab, coinPrefab, startPrefab, endPrefab, groundPrefab;
    //const string title = "RED >> GREEN";

    public static RenderManager instance;

    void Awake()
    {
        if (!instance) instance = this;
    }

    public static void Draw(Player player, Dungeon dungeon)
    {
        instance.label.text = $"{player.points} energy";

        foreach (var w in dungeon.wall)
        {
            Instantiate(instance.wallPrefab, w, Quaternion.identity, instance.transform);
        }

        var start = dungeon.ground.First();
        Instantiate(instance.startPrefab, start, Quaternion.identity, instance.transform);

        var end = dungeon.ground.Last();
        Instantiate(instance.endPrefab, end, Quaternion.identity, instance.transform);

        foreach (var g in dungeon.ground)
        {
            Instantiate(instance.groundPrefab, g, Quaternion.identity, instance.transform);
        }
        foreach (var c in dungeon.coin)
        {
            Instantiate(instance.coinPrefab, c, Quaternion.identity, instance.transform);
        }

        foreach (var t in dungeon.trap)
        {

            Instantiate(instance.trapPrefab, t, Quaternion.identity, instance.transform);
        }
        
    }
    public static void Clear(Player player, Dungeon dungeon)
    {
        foreach (Transform item in instance.transform)
        {
            Destroy(item.gameObject);
        }
    }
}