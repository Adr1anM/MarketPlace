using AutoMapper.QueryableExtensions;
using AutoMapper;
using MarketPlace.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using MarketPlace.Domain.Models;
using System.Linq.Dynamic.Core;
using MarketPlace.Application.Exceptions;


namespace MarketPlace.Application.Extensions
{
    public static class QueryableExtensions
    {
        public async static Task<Common.Models.PagedResult<TDto>> CreatePaginatedResultAsync<TEntity, TDto>(this IQueryable<TEntity> query, PagedRequest pagedRequest, IMapper mapper)
            where TEntity : Entity
            where TDto : class
        {
            query = query.ApplyFilters(pagedRequest);

            var total = await query.CountAsync();

            query = query.Paginate(pagedRequest);

            var projectionResult = query.ProjectTo<TDto>(mapper.ConfigurationProvider);

            projectionResult = projectionResult.Sort(pagedRequest);

            var listResult = await projectionResult.ToListAsync();

            return new Common.Models.PagedResult<TDto>()
            {
                Items = listResult,
                PageSize = pagedRequest.PageSize,
                PageIndex = pagedRequest.PageIndex,
                Total = total
            };
        }

        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var entities = query.Skip(pagedRequest.PageIndex * pagedRequest.PageSize).Take(pagedRequest.PageSize);
            return entities;
        }

 
        private static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        public static string MappToOperator(string oper)
        {
            return oper switch
            {
                "lt" => "<",
                "gt" => ">",
                "eq" => "=",
                "gt_o_eq" => ">=",
                "lt_o_eq" => "<=",
                "contains" => "Contains",
                "in" => "in",
                "between" => "between" ,
                _ => throw new NoSuchOperatorException(oper)
            };
        }

        private static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var predicate = new StringBuilder();
            var parameters = new List<object>();
            var requestFilters = pagedRequest.RequestFilters;

            for (int i = 0; i < requestFilters.Filters.Count; i++)
            {
                if (i > 0)
                {
                    predicate.Append($" {requestFilters.LogicalOperator} ");
                }

                string oper = MappToOperator(pagedRequest.RequestFilters.Filters[i].Operator);
                string filterPath = requestFilters.Filters[i].Path;

                string value = requestFilters.Filters[i].Value;

                if (oper == "Contains")
                {
                    predicate.Append($"{filterPath}.Contains(@{parameters.Count})");
                    parameters.Add(value);
                }
                else if (oper == "in")
                {
                    var values = value.Split(';').Select(v => v.Trim()).ToList();
                    var inParameters = new List<string>();

                    for (int j = 0; j < values.Count; j++)
                    {
                        inParameters.Add($"@{parameters.Count}");
                        parameters.Add(values[j]);
                    }

                    predicate.Append($"{filterPath} in ({string.Join(", ", inParameters)})");
                }
                else if (oper == "between")
                {
                    var values = value.Split(';').Select(v => v.Trim()).ToArray();
                    if (values.Length == 2 && DateTime.TryParse(values[0], out var dateValue1) && DateTime.TryParse(values[1], out var dateValue2))
                    {
                        predicate.Append($"({filterPath} >= @{parameters.Count} AND {filterPath} <= @{parameters.Count + 1})");
                        parameters.Add(dateValue1);
                        parameters.Add(dateValue2);
                    }
                    else if (values.Length == 2 && decimal.TryParse(values[0], out var decValue1) && decimal.TryParse(values[1], out var decValue2))
                    {
                        predicate.Append($"({filterPath} >= @{parameters.Count} AND {filterPath} <= @{parameters.Count + 1})");
                        parameters.Add(decValue1);
                        parameters.Add(decValue2);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid values for 'between' operator: {value}");
                    }
                }
                else if (DateTime.TryParse(value, out var dateValue))
                {
                    predicate.Append($"{filterPath} {oper} @{parameters.Count}");
                    parameters.Add(dateValue);
                }
                else
                {
                    predicate.Append($"{filterPath} {oper} @{parameters.Count}");
                    parameters.Add(value);
                }
            }

            if (requestFilters.Filters.Any())
            {   
                query = query.Where(predicate.ToString(), parameters.ToArray());
            }

            return query;
        }

    }
}
