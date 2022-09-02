
using System.Linq;
using UnityEngine;
public class Player : MonoBehaviour
{
    public event System.Action LineChanged;
    private string _line = "";
    public string line
    {
        get => _line;
        set
        {
            _line = value;
            LineChanged?.Invoke();
        }
    }
    public Vector2 pos
    {
        get => transform.position;
        set => transform.position = value;
    }
    public Vector2 dir
    {
        get => new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        set => transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg);
    }
    [HideInInspector]
    public int points = 100;

    public void NextOp(Dungeon dungeon)
    {
        points--;
        var input = line.First().ToString();
        switch (input)
        {
            case "w":
                if (dungeon.ground.Has(pos + dir))
                {
                    pos += dir;
                }
                break;
            case "a":
                dir = new Vector2(-dir.y, dir.x);
                break;
            case "d":
                dir = -new Vector2(-dir.y, dir.x);
                break;
            case "x":
                var target = pos + dir;
                if (dungeon.trap.Has(target))
                {
                    dungeon.trap.Del(target);
                }
                break;
            default:
                return;
        }
    }
    public void NextLevel(Dungeon dungeon)
    {
        if (pos == dungeon.ground.Last())
        {
            dungeon.Level++;
            dungeon.Generate();
            pos = Vector2.zero;
        }
    }
    public void ShouldDie(Dungeon dungeon)
    {
        if (dungeon.trap.Has(pos) || points < 0)
        {
            line = "";
            RenderManager.instance.gameOverMenu.SetActive(true);
        }
    }
    public void GetCoin(Dungeon dungeon)
    {
        if (dungeon.coin.Has(pos))
        {
            dungeon.coin.Del(pos);
            points = Mathf.Min(points + 50, 100);
        }
    }
    public string GetRender()
    {
        if (dir == Vector2.down) return "v";
        else if (dir == Vector2.up) return "^";
        else if (dir == Vector2.left) return "<";
        else return ">";
    }

}