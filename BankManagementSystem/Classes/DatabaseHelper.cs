using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public static class DatabaseHelper
{
  
    private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    // INSERT/UPDATE/DELETE → returns rows affected
    public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
            }

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    // For queries like SELECT COUNT(*) or INSERT ... output ID → returns single value
    public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
            }

            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
    public static SqlParameter CreateParameter(string name, object value)
    {
        return new SqlParameter(name, value);
    }

    public static void LogAction(int userId, string action)
    {
        try
        {
            // For demo purposes, if userId is 0, set to NULL (system operation)
            object userIdValue = userId > 0 ? (object)userId : DBNull.Value;
            
            // Truncate action if too long (safety measure)
            if (action.Length > 4000)
                action = action.Substring(0, 4000) + "... [TRUNCATED]";
            
            string sql = "INSERT INTO AuditLogs (UserID, Action) VALUES (@UID, @Action)";
            SqlParameter[] param = {
                CreateParameter("@UID", userIdValue),
                CreateParameter("@Action", action)
            };

            ExecuteNonQuery(sql, param);
        }
        catch (Exception ex)
        {
            // Log to Windows Event Log or Console as fallback
            System.Diagnostics.Debug.WriteLine($"Failed to log action: {ex.Message}");
            // Don't throw exception to avoid breaking the main application flow
        }
    }
}
