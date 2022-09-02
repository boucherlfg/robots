
using System.Collections.Generic;
using Random = System.Random;
using DateTime = System.DateTime;
using System.Linq;
using Vector2 = UnityEngine.Vector2;
public static class Ext
{
    public static Vector2[] Directions => new Vector2[]{
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };
    public static void Wait(double millis)
    {
        var now = DateTime.Now;
        while ((DateTime.Now - now).TotalMilliseconds < millis) ;
    }
    public static Random rand = new Random();
    public static T Random<T>(this IEnumerable<T> list)
    {
        return list.ElementAt(rand.Next(0, list.Count()));
    }
    public static void Del(this List<Vector2> list, Vector2 elem)
    {
        var choice = list.First(x => x == elem);
        list.Remove(choice);
    }
    public static bool Has(this IEnumerable<Vector2> list, Vector2 elem)
    {
        foreach (var b in list)
        {
            if (b == elem) return true;
        }
        return false;
    }
}