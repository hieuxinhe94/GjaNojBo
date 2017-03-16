using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class ReportConfig
{
    #region method ReportConfig
    public ReportConfig()
	{
	}
    #endregion

    #region setData
    public int setData(int Id, string HeaderReport, string FooterReport, bool State)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblReportConfig WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblReportConfig(Id,HeaderReport,FooterReport,State) VALUES(@Id,@HeaderReport,@FooterReport,@State) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblReportConfig SET HeaderReport = @HeaderReport, FooterReport = @FooterReport, State = @State WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("HeaderReport", SqlDbType.NVarChar).Value = HeaderReport;
            Cmd.Parameters.Add("FooterReport", SqlDbType.NVarChar).Value = FooterReport;
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
            Cmd.CommandText = "SELECT * FROM tblReportConfig WHERE Id = 1 ";
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

    #region method getDataOnHOme
    public DataTable getDataOnHOme()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblReportConfig WHERE Id = 1 AND State = 1";
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