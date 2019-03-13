using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Util
{
    public static class StringUtil
    {
        private readonly static string EncodeName = "shift_jis";

        public static string GetString(byte[] bytes, bool isMask)
        {
            byte[] values = null;
            if (isMask)
            {
                int index;
                for (index = bytes.Length - 1; index >= 0; index--)
                {
                    if (bytes[index] != 0x20)
                    {
                        break;
                    }
                }
                values = bytes.Select((x, i) => i > index ? x : (byte)(x ^ 128)).ToArray();
            }
            else
            {
                values = bytes;
            }

            return Encoding.GetEncoding(EncodeName).GetString(values).Trim();
        }
    }
}
