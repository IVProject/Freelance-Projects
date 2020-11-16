using System;

namespace Proekt.HighlightingManagement
{
    /// <summary>
    /// Adds the class to the context menu as an item. Class must be in one assembly with Highlighting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HighlightingItemAttribute : Attribute
    {
        public string menuItem;

        public HighlightingItemAttribute() { }

        public HighlightingItemAttribute(string item)
        {
            menuItem = item;
        }
    }
}
