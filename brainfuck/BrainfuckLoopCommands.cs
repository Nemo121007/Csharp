using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
            Dictionary<int, int> startLoops = new Dictionary<int, int>();
			Stack<int> loop = new Stack<int>();

            vm.RegisterCommand('[', b => { 
				if (startLoops.Count == 0)
				{
					Stack<int> loop = new Stack<int>();
					loop.Push(b.InstructionPointer);
					for (var i = b.InstructionPointer + 1; i != b.Instructions.Length; i++)
					{
						if (b.Instructions[i] == '[')
							loop.Push(i);
						else if (b.Instructions[i] == ']')
							startLoops[loop.Pop()] = i;
					}
				}
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = startLoops[b.InstructionPointer];
				else
					loop.Push(b.InstructionPointer);
			});
			
			vm.RegisterCommand(']', b => {
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = loop.Peek();
				else
					loop.Pop();
			});
		}
	}
}