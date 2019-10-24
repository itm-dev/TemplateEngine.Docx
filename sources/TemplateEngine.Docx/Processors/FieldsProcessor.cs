using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Wordprocessing;
using TemplateEngine.Docx.Errors;

namespace TemplateEngine.Docx.Processors
{
	internal class FieldsProcessor:IProcessor
	{
		private bool _isNeedToRemoveContentControls;

		public IProcessor SetRemoveContentControls(bool isNeedToRemove)
		{
			_isNeedToRemoveContentControls = isNeedToRemove;
			return this;
		}

		public ProcessResult FillContent(XElement contentControl, IEnumerable<IContentItem> items)
		{
			var processResult = ProcessResult.NotHandledResult;

			foreach (var contentItem in items)
			{
				processResult.Merge(FillContent(contentControl, contentItem));
			}

			if (processResult.Success && _isNeedToRemoveContentControls)
				contentControl.RemoveContentControl();

			return processResult;
		}

		public ProcessResult FillContent(XElement contentControl, IContentItem item)
		{
			var processResult = ProcessResult.NotHandledResult; 
			if (!(item is FieldContent))
			{
				processResult = ProcessResult.NotHandledResult;
				return processResult;
			}

			var field = item as FieldContent;

			// If there isn't a field with that name, add an error to the error string,
			// and continue with next field.
			if (contentControl == null)
			{
                processResult.AddError(new ContentControlNotFoundError(field));
                return processResult;
			}

		    string stdTagName = contentControl.SdtTagName(false);

		    if (stdTagName.Contains(":"))
		    {
		        try
		        {
		            var newValue = field.IsHidden
		                ? ""
		                : string.Format("{0:" + stdTagName.Split(':')[1] + "}", field.Value);
		            contentControl.ReplaceContentControlWithNewValue(newValue);
		        }
		        catch (Exception e)
		        {
		            processResult.AddError(new CustomContentItemError(field, e.Message));
		        }
            }
            else
		    {
		        var newValue = field.IsHidden ? "" : string.Format("{0}", field.Value);
		        contentControl.ReplaceContentControlWithNewValue(newValue);
		    }

			processResult.AddItemToHandled(item);

			return processResult;
		}
	}
}
