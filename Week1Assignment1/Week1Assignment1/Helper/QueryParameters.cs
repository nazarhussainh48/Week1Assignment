namespace Week1Assignment1.Helper
{
    public class QueryParameters
    {
        private const int maxPageSize = 10;

        /// <summary>
        /// Default page size.
        /// </summary>
        private int _pageSize = maxPageSize;

        /// <summary>
        /// Filter expression.
        /// </summary>
        private string _filterExpression;

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        public string? FilterExpression
        {
            get
            {
                return _filterExpression;
            }
            set
            {
                var t = value;
                _filterExpression = t;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringParams"/> class.
        /// By Default every list will be sorted based on the primary key
        /// Since the primary key is auto increment we'll have the latest records at the top
        /// If a use case needs to change this behaviour it can extend this class
        /// </summary>
        public QueryParameters()
        {
            OrderBy = "Id";
        }
    }
}
