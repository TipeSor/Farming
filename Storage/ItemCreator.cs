using TipeUtils;

namespace Farming.Storage
{
    public class ItemCreator()
    {
        private ItemId? _id;
        private readonly List<IItemAttribute> _attributes = [];

        public ItemCreator SetId(ItemId id)
        {
            _id ??= id;
            return this;
        }

        public ItemCreator AddAttribute(IItemAttribute attribute)
        {
            _attributes.Add(attribute);
            return this;
        }

        public Result<ItemData> Create()
        {
            if (_id == null)
                return Result<ItemData>.Error("Id is null.");

            return Result<ItemData>.Ok(new ItemData(_id, _attributes));
        }

        public Result<ItemData> Register(ItemRegistry registry)
        {
            Result<ItemData> result = Create();
            if (result.IsError)
                return result;

            // null id handled by create
            return registry.Register(_id!, result.Value);
        }
    }
}
