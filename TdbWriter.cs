using System;
using System.IO;
using System.Text;

namespace Swung0x48.Ballance.TdbReader
{
    public class TdbWriter : BinaryWriter
    {
        public TdbWriter(TdbStream output) : base(output)
        {
            if (output.WriteAsEncoded)
                throw new InvalidOperationException(
                    $"{nameof(output)} should set WriteAsEncoded to false before using this writer.");
        }

        public override void Write(string value)
        {
            Write(Encoding.ASCII.GetBytes(value));
            Write('\0');
        }
    }
}