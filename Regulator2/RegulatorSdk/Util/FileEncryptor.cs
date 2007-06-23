using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Regulator.SDK.Core
{
	/// <summary>
	/// Summary description for FileEncryptor.
	/// </summary>
	public class FileEncryptor
	{
		private FileEncryptor()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private static  string normalizePassword(string unNormalizedPassword)
		{
			string password = unNormalizedPassword;
			if (password.Length > 8)
				password=password.Substring(0, 8);
			else if (password.Length < 8)
			{
				int add=8-password.Length;
				for (int i=0; i<add; i++)
					password=password+i;
			}

			return password;

		}

		public  static bool EncryptFile(string filename,string password)
		{
			// The following function uses Rijndael encryption algorithm to encrypt file
			
			// Becouse the Rijndael algoritm is private-key algoritm we need one
			// private key and IV. I composed private key from password entered by user.
			// IV is the same as password but this probably is not good.
			// A key and IV have to be exactly 16 bytes length so we have to truncate
			// password if it is longer then 8 characters (16 bytes) or to add some characters
			// if password length is less then 8 characters.

			try
			{
				string outputPath= filename+".enc";
				password = normalizePassword(password);
				UnicodeEncoding UE = new UnicodeEncoding();
				byte[] key = UE.GetBytes(password);

				string cryptFile=outputPath;
				FileStream fsCrypt=new FileStream(cryptFile, FileMode.Create);

				RijndaelManaged RMCrypto = new RijndaelManaged();

				CryptoStream cs = new CryptoStream(fsCrypt,
					RMCrypto.CreateEncryptor(key, key),   
					CryptoStreamMode.Write);
			
				FileStream fsIn=new FileStream(filename,FileMode.Open);

				int data;
				while ((data=fsIn.ReadByte())!=-1)
					cs.WriteByte((byte) data);

				fsIn.Close();
				cs.Close();
				fsCrypt.Close();

				bool renameSuccess =
					RenameEncryptedToOriginal(filename,outputPath);
				return renameSuccess;
			}
			catch
			{
				return false;
			}
		}



		private  static bool RenameEncryptedToOriginal(string originalFileName,string encryptedFileName)
		{
			try
			{
				File.Delete(originalFileName);
				File.Move(encryptedFileName,originalFileName);
				return true;
			
			}
			catch(Exception )
			{
				return false;
			}
		}

		public  static bool DecryptFile(string filename,string password)
		{
			try
			{
				string outputFile = filename+".enc";
				password= normalizePassword(password);

				UnicodeEncoding UE = new UnicodeEncoding();
				byte[] key = UE.GetBytes(password);

				FileStream fsCrypt = new FileStream(filename,FileMode.Open);
			
				RijndaelManaged RMCrypto = new RijndaelManaged();

				CryptoStream cs = new CryptoStream(fsCrypt, 
					RMCrypto.CreateDecryptor(key, key), 
					CryptoStreamMode.Read);

				FileStream fsOut=new FileStream(outputFile,FileMode.Create);
				
				int data;
				while ((data=cs.ReadByte())!=-1)
					fsOut.WriteByte((byte) data);

				fsOut.Close();
				cs.Close();
				fsCrypt.Close();

				bool renameSuccess =
					RenameEncryptedToOriginal(filename,outputFile);
				return renameSuccess;

			}
			catch
			{
				return false;
			}
		}
	}
}
