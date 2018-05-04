using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Book> bookList = new List<Book>();
        public List<Genre> genreList = new List<Genre>();
        public List<Author> authorList = new List<Author>();
    }
}