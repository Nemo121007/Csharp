using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
	public class ReadonlyBytes : IReadOnlyList<byte>
	{
		private readonly byte[] array;
		private int hash;

		public ReadonlyBytes(params byte[] array)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			this.array = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
				this.array[i] = array[i];
			hash = CalculateHash();
		}

        public override int GetHashCode() => hash;

        int CalculateHash()
        {
            int hash = -985847861;
            if (array != null)
                foreach (byte b in array)
                    hash = unchecked(hash * -1521134295 + b.GetHashCode());
            return hash;
        }

        public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) 
				return false;

			var array = (ReadonlyBytes)obj;

			if (this.hash != array.hash)
				return false;

            if (this.Length != array.Length)
                return false;
            for (int i = 0; i < array.Length; i++)
            {
                if (this[i] != array[i])
                    return false;
            }
            return true;
        }

		public override string ToString()
		{
			string line = "[";
			foreach(byte b in this.array)
				line += b.ToString() + ", ";
			if (line.Length > 2)
				line = line.Remove(line.Length - 2);
			line += "]";
			return line;
		}

		public byte this[int index] => array[index];
		public int Count => array.Length;
		public int Length => array.Length;

		public IEnumerator<byte> GetEnumerator()
		{
			return ((IReadOnlyList<byte>)array).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}