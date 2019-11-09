using System;
using System.Text;

namespace UpFront
{
    /// <summary>
    /// Utility functions for writing primitives to char arrays or pointers without heap allocation
    /// </summary>
    public static class CharConverter
    {
        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">array to write to.</param>
        /// <param name="offset">position in the array to write number to</param>
        /// <param name="numbase">the numeric base to write the number in</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static int IntToUnicode(int value, char[] chars, int offset)
        {         
            if (value < 0)
            {
               int written = CharConverter.UintToUnicode((uint)-value, chars, offset + 1);

                // Add the minus afterwards to avoid modifying the char array incase of out of bounds.
                chars[offset] = '-';
                return ++written;
            }
            else
            {
                return CharConverter.UintToUnicode((uint)value, chars, offset);
            }           
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">pointer to write to</param>
        /// <param name="length">size of the pointer</param>
        /// <param name="offset">where in the pointer to write to</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int IntToUnicode(int value, char* chars, int length, int offset)
        {
            if (value < 0)
            {
                int written = CharConverter.UintToUnicode((uint)-value, chars, length, offset + 1);

                // Add the minus afterwards to avoid modifying the char array incase of out of bounds.
                chars[offset] = '-';
                return ++written;
            }
            else
            {
                return CharConverter.UintToUnicode((uint)value, chars, length, offset);
            }
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">array to write to.</param>
        /// <param name="offset">position in the array to write number to</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static int LongToUnicode(long value, char[] chars, int offset)
        {
            if (value < 0)
            {
                int written = CharConverter.UlongToUnicode((ulong)-value, chars, offset + 1);

                // Add the minus afterwards to avoid modifying the char array incase of out of bounds.
                chars[offset] = '-';
                return ++written;
            }
            else
            {
                return CharConverter.UlongToUnicode((ulong)value, chars, offset);
            }
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">pointer to write to</param>
        /// <param name="length">size of the pointer</param>
        /// <param name="offset">where in the pointer to write to</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int LongToUnicode(long value, char* chars, int length, int offset)
        {
            if (value < 0)
            {
                int written = CharConverter.UlongToUnicode((ulong)-value, chars, length, offset + 1);

                // Add the minus afterwards to avoid modifying the char array incase of out of bounds.
                chars[offset] = '-';
                return ++written;
            }
            else
            {
                return CharConverter.UlongToUnicode((ulong)value, chars, length, offset);
            }
        }


        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">array to write to.</param>
        /// <param name="offset">position in the array to write number to</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static unsafe int UintToUnicode(uint value, char[] chars, int offset)
        {
            fixed (char* charptr = chars)
            {
                return CharConverter.UintToUnicode(value, charptr, chars.Length, offset);
            }
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">pointer to write to</param>
        /// <param name="length">size of the pointer</param>
        /// <param name="offset">where in the pointer to write to</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int UintToUnicode(uint value, char* chars, int length, int offset)
        {
            int written = 0;

            char* charbuffer = stackalloc char[12];

            int cbIndex = 0;

            // reverse loop through individual digits of value in base 10
            do
            {
                uint digit = value % 10;

                // 48 is the unicode/ascii value of zero and assuming digit is 0-9 we can simply add 48 to write the correct char value.
                charbuffer[cbIndex++] = (char)(48 + digit);
                value /= 10;
            }
            while (value != 0);

            // With cbIndex holding the total number of chars we can now do a bounds check.
            if (cbIndex > (length - offset)) { throw new IndexOutOfRangeException(); }

            do
            {
                // write the char buffer reversed to the char pointer
                chars[offset + written++] = charbuffer[--cbIndex];
            }
            while (cbIndex > 0);       

            return written;
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">array to write to.</param>
        /// <param name="offset">position in the array to write number to</param>
        /// <param name="numbase">the numeric base to write the number in</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static unsafe int UlongToUnicode(ulong value, char[] chars, int offset)
        {
            fixed (char* charptr = chars)
            {
                return CharConverter.UlongToUnicode(value, charptr, chars.Length, offset);
            }
        }

        /// <summary>
        /// Writes a numeric value to a char array in base 10.
        /// </summary>
        /// <param name="value">number to write</param>
        /// <param name="chars">pointer to write to</param>
        /// <param name="length">size of the pointer</param>
        /// <param name="offset">where in the pointer to write to</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int UlongToUnicode(ulong value, char* chars, int length, int offset)
        {
            int written = 0;

            char* charbuffer = stackalloc char[20];

            int cbIndex = 0;

            // reverse loop through individual digits of value in base 10
            do
            {
                ulong digit = value % 10;

                // 48 is the unicode/ascii value of zero and assuming digit is 0-9 we can simply add 48 to write the correct char value.
                charbuffer[cbIndex++] = (char)(48 + digit);
                value /= 10;
            }
            while (value != 0);

            // With cbIndex holding the total number of chars we can now do a bounds check.
            if (cbIndex > (length - offset)) { throw new IndexOutOfRangeException(); }

            do
            {
                // write the char buffer reversed to the char pointer
                chars[offset + written++] = charbuffer[--cbIndex];
            }
            while (cbIndex > 0);

            return written;
        }


        /// <summary>
        /// Writes a floating point number to a char array in base 10.
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="chars">The array to write to</param>
        /// <param name="offset">Position in the array to write to</param>
        /// <param name="precision">How many precision digits to include</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static int FloatToUnicode(float value, char[] chars, int offset, int precision) 
        => CharConverter.DoubleToUnicode(value, chars, offset, precision);

        /// <summary>
        /// Writes a floating point number to a char array.
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="chars">The char pointer to write to</param>
        /// <param name="length">The total length of the char pointer</param>
        /// <param name="offset">Position in the char pointer to write to</param>
        /// <param name="precision">How many precision digits to include</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int FloatToUnicode(float value, char* chars, int length, int offset, int precision)
        => CharConverter.DoubleToUnicode(value, chars, length, offset, precision);

        /// <summary>
        /// Writes a floating point number to a char array.
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="chars">The array to write to</param>
        /// <param name="offset">Position in the array to write to</param>
        /// <param name="precision">How many precision digits to include</param>
        /// <returns>Total number of characters that were written to the array</returns>
        public static unsafe int DoubleToUnicode(double value, char[] chars, int offset, int precision)
        {
            fixed(char* charptr = chars)
            {
                return CharConverter.DoubleToUnicode(value, charptr, chars.Length, offset, precision);
            }
        }

        /// <summary>
        /// Writes a floating point number to a char array.
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="chars">The char pointer to write to</param>
        /// <param name="length">The total length of the char pointer</param>
        /// <param name="offset">Position in the char pointer to write to</param>
        /// <param name="precision">How many precision digits to include</param>
        /// <returns>Total number of characters that were written to the char pointer</returns>
        public static unsafe int DoubleToUnicode(double value, char* chars, int length, int offset, int precision)
        {
            // TODO: This conversion of double to chars needs some redoing as it can't handle values requiring scientific notation and can probably be simplified

            int written = 0;
            int space = length - offset;

            // Handle constants first
            if (double.IsNaN(value))
            {
                if (space < 3) { throw new IndexOutOfRangeException(); }

                chars[offset + written++] = 'N';
                chars[offset + written++] = 'a';
                chars[offset + written++] = 'N';
                return written;
            }
            else if (double.IsPositiveInfinity(value))
            {
                if (space < 1) { throw new IndexOutOfRangeException(); }
                chars[offset + written++] = '∞';
                return written;
            }
            else if (double.IsNegativeInfinity(value))
            {
                if (space < 2) { throw new IndexOutOfRangeException(); }
                chars[offset + written++] = '-';
                chars[offset + written++] = '∞';
                return written;
            }

            // correct precision to be a positive number and no higher than 15 for rounding purposes.
            precision = Math.Max(0, precision);
            precision = Math.Min(15, precision);

            ulong ipart = 0;
            ulong fpart = 0;
            bool isNeg = value < 0;

            double vtrunc = Math.Truncate(value);

            if (isNeg)
            {
                ipart = (ulong)-(long)vtrunc;
                fpart = (ulong)-(long)(Math.Round(value - vtrunc, precision) * Math.Pow(10, precision));
            }
            else
            {
                ipart = (ulong)vtrunc;
                fpart = (ulong)(Math.Round(value - vtrunc, precision) * Math.Pow(10, precision));
            }

            char* charbuffer = stackalloc char[20];

            int cbIndex = 0;

            // Start by handling the precision if requested.
            if (precision > 0)
            {
                // We loop until precision has been met instead of when there is no one digits to iterate. This pads out the result with zeros.
                do
                {
                    ulong digit = fpart % 10;
                    // 48 is the unicode/ascii value of zero and assuming digit is 0-9 we can simply add 48 to write the correct char value.
                    charbuffer[cbIndex++] = (char)(48 + digit);
                    fpart /= 10;
                    precision--;
                }
                while (precision > 0);

                charbuffer[cbIndex++] = '.';
            }

            do
            {
                // Just like above accept iterating through the integer part of the floating value.
                ulong digit = ipart % 10;
                charbuffer[cbIndex++] = (char)(48 + digit);
                ipart /= 10;
            }
            while (ipart != 0);

            if (isNeg) { charbuffer[cbIndex++] = '-'; }

            // With cbIndex holding the total number of chars we can now do a bounds check.
            if (cbIndex > space) { throw new IndexOutOfRangeException(); }

            do
            {
                // Write the reversed result to the char pointer
                chars[offset + written++] = charbuffer[--cbIndex];
            }
            while (cbIndex > 0);

            return written;
        }

        public static unsafe void AppendPrimitive(this StringBuilder stringBuilder, int value)
        {
            char* buffer = stackalloc char[12];

            var count = CharConverter.IntToUnicode(value, buffer, 12, 0);

            stringBuilder.Append(buffer, count);
        }

        public static unsafe void AppendPrimitive(this StringBuilder stringBuilder, long value)
        {
            char* buffer = stackalloc char[20];

            var count = CharConverter.LongToUnicode(value, buffer, 20, 0);

            stringBuilder.Append(buffer, count);
        }

        public static unsafe void AppendPrimitive(this StringBuilder stringBuilder, uint value)
        {
            char* buffer = stackalloc char[12];

            var count = CharConverter.UintToUnicode(value, buffer, 12, 0);

            stringBuilder.Append(buffer, count);
        }

        public static unsafe void AppendPrimitive(this StringBuilder stringBuilder, ulong value)
        {
            char* buffer = stackalloc char[20];

            var count = CharConverter.UlongToUnicode(value, buffer, 20, 0);

            stringBuilder.Append(buffer, count);
        }
    }
}
