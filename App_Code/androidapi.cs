using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for AndroidApi
/// </summary>
[WebService(Namespace = "http://tempuri.org/tvsapi")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class androidapi : System.Web.Services.WebService
{

    PartnerSMS sms = new PartnerSMS();
    Partners objPartner = new Partners();

    public androidapi()
    {
        
    }


    #region hello
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello TVS";
    }
    #endregion

    #region getMessagByAccount
    [WebMethod]
    public string getMessageByAccount(String acc)
    {
        //  call to get  message 
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP 1 tblPartnerSMS.Id , tblPartnerSMS.SMSContent AS SMS  FROM [tblPartnerSMS] WHERE [PartnerAccount] = @PartnerAccount AND tblPartnerSMS.State = 0  ORDER BY  tblPartnerSMS.DayCreate";
            Cmd.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = acc;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            String content = ds.Tables[0].Rows[0]["Id"].ToString() + "," + ds.Tables[0].Rows[0]["SMS"].ToString();       // SMS Content .

            //  set message state  has  seen .
            SqlCommand Cmd2 = sqlCon.CreateCommand();
            Cmd2.CommandText = " UPDATE tblPartnerSMS SET tblPartnerSMS.State = 1 , tblPartnerSMS.DayView = getDate() WHERE tblPartnerSMS.PartnerAccount = @PartnerAccount AND Id = (SELECT TOP 1 Id FROM tblPartnerSMS WHERE tblPartnerSMS.PartnerAccount = @PartnerAccount AND tblPartnerSMS.State = 0 ORDER BY DayCreate   )"; //tblPartnerSMS.SMSContent = @SMSContent AND 
            Cmd2.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = acc;
            Cmd2.Parameters.Add("SMSContent", SqlDbType.NVarChar).Value = content;
            Cmd2.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
            
        }
        catch
        {
            return null;
        }
        return objTable.Rows[0]["SMS"].ToString() == "" ? null : objTable.Rows[0]["SMS"].ToString();
    }
    #endregion

    #region checkLogin
    [WebMethod]
    public Boolean checkLogin(String acc, String pass)
    {
        // what's fullname ? 
        String FullName = "";
        return objPartner.checkForLogin(acc, pass, ref FullName);
    }
    #endregion

    #region register || chưa dùng tới 
    [WebMethod]
    public Boolean registerAccount(String acc, String pass)
    {
        return false;
    }

    #endregion

    #region getJsonMessage
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]  
    public string getJsonMessage(String acc,String pass)
    {
        //  call to get  message 
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT  tblPartnerSMS.Id , tblPartnerSMS.SMSContent AS SMS,tblPartnerSMS.Title AS Title,tblPartnerSMS.DayCreate AS DayCreate  FROM [tblPartnerSMS] WHERE [PartnerAccount] = @PartnerAccount   ORDER BY  tblPartnerSMS.DayCreate";
            Cmd.Parameters.Add("PartnerAccount", SqlDbType.NVarChar).Value = acc;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];
            sqlCon.Close();
            sqlCon.Dispose();

            /*
               JavaScriptSerializer serial = new JavaScriptSerializer();
               List<Dictionary<String, Mesages>> list = new List<Dictionary<String, Mesages>>();
               Dictionary<String,Mesages> objMsg;
               foreach(DataRow rowItem in objTable.Rows)
               {
                   objMsg = new Dictionary<String,Mesages>();
                   foreach(DataColumn itemCol  in objTable.Columns)
                   {
                       objMsg.Add(itemCol.ColumnName, new Mesages(Int32.Parse(rowItem["Id"].ToString()), rowItem["Title"].ToString(), rowItem["SMS"].ToString(), rowItem["DayCreate"].ToString()));
                   }
                   list.Add(objMsg);
                }
               return "{\"Message\": " + serial.Serialize(list) + "}";
             */
            
               JavaScriptSerializer serial = new JavaScriptSerializer();
               List<Dictionary<String, object>> list = new List<Dictionary<String, object>>();
               Dictionary<String, object> objMsg;
               foreach (DataRow rowItem in objTable.Rows)
               {
                   objMsg = new Dictionary<String, object>();
                   foreach (DataColumn itemCol in objTable.Columns)
                   {
                       objMsg.Add(itemCol.ColumnName,rowItem[itemCol].ToString());
                   }
                   list.Add(objMsg);
               }
               return "{\"Message\": "+ serial.Serialize(list) + "}";
        }
        catch
        {
            return null;
        }
    }
    #endregion
}
    #region class Message
class Mesages
{
                   
    public Mesages()
    {

    }
    public Mesages(int id, String tit , String con, String date)
    {
        this.Id = id;
        this.title = tit;
        this.content = con;
        this.date = date;
    }
   public int Id {get ;set;}
   public String title { get; set; }
   public String content { get; set; }
   public String date { get; set; }

#endregion

}
