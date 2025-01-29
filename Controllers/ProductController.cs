using CurdOperationProductsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;


namespace CurdOperationProductsDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly string connectionString = "Data Source=DESKTOP-5CPAHE8\\SQLEXPRESS01;Initial Catalog=ProductCategoryCrudDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";

        // Get All Products
        public ActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.ProductId, p.ProductName, c.CategoryId, c.CategoryName FROM Product p INNER JOIN Category c ON p.CategoryId = c.CategoryId", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }

            var paginatedProducts = PaginatedList<Product>.Create(products, pageIndex, pageSize);
            return View(paginatedProducts);
        }

        // Add a new product (GET and POST)
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName, CategoryId) VALUES (@ProductName, @CategoryId)", conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Helper to get categories
        private List<Category> GetCategories()
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
            return categories;
        }

        // Edit product (GET)
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.ProductId, p.ProductName, p.CategoryId, c.CategoryName FROM Product p INNER JOIN Category c ON p.CategoryId = c.CategoryId WHERE p.ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
            }

            ViewBag.Categories = GetCategories(); // Populate categories dropdown
            return View(product);
        }



        // Edit product (POST)
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName = @ProductName, CategoryId = @CategoryId WHERE ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }


        // Delete product (similar to Category Delete logic)
        // Delete GET Action
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.ProductId, p.ProductName, c.CategoryName FROM Product p INNER JOIN Category c ON p.CategoryId = c.CategoryId WHERE p.ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
            }

            if (product == null)
            {
                return NotFound(); // If product not found
            }

            return View(product); // Pass product to the view
        }



        // Delete POST Action
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index"); // Redirect to Product List after deletion
        }
    }
}
