using Newtonsoft.Json;

namespace VirtoCommerce.Storefront.Model.Cart
{
    partial class AddCartItem
    {
        public string ConfiguredProductId { get; set; }

        [JsonIgnore]
        public string ConfiguredGroupId { get; set; }
    }
}
