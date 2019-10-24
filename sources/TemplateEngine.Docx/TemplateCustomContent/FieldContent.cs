using System;

namespace TemplateEngine.Docx
{
	[ContentItemName("Field")]
	public class FieldContent : HiddenContent<FieldContent>, IEquatable<FieldContent>
	{
        public FieldContent()
        {
            
        }

        public FieldContent(string name, object value)
        {
            Name = name;
            Value = value;
        }
   
	    public object Value { get; set; }

	    #region Equals

        public bool Equals(FieldContent other)
		{
			if (other == null) return false;

			return Name.Equals(other.Name) &&
			       (Value != null ? Value.Equals(other.Value) : other.Value == null);
		}

		public override bool Equals(IContentItem other)
		{
			if (!(other is FieldContent)) return false;

			return Equals((FieldContent)other);
		}

		public override int GetHashCode()
		{
			return new { Name, Value }.GetHashCode();
		}

        #endregion
    }
}
