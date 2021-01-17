using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class InvigoratedAura : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 60;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invigorated Aura");
        }

        public override void AI()
        {
            if (Main.rand.NextBool(5))
            {
                Dust sparkle = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.InvigorationDust>())];
                sparkle.velocity *= 0.6f;
                sparkle.scale *= 0.6f;
                sparkle.noGravity = true;
            }
            Dust water = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 33)];
            water.velocity *= 0.6f;
            water.scale *= 1f;
            water.noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
            if (target.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().voidBroken && target.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().counterpart != 0 && MyWorld.plateauWeather != 3)
            {
                float lifeRatio = (float)target.life / (float)target.lifeMax;
                NPC cured = Main.npc[NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, target.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().counterpart,target.whoAmI)];
                cured.life = (int)((float)cured.lifeMax * lifeRatio);
                cured.Center = target.Center;
                target.active = false;
                if (QuestSystem.IsQuestActive("CureVoidbroken"))
                {
                    SpecialQuest quest = (SpecialQuest)QuestSystem.FindQuest("CureVoidbroken");
                    if (quest != null) quest.thingsDone++;
                }
            }
        }
    }
}