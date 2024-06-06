using System.Data;

namespace CRM.Models;

using System.Data.SqlClient;

public class Dal
{
    public Response Registration(Registration registration, SqlConnection connection)
    {
        Response response = new Response();
        connection.Open();
        SqlCommand checkEmailCmd = new SqlCommand(
            "SELECT COUNT(*) FROM Registration WHERE Email = @Email",
            connection
        );
        checkEmailCmd.Parameters.AddWithValue("@Email", registration.Email);
        int count = (int)checkEmailCmd.ExecuteScalar();

        if (count > 0)
        {
            response.StatusMessage = "Email already exists";
            response.StatusCode = 100;
            return response;
        }
        connection.Close();

        SqlCommand cmd = new SqlCommand(
            "INSERT INTO Registration (Name, Email, Password, PhoneNumber, IsActive, IsApproved) VALUES (@Name, @Email, @Password, @PhoneNumber, @IsActive, @IsApproved)",
            connection
        );
        cmd.Parameters.AddWithValue("@Name", registration.Name);
        cmd.Parameters.AddWithValue("@Email", registration.Email);
        cmd.Parameters.AddWithValue("@Password", registration.Password);
        cmd.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
        cmd.Parameters.AddWithValue("@IsActive", 1);
        cmd.Parameters.AddWithValue("@IsApproved", 0);
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

    public Response Login(Registration registration, SqlConnection connection)
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
            response.StatusMessage = "Login succsessfull";
            Models.Registration reg = new Registration();
            reg.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
            reg.Name = Convert.ToString(dt.Rows[0]["Email"]);
            response.Registration = reg;
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Login failed";
            response.Registration = null;
        }

        return response;
    }

    public Response UserApproval(Registration registration, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd = new SqlCommand(
            "UPDATE Registration SET IsApproved = 1 WHERE Id = '" + registration.Id + "'AND IsActive = 1'",
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

    public Response AddNews(News news, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd =
            new SqlCommand(
                "INSERT INTO News(Titile,Content,Email,IsActive,CreatedOn) VALUES ('" + news.Title + "','" +
                news.Content + "','" + news.Email + "','1,GETDATE())", connection);
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


    public Response AddArticle(Article article, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd =
            new SqlCommand(
                "INSERT INTO Article(Titile,Content,Email,Image,IsActive,IsApproved) VALUES ('" + article.Title +
                "','" +
                article.Content + "','" + article.Image + "',1,0)", connection);
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Article created";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Article creation failed";
        }

        return response;
    }

    public Response ArticleList(Article article, SqlConnection connection)
    {
        Response response = new Response();
        SqlDataAdapter dataAdapter = null;
        if (article.type == "User")
        {
            new SqlDataAdapter("SELECT * FROM Article  WHERE Email = '" + article.Email + "' AND  IsActive = 1",
                connection);
        }

        if (article.type == "Page")
        {
            new SqlDataAdapter("SELECT * FROM Article WHERE IsActive = 1", connection);
        }

        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);
        List<Article> lstArticle = new List<Article>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Article art = new Article();
                article.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                article.Title = Convert.ToString(dt.Rows[i]["Title"]);
                article.Content = Convert.ToString(dt.Rows[i]["Content"]);
                article.Email = Convert.ToString(dt.Rows[i]["Email"]);
                article.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                article.Image = Convert.ToString(dt.Rows[i]["Image"]);
                lstArticle.Add(art);
            }

            if (lstArticle.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article data found";
                response.ListArticles = lstArticle;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = " No Article data found";
                response.ListArticles = null;
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

    public Response ArticalApproval(Article article, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd = new SqlCommand(
            "UPDATE Article SET IsApproved = 1 WHERE Id = '" + article.Id + "'AND IsActive = 1'",
            connection);
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Article Approved";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Article approval filed";
        }

        return response;
    }
    
    public Response StaffRegistration(Staff staff, SqlConnection connection)
    {
        Response response = new Response();
        connection.Open();
        SqlCommand checkEmailCmd = new SqlCommand(
            "SELECT COUNT(*) FROM Staff WHERE Email = @Email",
            connection
        );
        checkEmailCmd.Parameters.AddWithValue("@Email", staff.Email);
        int count = (int)checkEmailCmd.ExecuteScalar();

        if (count > 0)
        {
            response.StatusMessage = "Email already exists";
            response.StatusCode = 100;
            return response;
        }
        connection.Close();
        SqlCommand cmd = new SqlCommand(
            "INSERT INTO Staff (Name, Email, Password, IsActive) VALUES (@Name, @Email, @Password, @IsActive)",
            connection
        );
        cmd.Parameters.AddWithValue("@Name", staff.Name);
        cmd.Parameters.AddWithValue("@Email", staff.Email);
        cmd.Parameters.AddWithValue("@Password", staff.Password);
        cmd.Parameters.AddWithValue("@IsActive", 1);
        connection.Open();
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Staff registration succsessfull";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Staff registration failed";
        }

        return response;
    }

    public Response DeleteStaff(Staff staff, SqlConnection connection)
    {
        Response response = new Response();
        SqlCommand cmd = new SqlCommand(
            "DELETE FROM Staff WHERE Id = @Id AND Email = @Email AND Password = @Password",
            connection
        );
        
        cmd.Parameters.AddWithValue("@Id", staff.Id);
        cmd.Parameters.AddWithValue("@Email", staff.Email);
        cmd.Parameters.AddWithValue("@Password", staff.Password);
        connection.Open();
        
        int i = cmd.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            response.StatusCode = 200;
            response.StatusMessage = "Staff deleted succsessfullly";
        }
        else
        {
            response.StatusCode = 100;
            response.StatusMessage = "Staff deleted failed";
        }

        return response;
    }

}