using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFoodNew.Data;
using OdeToFoodNew.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdeToFoodNew.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData data, IHtmlHelper helper)
        {
            _restaurantData = data;
            _htmlHelper = helper;
        }
        public IActionResult OnGet(int id)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>(); 
            Restaurant = _restaurantData.GetRestaurantById(id);
            if(Restaurant == null)
            {
                return RedirectToPage("./PageNotFound");
            }
            return Page(); 
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Restaurant = _restaurantData.UpdateRestaurant(Restaurant);
                //Need to use a new object so property matches what the framework is expecting for the relevant 
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id});
            }
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }
    }
}