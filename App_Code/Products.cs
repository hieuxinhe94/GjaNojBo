using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Products
{
    #region method Products
    public Products()
    {
    } 
    #endregion
   
    #region setProduct
    public int setProduct(int Id, string Code, string Name, string Specification, double Price, double Discount, bool State, string UrlImage, string IntroContent, int Madein, double Guarantee, int CatId, string Capacity)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblProduct WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblProduct( [Code],[Name],[Specification],[Price],[Discount],[State],[UrlImage],[IntroContent],[Madein],[Guarantee],[CatId],Capacity) VALUES(@Code,@Name, @Specification, @Price,@Discount,@State,@UrlImage,@IntroContent,@Madein,@Guarantee,@CatId,@Capacity) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblProduct SET Code = @Code, Name = @Name, Specification = @Specification, Price = @Price, Discount = @Discount, State = @State, UrlImage = @UrlImage, IntroContent = @IntroContent, Madein = @Madein, Guarantee = @Guarantee, CatId = @CatId, Capacity = @Capacity WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = Code;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            Cmd.Parameters.Add("Specification", SqlDbType.NVarChar).Value = Specification;
            Cmd.Parameters.Add("Capacity", SqlDbType.NVarChar).Value = Capacity;
            Cmd.Parameters.Add("Price", SqlDbType.Float).Value = Price;
            Cmd.Parameters.Add("Discount", SqlDbType.Float).Value = Discount;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = State;
            Cmd.Parameters.Add("UrlImage", SqlDbType.NVarChar).Value = UrlImage;
            Cmd.Parameters.Add("IntroContent", SqlDbType.NVarChar).Value = IntroContent;
            Cmd.Parameters.Add("MadeIn", SqlDbType.Int).Value = Madein;
            Cmd.Parameters.Add("Guarantee", SqlDbType.Float).Value = Guarantee;
            Cmd.Parameters.Add("CatId", SqlDbType.Int).Value = CatId;
            Cmd.ExecuteNonQuery();

            //Cap nhat thong bao toi khach hang lien quan
            if (Id > 0)
            {
                sqlQuery = "SELECT DISTINCT PartnerId FROM tblCategoryPartner WHERE CategoryCode = (SELECT Code FROM tblCategory WHERE Id = @CatId)";
                Cmd.CommandText = sqlQuery;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while(Rd.Read())
                {
                    this.setProductPartner(Id, Price, int.Parse(Rd["PartnerId"].ToString()));
                }
                Rd.Close();
            }

            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            throw new Exception("Không thể thêm , hoặc cập nhập Danh mục " + " Mã Lổi : " + " Trùng Mã Sản phẩm ");
        }
    }
    #endregion

    #region setPrice
    public int setPrice(int Id, double Price, string accountChange = "", string CategoryId = "")
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery += "   IF(@Price = (SELECT  Price FROM tblProduct where Id = @Id )) " +
				" BEGIN "+
				"	UPDATE tblProduct SET Price= 's'   WHERE Id = -1 "+             // break error
				" END "+
				" ELSE "+
				" BEGIN "+
                " UPDATE tblProduct SET Price = @Price WHERE Id = @Id " +
				" END ";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            Cmd.Parameters.Add("Price", SqlDbType.Float).Value = Price;
            Cmd.ExecuteNonQuery();

            // Thực hiện thành công câu lệnh trên/ Cap nhat thong bao toi khach hang lien quan
            #region setNotice
            if (Id > 0)
            {
                sqlQuery = "SELECT DISTINCT PartnerId FROM tblCategoryPartner WHERE CategoryCode = (SELECT Code FROM tblCategory WHERE Id = (SELECT CatId FROM tblProduct WHERE Id = @Id))";
                Cmd.CommandText = sqlQuery;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    this.setProductPartner(Id, Price, int.Parse(Rd["PartnerId"].ToString()));
                }
                Rd.Close();
            }
            #endregion

            if(accountChange!= "" && CategoryId!= "")
            { 
            #region setMsg
            // send notice for inbox 
            PartnerSMS objCreateSMS = new PartnerSMS();
            // lấy danh sách những khách hàng được phân quyền đã
            Partners objPartner = new Partners();
            DataTable objTb = new DataTable();
            objTb = objPartner.getPartnerByCat(int.Parse(CategoryId));
            if (objTb.Rows.Count > 0)
            {

                for (int j = 0; j < objTb.Rows.Count; j++)
                {
                    if (objPartner.getPartnerByAccout(objTb.Rows[j]["AccountName"].ToString()).Rows.Count > 0)
                    {
                        try
                        {
                            objCreateSMS.setData(" THAY ĐÔI GIÁ  "
                                , " " + getDataById(Id).Rows[0]["Name"].ToString() + "  GIÁ THAY ĐỔI THÀNH   " + Price + " VNĐ . ",
                                    int.Parse(objPartner.getPartnerByAccout(objTb.Rows[j]["AccountName"].ToString()).Rows[0]["Id"].ToString()),
                                    objTb.Rows[j]["AccountName"].ToString()
                                    ,accountChange);
                        }
                        catch { }
                    }
                }

            }

            #endregion
            }
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

    #region method setProductPartner
    private void setProductPartner(int ProductId, double Price, int PartnerId)
    {
        SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
        sqlCon.Open();
        string sqlQuery = "";
        sqlQuery = "IF NOT EXISTS (SELECT * FROM tblProductPartner WHERE ProductId = @ProductId AND PartnerId = @PartnerId) ";
        sqlQuery += "BEGIN INSERT INTO tblProductPartner(ProductId,Price,PriceOld,DayChange,PartnerId,Note,State) VALUES(@ProductId,@Price,@PriceOld,getdate(),@PartnerId,N'Sản phẩm mới',0) END ";
        sqlQuery += "ELSE BEGIN UPDATE tblProductPartner SET PriceOld = Price, Price = @Price, DayChange = getdate(), Note = N'Giá thay đổi', State = 0 WHERE ProductId = @ProductId AND PartnerId = @PartnerId AND Price <> @Price END";
        SqlCommand Cmd = sqlCon.CreateCommand();
        Cmd.CommandText = sqlQuery;
        Cmd.Parameters.Add("ProductId",SqlDbType.Int).Value = ProductId;
        Cmd.Parameters.Add("Price", SqlDbType.Float).Value = Price;
        Cmd.Parameters.Add("PriceOld", SqlDbType.Float).Value = Price;
        Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
        Cmd.ExecuteNonQuery();
        sqlCon.Close();
        sqlCon.Dispose();
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
            Cmd.CommandText = "SELECT COUNT(*) AS CountItem FROM tblProductPartner WHERE PartnerId = @PartnerId AND ISNULL(State,0) = 0";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
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

    #region updateProducts 
    public int updateProduct(int id,string code,string name,string name_short,double price,double discount,bool state,string urlImg,string introContent,int madeIn,double guarantee)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            string sqlQuery = "";
            sqlQuery = "IF NOT EXISTS (SELECT * FROM tblProduct WHERE Id = @Id)";
            sqlQuery += "BEGIN INSERT INTO tblProduct( [Code],[Name],[NameShort],[Price],[Discount],[State],[UrlImage],[IntroContent],[Madein],[Guarantee]) VALUES(@Code,@Name, @NameShort, @Price,Discount,@State,@UrlImage,@IntroContent,@Madein,@Guarantee) END ";
            sqlQuery += "ELSE BEGIN UPDATE tblProduct SET Code = @Code,Name = @Name,NameShort = @NameShort,Price = @Price,Discount=@Discount, State = @State,UrlImage=@UrlImage,IntroContent =@IntroContent,Madein=@Madein, Guarantee=@Guarantee WHERE Id = @Id END";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = 0;
            Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = code;
            Cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = name;
            Cmd.Parameters.Add("NameShort", SqlDbType.NVarChar).Value = name_short;
            Cmd.Parameters.Add("Price", SqlDbType.Float).Value = price;
            Cmd.Parameters.Add("Discount", SqlDbType.Float).Value = discount;
            Cmd.Parameters.Add("State", SqlDbType.Bit).Value = state;
            Cmd.Parameters.Add("UrlImage", SqlDbType.NVarChar).Value = urlImg;
            Cmd.Parameters.Add("IntroContent", SqlDbType.NVarChar).Value = introContent;
            Cmd.Parameters.Add("MadeIn", SqlDbType.Int).Value = madeIn;
            Cmd.Parameters.Add("Guarantee", SqlDbType.Float).Value = guarantee;
            Cmd.ExecuteNonQuery(); Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch
        {
            throw new Exception("Không thể hoặc cập nhập Danh mục " + " Mã Lổi : " + "Product.update");
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
            sqlQuery = "DELETE  tblProduct WHERE Id = @Id";
            Cmd.CommandText = sqlQuery;
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = id;
            Cmd.ExecuteNonQuery();
            sqlCon.Close();
            sqlCon.Dispose();
            return 1;
        }
        catch (Exception )
        {
            throw new Exception("Không thể xóa  Danh mục " + " Mã Lổi : " + "Product.Remove");
        }
    }
    #endregion

    #region method selectAllProduct
    public DataTable getAllProducts()
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, REPLACE(REPLACE(CAST(State AS nvarchar),'1','-'),'0','x') AS StateName, * FROM tblProduct";
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
            throw new Exception("Không thể Đổ dử liệu ra dataTable");
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
            Cmd.CommandText = "SELECT * FROM tblProduct WHERE Id = @Id";
            Cmd.Parameters.Add("Id",SqlDbType.Int).Value = Id;
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

    #region method getPriceById
    public double getPriceById(int Id)
    {
        double tmpValue = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT ISNULL(Price,0) AS Price FROM tblProduct WHERE Id = @Id";
            Cmd.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while(Rd.Read())
            {
                tmpValue = double.Parse(Rd["Price"].ToString());
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

    #region method getDataByCategoryId
    public DataTable getDataByCategoryId(int CatId, string SearchKey, string shortBy = "tblProduct.Name")
    {
        string sqlQueryName = "";
        if (SearchKey.Trim() != "")
        {
            sqlQueryName = " AND UPPER(RTRIM(LTRIM(Name))) LIKE N'%'+UPPER(RTRIM(LTRIM(@SearchKey)))+'%'  "+
                           " OR UPPER(RTRIM(LTRIM(Code))) LIKE N'%'+UPPER(RTRIM(LTRIM(@SearchKey)))+'%'   ";
        }
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT 0 AS TT, REPLACE(REPLACE(CAST(State AS nvarchar),'1',N'Sử dụng'),'0','Đóng') AS StateName, * FROM tblProduct WHERE CatId = @CatId " 
                + sqlQueryName 
                + " ORDER BY "+shortBy+" ";
            Cmd.Parameters.Add("CatId", SqlDbType.Int).Value = CatId;
            Cmd.Parameters.Add("SearchKey", SqlDbType.NVarChar).Value = SearchKey;
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
            throw new Exception("Không thể Đổ dử liệu ra dataTable");
        }
        return objTable;
    }
    #endregion

    #region method getDataByCategoryId
    public DataTable getDataByCategoryId(int CatId, int Number)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP " + Number + " 0 AS TT, * FROM tblProduct WHERE State = 1 AND CatId = @CatId";
            Cmd.Parameters.Add("CatId", SqlDbType.Int).Value = CatId;
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

    #region method getDataNewPost
    public DataTable getDataNewPost(int Number, string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP " + Number + " 0 AS TT, * FROM tblProduct WHERE State = 1 AND CatId IN (SELECT DISTINCT CategoryId FROM tblCategoryPartner WHERE PartnerId = @PartnerId) ORDER BY Id DESC";
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

    #region method getDataNewPostFilterByCatParent
    public DataTable getDataNewPostFilterByCatParent(int Number, string PartnerId,string ParentCategoryId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP " + Number + " 0 AS TT,tblCategory.Name, *  FROM tblProduct" +
                " LEFT JOIN tblCategory ON  tblCategory.Id = tblProduct.CatId  "+
                " WHERE tblProduct.State = 1 AND "+
                " CatId IN (SELECT DISTINCT CategoryId FROM tblCategoryPartner WHERE PartnerId = @PartnerId) " +
                " AND tblCategory.ParentId  = @ParentCategoryId "+
                " ORDER BY tblProduct.Id DESC ";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("ParentCategoryId", SqlDbType.Int).Value = ParentCategoryId;
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

    #region method getListCategoryFilterByCatParentAndAccount
    public DataTable getListCategoryFilterByCatParentAndAccount( string PartnerId, string ParentCategoryId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT DISTINCT tblCategory.Id , tblCategory.Name "
                        + "  from tblCategory left  join tblCategoryPartner  "
                        + " on tblCategory.Id = tblCategoryPartner.CategoryId "
                        + " Where tblCategoryPartner.PartnerId = @PartnerId"
                        + " AND tblCategory.ParentId = @ParentCategoryId "
                        + " AND tblCategory.State =1 ";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("ParentCategoryId", SqlDbType.Int).Value = ParentCategoryId;
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

    #region method getDataPartnerProductByCatId
    public DataTable getDataPartnerProductByCatId(int Number, string PartnerId, int CatId)
    {
        string sqlQueryCatId = "";
        if (CatId != 0)
        {
            sqlQueryCatId = " AND CatId = @CatId";
        }
        else
        {
            sqlQueryCatId = " AND CatId IN (SELECT DISTINCT CategoryId FROM tblCategoryPartner WHERE PartnerId = @PartnerId)";
        }
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT TOP " + Number + " 0 AS TT, * FROM tblProduct WHERE State = 1 "+sqlQueryCatId+"  ORDER BY Id DESC";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            Cmd.Parameters.Add("CatId", SqlDbType.Int).Value = CatId;
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

    #region method getDataPartnerCategory
    public DataTable getDataPartnerCategory(string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT DISTINCT CategoryId, (SELECT Name FROM tblCategory WHERE Id = CategoryId) AS CategoryName FROM tblCategoryPartner WHERE PartnerId = @PartnerId";
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

    #region method getDataPartnerProductPrice
    public DataTable getDataPartnerProductPrice(string PartnerId)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT A.Code, A.Name, A.Price, B.PriceOld, B.DayChange, B.Note FROM [dbo].[tblProduct] A,[dbo].[tblProductPartner] B WHERE A.Id = B.ProductId AND B.PartnerId = @PartnerId AND ISNULL(B.[State],0) = 0";
            Cmd.Parameters.Add("PartnerId", SqlDbType.Int).Value = PartnerId;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            objTable = ds.Tables[0];

            Cmd.CommandText = "UPDATE tblProductPartner SET State = 1 WHERE PartnerId = @PartnerId";
            Cmd.ExecuteNonQuery();

            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {
        }
        return objTable;
    }
    #endregion

    #region method getDataSearch
    public DataTable getDataSearch(string SearchKey)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand(); 
            Cmd.CommandText = "SELECT * FROM tblProduct WHERE UPPER(RTRIM(LTRIM(Name))) LIKE N'%'+UPPER(RTRIM(LTRIM(@SearchKey)))+'%' AND State = 1  ORDER BY Id DESC";
            Cmd.Parameters.Add("SearchKey",SqlDbType.NVarChar).Value = SearchKey;
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

    #region method getProductByAccount 
    public int getPartnerIdByAccount(string Account)
    {
        int productId = 0;
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT Id FROM tblProduct WHERE Account = @Account";
            Cmd.Parameters.Add("Account", SqlDbType.NVarChar).Value = Account;
            SqlDataReader Rd = Cmd.ExecuteReader();
            while (Rd.Read())
            {
                productId = int.Parse(Rd["Id"].ToString());
            }
            Rd.Close();
            sqlCon.Close();
            sqlCon.Dispose();
        }
        catch
        {

        }
        return productId;
    }
    #endregion

    #region method getProductById
    public DataTable getProductById(int id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT * FROM tblProduct WHERE Id= @Id AND State = 1";
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

        }
        return objTable;
    }
    #endregion

    #region method getProductByIdAdmin
    public DataTable getProductByIdAdmin(int id)
    {
        DataTable objTable = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TVSConn"].ConnectionString);
            sqlCon.Open();
            SqlCommand Cmd = sqlCon.CreateCommand();
            Cmd.CommandText = "SELECT tblProduct.*,tblCategory.Id as CatId ,tblCategory.Id as childId, " +
                 "(select tblCategory.Id  AS PArentName from tblCategory where tblCategory.id = (SELECT tblCategory.ParentId FROM tblProduct  INNER JOIN "+
                " tblCategory ON tblCategory.Id  =  tblProduct.CatId  "+
                " WHERE tblProduct.Id= @Id )  ) as ParentId " +
                " FROM tblProduct  INNER JOIN tblCategory ON tblCategory.Id  =  tblProduct.CatId  WHERE tblProduct.Id= @Id ";
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

        }
        return objTable;
    }
    #endregion
}