using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace GerenciamentoComercio_Domain.Utils.Security
{
    public class Security
    {
        protected static byte[] Key1 = new byte[] { 36, 217, 35, 77, 44, 17, 39, 154, 114 };
        protected static byte[] Key2 = new byte[] { 41, 87, 30, 74, 100, 41, 78, 10, 2, 78, 96 };

        public static string EncryptString(string sToEncrypt)
        {
            if (sToEncrypt == string.Empty) return "";

            RijndaelManaged CryptProvider = new RijndaelManaged();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            SHA1CryptoServiceProvider hashSHA = new SHA1CryptoServiceProvider();
            byte[] InputbyteArray = Encoding.UTF8.GetBytes(sToEncrypt);

            CryptProvider.Key = hashMD5.ComputeHash(Key1);
            CryptProvider.IV = hashMD5.ComputeHash(Key2);
            CryptProvider.Mode = CipherMode.ECB;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, CryptProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(InputbyteArray, 0, InputbyteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            byte[] b = ms.ToArray();
            int I;
            for (I = 0; I <= Information.UBound(b); I++)
                ret.AppendFormat("{0:X2}", b[I]);

            return ret.ToString();
        }
        public static string DecryptString(string sToDecrypt)
        {
            if (Strings.Trim(sToDecrypt) == string.Empty) return string.Empty;

            RijndaelManaged DES = new RijndaelManaged();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            DES.Key = hashMD5.ComputeHash(Key1);
            DES.IV = hashMD5.ComputeHash(Key2);
            DES.Mode = CipherMode.ECB;

            byte[] inputByteArray = new byte[Convert.ToInt32(sToDecrypt.Length / (double)2 - 1 + 1)];
            int X;
            for (X = 0; X <= sToDecrypt.Length / (double)2 - 1; X++)
            {
                int IJ = Convert.ToInt32(sToDecrypt.Substring(X * 2, 2), 16);
                ByteConverter BT = new ByteConverter();
                inputByteArray[X] = new byte();
                inputByteArray[X] = Convert.ToByte(IJ);
            }

            // Create the crypto objects
            System.IO.MemoryStream MS = new System.IO.MemoryStream();
            CryptoStream CS = new CryptoStream(MS, DES.CreateDecryptor(), CryptoStreamMode.Write);

            // Flush the data through the crypto stream into the memory stream
            CS.Write(inputByteArray, 0, inputByteArray.Length);
            CS.FlushFinalBlock();

            // Get the decrypted data back from the memory stream
            StringBuilder ret = new StringBuilder();
            byte[] B = MS.ToArray();
            int I;
            for (I = 0; I <= Information.UBound(B); I++)
                ret.Append(Strings.Chr(B[I]));
            return ret.ToString();
        }
    }
}
