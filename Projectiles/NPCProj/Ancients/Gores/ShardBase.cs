using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Gores
{
    public class ShardBase : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.timeLeft = 100000;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            projectile.Center = Main.player[Main.myPlayer].Center + new Vector2(0, - 300);

            if (!Main.player[Main.myPlayer].active || Main.player[Main.myPlayer].dead) projectile.Kill();

            if (!NPC.AnyNPCs(mod.NPCType("Izaris")) &&
            !NPC.AnyNPCs(mod.NPCType("Kirvein")) &&
            !NPC.AnyNPCs(mod.NPCType("Krecheus")) &&
            !NPC.AnyNPCs(mod.NPCType("Xernon")))
            {
                projectile.localAI[0]++;
                if (projectile.localAI[0] == 180)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/AncientMergeRise"));
                }
                if (projectile.localAI[0] == 300)
                {
                    NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("AncientAmalgam"));
                }
            }
        }      
    }
}