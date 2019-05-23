using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossSummons.SummonProj
{
    public class AncientSpawn : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;

            projectile.timeLeft = 20;
            projectile.tileCollide = false;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);

            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("Izaris"));
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("Kirvein"));
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("Krecheus"));
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("Xernon"));

            if (!MyWorld.downedAncients)
            {
                if (MyWorld.ancientSummons == 0)
                {
                    Main.NewText("I've waited centuries for this!", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 1)
                {
                    Main.NewText("Back for more?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 2)
                {
                    Main.NewText("I could do this all day!", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 3)
                {
                    Main.NewText("You should have gone for the head...", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 4)
                {
                    Main.NewText("Really? Still trying?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 5)
                {
                    Main.NewText("Give up already.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons > 5 && MyWorld.ancientSummons < 10)
                {
                    Main.NewText("Are you seriously gonna keep dying?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 10)
                {
                    Main.NewText("As much fun slaying you is, there are better things to do with both our lives.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 20)
                {
                    Main.NewText("Lunatic.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 100)
                {
                    Main.NewText("I just dont think you are cut out to do this... Go find some other hobby.", new Color(3, 188, 127));
                }
            }
            else
            {
                if (MyWorld.ancientKills > 0)
                {
                    Main.NewText("You bring me back to this awful land... Why?", new Color(3, 188, 127));
                }
            }
            Projectile.NewProjectile(player.Center.X, player.Center.Y - 300, 0f, 0f, mod.ProjectileType("ShardBase"), 0, 0f, player.whoAmI);

            MyWorld.ancientSummons++;

            projectile.Kill();
        }
    }
}