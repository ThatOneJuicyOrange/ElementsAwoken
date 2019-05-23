using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Aqueous
{
    public class AqueousMinion1 : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            npc.npcSlots = 0f;
            npc.damage = 19;
            npc.width = 26; //324
            npc.height = 20; //216
            npc.defense = 30;
            npc.lifeMax = 5000;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[24] = true;
            npc.noTileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous Minion");
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            /* npc.position.X = Main.player[npc.target].position.X + 350;
             npc.position.Y = Main.player[npc.target].position.Y + -350;*/
            npc.TargetClosest(true);
            if (npc.velocity.X >= 0f)
            {
                npc.spriteDirection = 1;
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                npc.rotation = direction.ToRotation();
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                npc.rotation = direction.ToRotation() - 3.14f;
            }
            Player P = Main.player[npc.target];
            if (!P.active)
            {
                npc.active = false;
            }
            Vector2 offset = new Vector2(500, 0);;
            npc.ai[0] += 0.01f;
            npc.Center = P.Center + offset.RotatedBy(npc.ai[0] + npc.ai[1] * (Math.PI * 2 / 8));


            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            if (Main.netMode != 1 && shootTimer == 0f)
            {
                float Speed = 3.5f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 30;  //projectile damage
                int type = mod.ProjectileType("Waterball");  //put your projectile
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                shootTimer = 150f + Main.rand.Next(-30, 50);
            }
            if (!NPC.AnyNPCs(mod.NPCType("Aqueous")))
            {
                npc.active = false;
            }
        }
    }
}