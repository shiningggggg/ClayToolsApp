using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteBinaryFile();
            ReadBinaryFile();
        }
        static void WriteBinaryFile()
        {
            BinaryWriter bw;
            int i = 25591;
            double d = 3.14159;
            bool b = true;
            string s = "I am Happy";
            string c = "文件头";

            try
            {
                bw = new BinaryWriter(new FileStream("mydata1", FileMode.Create));
                bw.Write(i);
                bw.Write("\r\n");
                bw.Write(d);
                bw.Write(b);
                bw.Write(s);
                bw.Write(c);
                bw.Close();
            }
            catch
            {
                Console.WriteLine("创建文件失败");
            }
            Console.WriteLine("OK");
            Console.Read();
        }
        static void ReadBinaryFile()
        {
            BinaryReader br;
            try
            {
                Stream stream = new FileStream("mydata1", FileMode.Open);
                br = new BinaryReader(stream);
                byte[] bs = br.ReadBytes(4);
                Console.WriteLine(BitConverter.ToInt32(bs,0));
                br.Read();
                br.Read();
                br.Read();

                Console.WriteLine(br.ReadDouble());
                Console.WriteLine(br.ReadBoolean());
                Console.WriteLine(br.ReadString());
                Console.WriteLine(br.ReadString());
            }
            catch
            {
                Console.WriteLine("读取文件失败"); 
            }
            Console.Read();
        }
    }
}
