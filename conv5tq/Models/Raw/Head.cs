using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Models.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Head
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] genreName;  // ジャンル名
        public short pass;      // 未使用
        public short size;      // ジャンル中の問題数（ブロック数と等価）
        public short skip;      // アクセスのために読み飛ばすブロック数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] file;  // プレーヤーデータファイル名
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] code;   // マジックコード（5TAKUQDT or 5TAKUQDX）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 214)]
        public byte[] fill; // ブロックアライメントの調整用詰めもの
    }
}
