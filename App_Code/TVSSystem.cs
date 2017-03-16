using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class TVSSystem
{
    #region method TVSSystem
    public TVSSystem()
    {
    } 
    #endregion

    #region method getCountOfObjects
    public int getCountOfObjects(string Table)
    {
        int tmpValue = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT COUNT(*) AS CountItem FROM "+Table;
            SqlDataAdapter da = new SqlDataAdapter();
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

    #region method getTimeFromServer
    public String getTimeFromServer( )
    {
      
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT getdate() AS time" ;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand= Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable tb = new DataTable();
            tb = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();
            return tb.Rows[0]["time"].ToString();
        }
        catch
        {
            return "Không xác định được thời gian ...";
        }
       
    }
    #endregion

    #region insertUserOnline
    public void setOnline(string userName,string other = " ")
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "";
            sqlQuery += " INSERT INTO tblAccessInfo ([UserName],[State],[OtherInfo]) VALUES( @UserName , @State,  @OtherInfo )  ";
            sqlQuery += "";
            Cmd.CommandText = sqlQuery;

            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = userName ;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = 1;
            Cmd.Parameters.Add("OtherInfo", SqlDbType.NVarChar).Value = other;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return ;
        }
        catch
        {
            throw new Exception("  " + " Mã Lổi : " + "Onlie status");
        }
    }
    #endregion

    #region removeUserOnline
    public void removeUserOnline(string userName,string otherInfo= " ")
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            // có thể trùng -> bị lỗi , thôi kko quan trọng
            sqlQuery += "  UPDATE tblAccessInfo SET   State = @State,OtherInfo = @OtherInfo WHERE UserName = @UserName ";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = userName;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = 0;
            Cmd.Parameters.Add("OtherInfo", SqlDbType.NVarChar).Value = otherInfo ;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return ;
        }
        catch
        {
            throw new Exception(" " + " Mã Lổi : " + "Không thiết lập offline được ");
        }
    }
    #endregion

    #region gettUserOnline
    public DataTable gettUserOnline()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
          
            sqlQuery += " SELECT * FROM tblAccessInfo WHERE  tblAccessInfo.State = 1 ";
         
            Cmd.CommandText = sqlQuery;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return  ds.Tables[0];
        }
        catch
        {
           // throw new Exception("  " + " Mã Lổi : " + "Onlie status");
            return new DataTable();
        }
    }
    #endregion

    #region countUserOnline
    public int countUserOnline()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";

            sqlQuery += " SELECT COUNT(tblAccessInfo.Id) AS TotalUser FROM tblAccessInfo WHERE  tblAccessInfo.State = 1 ";

            Cmd.CommandText = sqlQuery;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);

            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return int.Parse(ds.Tables[0].Rows[0]["TotalUser"].ToString());
        }
        catch
        {
            // throw new Exception("  " + " Mã Lổi : " + "Onlie status");
            return 1;
        }
    }
    #endregion

}