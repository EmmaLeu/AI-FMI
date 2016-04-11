using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Repos
{
    public class NewsRepo
    {
        private FMIContext context;

        public NewsRepo(FMIContext context)
        {
            this.context = context;
        }

        public List<News> GetLatestNews(int howMany)
        {
            return context.News
                .OrderByDescending(i => i.NewsID)
                .Take(howMany)
                .ToList();
        }

        public List<News> GetNews()
        {
            return context.News
                .OrderByDescending(i => i.NewsID)
                .ToList();
        }

        public void AddNews(News news, User user)
        {
            news.User = user;
            var newNews = context.News.Add(news);
            context.SaveChanges();
        }

        public void UpdateNews(News news)
        {
            var newsToUpdate = context.News
                .Where(u => u.NewsID == news.NewsID)
                .FirstOrDefault();

            if (newsToUpdate != null)
            {
                newsToUpdate.Description = news.Description;
                newsToUpdate.Title = news.Title;
                newsToUpdate.Link = news.Link;
                newsToUpdate.LinkText = news.LinkText;
                context.SaveChanges();
            }
        }

        public void DeleteNews(int newsID)
        {
            var news = context.News.Where(u => u.NewsID == newsID).FirstOrDefault();
            context.News.Remove(news);
            context.SaveChanges();
        }
    }
}
