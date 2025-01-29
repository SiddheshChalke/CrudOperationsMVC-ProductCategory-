//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;


//namespace CurdOperationProductsDemo.Models
//{
//    public class ProductDAL
//    {
//        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//        public List<Product> GetProducts(int pageNumber, int pageSize, out int totalRecords)
//        {
//            List<Product> products = new List<Product>();
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("GetPagedProducts", con);
//                cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
//                cmd.Parameters.AddWithValue("@PageSize", pageSize);
//                SqlParameter outputParam = new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };
//                cmd.Parameters.Add(outputParam);
//                con.Open();

//                SqlDataReader dr = cmd.ExecuteReader();
//                while (dr.Read())
//                {
//                    products.Add(new Product
//                    {
//                        ProductId = (int)dr["ProductId"],
//                        ProductName = dr["ProductName"].ToString(),
//                        CategoryId = (int)dr["CategoryId"],
//                        CategoryName = dr["CategoryName"].ToString()
//                    });
//                }

//                totalRecords = (int)outputParam.Value;
//            }
//            return products;
//        }
//    }
//}
