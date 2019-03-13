using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Models.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Data
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116)]
        public byte[] message;    // 問題
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] choices1;    // 選択（計５つ）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] choices2;    // 選択（計５つ）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] choices3;    // 選択（計５つ）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] choices4;    // 選択（計５つ）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] choices5;    // 選択（計５つ）
    }
}
