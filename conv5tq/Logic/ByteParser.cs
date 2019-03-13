using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Models.Raw;

namespace Conv5tq.Logic
{
    public class ByteParser
    {
        public RawData Execute(string fileName)
        {
            var result = new RawData();
            var bytes = File.ReadAllBytes(fileName);
            result.Head = Deserialize<Head>(bytes, 0, 8).ToList();
            result.Data = Deserialize<Data>(bytes, 2048).ToList();
            return result;
        }

        private IEnumerable<T> Deserialize<T>(byte[] bytes, int startOffset = 0, int count = 0) where T : struct
        {
            if (bytes == null)
            {
                yield break;
            }

            var recordSize = Marshal.SizeOf(typeof(T));
            var size = bytes.Length;
            var endOffset = count > 0 ? startOffset + (recordSize * count) : size;
            var offset = startOffset;
            while (offset < endOffset)
            {
                var buffer = new byte[recordSize];
                Buffer.BlockCopy(bytes, offset, buffer, 0, recordSize);
                offset += recordSize;
                var result = this.Deserialize<T>(buffer);
                yield return result;
            }
        }

        private T Deserialize<T>(byte[] bytes) where T : struct
        {
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T data = (T)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(T));
            gch.Free();
            return data;
        }
    }
}
