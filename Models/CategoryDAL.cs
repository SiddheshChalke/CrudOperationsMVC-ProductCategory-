//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;

//namespace CurdOperationProductsDemo.Models
//{
//    public class CategoryDAL
//    {
//        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//        public List<Category> GetCategories()
//        {
//            List<Category> categories = new List<Category>();
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("SELECT * FROM Category", con);
//                con.Open();
//                SqlDataReader dr = cmd.ExecuteReader();
//                while (dr.Read())
//                {
//                    categories.Add(new Category
//                    {
//                        CategoryId = (int)dr["CategoryId"],
//                        CategoryName = dr["CategoryName"].ToString()
//                    });
//                }
//            }
//            return categories;
//        }

//        public void AddCategory(Category category)
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@CategoryName)", con);
//                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
//                con.Open();
//                cmd.ExecuteNonQuery();
//            }
//        }

//        public void UpdateCategory(Category category)
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", con);
//                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
//                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
//                con.Open();
//                cmd.ExecuteNonQuery();
//            }
//        }

//        public void DeleteCategory(int categoryId)
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE CategoryId = @CategoryId", con);
//                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
//                con.Open();
//                cmd.ExecuteNonQuery();
//            }
//        }
//    }
//}
