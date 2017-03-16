using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

public class Partners
{
    #region method Partners
    public Partners()
    {

    } 
    #endregion

    #region method addPartner
    public int addPartner(int Id, string Name, string Address, string Phone, string Email, bool State, string Account, string Password, string Avartar, string SocialNetwork)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblPartner WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblPartner([Name],[Address],[Phone],[Email],[DayCreate],[State],[Account],[Password],[Avartar],SocialNetwork) VALUES(@Name,@Address, @Phone, @Email,getDate(),@State,@Account,@Password,@Avartar,@SocialNetwork) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblPartner SET Name = @Name, Address = @Address, Phone = @Phone, Email = @Email, DayCreate = getDate(), State = @State, Account = @Account, Avartar = @Avartar, SocialNetwork = @SocialNetwork WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            Cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = Address;
            Cmd.Parameters.Add("Phone", SqlDbType.NVarChar).Value = Phone;
            Cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = Email;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = this.CryptographyMD5(Password);
            Cmd.Parameters.Add("Avartar", SqlDbType.NVarChar).Value = Avartar;
            Cmd.Parameters.Add("SocialNetwork", SqlDbType.NVarChar).Value = SocialNetwork;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #region method updatePartners
    public int updatePartners(int id, string name, string address, string phone, string email, bool state, string account, string pwd, string avatar)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblPartner WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblPartner([Name],[Address],[Phone],[Email],[DayCreate],[State],[Account],[Password],[Avartar]) VALUES(@Name,@Address, @Phone, @Email,getDate(),@State,@Account,@Password,@Avartar) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblPartner SET Name = @Name,Address = @Address,Phone = @Phone,Email = @Email,DayCreate=getDate(),State=@State, Account = @Account,Password=@Password,Avartar =@Avartar WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = name;
            Cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = address;
            Cmd.Parameters.Add("Phone", SqlDbType.NVarChar).Value = phone;
            Cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = email;
            //  Cmd.Parameters.Add("DayCreate", SqlDbType.NVarChar).Value =  pn.getDayCreate();
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = state;
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = account;
            Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = pwd;
            Cmd.Parameters.Add("Avartar", SqlDbType.NVarChar).Value = avatar;
       
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            throw new Exception("Không thể hoặc cập nhập Danh mục " + " Mã Lổi : " + "Cat_1");
        }
    }
    #endregion

    #region method updatePartnerSocialNetwork
    public void updatePartnerSocialNetwork(string Account, string SocialNetwork, string SocialNetworkType)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery += "UPDATE tblPartner SET SocialNetwork = @SocialNetwork, SocialNetworkType = @SocialNetworkType WHERE Account = @Account";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            Cmd.Parameters.Add("SocialNetwork", SqlDbType.NVarChar).Value = SocialNetwork;
            Cmd.Parameters.Add("SocialNetworkType", SqlDbType.NVarChar).Value = SocialNetworkType;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method updatePartnerPassword
    public void updatePartnerPassword(string Account, string Password)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery += "UPDATE tblPartner SET Password = @Password WHERE Account = @Account";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = this.CryptographyMD5(Password);
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method delData
    public int delData(int id)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblPartner WHERE Id = @Id";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #region method getPartnerById
    public DataTable getPartnerById(int id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblPartner WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            throw new Exception("Không thể đưa  dử liệu ra bảng");
        }
        return objTable;
    }
    #endregion

    #region method getPartnerByCat
    public DataTable getPartnerByCat(int CatId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand(); 
            Cmd.CommandText = " SELECT DISTINCT tblPartner.Account AS AccountName,[PartnerId],[PartnerName],[ViewState],[CategoryId]FROM [tblCategoryPartner] LEFT JOIN tblPartner ON [tblCategoryPartner].PartnerId = tblPartner.Id WHERE [CategoryId] = @CategoryId";
            Cmd.Parameters.Add("CategoryId", SqlDbType.Int).Value = CatId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            throw new Exception("Không thể đưa  dử liệu ra bảng");
        }
        return objTable;
    }
    #endregion

    #region method getPartnerByAccout
    public DataTable getPartnerByAccout(string acc)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP 1 * FROM tblPartner WHERE Account = @Account";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = acc;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            throw new Exception("Không thể đưa  dử liệu ra bảng");
        }
        return objTable;
    }
    #endregion

    #region method getPartnerIdByAccount
    public int getPartnerIdByAccount(string Account)        
    {
        int PartnerId = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Id FROM tblPartner WHERE Account = @Account";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                PartnerId = int.Parse(Rd["Id"].ToString());
            }
            Rd.Close();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            throw new Exception("Không  thể tìm thấy đối tượng có id = " + PartnerId);
        }
        return PartnerId;
    }
    #endregion

    #region method getPartner
    public DataTable getAllPartner()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, ISNULL((SELECT COUNT(*) FROM tblQuotationPartner WHERE PartnerId = tblPartner.Id),0) AS CountQuotation, ISNULL((SELECT COUNT(*) FROM tblCategoryPartner WHERE PartnerId = tblPartner.Id),0) AS CountCategory FROM tblPartner  ORDER BY tblPartner.Name";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

            for (int i = 0; i < objTable.Rows.Count; i++)
            {
                objTable.Rows[i]["TT"] = (i + 1);
            }

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region method getDataForCombobox
    public DataTable getDataForCombobox(string QuotationCode)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = " SELECT Id, Name FROM tblPartner WHERE Id NOT IN (SELECT PartnerId FROM tblQuotationPartner WHERE QuotationCode = @QuotationCode) ORDER BY Name";
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            objTable.Rows.Add(0,"Chọn đối tác");
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region method checkUserName
    public bool checkUserName(string Account)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblPartner WHERE Account = @Account";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                tmpValue = true;
            }
            Rd.Close();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
        return tmpValue;
    }
    #endregion

    #region Method CryptographyMD5
    public string CryptographyMD5(string source)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider objMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] bytHash = objMD5.ComputeHash(buffer);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytHash.Length; i++)
        {
            sb.Append(bytHash[i].ToString("x2"));
        }
        return (sb).ToString();
        //string result = "";
        //foreach (byte a in bytHash)
        //{
        //    result += int.Parse(a.ToString(), System.Globalization.NumberStyles.HexNumber).ToString();
        //}
        //return result;
    }
    #endregion

    #region method checkForLogin
    public bool checkForLogin(string Account, string Password, ref string FullName)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblPartner WHERE Account = @Account ";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;

            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                String s = Rd["Password"].ToString();
                if (Rd["Password"].ToString() == this.CryptographyMD5(Password))
                {
                    
                    tmpValue = true;
                }
                FullName = Rd["Name"].ToString();
            }
            Rd.Close();

            Cmd.CommandText = "UPDATE tblPartner SET LastAccess = getdate() WHERE Account = @Account";
            Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
        return tmpValue;
    }
    #endregion

    #region method checkForLoginWithoutMd5
    public bool checkForLoginWithoutMd5(string Account, string Password, ref string FullName)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblPartner WHERE Account = @Account ";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;

            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                String s = Rd["Password"].ToString();
                if (Rd["Password"].ToString() == Password)
                {

                    tmpValue = true;
                }
                FullName = Rd["Name"].ToString();
            }
            Rd.Close();

            Cmd.CommandText = "UPDATE tblPartner SET LastAccess = getdate() WHERE Account = @Account";
            Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
        return tmpValue;
    }
    #endregion

    #region PartnerGroup

    #region method getDataPartnerGroup
    public DataTable getDataPartnerGroup()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            //Cmd.CommandText = "SELECT 0 AS TT, *, (SELECT COUNT(*) FROM tblPartnerGroupDetailt WHERE GroupId = tblPartnerGroup.Id) AS CountItem FROM tblPartnerGroup";
            Cmd.CommandText = "SELECT 0 AS TT, [Id],[Code],[Name],[State],[UrlImage],[ParentId]   FROM tblCategory where [ParentId] = 0 AND [tblCategory].State = 1";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

            for (int i = 0; i < objTable.Rows.Count; i++)
            {
                objTable.Rows[i]["TT"] = (i + 1);
            }

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region method setPartnerGroup
    public int setPartnerGroup(int Id, string Name, bool State)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblPartnerGroup WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblPartnerGroup(Name,State) VALUES(@Name,@State) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblPartnerGroup SET Name = @Name WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #region method delPartnerGroup
    public int delPartnerGroup(int id)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblPartnerGroup WHERE Id = @Id";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.ExecuteNonQuery();

            sqlQuery = "DELETE tblPartnerGroupDetailt WHERE GroupId = @Id";
            Cmd.CommandText = sqlQuery;
            Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #region method getPartnerGroupById
    public DataTable getPartnerGroupById(int id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblPartnerGroup WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            throw new Exception("Không thể đưa  dử liệu ra bảng");
        }
        return objTable;
    }
    #endregion

    #endregion

    #region PartnerGroupDetailt

    #region method getPartnerGroupDetailt
    public DataTable getPartnerGroupDetailt(int GroupId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, * , (SELECT Name FROM tblPartner WHERE Id = tblPartnerGroupDetailt.PartnerId) AS PartnerName FROM tblPartnerGroupDetailt WHERE GroupId = @GroupId";
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

            for (int i = 0; i < objTable.Rows.Count; i++)
            {
                objTable.Rows[i]["TT"] = (i + 1);
            }

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region method setPartnerGroupDetailt
    public int setPartnerGroupDetailt(int GroupId, int PartnerId)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblPartnerGroupDetailt WHERE GroupId = @GroupId AND PartnerId = @PartnerId)";
            sqlQuery += "BEGIN INSERT INTO tblPartnerGroupDetailt(GroupId,PartnerId) VALUES(@GroupId,@PartnerId) END ";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #region method delPartnerGroupDetailt
    public int delPartnerGroupDetailt(int GroupId, int PartnerId)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblPartnerGroupDetailt WHERE GroupId = @GroupId AND PartnerId = @PartnerId";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    #endregion

    #region getPartnerDataParentCategory
    public DataTable getPartnerDataParentCategory(string PartnerId)
    {
     DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = " IF (@PartnerId = 1008 ) BEGIN SELECT * FROM tblCategory  WHERE ParentId = 0 END ELSE BEGIN       SELECT * FROM tblCategory WHERE Id IN (  SELECT DISTINCT  tblCategory.ParentId FROM  [tblCategoryPartner]  Left join tblCategory on tblCategory.Id = [tblCategoryPartner].CategoryId WHERE PartnerId = @PartnerId AND tblCategory.State = 1 ) END";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
          
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion



   
}