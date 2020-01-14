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
    public class BarrenOrbital : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/Minions/BarrenSoul"; } }

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

            NPC parent = Main.npc[(int)npc.ai[1]];
            npc.ai[0] += 3f;
            int distance = 125;
            double rad = npc.ai[0] * (Math.PI / 180); // angle to radians
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
            if (!parent.active) npc.active = false;

            npc.ai[2]--;
            if (npc.ai[2] <= 0)
            {
                int projectileBaseDamage = 90;
                int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.8f);

                float Speed = 10f;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);

                Projectile blast = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ExtinctionBlast"), projDamage, 0f, 0)];
                blast.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;

                npc.ai[2] = Main.rand.Next(600, 800);
                if (Main.expertMode) npc.ai[2] -= 100;
                if (MyWorld.awakenedMode) npc.ai[2] -= 100;
            }
            npc.rotation *= 0f;
        }
    }
}