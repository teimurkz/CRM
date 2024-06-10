using System.Data;

namespace CRM.Models;

using System.Data.SqlClient;

public class Dal
{
    public Response Registration(Registration registration, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd =
            new SqlCommand("INSERT INTO Registration(Name,Email,Password,PhoneNumber,IsActive,IsApproved) VALUES ('" +
                           registration.Name + "','" + registration.Email + "','" + registration.Password + "','" +
                           registration.PhoneNumber + "',1,0)", connection);

        
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Registration succsessfull";
            
        }
        else
        {
                response.StatusCode = 100;
                response.StatusMessage = "Registration failed";
            
        }
        return response;
    }

    public Response Login(Registration registration,SqlConnection connection)
    {
        SqlDataAdapter dataAdapter =
            new SqlDataAdapter(
                "SELECT * FROM Registration WHERE Email = '" + registration.Email + "'AND Password = '" +
                registration.Password + "'", connection);
        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);
        Response response = new Response();
        if (dt.Rows.Count > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Registration succsessfull";
            Registration reg = new Registration();
            reg.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
            reg.Email = Convert.ToString(dt.Rows[0]["Email"]);
            response.Registration = reg;
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Registration failed";
            response.Registration = null;
        }

        return response;
    }

    public Response UserApproval(Registration registration,SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd = new SqlCommand("UPDATE Registration SET IsApproved = 1 WHERE Id = '" + registration.Id + "'AND IsActive = 1'",
            connection);
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "User Approved";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "User approval filed";  
        }
        return response;
    }

    public Response News(News news, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd =
            new SqlCommand(
                "INSERT INTO News(Titile,Content,Email,IsActive,CreatedOn) VALUES ('" + news.Title + "','" +
                news.Content + "','" + news.Email + "','1',GETDATE())", connection);
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "News created";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "News creation failed";
        }
        
        return response;
    }

    public Response NewsList(SqlConnection connection)
    {
        Response response = new Response();
        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM News WHERE IsActive = 1", connection);
        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);
        List<News> lstNews = new List<News>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                News news = new News();
                news.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                news.Title = Convert.ToString(dt.Rows[i]["Title"]);
                news.Content = Convert.ToString(dt.Rows[i]["Content"]);
                news.Email = Convert.ToString(dt.Rows[i]["Email"]);
                news.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                news.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                lstNews.Add(news);
            }

            if (lstNews.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "News data found";
                response.listNews = lstNews;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = " No News data found";
                response.listNews = null; 
            }
          
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = " No News data found";
            response.listNews = null;  
        }
        return response;
    }
}