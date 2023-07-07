using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;

namespace PetShop.Services
{
    public class SelectListService  : ISelectListService
    {
        //Used to fill in select inputs in add and edit pages
        public List<SelectListItem> GetCategoryList(List<Category> categories)
        {
            List<SelectListItem> selectList = BuildCategoryList(categories);
            return selectList;
        }

        //Used to fill in select inputs for catalog and admin category filter dropdowns
        //includes an All option for the list
        public SelectList GetCategoryListForFilter(List<Category> categories)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var item = new SelectListItem() { Value = "All", Text = "All" };
            selectListItems.Insert(0, item);
            selectListItems.AddRange(BuildCategoryList(categories));

            SelectList selectList = new SelectList(selectListItems, "Value", "Text");

            return selectList;
        }

        //Builds a selectList from the category names in db 
        private static List<SelectListItem> BuildCategoryList(List<Category> categories)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                var itemNext = new SelectListItem() { Value = category.Id.ToString(), Text = category.Name };
                selectList.Add(itemNext);
            }

            return selectList;
        }
    }

}
