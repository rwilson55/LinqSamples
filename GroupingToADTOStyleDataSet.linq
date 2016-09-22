<Query Kind="Expression">
  <Connection>
    <ID>688568f5-1732-48f7-826a-12d784262a24</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//this is a multi-column group
//grouping data placed in a local temp data set for further processing 
//.Key allows you to have access to the value(s) in your group key(s)
//if you have multiple group columns they must be in an anonymous datatype
//to create a DTO type collection you can use .ToList() on the temp data set
//you can have a custom anonymous data collection by using a nested query

//step A
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} 
	
//step B DTO type data set
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategoryID = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItem = tempdataset.ToList()
				}
				
//step C DTO custom style data set
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategoryID = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItem = from x in tempdataset
							select new {
										ItemID = x.ItemID,
										FoodDescription = x.Description,
										TimesServed = x.BillItems.Count()
							}
				}