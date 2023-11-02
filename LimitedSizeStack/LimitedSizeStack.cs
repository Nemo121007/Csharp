using System;
using System.Collections;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
    LinkedList<T> stack = new LinkedList<T>();
    private int len = 0;
    private int counter = 0;
    public LimitedSizeStack(int undoLimit)
    {
        len = undoLimit;
    }

    public void Push(T item)
    {
        if (len == 0)
            return;
        if (counter == len)
            stack.RemoveFirst();
        else
            counter++;
        stack.AddLast(item);
    }

    public T Pop()
    {
        if (counter == 0)
            throw new InvalidOperationException();
        var a = stack.Last.Value;
        stack.RemoveLast();
        counter--;
        return a;
    }

    public int Count => counter;
}