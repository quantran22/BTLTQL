using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTQL.Models
{
    [Table("Article")]
    public class Article
    {
        [Key]
        public string ArticleID { get; set; }
        public string Author { get; set; }
        [AllowHtml]
        public string ArticleContent { get; set; }
    }
   
}