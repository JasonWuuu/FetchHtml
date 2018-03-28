using System;
using System.Collections.Generic;
using System.Text;

namespace FetchHtml.Models
{
    public abstract class QueryModel
    {
        public string Url { get; set; }

        public string Title { get; set; }
    }


    public class MainPageQueryModel : QueryModel
    {
        private List<SubPageQueryModel> _subPagelist = new List<SubPageQueryModel>();
        public IEnumerable<string> KeyWords { get; set; }

        public List<SubPageQueryModel> SubPageQueryModels
        {
            get
            {
                return _subPagelist;
            }
            set
            {
                _subPagelist = value;
            }
        }

    }

    public class SubPageQueryModel : QueryModel
    {

    }

}
