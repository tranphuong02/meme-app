using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Datatable.RequestBinder;
using Framework.Datatable.RequestParser;
using Framework.Logger.Log4Net;
using Microsoft.Practices.Unity;
using Transverse;
using Transverse.Enums;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business.Resource;
using Transverse.Models.Business.User;
using Transverse.Models.DAL;

namespace BusinessLogic
{
    public class ResourceBusiness : IResourceBusiness
    {
        [Dependency]
        public IResourceRepository ResourceRepository { get; set; }
        public DataTablesResponse GetListGenre(IDataTablesRequest dataTableParam, ResourceGenreSearchViewModel searchViewModel)
        {
            try
            {
                var query = ResourceRepository.GetAll(x => x.IsDeleted == false && x.Type == (int) ResourceType.Genre, null, x => x.Genres);
                if (searchViewModel.GenreId != Constants.AllValue)
                {
                    query = query.Where(x => x.Genres.Any(g => g.IsDeleted == false && g.Id == searchViewModel.GenreId));
                }

                var dataTableHelper = new DataTableHelper<ResourceGenreViewModel, Resource>(query, x => new ResourceGenreViewModel
                {
                    Id = x.Id,
                    Url = x.Url,
                    Tag = x.Tag,
                    Genre = x.Genres.AsQueryable().Any(y => y.IsDeleted == false) ? x.Genres.AsQueryable().FirstOrDefault().Title : ""
                });

                var entities = dataTableHelper.GetDataVMForResponse(dataTableParam);
                var result = dataTableHelper.GetDataToList(dataTableParam, entities);

                return new DataTablesResponse(dataTableParam.Draw, result, entities.Count(), entities.Count());
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new DataTablesResponse(dataTableParam.Draw, new List<ResourceGenreViewModel>(), 0, 0);
            }
        }

        public DataTablesResponse GetListAuthor(IDataTablesRequest dataTableParam, ResourceAuthorSearchViewModel searchViewModel)
        {
            try
            {
                var query = ResourceRepository.GetAll(x => x.IsDeleted == false && x.Type == (int)ResourceType.Author, null, x => x.Authors);
                if (searchViewModel.AuthorId != Constants.AllValue)
                {
                    query = query.Where(x => x.Authors.Any(g => g.IsDeleted == false && g.Id == searchViewModel.AuthorId));
                }

                var dataTableHelper = new DataTableHelper<ResourceAuthorViewModel, Resource>(query, x => new ResourceAuthorViewModel
                {
                    Id = x.Id,
                    Url = x.Url,
                    Tag = x.Tag,
                    Author = x.Authors.AsQueryable().Any(y => y.IsDeleted == false) ? x.Authors.AsQueryable().FirstOrDefault().Name : ""
                });

                var entities = dataTableHelper.GetDataVMForResponse(dataTableParam);
                var result = dataTableHelper.GetDataToList(dataTableParam, entities);

                return new DataTablesResponse(dataTableParam.Draw, result, entities.Count(), entities.Count());
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new DataTablesResponse(dataTableParam.Draw, new List<ResourceAuthorViewModel>(), 0, 0);
            }
        }

        public DataTablesResponse GetListChapter(IDataTablesRequest dataTableParam, ResourceChapterSearchViewModel searchViewModel)
        {
            try
            {
                var query = ResourceRepository.GetAll(x => x.IsDeleted == false && x.Type == (int)ResourceType.Chapter, null, x => x.Authors, x=> x.Genres, x=> x.ChapterResources);
                if (searchViewModel.AuthorId != Constants.AllValue)
                {
                    query = query.Where(x => x.Authors.Any(g => g.IsDeleted == false && g.Id == searchViewModel.AuthorId));
                }
                if (searchViewModel.GenreId != Constants.AllValue)
                {
                    query = query.Where(x => x.Genres.Any(g => g.IsDeleted == false && g.Id == searchViewModel.GenreId));
                }
                if (searchViewModel.CategoryId != Constants.AllValue)
                {
                    query = query.Where(x => x.ChapterResources.Any(g => g.IsDeleted == false && g.Chapter.CategoryId == searchViewModel.CategoryId));
                }
                if (searchViewModel.ChapterId != Constants.AllValue)
                {
                    query = query.Where(x => x.ChapterResources.Any(g => g.IsDeleted == false && g.ChapterId == searchViewModel.ChapterId));
                }

                var dataTableHelper = new DataTableHelper<ResourceChapterViewModel, Resource>(query, x => new ResourceChapterViewModel
                {
                    Id = x.Id,
                    Url = x.Url,
                    Tag = x.Tag,
                    Order = x.Order,
                    Chapter = x.ChapterResources.AsQueryable().Any(y => y.IsDeleted == false) ? x.ChapterResources.AsQueryable().FirstOrDefault().Chapter.Title : ""
                });

                var entities = dataTableHelper.GetDataVMForResponse(dataTableParam);
                var result = dataTableHelper.GetDataToList(dataTableParam, entities);

                return new DataTablesResponse(dataTableParam.Draw, result, entities.Count(), entities.Count());
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new DataTablesResponse(dataTableParam.Draw, new List<ResourceChapterViewModel>(), 0, 0);
            }
        }
    }
}