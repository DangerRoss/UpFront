using System.Text;
using UpFront.Buffers;

namespace UpFront
{
    /// <summary>
    /// Represents a sequence of characters that can be modified. 
    /// MutableString is intended to be used as a StringBuilder or a char buffer.
    /// </summary>
    public sealed class MutableString : ArrayBuffer<char>
    {
        public MutableString() : base() { }

        public MutableString(int initialCapactiy) : base(initialCapactiy) { }

        public override string ToString() => new string(this.buffer, 0, this.Length);

        public void Write(string s)
        {
            this.Reserve(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                this.buffer[this.usagebound++] = s[i];
            }
        }

        public void WriteSByte(sbyte sb)
        {
            this.Reserve(4);
            this.usagebound += CharConverter.IntToUnicode(sb, this.buffer, this.usagebound);
        }

        public void WriteShort(short s)
        {
            this.Reserve(6);
            this.usagebound += CharConverter.IntToUnicode(s, this.buffer, this.usagebound);
        }

        public void WriteInt(int i)
        {
            this.Reserve(11);
            this.usagebound += CharConverter.IntToUnicode(i, this.buffer, this.usagebound);
        }

        public void WriteLong(long l)
        {
            this.Reserve(20);
            this.usagebound += CharConverter.LongToUnicode(l, this.buffer, this.usagebound);
        }

        public void WriteByte(byte b)
        {
            this.Reserve(4);
            this.usagebound += CharConverter.UintToUnicode(b, this.buffer, this.usagebound);
        }

        public void WriteUShort(ushort us)
        {
            this.Reserve(6);
            this.usagebound += CharConverter.UintToUnicode(us, this.buffer, this.usagebound);
        }

        public void WriteUInt(uint ui)
        {
            this.Reserve(11);
            this.usagebound += CharConverter.UintToUnicode(ui, this.buffer, this.usagebound);
        }

        public void WriteULong(ulong ul)
        {
            this.Reserve(20);
            this.usagebound += CharConverter.UlongToUnicode(ul, this.buffer, this.usagebound);
        }

        public void WriteFloat(float f, int remainders)
        {
            this.Reserve(20);
            this.usagebound += CharConverter.FloatToUnicode(f, this.buffer, this.usagebound, remainders);
        }

        public void WriteDouble(double f, int remainders)
        {
            this.Reserve(20);
            this.usagebound += CharConverter.DoubleToUnicode(f, this.buffer, this.usagebound, remainders);
        }

        public static implicit operator MutableString (string s)
        {
            var ms = new MutableString(s.Length);
            ms.Write(s);
            return ms;
        }

        public static explicit operator string (MutableString ms) => ms.ToString();

        public static explicit operator StringBuilder (MutableString ms)
        {
            var sb = new StringBuilder(ms.Length);
            sb.Append(ms.GetArray(), 0, ms.Length);
            return sb;
        }

        public static explicit operator MutableString (StringBuilder sb)
        {
            var ms = new MutableString(sb.Length);
            ms.Write(sb.ToString());
            return ms;
        }

    }
}
