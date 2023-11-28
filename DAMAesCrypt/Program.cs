using System.Net.Security;
using System.Security.Cryptography;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter text to encrypt: ");
        var text = Console.ReadLine();

        if (text != null)
        {
            EncriptData(text);
        }
    }

    private static void EncriptData(string text)
    {
        Aes aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();

        Console.WriteLine("Text to encript:" + text);
        Console.WriteLine("Encript key:" + Convert.ToBase64String(aes.Key));
        Console.WriteLine("Encript vector:" + Convert.ToBase64String(aes.IV));
        Console.WriteLine($"Encript mode: {aes.Mode}");
        Console.WriteLine($"Aes Key Size : {aes.KeySize}");

        // Create encryptor object
        ICryptoTransform encryptor = aes.CreateEncryptor();

        byte[] encryptedData;

        //Encryption will be done in a memory stream through a CryptoStream object
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(text);
                }
                encryptedData = ms.ToArray();
            }
        }

        Console.WriteLine("Encripted text:" + Convert.ToBase64String(encryptedData));
    }
}