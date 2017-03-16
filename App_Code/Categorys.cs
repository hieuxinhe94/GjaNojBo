using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Categorys
{
    #region method Categorys
    public Categorys()
    {
    } 
    #endregion

    #region addCategory
    public int addCategory(int id, string code, string name, bool state, int ParentId, string UrlImage = "")
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblCategory WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblCategory(Code,[Name],[State],ParentId,UrlImage) VALUES(@Code,@Name,@State, @ParentId,@UrlImage) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblCategory SET Code = @Code, Name = @Name, State = @State, ParentId = @ParentId , UrlImage = @UrlImage WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value =code;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value =name;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = state;
            Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
            Cmd.Parameters.Add("UrlImage", SqlDbType.NVarChar).Value = UrlImage;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            throw new Exception("Không thể thêm , hoặc cập nhập Danh mục " + " Mã Lổi : " + "Cat_1");
        }
    }
    #endregion

    #region updateCategory 
    public int updateCategory(int id, string code, string name, bool state, int positionPageIndex, int positionMenuIndex)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblCategory WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblCategory(Code,Name,State,PositionPage,PositionMenu) VALUES(@Code,@Name,@State, @PositionPage, @PositionMenu) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblCategory SET Code = @Code,Name = @Name,State = @State,PositionPage = @PositionMenu,PositionMenu WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = code;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = name;
            Cmd.Parameters.Add("State", SqlDbType.NVarChar).Value = state;
            Cmd.Parameters.Add("PositionMenu", SqlDbType.NVarChar).Value =positionMenuIndex;
            Cmd.Parameters.Add("PositionPageIndex", SqlDbType.NVarChar).Value = positionPageIndex;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            throw new Exception("Không thể thêm , hoặc cập nhập Danh mục " + " Mã Lổi : " + "Cat_1");
        }
    }
    #endregion

    #region delData 
    public int delData(int id)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery += "DELETE  tblCategory WHERE Id = @Id";
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

    #region method getData
    public DataTable getData()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT,  REPLACE(REPLACE(CAST(State AS nvarchar),'1','-'),'0','x') AS StateName,  *, (SELECT Name FROM tblCategory WHERE Id = A.ParentId) AS ParentName FROM tblCategory A";
         
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

    #region method getData
    public DataTable getData(int ParentId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT,  REPLACE(REPLACE(CAST(State AS nvarchar),'1','-'),'0','x') AS StateName,  *, (SELECT Name FROM tblCategory WHERE Id = A.ParentId) AS ParentName FROM tblCategory A WHERE A.ParentId = @ParentId";
            Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
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

    #region method getDataAsLeftMenu
    public DataTable getDataAsLeftMenu(string PartnerId)
    {
        DataTable objTable = new DataTable();
        if (PartnerId == "")
        {
            return objTable;
        }
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = " SELECT 0 AS TT,  * FROM tblCategory WHERE State = 1 AND Code IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner WHERE PartnerId = @PartnerId) ORDER BY PositionMenuIndex ";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
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

    #region method getChildDataAsLeftMenuVer2   || Cách 2
    public DataTable getChildDataAsLeftMenuVer2(string PartnerId,String ParentId)
    {
        DataTable objTable = new DataTable();
        if (PartnerId == "")
        {
            return objTable;
        }
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = " IF (@PartnerId = 1008 )  BEGIN SELECT  0 AS TT,  * FROM tblCategory WHERE State = 1 AND Code IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner ) AND ParentId = @ParentId ORDER BY Name  END "+
                "ELSE BEGIN  SELECT  0 AS TT,  * FROM tblCategory WHERE State = 1 AND Code IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner WHERE PartnerId = @PartnerId) AND ParentId = @ParentId ORDER BY Name END  ";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("ParentId", SqlDbType.Int).Value = ParentId;
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

    #region method getDataAsLeftMenuVer2  Cách2
    public DataTable getDataAsLeftMenuVer2(string PartnerId)
    {
        DataTable objTable = new DataTable();
        if (PartnerId == "")
        {
            return objTable;
        }
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblCategory WHERE ParentId = 0 AND State = 1  AND  Id IN (SELECT DISTINCT tblCategory.ParentId FROM tblCategory WHERE State = 1 AND Code IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner WHERE PartnerId = @PartnerId )  )  ";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
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

    #region method getDataOnHomePage
    public DataTable getDataOnHomePage(string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP 5 0 AS TT,  * FROM tblCategory WHERE State = 1 AND Code IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner WHERE PartnerId = @PartnerId) ORDER BY PositionPageIndex";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
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

    #region method getDataByCategory
    public DataTable getDataByCategory(int CatId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP 5 0 AS TT,  * FROM tblCategory WHERE State = 1 AND Id = @Id ORDER BY PositionPageIndex";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = CatId;
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

    #region method IsParentCategory
    public bool IsParentCategory(int CatId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM [tblCategory] WHERE Id = @Id AND ParentId = 0 AND State = 1 ";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = CatId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

          

            sqlCon.Close();
            sqlCon.Dispose();
            return (objTable.Rows.Count > 0 ? true : false);
        }
        catch
        {
        }
        return false;
    }
    #endregion

    #region method getCategoryById
    public DataTable getCategoryById(int Id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT,  * FROM tblCategory Where Id = @id ";
            Cmd.Parameters.Add("id", SqlDbType.Int).Value = Id;
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
            throw new Exception("Không thể Đổ dử liệu ra dataTable");
        }
        return objTable;
    }
    #endregion

    #region method getCategoryIdByCode
    public int getCategoryIdByCode(string Code)
    {
        int tmpValue = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Id FROM tblCategory Where Code = @Code ";
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while(Rd.Read())
            {
                tmpValue = int.Parse(Rd["Id"].ToString());
            }
            Rd.Close();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
            tmpValue = 0;
        }
        return tmpValue;
    }
    #endregion

    #region method getDataForCombobox
    public DataTable getDataForCombobox(int PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Code, Name FROM tblCategory WHERE Code NOT IN (SELECT DISTINCT CategoryCode FROM tblCategoryPartner WHERE PartnerId = @PartnerId) AND ParentId = 0 ORDER BY Name";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
           
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            objTable = ds.Tables[0];
            objTable.Rows.Add(0, " —— TẤT CẢ NHÓM HÀNG —— ");
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region Data Partner

    #region setDataPartner
    public int setDataPartner(string CategoryCode, string PartnerId, string PartnerName)
    {
        try
        {
            if (PartnerId == "" || PartnerId == "0" || CategoryCode == "0" || CategoryCode == "")
            {
                return 0;
            }
            int CategoryId = 0;
            CategoryId = this.getCategoryIdByCode(CategoryCode);
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblCategoryPartner WHERE CategoryCode = @CategoryCode AND PartnerId = @PartnerId)";
            sqlQuery += "BEGIN INSERT INTO tblCategoryPartner(CategoryId,CategoryCode,PartnerId,PartnerName,DayCreate) VALUES(@CategoryId,@CategoryCode,@PartnerId,@PartnerName,getdate()) END ";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("CategoryId", SqlDbType.Int).Value = CategoryId;
            Cmd.Parameters.Add("CategoryCode", SqlDbType.NVarChar).Value = CategoryCode;
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("PartnerName", SqlDbType.NVarChar).Value = PartnerName;
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

    #region method getDataPartner
    public DataTable getDataPartner(string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, ISNULL((SELECT Name FROM tblCategory WHERE Code = tblCategoryPartner.CategoryCode),'') AS CategoryName FROM tblCategoryPartner WHERE PartnerId = @PartnerId";
            Cmd.Parameters.Add("PartnerId", SqlDbType.NVarChar).Value = PartnerId;
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

    #region method getDataPartnerById
    public DataTable getDataPartnerById(string CategoryId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, * FROM tblCategoryPartner WHERE CategoryCode = (SELECT TOP 1 Code FROM tblCategory WHERE Id = @CategoryId)";
            Cmd.Parameters.Add("CategoryId", SqlDbType.Int).Value = CategoryId;
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

    #region delDataPartner
    public int delDataPartner(string CategoryCode, string PartnerId)
    {
        try
        {
            if (PartnerId == "")
            {
                PartnerId = "0";
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblCategoryPartner WHERE CategoryCode = @CategoryCode AND PartnerId = @PartnerId";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("CategoryCode", SqlDbType.NVarChar).Value = CategoryCode;
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

    #region getChildCategoryInParentFilerByAccount
   // public DataTable getChildCategoryInParentFilerByAccount(string account,string ParentCategoryId)
 //   {



 //   }
    
    #endregion
}