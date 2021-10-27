using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    internal class TableColumn<T> where T : class
    {
        public string Title { get; }
        public int Width { get; }
        public Func<T, string> GetFormattedValue { get; }

        public TableColumn(string title, int width,
            Func<T, string> getFormattedValue)
        {
            Title = title;
            Width = width;
            GetFormattedValue = getFormattedValue;
        }

        public string PrintCell(T obj) =>
            string.Format("{0," + Width + "}", GetFormattedValue(obj));

        public string PrintTitle() =>
            string.Format("{0," + -Width + "}", Title);
    }
}
