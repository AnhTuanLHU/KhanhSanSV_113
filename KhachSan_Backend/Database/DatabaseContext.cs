using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
public class DatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<User> CreateUser(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.PasswordHash);
            parameters.Add("@Phone", user.Phone);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "Users_CreateUser",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task<User> AuthenticateUser(string email, string password)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@Password", password);

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "Users_CheckLogin",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task<User> GetUserById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", id);

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "Users_GetByID",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task UpdateUserRefreshToken(int userId, string refreshToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@RefreshToken", refreshToken);

            await connection.ExecuteAsync(
                "Users_Update",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}