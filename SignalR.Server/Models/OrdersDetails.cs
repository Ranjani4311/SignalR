using System;
using System.Collections.Generic;

namespace SignalR.Server.Models
{
    public class InventoryDetails
    {
        // Keep this as public if other parts of your app bind directly; otherwise prefer private.
        public static readonly List<InventoryDetails> inventory = new List<InventoryDetails>();

        public InventoryDetails() { }

        public InventoryDetails(
            string ItemId, string ItemName, string Category, int Quantity, int UnitPrice, int LocationCode,
            string SupplierName, string ReorderLevel, DateTime LastUpdated, string Status)
        {
            this.ItemId = ItemId;
            this.ItemName = ItemName;
            this.Category = Category;
            this.Quantity = Quantity;
            this.UnitPrice = UnitPrice;
            this.LocationCode = LocationCode;
            this.SupplierName = SupplierName;
            this.ReorderLevel = ReorderLevel;
            this.LastUpdated = LastUpdated;
            this.Status = Status;
        }

        public static List<InventoryDetails> GetAllRecords()
        {
            int total = 1500;

            // Ensure we always have exactly `total` deterministic records.
            if (inventory.Count != total)
            {
                

                // Templates for variety (cycled deterministically)
                var itemNames = new[]
                {
                    "Wireless Mouse", "Mechanical Keyboard", "Office Chair", "LED Desk Lamp", "Notebook A5",
                    "USB-C Hub", "27\" Monitor", "Webcam HD", "Laptop Stand", "Gel Pen Set"
                };

                var categories = new[]
                {
                    "Electronics", "Electronics", "Furniture", "Accessories", "Stationery",
                    "Accessories", "Electronics", "Electronics", "Accessories", "Stationery"
                };

                var suppliers = new[]
                {
                    "TechSupply Co.", "KeyPro Distributors", "Comfort Works", "BrightLite Pvt Ltd", "PaperHouse",
                    "Connectix Imports", "VisionDisplays", "StreamCam Ltd", "ErgoWare", "InkFlow"
                };

                var reorderLevels = new[] { "20", "15", "10", "25", "50", "30", "12", "18", "22", "40" };
                var basePrices = new[] { 450, 2300, 5600, 850, 60, 1200, 18999, 3299, 999, 199 };

                var baseDate = new DateTime(2026, 2, 1);

                for (int n = 1; n <= total; n++)
                {
                    int t = (n - 1) % itemNames.Length;
                    string itemId = $"ITM{n:D4}";

                    // Quantity: vary but keep within safe positive range
                    int qty = 5 + ((n * 7) % 500);

                    // Unit price: base + small deterministic variance
                    int unitPrice = basePrices[t] + ((n * 3) % 120);

                    // LocationCode: block*100 + slot (block 1..15, slot 1..100) => 101..1600
                    int block = ((n - 1) / 100) + 1;
                    int slot = ((n - 1) % 100) + 1;
                    int locationCode = block * 100 + slot;

                    string supplier = suppliers[t];
                    string reorderLevelStr = reorderLevels[t];
                    int reorderLevel = int.Parse(reorderLevelStr);

                    DateTime lastUpdated = baseDate.AddDays((n * 3) % 540);
                    string status = qty <= reorderLevel ? "Low Stock" : "In Stock";

                    inventory.Add(new InventoryDetails(
                        itemId,
                        itemNames[t],
                        categories[t],
                        qty,
                        unitPrice,
                        locationCode,
                        supplier,
                        reorderLevelStr,
                        lastUpdated,
                        status
                    ));
                }
            }

            return inventory;
        }
  

        // Properties (match your definition)
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int LocationCode { get; set; }
        public string SupplierName { get; set; }
        public string ReorderLevel { get; set; } // string per your signature
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; }
    }
}



