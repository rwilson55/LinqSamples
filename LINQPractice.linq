<Query Kind="Statements">
  <Connection>
    <ID>c87ac79b-50bd-4bf0-a9c6-0a50e51489c7</ID>
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

//when you need to use multiple steps to solve a problem,
//switch your language choice to either Statement(s) or Program

//the results of each query will now be saved in a variable
//the variable can then be used in future queries

var maxcount = (from x in MediaTypes
	select x.Tracks.Count()).Max();
	
//to display the contents of a variable in LinqPad
// you use the method .Dump()
maxcount.Dump();

//use a value in a preceding created variable
var popularMediaType = from x in MediaTypes
						where x.Tracks.Count() == maxcount
						select new{
									Type = x.Name,
									TCount = x.Tracks.Count()
						};
popularMediaType.Dump();

//Can this set of statements be done as one complete query?
//the answer is "possibly" and, in this case, "yes"
//in this example maxcount could be exchanged for the query that 
// 	actually created the value in the first place
//	and this substituted query is a subquery

var popularMediaTypeSubQuery = from x in MediaTypes
						where x.Tracks.Count() == (from y in MediaTypes
													select y.Tracks.Count()).Max()
						select new{
									Type = x.Name,
									TCount = x.Tracks.Count()
						};
popularMediaTypeSubQuery.Dump();

//using the method syntax to determine the count value for the where expression
//this demonstrates that queries can be constructed using both query syntax and method syntax
var popularMediaTypeSubMethod = from x in MediaTypes
						where x.Tracks.Count() == MediaTypes.Select(mt => mt.Tracks.Count()).Max()
						select new{
									Type = x.Name,
									TCount = x.Tracks.Count()
						};
popularMediaTypeSubMethod.Dump();