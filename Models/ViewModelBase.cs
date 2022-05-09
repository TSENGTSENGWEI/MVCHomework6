using MVCHomework6.Data.Database;
using X.PagedList;

namespace MVCHomework6.Data
{
    public class ViewModelBase
    {
        public IPagedList<Articles> ArticlesViewModel { get; set; }
        public List<TagCloud> TagClouldViewModel { get; set; }
    }
}