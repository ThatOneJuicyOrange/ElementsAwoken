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
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

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
            Player player = Main.player[projectile.owner];
            if (Main.netMode == 0)
            {
                if (!player.active || player.dead) projectile.Kill();
            }
            else
            {
                if (projectile.ai[0] == -1) projectile.Kill();
                else
                {
                    player = Main.player[(int)projectile.ai[0]];
                }
                if (!player.active || player.dead)
                {
                    projectile.ai[0] = FindActivePlayer();
                }
            }
            projectile.Center = player.Center + new Vector2(0, -300);
            if (!AnyAncients())
            {
                projectile.ai[1]++;
                if (projectile.ai[1] == 180)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/AncientMergeRise"));
                }
                if (projectile.ai[1] == 300)
                {
                    if (Main.autoPause == true)
                    {
                        Main.NewText("Using autopause huh? What a wimp", Color.LightCyan.R, Color.LightCyan.G, Color.LightCyan.B);
                    }
                    NPC aa = Main.npc[NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("AncientAmalgam"))];
                    aa.netUpdate = true;
                }
            }
        }
        private int FindActivePlayer()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player p = Main.player[i];
                if (p.active)
                {
                    return i;
                }
            }
            return -1;
        }
        private bool AnyAncients()
        {
            if (NPC.AnyNPCs(mod.NPCType("Izaris"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Kirvein"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Krecheus"))) return true;
            if (NPC.AnyNPCs(mod.NPCType("Xernon"))) return true;
            return false;
        }
    }
}