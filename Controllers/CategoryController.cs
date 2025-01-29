using CurdOperationProductsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace CurdOperationProductsDemo.Controllers
{
    public class CategoryController : Controller
    {

        private readonly string connectionString = "Data Source=DESKTOP-5CPAHE8\\SQLEXPRESS01;Initial Catalog=ProductCategoryCrudDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";

        // Get All Categories
        public ActionResult Index()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Category", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            return View(categories);
        }

        // Add a new category (GET and POST)
        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@CategoryName)", conn);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Edit category (GET and POST)
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE CategoryId = @CategoryId", conn);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", conn);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Delete category
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE CategoryId = @CategoryId", conn);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}

