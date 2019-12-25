using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class PutridOre : ModItem
    {
        public int soundCD = 0;

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = 7;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Ore");
            Tooltip.SetDefault("It smells awful\nCreated by throwing iron ore and sun fragments into a 'Putrifier'");
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            soundCD--;
            if (item.lavaWet)
            {
                Item conPyroplasm = null;
                for (int k = 0; k < Main.item.Length; k++)
                {
                    Item other = Main.item[k];
                    if (Vector2.Distance(item.Center, other.Center) <= 50 && other.active)
                    {
                        if (other.type == mod.ItemType("ConcerntratedPyroplasm"))
                        {
                            conPyroplasm = other;
                        }
                    }
                }
                if (conPyroplasm != null && conPyroplasm.active)
                {
                    if (conPyroplasm.stack > 0 && item.stack > 0)
                    {
                        if (conPyroplasm.stack > 1)
                        {
                            conPyroplasm.stack--;
                        }
                        else
                        {
                            conPyroplasm.active = false;
                        }
                        if (item.stack > 1)
                        {
                            item.stack--;
                        }
                        else
                        {
                            item.active = false;
                        }
                        Projectile.NewProjectile(item.Center.X, item.Center.Y, Main.rand.NextFloat(-2f, 2f), -4f, mod.ProjectileType("BlightfireSpawner"), 0, 0, Main.myPlayer, 0f, 0f);
                        if (soundCD <= 0)
                        {
                            Main.PlaySound(SoundID.Item103, item.Center);
                            soundCD = 20;
                        }
                    }
                }
            }
        }
    }
}
