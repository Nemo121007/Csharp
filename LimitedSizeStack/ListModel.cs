using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class ListModel<TItem>
{
	public List<TItem> Items { get; }
	public int UndoLimit;
	LimitedSizeStack<History> hictory;
        
	public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
		hictory = new LimitedSizeStack<History>(undoLimit);
	}

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = items;
		UndoLimit = undoLimit;
	}

	public void AddItem(TItem item)
	{
		Items.Add(item);
		hictory.Push(new History { index = hictory.Count, move = "add", elment = item });
	}

	public void RemoveItem(int index)
	{
        hictory.Push(new History { index = index, move = "delete", elment = Items[index] });
        Items.RemoveAt(index);
	}

	public bool CanUndo()
	{
		return (hictory.Count != 0);
	}

	public void Undo()
	{
		var pastMove = hictory.Pop();
		if (pastMove.move == "add")
		{
			Items.RemoveAt(pastMove.index);
		}
		else 
		if (pastMove.move == "delete")
		{
			Items.Insert(pastMove.index, pastMove.elment);
		}
		else
			throw new NotImplementedException();
	}
	public class History
	{
		public int index;
		public string move;
		public TItem elment;
	}
}


