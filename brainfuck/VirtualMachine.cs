using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		Dictionary<char, Action<IVirtualMachine>> InstructionMachines { get; set; }

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			InstructionPointer = 0;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionMachines = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			InstructionMachines.Add(symbol, execute);
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (InstructionMachines.TryGetValue(Instructions[InstructionPointer], out var action))
					action(this);
				InstructionPointer++;

			}
		}
	}
}