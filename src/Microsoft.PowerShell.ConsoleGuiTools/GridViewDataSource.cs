// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using NStack;

using Terminal.Gui;

namespace OutGridView.Cmdlet;

internal sealed class GridViewDataSource : IListDataSource, IDisposable
{
    public List<GridViewRow> GridViewRowList { get; set; }

    public int Count => GridViewRowList.Count;
    
    public int Length => GridViewRowList.Count;
    
    public bool SuspendCollectionChangedEvent { get; set; }
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public GridViewDataSource(List<GridViewRow> itemList)
    {
        GridViewRowList = itemList;
    }

    public void Render(ListView container, bool selected, int item, int col, int line, int width, int height)
    {
        container.Move(col, line);
        RenderUstr(Application.Driver, GridViewRowList[item].DisplayString, col, line, width);
    }

    public bool IsMarked(int item) => GridViewRowList[item].IsMarked;

    public void SetMark(int item, bool value)
    {
        var oldValue = GridViewRowList[item].IsMarked;
        GridViewRowList[item].IsMarked = value;
        var args = new RowMarkedEventArgs
        {
            Row = GridViewRowList[item],
            OldValue = oldValue
        };
        MarkChanged?.Invoke(this, args);
    }

    public sealed class RowMarkedEventArgs : EventArgs
    {
        public required GridViewRow Row { get; set; }
        public bool OldValue { get; set; }
    }

    public event EventHandler<RowMarkedEventArgs>? MarkChanged;

    public IList ToList()
    {
        return GridViewRowList;
    }

    // A slightly adapted method from gui.cs
    private static void RenderUstr(IConsoleDriver driver, ustring ustr, int col, int line, int width)
    {
        int used = 0;
        int index = 0;
        while (index < ustr.Length)
        {
            (var rune, var size) = Utf8.DecodeRune(ustr, index, index - ustr.Length);
            var count = Rune.ColumnWidth(rune);
            if (used + count > width) break;
            driver.AddRune(rune);
            used += count;
            index += size;
        }

        while (used < width)
        {
            driver.AddRune(' ');
            used++;
        }
    }
    
    public void Dispose()
    {
        // No resources to dispose currently
    }
}
