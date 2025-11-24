using Farming.Concurrency;
using Farming.Storage;
using TipeUtils;

namespace Farming.Crafting
{
    public class CraftingSystem
    {
        public static Result Craft(
            Inventory target,
            RecipeData recipe,
            uint multiplier)
        {
            return Locking.LockAll([target], () =>
            {
                Inventory _target = target.Clone();

                foreach (ItemStack stack in recipe.Input)
                {
                    ItemStack buffer = new(stack.ItemData, 0);
                    Result result = _target.GetItem(ref buffer, stack.Amount * multiplier);
                    if (result.IsError)
                        return Result.Error($"Crafting failed: {result.Message}");
                }

                foreach (ItemStack stack in recipe.Output)
                {
                    ItemStack buffer = new(stack.ItemData, stack.Amount * multiplier);
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

