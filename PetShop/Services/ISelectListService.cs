using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;

namespace PetShop.Services
{
    public interface ISelectListService
    {
        List<SelectListItem> GetCategoryList(List<Category> categories);
        SelectList GetCategoryListForFilter(List<Category> categories);
    }
}
