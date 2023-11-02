using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { unchecked { b.Memory[b.MemoryPointer]++; };  });
			vm.RegisterCommand('-', b => { unchecked { b.Memory[b.MemoryPointer]--; }; });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });

			vm.RegisterCommand('>', b =>
			{
				if (b.MemoryPointer == b.Memory.Length - 1)
					b.MemoryPointer = 0;
				else
					b.MemoryPointer++;
			});
			vm.RegisterCommand('<', b =>
			{
				if (b.MemoryPointer == 0)
					b.MemoryPointer = b.Memory.Length - 1;
				else
					b.MemoryPointer--;
			});

			for (char c = 'A'; c <= 'Z'; c++)
				ASCII(c);
			for (char c = 'a'; c <= 'z'; c++)
				ASCII(c);
			for (char c = '0'; c <= '9'; c++)
				ASCII(c);

			void ASCII(char c)
			{
				char k = c;
				vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(k); });
			}
        }
	}
}