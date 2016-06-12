using System;

namespace T4Console
{
	/// <summary>
	/// 
	/// </summary>
	public class Products 
	{
				///<summary>
		/// 
		///</summary>
		public int ProductID { get; set; }
				///<summary>
		/// 
		///</summary>
		public string ProductName { get; set; }
				///<summary>
		/// 
		///</summary>
		public int? SupplierID { get; set; }
				///<summary>
		/// 
		///</summary>
		public int? CategoryID { get; set; }
				///<summary>
		/// 
		///</summary>
		public string QuantityPerUnit { get; set; }
				///<summary>
		/// 
		///</summary>
		public decimal? UnitPrice { get; set; }
				///<summary>
		/// 
		///</summary>
		public short? UnitsInStock { get; set; }
				///<summary>
		/// 
		///</summary>
		public short? UnitsOnOrder { get; set; }
				///<summary>
		/// 
		///</summary>
		public short? ReorderLevel { get; set; }
				///<summary>
		/// 
		///</summary>
		public bool Discontinued { get; set; }
			}
}
