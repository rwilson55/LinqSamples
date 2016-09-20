<Query Kind="Program">
  <Connection>
    <ID>3b6aedc3-d537-4a03-8000-630c7fb4b2f7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	//a list of bill counts for all waiters
	//This query will create a flat dataset
	//The columns are native data types (ie int, string,....)
	//One is not concerned with repeated data in a column
	//Instead of using an anonymous datatype (new[....])
	//we wish to use a defined class definition
	var BestWaiter = from x in Waiters
				select new WaiterBillCounts{
					Name = x.FirstName + " " + x.LastName,
					TCount = x.Bills.Count()
				};
BestWaiter.Dump();

var paramMonth = 4;
var paramYear = 2014;
var waiterbills = from x in Waiters
					where x.LastName.Contains("k")
					orderby x.LastName, x.FirstName
					select new WaiterBills{
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0
										&& y.BillDate.Month == DateTime.Today.Month - paramMonth
										&& y.BillDate.Year == paramYear
										select new BillItemSummary{
											BillID = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
													}
										).ToList()
								};
waiterbills.Dump();
}

// Define other methods and classes here
//An example of a POCO class
public class WaiterBillCounts
{
	//whatever receiving field on your query in your select 
	//appears as a property in this class
	public string Name{get; set;}
	public int TCount{get; set;}
}

public class BillItemSummary
{
	public int BillID{get;set;}
	public DateTime BillDate{get;set;}
	public int? TableID{get;set;}
	public decimal Total{get;set;}
}

//An example of a DTO class (structured)

public class WaiterBills
{
	public string Name{get;set;}
	public int TotalBillCount{get;set;}
	public List<BillItemSummary> BillInfo{get;set;}
}