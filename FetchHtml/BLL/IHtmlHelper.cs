﻿using FetchHtml.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FetchHtml.BLL
{

    public interface IHtmlHelper
    {
        Task<IEnumerable<QueryResult>>  Execute(IQueryModel queryModel);

        bool CanExecute(IQueryModel queryModel);
    }

    public interface IHtmlHelper<in T> : IHtmlHelper where T : IQueryModel
    {
        Task<IEnumerable<QueryResult>> Execute(T t);

    }


}