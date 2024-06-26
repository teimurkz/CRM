﻿namespace CRM.Models;

public class Response
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public List<Registration> listRegistration { get; set; }
    public List<Article> ListArticles { get; set; }
    public List<News> listNews { get; set; }
    public List<Event> listEvents { get; set; }
    public Registration Registration { get; set; }
}