using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyDictionary = System.Collections.Generic.Dictionary<string, string>;

namespace PricesCollector
{
    class Configuration
    {
        private string configFile = Directory.GetCurrentDirectory() + "\\config.txt";
        private string configFileEncrypted = Directory.GetCurrentDirectory() + "\\config.bin";
        private string key = "1234512345678976";

        public Configuration()
        {

        }

        public void saveDictionaryToFile(MyDictionary dict)
        {
            using (StreamWriter file = new StreamWriter(configFile))
            {
                foreach (var entry in dict)
                {
                    file.WriteLine("{0}:{1}", entry.Key, entry.Value);
                }
            }

            EncryptFile(configFile, configFileEncrypted, key);

            if (File.Exists(configFile))
            {
                // Remove config source file
                File.Delete(configFile);
            }
        }

        public MyDictionary loadDictionaryFromFile()
        {
            if (File.Exists(configFileEncrypted))
            {
                DecryptFile(configFileEncrypted, configFile, key);
            }

            MyDictionary myDict = new MyDictionary();
            if (File.Exists(configFile))
            {
                using (StreamReader file = new StreamReader(configFile))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line == "")
                        {
                            continue;
                        }
                        int index = line.IndexOf(":");
                        if (index < 0)
                        {
                            continue;
                        }
                        string key = line.Substring(0, index);
                        string value = line.Substring(index + 1);
                        myDict[key] = value;
                    }
                }
            }

            // Remove decrypted file
            File.Delete(configFile);
            return myDict;
        }

        private static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to encrypt file
                Console.WriteLine("failed to encrypt file");
            }
        }
        private static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to decrypt file
            }
        }













    }
}
