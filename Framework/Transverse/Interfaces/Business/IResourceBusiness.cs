using Framework.Datatable.RequestBinder;
using Framework.DI.Contracts.Interfaces;
using Transverse.Models.Business.Resource;

namespace Transverse.Interfaces.Business
{
    public interface IResourceBusiness : IDependency
    {
        DataTablesResponse GetListGenre(IDataTablesRequest dataTableParam, ResourceGenreSearchViewModel searchViewModel);
        DataTablesResponse GetListAuthor(IDataTablesRequest dataTableParam, ResourceAuthorSearchViewModel searchViewModel);
        DataTablesResponse GetListChapter(IDataTablesRequest dataTableParam, ResourceChapterSearchViewModel searchViewModel);
    }
}