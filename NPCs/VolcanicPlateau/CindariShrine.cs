using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class CindariShrine : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 50;

            npc.aiStyle = -1;

            npc.lifeMax = 2;
            npc.alpha = 255;

            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.friendly = true;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            if (npc.ai[0] == 0 && !MyWorld.spokenToCindari)
            {
                for (int k = 0; k < Main.maxItems; k++)
                {
                    Item item = Main.item[k];
                    if (item.type == ModContent.ItemType<Items.BossSummons.InfernaceSummon>() && Vector2.Distance(item.position, npc.Center) < 60)
                    {
                        npc.ai[2]++;
                        if (npc.ai[2] > 30)
                        {
                            npc.ai[0] = 1;
                            for (int l = 0; l < 20; l++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(item.position, item.width, item.height, 31)];
                                dust.noGravity = true;
                                dust.scale = 0.2f;
                                dust.velocity *= 0.1f;
                                dust.velocity.Y = Main.rand.NextFloat(-3, -7);
                                dust.fadeIn = 2f;
                            }
                            if (item.stack == 1) item.TurnToAir();
                            else item.stack--;
                        }
                    }
                }
            }
            else
            {
                npc.ai[1]++;
                for (int l = 0; l < 5; l++)
                {
                    Vector2 position = npc.Center + Main.rand.NextVector2Circular(npc.width * 0.5f, npc.height * 0.5f);
                    Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                    dust.velocity.Y = Main.rand.NextFloat(-6, -1);
                    dust.noGravity = true;
                    dust.fadeIn = 1.1f;
                    dust = Dust.NewDustPerfect(position, 31, Vector2.Zero);
                    dust.velocity.Y = Main.rand.NextFloat(-10, -5);
                    dust.noGravity = true;
                    dust.fadeIn = 0.9f;
                }
                int between = 400;
                npc.ai[1]++;
                if (npc.ai[1] == between)
                {
                    int text = CombatText.NewText(npc.getRect(), Color.OrangeRed, "My ring? How did you find it?", false, false);
                    Main.combatText[text].lifeTime = 200;
                }
                else if (npc.ai[1] == between * 2)
                {
                    int text = CombatText.NewText(npc.getRect(), Color.OrangeRed, "My thanks for allowing me presence in this world, even if only for a short while.", false, false);
                    Main.combatText[text].lifeTime = 200;
                }
                else if (npc.ai[1] == between * 3)
                {
                    int text = CombatText.NewText(npc.getRect(), Color.OrangeRed, "I can show to you the history of the creatures living within this land.", false, false);
                    Main.combatText[text].lifeTime = 200;
                }
                else if (npc.ai[1] == between * 4)
                {
                    int text = CombatText.NewText(npc.getRect(), Color.OrangeRed, "But only if you vow to end your slaughter.", false, false);
                    Main.combatText[text].lifeTime = 200;
                }
                else if (npc.ai[1] == between * 5)
                {
                    int text = CombatText.NewText(npc.getRect(), Color.OrangeRed, "Take this tome, it contains all the information for the creatures in the plateau.", false, false);
                    Main.combatText[text].lifeTime = 200;
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Other.LorekeepersTome>());
                }
                else if (npc.ai[1] > between * 6)
                {
                    npc.active = false;
                    MyWorld.spokenToCindari = true;
                }
            }
        }
    }
}