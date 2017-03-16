using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Quotation
{
    #region method Quotation
    public Quotation()
    {
    } 
    #endregion

    #region setData
    public int setData(int Id, string Code, string Name, string Discount, string FileUrl, int PartnerId, bool State)
    {
        try
        {
            if (Discount.Trim() == "")
            {
                Discount = "0";
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblQuotation WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblQuotation(Code,Name,DayCreate,Discount,FileUrl,PartnerId,State) VALUES(@Code,@Name,getdate(),@Discount,@FileUrl,@PartnerId, @State) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblQuotation SET Code = @Code, Name = @Name, Discount = @Discount, PartnerId = @PartnerId, State = @State, DayCreate = getdate(), FileUrl = @FileUrl WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            Cmd.Parameters.Add("Discount", SqlDbType.Float).Value = Discount;
            Cmd.Parameters.Add("PartnerId", SqlDbType.Float).Value = PartnerId;
            Cmd.Parameters.Add("FileUrl", SqlDbType.NVarChar).Value = FileUrl;
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

    #region method getData
    public DataTable getData()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT,  REPLACE(REPLACE(CAST(State AS nvarchar),'1','-'),'0','x') AS StateName,  * FROM tblQuotation ORDER BY DayCreate DESC";

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

    #region method getDataByPartnerId
    public DataTable getDataByPartnerId(string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            if (PartnerId.Trim() == "")
            {
                return objTable;
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT,  REPLACE(REPLACE(CAST(State AS nvarchar),'1','-'),'0','x') AS StateName,  * FROM tblQuotation, tblQuotationPartner WHERE tblQuotation.Code = tblQuotationPartner.QuotationCode AND tblQuotationPartner.PartnerId = @PartnerId ORDER BY tblQuotation.DayCreate DESC";
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

    #region method getData
    public DataTable getData(int Id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblQuotation WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
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

    #region method getDataByPartner
    public DataTable getDataByPartner(int Id, string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblQuotation WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

            if (objTable.Rows.Count > 0)
            {
                Cmd.CommandText = "UPDATE tblQuotationPartner SET ViewState = 1 WHERE PartnerId = @PartnerId AND QuotationCode = @QuotationCode";
                Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
                Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = objTable.Rows[0]["Code"].ToString();
                Cmd.ExecuteNonQuery();
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

    #region method getDataByPartnerUnView
    public int getDataByPartnerUnView(int PartnerId)
    {
        int tmpValue = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT COUNT(*) AS CountItem FROM tblQuotationPartner WHERE PartnerId = @PartnerId AND ISNULL(ViewState,0) = 0";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while(Rd.Read())
            {
                tmpValue = int.Parse(Rd["CountItem"].ToString());
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

    #region delData
    public void delData(string QuotationCode)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblQuotationDetailt WHERE QuotationCode = @QuotationCode";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
            Cmd.ExecuteNonQuery();

            sqlQuery = "DELETE tblQuotation WHERE Code = @QuotationCode";
            Cmd.CommandText = sqlQuery;
            Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region checkCode
    public bool checkCode(string Code)
    {
        bool tmpValue = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "SELECT * FROM tblQuotation WHERE Code = @Code";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                tmpValue = true;
            }
            Rd.Close();
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

    #region Data Detailt

    #region setDataDetailt
    public int setDataDetailt(string QuotationCode, string ProductId, string ProductName, string ProductPrice)
    {
        try
        {
            if (ProductPrice == "")
            {
                ProductPrice = "0";
            }
            if (ProductId == "")
            {
                ProductId = "0";
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblQuotationDetailt WHERE QuotationCode = @QuotationCode AND ProductId = @ProductId)";
            sqlQuery += "BEGIN INSERT INTO tblQuotationDetailt(QuotationCode,ProductId,ProductName,ProductPrice,DayCreate) VALUES(@QuotationCode,@ProductId,@ProductName,@ProductPrice,getdate()) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblQuotationDetailt SET QuotationCode = @QuotationCode, ProductName = @ProductName, ProductPrice = @ProductPrice, DayCreate = getdate() WHERE QuotationCode = @QuotationCode AND ProductId = @ProductId END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
            Cmd.Parameters.Add("ProductId", SqlDbType.Int).Value = ProductId;
            Cmd.Parameters.Add("ProductName", SqlDbType.NVarChar).Value = ProductName;
            Cmd.Parameters.Add("ProductPrice", SqlDbType.Float).Value = ProductPrice;
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

    #region method getDataDetailt
    public DataTable getDataDetailt(string QuotationId, string QuotationCode)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, '" + QuotationCode + "' AS QuotationCode, " + QuotationId + " AS QuotationId FROM tblQuotationDetailt WHERE QuotationCode = @QuotationCode";
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
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

    #region method getDataDetailtById
    public DataTable getDataDetailtById(string QuotationId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, * FROM tblQuotationDetailt WHERE QuotationCode = (SELECT TOP 1 Code FROM tblQuotation WHERE Id = @QuotationId)";
            Cmd.Parameters.Add("QuotationId", SqlDbType.Int).Value = QuotationId;
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

    #region delDataDetailt
    public int delDataDetailt(string QuotationCode, string ProductId)
    {
        try
        {
            if (ProductId == "")
            {
                ProductId = "0";
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "DELETE tblQuotationDetailt WHERE QuotationCode = @QuotationCode AND ProductId = @ProductId";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
            Cmd.Parameters.Add("ProductId", SqlDbType.Int).Value = ProductId;
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

    #region Data Partner

    #region setDataPartner
    public int setDataPartner(string QuotationCode, string PartnerId, string PartnerName)
    {
        try
        {
            if (PartnerId == "" || PartnerId == "0")
            {
                return 0;
            }
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblQuotationPartner WHERE QuotationCode = @QuotationCode AND PartnerId = @PartnerId)";
            sqlQuery += "BEGIN INSERT INTO tblQuotationPartner(QuotationCode,PartnerId,PartnerName,DayCreate) VALUES(@QuotationCode,@PartnerId,@PartnerName,getdate()) END ";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
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
    public DataTable getDataPartner(string QuotationId, string QuotationCode)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, *, '" + QuotationCode + "' AS QuotationCode, " + QuotationId + " AS QuotationId FROM tblQuotationPartner WHERE QuotationCode = @QuotationCode";
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
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
    public DataTable getDataPartnerById(string QuotationId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, * FROM tblQuotationPartner WHERE QuotationCode = (SELECT TOP 1 Code FROM tblQuotation WHERE Id = @QuotationId)";
            Cmd.Parameters.Add("QuotationId", SqlDbType.Int).Value = QuotationId;
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
    public int delDataPartner(string QuotationCode, string PartnerId)
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
            sqlQuery = "DELETE tblQuotationPartner WHERE QuotationCode = @QuotationCode AND PartnerId = @PartnerId";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("QuotationCode", SqlDbType.NVarChar).Value = QuotationCode;
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
}