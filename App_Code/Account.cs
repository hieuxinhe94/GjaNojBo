using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

public class Account
{
    #region method Account
    public Account()
    {
    } 
    #endregion

    #region method setData
    public int setData(int Id, string UserName, string FullName, string Email, string PassWord, int GroupId, bool State)
    {
        int tmpValue = 0;
        try
        {
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblAccount WHERE Id = @Id) ";
            sqlQuery += "BEGIN INSERT INTO tblAccount(UserName,FullName,Email,PassWord,GroupId,State) VALUES(@UserName,@FullName,@Email,@PassWord,@GroupId,@State) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblAccount SET FullName = @FullName, Email = @Email, GroupId = @GroupId, State = @State  WHERE Id = @Id END ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.Parameters.Add("FullName", SqlDbType.NVarChar).Value = FullName;
            Cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = Email;
            Cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = this.CryptographyMD5(PassWord);
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            tmpValue = 1;
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method setData
    public int setData(string UserName, string FullName, string Email, string PassWord)
    {
        int tmpValue = 0;
        try
        {
            string sqlQuery = "";
            sqlQuery = "UPDATE tblAccount SET FullName = @FullName, Email = @Email WHERE UserName = @UserName";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.Parameters.Add("FullName", SqlDbType.NVarChar).Value = FullName;
            Cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = Email;
            Cmd.ExecuteNonQuery();

            if (PassWord.Trim() != "")
            {
                Cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = this.CryptographyMD5(PassWord);
                sqlQuery = "UPDATE tblAccount SET PassWord = @PassWord WHERE UserName = @UserName";
                Cmd.CommandText = sqlQuery;
                Cmd.ExecuteNonQuery();
            }

            sqlCon.Close();
            sqlCon.Dispose();
            tmpValue = 1;
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method getData
    public DataTable getData()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, (SELECT Name FROM tblAccountGroup WHERE Id = GroupId) AS GroupName FROM tblAccount";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["TT"] = (i + 1);
            }
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataById
    public DataTable getDataById(int Id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblAccount WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataByUserName
    public DataTable getDataByUserName(string UserName)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblAccount WHERE UserName = @UserName";
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method checkUserName
    public bool checkUserName(string UserName)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblAccount WHERE UserName = @UserName";
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while(Rd.Read())
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

    #region method delData
    public void delData(int Id)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "DELETE tblAccount WHERE Id = @Id ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
    }
    #endregion

    #region method getDataGroupToCombobox
    public DataTable getDataGroupToCombobox()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Id, Name FROM tblAccountGroup WHERE State = 1";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region Method CryptographyMD5
    public string CryptographyMD5(string source)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider objMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] bytHash = objMD5.ComputeHash(buffer);
        //string result = "";
        //foreach (byte a in bytHash)
        //{
        //    result += int.Parse(a.ToString(), System.Globalization.NumberStyles.HexNumber).ToString();
        //}
        //return result;
        // new case for md5 cytography
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytHash.Length; i++)
        {
            sb.Append(bytHash[i].ToString("x2"));
        }
        return (sb).ToString();
        //
    }
    #endregion

    #region method checkForLogin
    public bool checkForLogin(string UserName, string Password, ref string FullName)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblAccount WHERE UserName = @UserName ";
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;

            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                if (Rd["Password"].ToString() == this.CryptographyMD5(Password))
                {
                    tmpValue = true;
                }
                FullName = Rd["FullName"].ToString();
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

    #region method checkForFunction
    public bool checkForFunction(string UserName, int FunctionId, ref bool View, ref bool Add, ref bool Edit, ref bool Del, ref bool Orther)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM [dbo].[tblAccountGroupPer] WHERE [GroupId] = (SELECT GroupId FROM tblAccount WHERE UserName = @UserName) AND FunctionId = @FunctionId";
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.Parameters.Add("FunctionId", SqlDbType.Int).Value = FunctionId;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                tmpValue = true;
                View = bool.Parse(Rd["View"].ToString());
                Add = bool.Parse(Rd["Add"].ToString());
                Edit = bool.Parse(Rd["Edit"].ToString());
                Del = bool.Parse(Rd["Del"].ToString());
                Orther = bool.Parse(Rd["Orther"].ToString());
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

    #region AccounttGroup
    
    #region method setDataGroupAccount
    public int setDataGroupAccount(int Id, string Name, bool State)
    {
        int tmpValue = 0;
        try
        {
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblAccountGroup WHERE Id = @Id) ";
            sqlQuery += "BEGIN INSERT INTO tblAccountGroup(Name,State) VALUES(@Name,@State) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblAccountGroup SET Name = @Name, State = @State WHERE Id = @Id END ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            tmpValue = 1;
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method getDataGroupAccount
    public DataTable getDataGroupAccount()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, * FROM tblAccountGroup";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["TT"] = (i + 1);
            }
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataGroupAccountById
    public DataTable getDataGroupAccountById(int Id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblAccountGroup WHERE Id = @Id";
            Cmd.Parameters.Add("Id",SqlDbType.Int).Value = Id;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method delDataGroupAccount
    public void delDataGroupAccount(int Id)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "DELETE tblAccountGroup WHERE Id = @Id ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
    }
    #endregion

    #endregion

    #region Permission

    #region method getDataPermissionToCombobox
    public DataTable getDataPermissionToCombobox()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Id, Name FROM tblFunction";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataPermission
    public DataTable getDataPermission(int GroupId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, ISNULL((SELECT [Name] FROM tblFunction WHERE Id = FunctionId),'') AS FunctionName, REPLACE(REPLACE(CAST([View] AS nvarchar),'1',N'Xem'),'0','--') AS Xem, REPLACE(REPLACE(CAST([Add] AS nvarchar),'1',N'Thêm'),'0','--') AS Them, REPLACE(REPLACE(CAST([Edit] AS nvarchar),'1',N'Sửa'),'0','--') AS Sua, REPLACE(REPLACE(CAST([Del] AS nvarchar),'1',N'Xóa'),'0','--') AS Xoa, REPLACE(REPLACE(CAST([Orther] AS nvarchar),'1',N'Khác'),'0','--') AS Khac FROM tblAccountGroupPer WHERE GroupId = @GroupId"; 
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["TT"] = (i + 1);
            }
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method setDataPermission
    public int setDataPermission(int GroupId, int FunctionId, bool View, bool Add, bool Edit, bool Del, bool Orther)
    {
        int tmpValue = 0;
        try
        {
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblAccountGroupPer WHERE GroupId = @GroupId AND FunctionId = @FunctionId) ";
            sqlQuery += "BEGIN INSERT INTO tblAccountGroupPer(GroupId,FunctionId,[View],[Add],[Edit],[Del],[Orther]) VALUES(@GroupId,@FunctionId,@View,@Add,@Edit,@Del,@Orther) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblAccountGroupPer SET [View] = @View, [Add] = @Add, [Edit] = @Edit, [Del] = @Del, [Orther] = @Orther WHERE GroupId = @GroupId AND FunctionId = @FunctionId END ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.Parameters.Add("FunctionId", SqlDbType.Int).Value = FunctionId;
            Cmd.Parameters.Add("View", SqlDbType.Bit).Value = View;
            Cmd.Parameters.Add("Add", SqlDbType.Bit).Value = Add;
            Cmd.Parameters.Add("Edit", SqlDbType.Bit).Value = Edit;
            Cmd.Parameters.Add("Del", SqlDbType.Bit).Value = Del;
            Cmd.Parameters.Add("Orther", SqlDbType.Bit).Value = Orther;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            tmpValue = 1;
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method delDataPermission
    public void delDataPermission(int GroupId, int FunctionId)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "DELETE tblAccountGroupPer WHERE GroupId = @GroupId AND FunctionId = @FunctionId";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.Parameters.Add("FunctionId", SqlDbType.Int).Value = FunctionId;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
    }
    #endregion

    #endregion

    #region Category
    
    #region method getDataCategoryToCombobox
    public DataTable getDataCategoryToCombobox(int ParentId,string Account)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();                                    // LEFT JOIN tblAccountCategory ON tblAccountCategory.[CategoryId] = tblCategory.Id
            Cmd.CommandText = "IF (@Account = 'admin') BEGIN SELECT * FROM tblCategory  WHERE ParentId = 0 END ELSE BEGIN  SELECT tblCategory.*  FROM tblCategory LEFT JOIN tblAccountCategory ON tblAccountCategory.[CategoryId] = tblCategory.Id WHERE ParentId = 0 AND UserName = @Account  AND tblCategory.State = 1 END";
           // SELECT * FROM tblCategory LEFT JOIN tblAccountCategory ON tblAccountCategory.[CategoryId] = tblCategory.Id WHERE ParentId = @ParentId AND UserName = @Account AND tblCategory.State = 1 ";
            Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
            objTable.Rows.Add(0, "Nhóm sản phẩm");
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataCategoryToCombobox1
    public DataTable getDataCategoryToCombobox1(int ParentId)
    {
        DataTable objTable = new DataTable();
        try
        {
            if (ParentId > 0)
            {
                SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT Id, Name FROM tblCategory WHERE ParentId = @ParentId";
                Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            objTable.Rows.Add(0, "Nhóm sản phẩm");
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataCategoryToCombobox
    public DataTable getDataCategoryToCombobox(int ParentId)
    {
        DataTable objTable = new DataTable();
        try
        {
           
                SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT Id, Name FROM tblCategory WHERE ParentId = @ParentId";
                Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
                objTable.Rows.Add(0, "Nhóm sản phẩm");
            }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataCategoryToCombobox1
    public DataTable getDataCategoryToCombobox1(int ParentId, string UserName)
    {
        DataTable objTable = new DataTable();
        try
        {
            if (ParentId > 0)
            {
                SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = " SELECT Id, Name FROM tblCategory WHERE ParentId = @ParentId AND Id IN (SELECT CategoryId FROM tblAccountCategory WHERE UserName = @UserName) ";
                Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
                Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            objTable.Rows.Add(0, "Nhóm sản phẩm");
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataCategoryToCombobox2
    public DataTable getDataCategoryToCombobox2(string Code)
    {
        DataTable objTable = new DataTable();
        try
        {
            if (Code != "0")
            {
                SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = "SELECT Code, Name FROM tblCategory WHERE ParentId = (SELECT Id FROM tblCategory WHERE Code = @Code)";
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sqlCon.Close();
                sqlCon.Dispose();
                objTable = ds.Tables[0];
            }
            objTable.Rows.Add(0, "   —— TẤT CẢ TRONG NHÓM HÀNG —— ");
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method getDataCategory
    public DataTable getDataCategory(string UserName, int itemId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, " + itemId + " AS ItemId, tblAccountCategory.Id, Name FROM tblAccountCategory, tblCategory WHERE tblAccountCategory.CategoryId = tblCategory.Id AND tblAccountCategory.UserName = @UserName";
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["TT"] = (i + 1);
            }
            sqlCon.Close();
            sqlCon.Dispose();
            objTable = ds.Tables[0];
        }
        catch
        {

        }
        return objTable;
    }
    #endregion

    #region method setDataCategory
    public int setDataCategory(string UserName, int CategoryId)
    {
        int tmpValue = 0;
        try
        {
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblAccountCategory WHERE UserName = @UserName AND CategoryId = @CategoryId) ";
            sqlQuery += "BEGIN INSERT INTO tblAccountCategory(UserName,CategoryId) VALUES(@UserName,@CategoryId) END ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.Parameters.Add("CategoryId", SqlDbType.Int).Value = CategoryId;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            tmpValue = 1;
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method delDataCategory
    public void delDataCategory(string UserName, int Id)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "DELETE tblAccountCategory WHERE UserName = @UserName AND Id = @Id";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
    }
    #endregion

    #endregion
}