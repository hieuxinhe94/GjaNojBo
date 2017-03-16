using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class PartnerSMS
{
    #region method PartnerSMS
    public PartnerSMS()
    {
    } 
    #endregion

    #region method sendSMSPartnerByGroup
    public void sendSMSPartnerByGroup(string title,string SMSContent, int GroupId, string UserName)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "SELECT PartnerId, Account FROM tblPartnerGroupDetailt, tblPartner WHERE tblPartnerGroupDetailt.PartnerId = tblPartner.Id AND GroupId = @GroupId";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.Parameters.Add("GroupId", SqlDbType.Int).Value = GroupId;
            Cmd.CommandText = sqlQuery;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while(Rd.Read())
            {
                this.setData(title,SMSContent, int.Parse(Rd["PartnerId"].ToString()), Rd["Account"].ToString(), UserName);
            }
            Rd.Close();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method setData
    public void setData(string title ,string SMSContent, int PartnerId, string PartnerAccount, string UserName)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = " INSERT INTO tblPartnerSMS(Title,SMSContent,PartnerId,PartnerAccount,UserName,DayCreate,State) VALUES(@Title,@SMSContent,@PartnerId,@PartnerAccount,@UserName,getdate(),0)";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = title;
            Cmd.Parameters.Add("SMSContent", SqlDbType.NVarChar).Value = SMSContent;
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = PartnerAccount;
            Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = UserName;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method setSeenMsgByPartner
    public void setSeenMsgByPartner( string PartnerAccount)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = " UPDATE tblPartnerSMS SET tblPartnerSMS.State = 1 WHERE PartnerAccount = @PartnerAccount ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = PartnerAccount;
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
    public void delData(int id)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = "DELETE FROM  tblPartnerSMS WHERE tblPartnerSMS.Id = @Id ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method delDataWithTime
    public void delDataWithTime(int hours)
    {
        try
        {
            string sqlQuery = "";
            sqlQuery = " DELETE FROM  tblPartnerSMS  WHERE  DayCreate  <= DATEADD(dd,-@Hours,GETDATE()) ";
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("@Hours", SqlDbType.Int).Value = hours;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
    }
    #endregion

    #region method getData
    public DataTable getData(string UserName)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "IF (@UserName = 'admin') BEGIN   SELECT *, (SELECT Name FROM tblPartner WHERE Id = PartnerId) AS PartnerName, REPLACE(REPLACE(CAST(State AS nvarchar),'1','Đã xem'),'0','Chưa xem') AS StateName FROM [dbo].[tblPartnerSMS] ORDER BY DayCreate DESC  END " +
                " ELSE BEGIN   SELECT *, (SELECT Name FROM tblPartner WHERE Id = PartnerId) AS PartnerName, REPLACE(REPLACE(CAST(State AS nvarchar),'1','Đã xem'),'0','Chưa xem') AS StateName FROM [dbo].[tblPartnerSMS] WHERE UserName = @UserName ORDER BY DayCreate DESC END";
            Cmd.Parameters.Add("UserName",SqlDbType.NVarChar).Value = UserName;
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

    #region method getMSGbyAcount
    public DataTable getMSGbyAcount(string Account)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = " SELECT *,(SELECT Name FROM tblPartner WHERE Id = PartnerId) AS PartnerName, REPLACE(REPLACE(CAST(State AS nvarchar),'1','Đã xem'),'0','Chưa xem') AS StateName"+
                " FROM [dbo].[tblPartnerSMS] WHERE [PartnerAccount] = @PartnerAccount AND " +
                //"  ( DayCreate BETWEEN dateadd(day,datediff(day,1,GETDATE()),0) AND GETDATE() OR  " +
                "  tblPartnerSMS.State =  0  " +
                "  ORDER BY DayCreate DESC";
            Cmd.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = Account;

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
}