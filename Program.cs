using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;



namespace Demo_Decryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo Decryption using crypto objects...");
            try
            {
                using (FileStream fileStream = new(@"C:\Users\Sravani.Ravulakollu\source\repos\Demo_Decryption1\Demo_Decryption1\TextData.txt", FileMode.Open))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] iv = new byte[aes.IV.Length];
                        int numBytesToRead = aes.IV.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) break;



                            numBytesRead += n;
                            numBytesToRead -= n;
                        }



                        byte[] key =
                        {
0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
};



                        using (CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateDecryptor(key, iv),
                        CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new(cryptoStream))
                            {
                                string decryptedMessage = decryptReader.ReadToEnd();
                                Console.WriteLine("The decrypted original message: Hello World...!!!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data cant be Decrypted {ex}");
                throw;
            }
        }
    }
}