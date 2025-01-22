export function initializeDataTable({
    tableId,
    apiUrl,
    method,
    columns,
    ordering = true,
    additionalParameters = null,
    failureCallback = null
}) {
    if ($.fn.DataTable.isDataTable(tableId)) {
        // If DataTable already exists, return its instance
        return $(tableId).DataTable();
    }
    return $(tableId).DataTable({

        processing: true, // Show processing indicator
        serverSide: true, // Enable server-side processing
        ordering: ordering !== false,
        ajax: {
            url: apiUrl, // API endpoint
            type: "POST",
            contentType: "application/json",
            data: function (d) {
                const parameters = [d]; // Default DataTables request object
                if (typeof additionalParameters === "function") {
                    parameters.push(...additionalParameters());
                }
                else {
                    if (additionalParameters) {
                        parameters.push(...additionalParameters); // Append additional parameters if provided
                    }   
                }
                return JSON.stringify({
                    Method: method, // Method to call in the backend
                    Parameters: [...parameters] // Sending the entire DataTables request object as the first parameter
                });
            },
            dataSrc: function (json) {
                return json.data; // Path to the array in your API response
            },
            dataFilter: function (jsonString) {
                const json = JSON.parse(jsonString);

                if (!json.isSuccess) {
                    // Call the failure callback function if it's provided
                    if (failureCallback) {
                        failureCallback(json.error);
                    } else {
                        console.warn('You Mast To Add CallBack In Data Table Initialize');
                        console.error(json.error);
                    }
                    return JSON.stringify({
                        draw: 1,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                }

                // Extract the necessary fields for DataTables
                return JSON.stringify({
                    draw: json.data.draw,
                    recordsTotal: json.data.recordsTotal,
                    recordsFiltered: json.data.recordsFiltered,
                    data: json.data.data
                });
            }
        },
        columns: columns,
        lengthMenu: [5,1, 10, 25, 50, 100],
        language: {
            processing: '<div class="spinner-border text-primary" role="status"></div>',
            paginate: {
                //previous: `<button class='btn btn-sm btn-primary'>Previous</button>`,
                //next: `<button class='btn btn-sm btn-primary'>Next</button>`,
            },
            search: "Filter records:",
        },

    });
}


export function reinitializeDataTable({
    tableId,
    apiUrl,
    method,
    columns,
    additionalParameters = null,
    failureCallback = null
}) {
    // 1. Check if DataTable is initialized and destroy it if needed
    if ($.fn.DataTable.isDataTable(tableId)) {
        $(tableId).DataTable().clear().destroy(); // Destroy the existing DataTable instance
    }

    // 2. Reinitialize the DataTable
    return initializeDataTable({
        tableId: tableId,
        apiUrl: apiUrl,
        method: method,
        columns: columns,
        additionalParameters: additionalParameters,
        failureCallback: failureCallback
    });
}