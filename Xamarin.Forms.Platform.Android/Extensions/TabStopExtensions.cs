using System.Collections.Generic;
using AView = Android.Views.View;

namespace Xamarin.Forms.Platform.Android
{
	internal static class TabStopExtensions
	{
		public static AView GetNextTabStop(this VisualElement ve, bool forwardDirection, IDictionary<int, List<VisualElement>> tabIndexes, int maxAttempts)
		{
			if (maxAttempts <= 0 || tabIndexes == null)
				return null;

			int tabIndex = ve.TabIndex;
			VisualElement nextElement = ve;
			AView nextControl = null;
			int attempt = 0;
			do
			{
				nextElement = nextElement?.FindNextElement(forwardDirection, tabIndexes, ref tabIndex);
				var renderer = nextElement?.GetRenderer();
				nextControl = (renderer as ITabStop)?.TabStop;
			} while (!(nextControl?.Focusable == true || ++attempt >= maxAttempts));

			return nextControl;
		}

		public static AView GetFirstTabStop(IDictionary<int, List<VisualElement>> tabIndexes, int maxAttempts)
		{
			if (maxAttempts <= 0 || tabIndexes == null)
				return null;

			VisualElement ve = TabIndexExtensions.GetFirstNonLayoutTabStop(tabIndexes);
			var renderer = ve?.GetRenderer();
			var control = (renderer as ITabStop)?.TabStop;
			if (control?.Focusable == true)
				return control;

			return ve?.GetNextTabStop(true, tabIndexes, maxAttempts);
		}
	}
}