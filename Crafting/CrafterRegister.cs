using Farming.Core;
using TipeUtils;

namespace Farming.Crafting
{
    public class CrafterRegistry : Registry<CrafterId, CrafterData>
    {
        public Result<CrafterData> Register(CrafterId id, string name, RecipeBook recipeBook)
            => Register(id, new(id, name, recipeBook));
    }
}
