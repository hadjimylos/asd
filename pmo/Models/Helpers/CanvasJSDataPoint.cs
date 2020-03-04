using System.Runtime.Serialization;

namespace ViewModels
{
    [DataContract]
    public class CanvasJSDataPoint
    {
		public CanvasJSDataPoint(string label, string category, decimal y, string color)
		{
			this.Label = label;
			this.Category = category;
			this.Y = y;
			this.Color = color;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "category")]
		public string Category;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public decimal Y;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "color")]
		public string Color;
	}
}
