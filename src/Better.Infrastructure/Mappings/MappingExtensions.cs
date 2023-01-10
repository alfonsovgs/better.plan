using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Better.Application.Common.Models;

namespace Better.Infrastructure.Mappings;

public static class MappingExtensions
{
    public static async Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    {
        var source = queryable.AsNoTracking();
        int count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}