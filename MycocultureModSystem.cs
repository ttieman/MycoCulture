using Mycoculture.BlockBehaviors;
using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace Mycoculture
{



    public class MycocultureModSystem : ModSystem
    {

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            api.Logger.Notification("✅ Mycoculture Loaded");
            base.Start(api);

            api.RegisterBlockBehaviorClass("mushroomtap", typeof(BlockBehaviorMushroomTap));
        }




        public override void StartServerSide(ICoreServerAPI api)
        {
            Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("mycoculture:hello"));
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("mycoculture:hello"));
        }

    }
}
