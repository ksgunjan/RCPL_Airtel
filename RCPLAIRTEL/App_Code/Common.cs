using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region "Variables"
    private static Common instance = new Common();
    #endregion
    #region "Static"
    public static Common Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    #region methods
    public string Decrypt(string TextToBeDecrypted)
    {
        RijndaelManaged RijndaelCipher = new RijndaelManaged();

        string Password = "CSC";
        string DecryptedData;

        try
        {
            byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            //Making of the key for decryption
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric Rijndael decryptor object.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(EncryptedData);
            //Defines the cryptographics stream for decryption.THe stream contains decrpted data
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];
            Int32 DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();

            //Converting to string
            DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);


        }
        catch
        {
            DecryptedData = TextToBeDecrypted;
        }
        return DecryptedData;
    }
    public string Encrypt(string TextToBeEncrypted)
    {
        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        string Password = "CSC";
        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
        //Creates a symmetric encryptor object.
        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
        //Defines a stream that links data streams to cryptographic transformations
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(PlainText, 0, PlainText.Length);
        //Writes the final state and clears the buffer
        cryptoStream.FlushFinalBlock();
        byte[] CipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string EncryptedData = Convert.ToBase64String(CipherBytes);

        return EncryptedData;

    }
    public string RameshQueryString(string RawURLs, string QueryStringName)
    {
        //starting index at e.g http://x.aspx?Propid=123 will return 21
        RawURLs = RawURLs.Replace("%3d", "=");
        RawURLs = RawURLs.Replace("%2f", "/");
        int start = RawURLs.IndexOf(QueryStringName, 0) + QueryStringName.Length;

        int length = RawURLs.IndexOf("&", start);
        if (length > 0)
            length = length - start;
        else

            length = RawURLs.Length - start;
        return RawURLs.Substring(start, length);

    }
    public void FillComboBox(DropDownList cmb, DataTable dt, string TextField, string ValueField)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = TextField;
        cmb.DataValueField = ValueField;
        cmb.DataBind();
    }
    public void FillComboBoxList(CheckBoxList cmb, DataTable dt, string TextField, string ValueField)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = TextField;
        cmb.DataValueField = ValueField;
        cmb.DataBind();
    }
    public void FillComboBoxList1(RadioButtonList cmb, DataTable dt, string TextField, string ValueField)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = TextField;
        cmb.DataValueField = ValueField;
        cmb.DataBind();
    }
    public string RSQandSQLInjection(string SingleQuote, string level)
    {
        if (level == "hard")
        {
            SingleQuote = SingleQuote.Replace("'where ", "");
            SingleQuote = SingleQuote.Replace(" where ", "");
            SingleQuote = SingleQuote.Replace(" is ", "");
            SingleQuote = SingleQuote.Replace(" or ", "");
            SingleQuote = SingleQuote.Replace("'or ", "");
            SingleQuote = SingleQuote.Replace(" from ", "");
            SingleQuote = SingleQuote.Replace("'table ", "");
            SingleQuote = SingleQuote.Replace(" table ", "");
            SingleQuote = SingleQuote.Replace("'view ", "");
            SingleQuote = SingleQuote.Replace(" view ", "");
            SingleQuote = SingleQuote.Replace("'exec ", "");
            SingleQuote = SingleQuote.Replace(" exec ", "");
            SingleQuote = SingleQuote.Replace("'and ", "");
            SingleQuote = SingleQuote.Replace(" and ", "");
            SingleQuote = SingleQuote.Replace("'drop ", "");
            SingleQuote = SingleQuote.Replace(" drop ", "");
            SingleQuote = SingleQuote.Replace("'delete ", "");
            SingleQuote = SingleQuote.Replace(" delete ", "");
            SingleQuote = SingleQuote.Replace("'alter ", "");
            SingleQuote = SingleQuote.Replace(" alter ", "");
            SingleQuote = SingleQuote.Replace("'update ", "");
            SingleQuote = SingleQuote.Replace(" update ", "");
            SingleQuote = SingleQuote.Replace("'select ", "");
            SingleQuote = SingleQuote.Replace(" select ", "");
            SingleQuote = SingleQuote.Replace("'insert ", "");
            SingleQuote = SingleQuote.Replace(" insert ", "");
            SingleQuote = SingleQuote.Replace("'create ", "");
            SingleQuote = SingleQuote.Replace(" create ", "");
            SingleQuote = SingleQuote.Replace("=", "");
            SingleQuote = SingleQuote.Replace("*", "");
            SingleQuote = SingleQuote.Replace(" from ", "");
            SingleQuote = SingleQuote.Replace("'from ", "");
            //SingleQuote = SingleQuote.Replace(",", "");
            SingleQuote = SingleQuote.Replace(";", "");
            SingleQuote = SingleQuote.Replace(":", "");
            //SingleQuote = SingleQuote.Replace("/", "");
            SingleQuote = SingleQuote.Replace("@@", "");
            //SingleQuote = SingleQuote.Replace("&", "");
            //SingleQuote = SingleQuote.Replace("%", "");
            SingleQuote = SingleQuote.Replace("'", "");
            return SingleQuote;
        }
        else
        {

            SingleQuote = SingleQuote.Replace("'", "");
            SingleQuote = SingleQuote.Replace("'drop ", "");
            SingleQuote = SingleQuote.Replace(" drop ", "");
            SingleQuote = SingleQuote.Replace("'delete ", "");
            SingleQuote = SingleQuote.Replace(" delete ", "");
            SingleQuote = SingleQuote.Replace("'alter ", "");
            SingleQuote = SingleQuote.Replace(" alter ", "");
            SingleQuote = SingleQuote.Replace("=", "");
            return SingleQuote;
        }
    }
    #endregion
}