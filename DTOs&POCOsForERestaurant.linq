<Query Kind="Expression">
  <Connection>
    <ID>3b6aedc3-d537-4a03-8000-630c7fb4b2f7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from food in Items
	group food by new {food.MenuCategory.Description} into tempdataset
	select new{
		MenuCategoryDescription = tempdataset.Key.Description,
		FoodItems = from x in tempdataset
					select new {ItemID = x.ItemID,
								FoodDescription = x.Description,
								CurrentPrice = x.CurrentPrice,
								TimeServed = x.BillItems.Count()
								}
	}
				
from food in Items
	orderby food.MenuCategory.Description
	select new {
				MenuCategoryDescription = food.MenuCategory.Description,
				ItemID = food.ItemID,
				FoodDescription = food.Description,
				CurrentPrice = food.CurrentPrice,
				TimeServed = food.BillItems.Count()
	}