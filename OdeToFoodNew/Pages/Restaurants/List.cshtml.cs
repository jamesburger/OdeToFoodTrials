using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFoodNew.Data;
using OdeToFoodNew.Core;

namespace OdeToFoodNew.Pages.Restaurants
{
    public class ListModel : PageModel
    {

        private readonly IConfiguration _configuration;

        private readonly IRestaurantData _restaurantData;

        public string Message { get; set; }

        public List<Restaurant> Restaurants { get; set; }

        //This attribute allows for model binding right out the gate. Usually it only works for POST operations so we have to add a qualifier
        //That allows the binding to work properly on GET functions as well
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration configuration, IRestaurantData restaurantData)
        {
            _configuration = configuration;
            _restaurantData = restaurantData;
        }
        //Making the OnGet parameter match the name of an input field in the markup will make the framework try to plug whatever input is given into this method
        //If nothing is passed it passes a null value
        public void OnGet()
        {
            Message = _configuration["Message"];
            Restaurants = _restaurantData.GetRestaurantsByName(SearchTerm).ToList();
        }
    }
}