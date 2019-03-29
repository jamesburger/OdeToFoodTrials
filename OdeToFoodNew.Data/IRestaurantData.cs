using OdeToFoodNew.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFoodNew.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetRestaurantById(int it);
        int Commit();
        Restaurant UpdateRestaurant(Restaurant updatedRestaurant);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly IEnumerable<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Sidetracks", Location = "Ypsilanti", Cuisine = CuisineType.None},
                new Restaurant {Id = 2, Name = "Corner Brewery", Location = "Ypsilanti", Cuisine = CuisineType.None},
                new Restaurant {Id = 3, Name = "Tower Inn", Location = "Ypsilanti", Cuisine = CuisineType.Italian},
            };
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(j => j.Id == id);
        }

        public Restaurant UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(j => j.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
