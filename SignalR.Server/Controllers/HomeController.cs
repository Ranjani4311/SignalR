using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SignalR.Server.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using System.Collections;

namespace SignalR.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Fetch data source
        public IActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = InventoryDetails.GetAllRecords().ToList();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
                {
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
                }
            int count = DataSource.Cast<InventoryDetails>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        //Add the record

        public ActionResult Update([FromBody] CRUDModel<InventoryDetails> value)
        {
            var ord = value.value;
            InventoryDetails val = InventoryDetails.GetAllRecords().Where(or => or.ItemId == ord.ItemId).FirstOrDefault();
            val.ItemId = ord.ItemId;
            val.ItemName = ord.ItemName;
            val.Category = ord.Category;
            val.Quantity = ord.Quantity;
            val.UnitPrice = ord.UnitPrice;
            val.LocationCode = ord.LocationCode;
            val.SupplierName = ord.SupplierName;
            val.ReorderLevel = ord.ReorderLevel;
            val.LastUpdated = ord.LastUpdated;
            val.Status = ord.Status;
           

            return Json(value.value);
        }
        //insert the record
        public ActionResult Insert([FromBody] CRUDModel<InventoryDetails> value)
        {

            InventoryDetails.GetAllRecords().Insert(0, value.value);
            return Json(value.value);
        }
        //Delete the record
        public ActionResult Delete([FromBody] CRUDModel<InventoryDetails> value)
        {

            // Find the record with the matching ItemId
            InventoryDetails.GetAllRecords().Remove(InventoryDetails.GetAllRecords().Where(or => or.ItemId == value.key.ToString()).FirstOrDefault());
            return Json(value);

        }
    }

}
public class Data
{
    public bool requiresCounts { get; set; }
    public int skip { get; set; }
    public int take { get; set; }
    public List<Wheres> where { get; set; }
}

public class CRUDModel<T> where T : class
{
    public string action { get; set; }
    public string table { get; set; }
    public string keyColumn { get; set; }
    public object key { get; set; }
    public T value { get; set; }
    public List<T> added { get; set; }
    public List<T> changed { get; set; }
    public List<T> deleted { get; set; }
    public IDictionary<string, object> @params { get; set; }
}
public class Wheres
{
    public List<Predicates> predicates { get; set; }
    public string field { get; set; }
    public bool ignoreCase { get; set; }
    public bool isComplex { get; set; }
    public string value { get; set; }
    public string Operator { get; set; }
}
public class Predicates
{
    public string value { get; set; }
    public string field { get; set; }
    public bool isComplex { get; set; }
    public bool ignoreCase { get; set; }
    public string Operator { get; set; }
}