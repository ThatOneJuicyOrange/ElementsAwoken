using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan.Minions
{
    public class BarrenSoul : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 44;

            npc.damage = 0;
            npc.defense = 20;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;

            animationType = 5;
            npc.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barren Soul");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 1f, 0.2f, 0.55f);
            npc.ai[2]++;
            int minAlpha = 50;
            int projDamage = Main.expertMode ? 160 : 120;
            if (MyWorld.awakenedMode) projDamage = 140;
            if (npc.ai[2] < 60)
            {
                if (npc.alpha > minAlpha) npc.alpha -= (255 - minAlpha) / 60;
            }
            else if (npc.ai[2] == 150)
            {
                if (MyWorld.awakenedMode)
                {
                    float Speed = 10f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile beam = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("BarrenBeam"), projDamage, 0f, 0)];
                    beam.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                else
                {
                    npc.ai[0] = P.Center.X;
                    npc.ai[1] = P.Center.Y;
                }
            }
            else if (npc.ai[2] == 180)
            {
                if (!MyWorld.awakenedMode)
                {
                    float Speed = 10f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - npc.ai[1], npc.Center.X - npc.ai[0]);
                    Projectile blast = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ExtinctionBlast"), projDamage, 0f, 0)];
                    blast.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
            }
            else if (npc.ai[2] > 180)
            {
                npc.alpha += (255 - minAlpha) / 60;
                if (npc.alpha >= 255) npc.active = false;
            }
            npc.rotation *= 0f;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}