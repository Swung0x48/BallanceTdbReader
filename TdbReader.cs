using System;
using System.IO;
using System.Text;

namespace Swung0x48.Ballance.TdbReader
{
    public class TdbReader: BinaryReader
    {
        public TdbReader(TdbStream input) : base(input)
        {
            if (input.ReadAsEncoded)
                throw new InvalidOperationException(
                    $"{nameof(input)} should set WriteAsEncoded to false before using this writer.");

        }

        public override string ReadString()
        {
            var sb = new StringBuilder();

            try
            {
                var ch = base.ReadChar();
                while (ch != 0)
                {
                    sb.Append(ch);
                    ch = base.ReadChar();
                }
            }
            catch (EndOfStreamException) {}

            return sb.ToString();
        }
    }
}