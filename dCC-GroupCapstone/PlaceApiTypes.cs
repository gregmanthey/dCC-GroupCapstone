using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dCC_GroupCapstone
{
    public static class PlaceApiTypes
    {
        public static List<string> ReligiousPlaces = new List<string>() { "church","hindu_temple","synagogue","mosque" };
        public static List<string> Food = new List<string>() { "cafe","meal_deliver","meal_takeaway","restaurant","bar" };
        public static List<string> Shopping = new List<string>() { "clothing_store", "department_store", "electronic_store", "jewelry_store", "shoe_store", "shopping_mall" };
        public static List<string> TouristAttractions = new List<string>() { "art_gallery", "amusement_park", "aquarium", "spa", "stadium", "museum", "tourist_attraction" };
        public static List<string> NightLife = new List<string>() { "bowling_alley", "casino", "movie_theater", "night_club", "liquor_store" };
        public static List<string> Outdoors = new List<string>() { "park", "zoo" };
        public static List<string> Lodging = new List<string>() { "campground", "rv_park", "lodging" };
        public static List<string> Irrelevant = new List<string>() { "bus_station", "airport", "atm", "city_hall", "convenience_store", "embassy", "bicycle_shop", "storage", "courthouse", "dentist", "store", "electrician", "moving_company", "pharmacy", "pet_store", "gas_station", "hospital", "hardware_store", "subway_station", "supermarket", "train_station", "taxi_stand", "florist", "fire_station", "drugstore", "bank", "bakery", "beauty_salon", "book_store", "car_dealer", "car_rental", "car_repair", "home_goods_store", "secondary_school", "school", "transit_station", "vetrinary_care", "university", "travel_agency", "painter", "real_estate_agency", "roofing_contractor", "primary_school", "post_office", "library", "police", "plumber", "movie_rental", "doctor", "physiotherapist", "laundry", "parking", "light_rail_station", "locksmith", "local_government_office", "lawyer", "insurance_agency", "hair_care", "gym", "car_wash", "grocery", "supermarket", "furniture_store", "funeral_home" };

    }
}