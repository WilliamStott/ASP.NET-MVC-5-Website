using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

public class DataBase
{
    private SqlConnection connection()
    {
        //Connection string is inside the top level web.config
        return new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ToString());
    }


    public async Task<DataTable> listCourses(string columnName)
    {
        DataTable dt = new DataTable();
        dt.TableName = "courses";
        try
        {
            
            using (SqlConnection conn = connection())
            {
                //Depending on if the column name is equal to all will pick the corresponding procedure
                using (SqlCommand cmd = new SqlCommand(columnName == "All" ? "GET_COURSE_ALL" : "GET_COURSE_COLUMN", conn))
                {
                    //Sets the command type to stored procuedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    //If columname is all then add the column name as a paramater for the stored procedure
                    if(columnName != "All") cmd.Parameters.Add("@columnName", SqlDbType.VarChar, 255).Value = columnName;
                    //Opens the connection as async so the main thread doesn't hang whilst waiting for a reply
                    await conn.OpenAsync();
                    //Fills the datatable with the data from the sql database
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
        }
        catch (SqlException e)
        {
            throw e;
        }
    }

    public async Task<bool> addCourse(CourseInfo ci)
    {
        try
        {
            using (SqlConnection conn = connection())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_COURSE", conn))
                {
                    //Sets the paramaters for the stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@courseName", SqlDbType.VarChar, 255).Value = ci.courseName;
                    cmd.Parameters.Add("@courseCredits", SqlDbType.Int).Value = ci.courseCredits;
                    cmd.Parameters.Add("@courseDuration", SqlDbType.Int).Value = ci.courseDuration;
                    cmd.Parameters.Add("@courseTutor", SqlDbType.VarChar, 255).Value = ci.courseTutor;
                    await conn.OpenAsync();
                    //Executes without waiting for a return
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        catch (SqlException e)
        {
            return false;
            throw e;
        }
    }

    public async Task<DataTable> searchCourses(string columnName, string searchTerm)
    {
        DataTable dt = new DataTable();
        dt.TableName = "courses";
        try
        {
            using (SqlConnection conn = connection())
            {
                //Checks if the column is set to all, picks correct procedure to execute depending
                using (SqlCommand cmd = new SqlCommand(columnName == "All" ? "SEARCH_COURSE_ALL" : "SEARCH_COURSE_COLUMN", conn))
                {
                    //Sets stored procedure peramaters 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar, 255).Value = searchTerm;
                    if (columnName != "All") cmd.Parameters.Add("@columnName", SqlDbType.VarChar, 255).Value = columnName;
                    //Open as async
                    await conn.OpenAsync();
                    //Fill datatable
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
        }
        catch (SqlException e)
        {
            throw e;
        }
    }

}