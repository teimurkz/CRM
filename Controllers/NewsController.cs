using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CRM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly IConfiguration _configuration;


    public NewsController(IConfiguration configuration)
    {
        _configuration = configuration;
            
    }
    
    [HttpPost]
    [Route("AddNews")]
    public Response AddNews(News news)
    {
        Response response = new Response();
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
        Dal dal = new Dal();
        response = dal.AddNews(news, connection);
        return response;
    }
    [HttpGet]
    [Route("NewsList")]
    public Response NewsList()
    {
        Response response = new Response();
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
        Dal dal = new Dal();
        response = dal.NewsList(connection);
        return response;
    }
}