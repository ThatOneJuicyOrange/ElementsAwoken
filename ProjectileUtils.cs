using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementsAwoken
{
    public static class ProjectileUtils
    {
        public static int CountProjectiles(int Type)
        {
            int num = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    num++;
                }
            }
            return num;
        }
        public static int CountProjectiles(int type,int ownerID)
        {
            int num = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type && Main.projectile[i].owner == ownerID)
                {
                    num++;
                }
            }
            return num;
        }
        public static bool HasLeastTimeleft(int whoAmI)
        {
            Projectile thisProj = Main.projectile[whoAmI];
            bool notLeast = false;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == thisProj.type)
                {
                    if (thisProj.timeLeft > Main.projectile[i].timeLeft)
                    {
                        notLeast = true;
                    }
                }
            }
            return !notLeast; // returns whether it has the least amount of time left instead of not the least
        }
    }
}
