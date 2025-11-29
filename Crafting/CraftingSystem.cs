using Farming.Concurrency;
using Farming.Storage;
using TipeUtils;

namespace Farming.Crafting
{
    public class CraftingSystem
    {
        public static Result Craft(
            IInventory target,
            RecipeData recipe,
            uint multiplier)
        {
            return Locking.LockAll([target], () =>
            {
                IInventory _target = target.Clone();

                foreach ((ItemData data, uint amount) in recipe.Input)
                {
                    ItemStack buffer = new(data, 0);
                    Result result = _target.GetItem(ref buffer, amount * multiplier);
                    if (result.IsError)
                        return Result.Error($"Crafting failed: {result.Message}");
                }

                foreach ((ItemData data, uint amount) in recipe.Output)
                {
                    ItemStack buffer = new(data, amount * multiplier);
                    Result result = _target.AddItem(ref buffer);
                    if (result.IsError)
                        return Result.Error($"Crafting failed: {result.Message}");
                }

                target.ReplaceWith(_target);
                return Result.Ok();
            });
        }
    }
}

