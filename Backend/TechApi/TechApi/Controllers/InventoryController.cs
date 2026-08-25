using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TechApi.Models;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly string ConnectionString;

        public InventoryController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        [HttpPost]
        public ActionResult SaveInventoryData(Inventory InventoryDto)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_SaveInventoryData", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ProductID", InventoryDto.ProductID);
            command.Parameters.AddWithValue("@Productname", InventoryDto.Productname);
            command.Parameters.AddWithValue("@Avaliblestock", InventoryDto.Avaliblestock);
            command.Parameters.AddWithValue("@Reorderstock", InventoryDto.Reorderstock);

            connection.Open();
            command.ExecuteNonQuery();

            return Ok("Inventory data saved successfully.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Inventory>> GetInventoryData()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_GetInventoryData", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            connection.Open();

            List<Inventory> response = new List<Inventory>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Inventory inventory = new Inventory
                    {
                        ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                        Productname = reader.GetString(reader.GetOrdinal("Productname")),
                        Avaliblestock = reader.GetInt32(reader.GetOrdinal("Avaliblestock")),
                        Reorderstock = reader.GetInt32(reader.GetOrdinal("Reorderstock"))
                    };
                    response.Add(inventory);
                }
            }

            return Ok(response);
        }

        [HttpPut("{ProductID}")]
        public ActionResult UpdateInventoryData(int ProductID, Inventory InventoryDto)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_UpdateInventoryData", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ProductID", ProductID);
            command.Parameters.AddWithValue("@Productname", InventoryDto.Productname);
            command.Parameters.AddWithValue("@Avaliblestock", InventoryDto.Avaliblestock);
            command.Parameters.AddWithValue("@Reorderstock", InventoryDto.Reorderstock);

            connection.Open();
            command.ExecuteNonQuery();

            return Ok("Inventory data updated successfully.");
        }

        [HttpDelete("{ProductID}")]
        public ActionResult DeleteInventoryData(int ProductID)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_DeleteInventoryData", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ProductID", ProductID);

            connection.Open();
            command.ExecuteNonQuery();

            return NoContent(); // 204 — koi body nahi, koi parsing issue nahi
        }
    }
}
