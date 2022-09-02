using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Dungeon : MonoBehaviour
{
    int levelUntilCoin = 2;
    [HideInInspector]
    public int size = 10;
    int level = 0;
    [HideInInspector]
    public int Level
    {
        get => level;
        set
        {
            int dif = value - level;
            levelUntilCoin -= dif;
            level = value;
        }
    }
    [HideInInspector]
    public List<Vector2> ground, wall, coin, trap;
    public Dungeon()
    {
        ground = new List<Vector2>();
        wall = new List<Vector2>();
        coin = new List<Vector2>();
        trap = new List<Vector2>();
    }
    public int Generate()
    {
        int size = this.size + Level;
        int i = 0;
        ground = new List<Vector2>();
        wall = new List<Vector2>();
        coin = new List<Vector2>();
        trap = new List<Vector2>();

        //generate walkable area
        {
            var pos = Vector2.zero;
            ground.Add(pos);

            while (ground.Count < size)
            {
                i++;
                pos += Ext.Directions.Random();
                if (ground.Has(pos)) continue;
                ground.Add(pos);
            }
        }

        //generate walls
        foreach (var pos in ground)
        {
            foreach (var dir in Ext.Directions)
            {
                var target = pos + dir;
                if (ground.Has(target)) continue;
                wall.Add(target);
            }
        }

        //generate coins
        if (levelUntilCoin <= 0)
        {
            levelUntilCoin = Ext.rand.Next(2, 5);
            while (coin.Count < 1)
            {
                var pos = ground.Random();
                if (coin.Has(pos)) continue;
                if (ground.First() == pos || ground.Last() == pos) continue;
                coin.Add(pos);
            }
        }

        //generate traps
        while (trap.Count < Level / 5)
        {
            var pos = ground.Random();
            if (trap.Has(pos)) continue;
            if (coin.Has(pos)) continue;
            if (ground.First() == pos || ground.Last() == pos) continue;
            trap.Add(pos);
        }
        return i;
    }
    public void Restart(Player player)
    {
        level = 0;
        Generate();
        player.points = 100;
        player.pos = Vector2.zero;
        player.dir = Vector2.down;
        RenderManager.Draw(player, this);
    }
}
