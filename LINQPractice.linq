<Query Kind="Expression">
  <Connection>
    <ID>c87ac79b-50bd-4bf0-a9c6-0a50e51489c7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sample of entity subset
//sample of entity navigation from child to parent on where
//reminder that code is C# and thus appropriate methods can be used .Equals()
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") && 
x.SupportRepIdEmployee.LastName.Equals("Peacock")
select new{
		Name = x.LastName + ", " + x.FirstName,
		City = x.City,
		State = x.State,
		Phone = x.Phone,
		Email = x.Email
}

//use of aggregates in queries
//.Count() count the number of instances of the collection references
//.Sum() totals a specific field or expression that you will likely want to use on a delegate
//		to indicate the collection instance attribute to be used
//.Average() averages a specific field or expression that you will likely want to use on a delegate
//		to indicate the collection instance attribute to be used

from x in Albums
orderby x.Title
where x.Tracks.Count() > 0 // this accounts for albums that might not have tracks listed
select new{
		Title = x.Title,
		NumberOfTracks = x.Tracks.Count(),
		AlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
		AverageTrackLengthInSecondsA = (x.Tracks.Average(y => y.Milliseconds)) / 1000,
		AverageTrackLengthInSecondsB = (x.Tracks.Average(y => y.Milliseconds / 1000)) 
}

from x in MediaTypes
select new {
		Name = x.Name,
		NumberOfTracks = x.Tracks.Max(y => y.MediaTypeId)
}
