using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

static class Program
{
    static void Main()
    {
        Player player = new Player();
        Dungeon dungeon = new Dungeon();
        dungeon.Generate();
        RenderManager.Draw(player, dungeon);

        do
        {
            
        }
        while (true);
    }
    
}