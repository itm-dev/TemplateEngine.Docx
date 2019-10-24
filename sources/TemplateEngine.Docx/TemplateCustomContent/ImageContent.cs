using System;
using System.Linq;

namespace TemplateEngine.Docx
{
	[ContentItemName("Image")]
	public class ImageContent : HiddenContent<ImageContent>, IEquatable<ImageContent>
    {
        public ImageContent()
        {
            
        }

        public ImageContent(string name, byte[] binary)
        {
            Name = name;
            Binary = binary;
        }

        public ImageContent(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
        }

        public byte[] Binary { get; set; }
        public string FilePath { get; set; }

        #region Equals

        public bool Equals(ImageContent other)
		{
			if (other == null) return false;

			return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase) &&
			       (Binary != null ? Binary.SequenceEqual(other.Binary) : other.Binary == null) &&
			       (FilePath != null ? FilePath.Equals(other.FilePath) : other.FilePath == null);
		}

		public override bool Equals(IContentItem other)
		{
			if (!(other is ImageContent)) return false;

			return Equals((ImageContent)other);
		}

		public override int GetHashCode()
		{
			return new {Name, Binary, FilePath}.GetHashCode();
		}

		#endregion
	}
}
