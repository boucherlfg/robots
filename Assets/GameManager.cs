using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dungeon dungeon;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        dungeon.Generate();
        RenderManager.Draw(player, dungeon);
    }
    public void GetInput(string input)
    {
        player.line += input;
    }
    public void RemoveInput()
    {
        if (player.line.Length <= 0) return;
        player.line = player.line.Substring(0, player.line.Length - 1);
    }

    public void Run()
    {
        StartCoroutine(RunInstructions());
    }

    IEnumerator RunInstructions()
    {
        while (player.line.Length > 0)
        {
            player.NextOp(dungeon);
            player.line = player.line.Substring(1);
            RenderManager.Clear(player, dungeon);
            player.GetCoin(dungeon);
            player.NextLevel(dungeon);
            player.ShouldDie(dungeon);
            RenderManager.Draw(player, dungeon);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
