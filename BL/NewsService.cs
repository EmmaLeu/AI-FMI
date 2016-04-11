using BE;
using DA;
using System.Collections.Generic;

namespace BL
{
    public class NewsService
    {
        private Repositories repository;

        public NewsService(Repositories repository)
        {
            this.repository = repository;
        }

        public List<News> GetLatestNews(int howMany)
        {
            return repository.NewsRepo.GetLatestNews(howMany);
        }

        public void UpdateNews(News newItem)
        {
            repository.NewsRepo.UpdateNews(newItem);
        }
        public void DeleteNews(int newsId)
        {
            repository.NewsRepo.DeleteNews(newsId);
        }
        public void AddNews(News news, User user)
        {
            repository.NewsRepo.AddNews(news, user);
        }
        public List<News> GetNews()
        {
            return repository.NewsRepo.GetNews();
        }
    }
}
