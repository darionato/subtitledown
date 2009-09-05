using System;
using System.Text;
using System.Collections;
using System.IO;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;


namespace Badlydone.Unzipper
{

    public class clUnzipper : IDisposable
    {
        public string[] Estrai(string file)
        {
            string[] ret = null;
            int newsize = 0;

            if (!File.Exists(file))
            {
                Console.WriteLine("Cannot find file '{0}'", file);
                return null;
            }

            string directoryName = Path.GetDirectoryName(file);

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(file)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    newsize++;

                    Console.WriteLine(theEntry.Name);

                    string fileName = Path.GetFileName(theEntry.Name);

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(System.IO.Path.Combine(directoryName, fileName)))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    string[] tmp = new string[newsize];
                    tmp[newsize - 1] = System.IO.Path.Combine(directoryName, fileName);
                    if (ret != null)
                        System.Array.Copy(ret, tmp,
                        System.Math.Min(ret.Length, tmp.Length));
                    ret = tmp;

                }
            }

            return ret;

        }

        public void Dispose()
        {
            Console.WriteLine("clUnzip disposed");
        }

    }
}