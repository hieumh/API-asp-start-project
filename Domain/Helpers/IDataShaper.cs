﻿using System.Dynamic;

namespace API_asp_start_project.Domain.Helpers
{
    public interface IDataShaper<T>
    {
        IQueryable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
