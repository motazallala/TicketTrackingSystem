namespace TicketTrackingSystem.Common.Model;
public class DataTablesRequest
{
    public int Draw { get; set; } // Draw counter for DataTables
    public int Start { get; set; } // Starting index for pagination
    public int Length { get; set; } // Number of records per page

    public Search Search { get; set; } // Global search
    public List<Order> Order { get; set; } // Sorting details
    public List<Column> Columns { get; set; } // Column definitions
}

public class Search
{
    public string Value { get; set; } // Global search value
    public bool Regex { get; set; } // Whether the search is a regex
}

public class Order
{
    public int Column { get; set; } // Column index for sorting
    public string Dir { get; set; } // Sorting direction ("asc" or "desc")
}

public class Column
{
    public string? Data { get; set; } // Column name or property
    public string Name { get; set; } // Optional column name
    public bool Searchable { get; set; } // Is this column searchable?
    public bool Orderable { get; set; } // Is this column sortable?
    public Search Search { get; set; } // Column-specific search
}


