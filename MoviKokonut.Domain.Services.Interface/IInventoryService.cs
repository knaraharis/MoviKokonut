
using MoviKokonut.Domain;
namespace MoviKokonut.Domain.Services.Interface
{
    public interface IInventoryService
    {
       
        bool InventoryAndPriceCheck(ShoppingCart.ShoppingCart shoppingCart);
    }
}