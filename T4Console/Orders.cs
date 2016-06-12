using System;

namespace T4Console
{
	/// <summary>
	/// 
	/// </summary>
	public class Orders 
	{
				///<summary>
		/// 
		///</summary>
		public int OrderID { get; set; }
				///<summary>
		/// 
		///</summary>
		public string CustomerID { get; set; }
				///<summary>
		/// 
		///</summary>
		public int? EmployeeID { get; set; }
				///<summary>
		/// 
		///</summary>
		public DateTime? OrderDate { get; set; }
				///<summary>
		/// 
		///</summary>
		public DateTime? RequiredDate { get; set; }
				///<summary>
		/// 
		///</summary>
		public DateTime? ShippedDate { get; set; }
				///<summary>
		/// 
		///</summary>
		public int? ShipVia { get; set; }
				///<summary>
		/// 
		///</summary>
		public decimal? Freight { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipName { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipAddress { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipCity { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipRegion { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipPostalCode { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ShipCountry { get; set; }
			}
}
