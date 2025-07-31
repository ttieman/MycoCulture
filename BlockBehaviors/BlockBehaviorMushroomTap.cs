
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;



namespace Mycoculture.BlockBehaviors
{
    //We are adding additional block behaviors here so we inherit from the BlockBehavior Class
    internal class BlockBehaviorMushroomTap : BlockBehavior
    {
        //base constructor for the class
        public BlockBehaviorMushroomTap(Block block) : base(block)
        {
            
        }


        // This is where we read json values from the blocktype, we are just initializing with the base properties
        public override void Initialize(JsonObject properties) {

            //pass base properties into base method for instantiation
            base.Initialize(properties);
            return;
        }


        //This returns the Interaction Help during player block selection event
        public override WorldInteraction[] GetPlacedBlockInteractionHelp(IWorldAccessor world, BlockSelection selection, IPlayer forPlayer, ref EnumHandling handling)
        {
            // get existing interactions
            var baseInteractions = base.GetPlacedBlockInteractionHelp(world, selection, forPlayer, ref handling);

            // Define your custom interaction
            //This should be a message with a hotkey for right click + ctrl key 
            var myInteraction = new WorldInteraction()
            {
                ActionLangCode = "mycoculture:blockhelp-mushroomtap",
                MouseButton = EnumMouseButton.Right,
                HotKeyCode = "ctrl"
            };

            // create a new List of WorldInteractions
            var all = new List<WorldInteraction>();

            //check for base interactions to prevent null insertion
            if (baseInteractions != null)
            {
                all.AddRange(baseInteractions);
            }

            //add custom interations
            all.Add(myInteraction);

            // Prevent blocking other interactions
            handling = EnumHandling.PassThrough;


            //return all interactions as an array
            return all.ToArray();
        }


        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
        {
            //return the control scheme of the player entity 
            var controls = byPlayer?.WorldData?.EntityControls;
            // return if the player has a bare hand or not by checking the active hotbar slot
            var barehand = byPlayer?.InventoryManager?.ActiveHotbarSlot?.Empty;
            //return the server world
            var serverWorld = world as IServerWorldAccessor;


            //check for controls, ctrl key, and if the players hand is empty
            if (controls != null && controls.CtrlKey && barehand == true)
            {
                //if so we log the players name in the server console
                serverWorld?.Logger.Notification($"{byPlayer.PlayerName} tapped mushroom at {blockSel.Position}");

                //prevent all default interactions 
                handling = EnumHandling.PreventDefault;

                return true;
            }

            //return all other block interactions
                return base.OnBlockInteractStart(world, byPlayer, blockSel, ref handling);
        }
    }
}
