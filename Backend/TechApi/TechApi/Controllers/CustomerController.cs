using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TechApi.Models;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private const string ConnectionString =
            "Server=DESKTOP-JMDLINI\\SQLEXPRESS01;Database=TechApi;Trusted_Connection=True;MultipleActiveResultSets=True;";

        [HttpPost]
        public ActionResult SaveCustomerDetails(CustomerRequestDto customerRequestDto)
        {
            if (!DateTime.TryParse(customerRequestDto.RegistrationDate, out DateTime registrationDate))
            {
                return BadRequest("Invalid RegistrationDate format. Use yyyy-MM-dd.");
            }

            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_SaveCustomerDetails", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Customer_ID", customerRequestDto.CustomerId);
            command.Parameters.AddWithValue("@Customer_name", customerRequestDto.CustomerName);
            command.Parameters.AddWithValue("@Customer_email", customerRequestDto.CustomerEmail);
            command.Parameters.AddWithValue("@Customer_phonenumber", customerRequestDto.CustomerPhone);
            command.Parameters.AddWithValue("@Resgistation_Date", registrationDate);

            connection.Open();
            command.ExecuteNonQuery();

            return Ok("Customer data saved successfully.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomerDetails()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_GetCustomerDetails", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            connection.Open();

            List<CustomerDto> response = new List<CustomerDto>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CustomerDto customer = new CustomerDto
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("Customer_ID")),
                        CustomerName = reader.GetString(reader.GetOrdinal("Customer_name")),
                        CustomerEmail = reader.GetString(reader.GetOrdinal("Customer_email")),
                        CustomerPhone = reader.GetString(reader.GetOrdinal("Customer_phonenumber")),
                        RegistrationDate = reader.GetDateTime(reader.GetOrdinal("Resgistation_Date")).ToString("yyyy-MM-dd")
                    };
                    response.Add(customer);
                }
            }

            return Ok(response);
        }

        [HttpPut("{CustomerId}")]
        public ActionResult UpdateCustomerDetails(int CustomerId, CustomerRequestDto customerRequestDto)
        {
            if (!DateTime.TryParse(customerRequestDto.RegistrationDate, out DateTime registrationDate))
            {
                return BadRequest("Invalid RegistrationDate format. Use yyyy-MM-dd.");
            }

            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_UpdateCustomerDetails", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Customer_ID", CustomerId);
            command.Parameters.AddWithValue("@Customer_name", customerRequestDto.CustomerName);
            command.Parameters.AddWithValue("@Customer_email", customerRequestDto.CustomerEmail);
            command.Parameters.AddWithValue("@Customer_phonenumber", customerRequestDto.CustomerPhone);
            command.Parameters.AddWithValue("@Resgistation_Date", registrationDate);

            connection.Open();
            command.ExecuteNonQuery();

            return Ok("Customer Detail updated successfully.");
        }

        [HttpDelete("{CustomerId}")]
        public ActionResult DeleteCustomerDetails(int CustomerId)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand("sp_DeleteCustomerDetails", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Customer_ID", CustomerId);

            connection.Open();
            command.ExecuteNonQuery();

            return NoContent();
        }
    }
}